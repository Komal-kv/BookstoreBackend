using CommonLayer.Admin;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class AdminRL : IAdminRL
    {
        private readonly IConfiguration configuration;
        public AdminRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public AdminModel AdminLogin(string Email, string Password)
        {
            
            try
            {
                AdminModel model = new AdminModel();
                using (SqlConnection con = new SqlConnection(configuration["ConnectionStrings:BookStore"]))
                {
                    SqlCommand cmd = new SqlCommand("SpAdminLogin", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EmailId", Email);
                    cmd.Parameters.AddWithValue("@Password", Password);
                    con.Open();

                    SqlDataReader sdr = cmd.ExecuteReader();

                    if (sdr.HasRows)
                    {
                        int AdminId = 0;
                        while (sdr.Read())
                        {
                            model.EmailId = Convert.ToString(sdr["EmailId"]);
                            model.Password = Convert.ToString(sdr["Password"]);
                            AdminId = Convert.ToInt32(sdr["Admin"]);

                        }
                        con.Close();
                        model.Token = this.GenerateSecurityToken(model.EmailId, AdminId);

                        return model;

                    }
                    else
                    {
                        con.Close();
                        return null;
                    }
                    
                }
            }
            catch(Exception ex)
            {

                throw ex;
            }
        }

        public string GenerateSecurityToken(string Email, long AdminId)
        {
            var SecurityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN"));
            var credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Role , "Admin"),
                new Claim(ClaimTypes.Email, Email),
                new Claim("AdminId", AdminId.ToString())
            };
            var token = new JwtSecurityToken(
                this.configuration["Jwt:Issuer"],
                this.configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

