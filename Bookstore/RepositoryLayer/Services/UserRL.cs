using CommonLayer.Model;
using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        private readonly IConfiguration configuration;
        
        public UserRL(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public LoginModel Login(LoginModel login)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(configuration["ConnectionStrings:BookStore"]))
                {
                    SqlCommand cmd = new SqlCommand("spAddUserLogIn", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    LoginModel model = new LoginModel();
                    var encryptedPassword = EncryptPassword(login.Password);

                    cmd.Parameters.AddWithValue("@Email", login.Email);
                    cmd.Parameters.AddWithValue("@Password", encryptedPassword);

                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    SqlDataReader sdr = cmd.ExecuteReader();
                   

                    if (sdr.HasRows)
                    {
                        ///The HasRows property returns information about the current result set.

                        int UserId = 0;

                        while (sdr.Read())
                        {


                            model.Email = Convert.ToString(sdr["Email"]);
                            encryptedPassword = Convert.ToString(sdr["Password"]);
                            UserId = Convert.ToInt32(sdr["UserId"]);



                        }
                        con.Close();
                        model.Password = this.GenerateSecurityToken(model.Email, UserId);

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

        public string UserForgetPassword(string Email)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(configuration["ConnectionStrings:BookStore"]))
                {
                    SqlCommand cmd = new SqlCommand("spUserForgetPasswrd", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Email", Email);

                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        int UserId = 0;
                        while (rdr.Read())
                        {
                            Email = Convert.ToString(rdr["Email"]);
                            UserId = Convert.ToInt32(rdr["UserId"]);
                        }

                        con.Close();
                        MessageQueue BookstoreQ;

                        if (MessageQueue.Exists(@".\Private$\BookstoreQueue"))
                            BookstoreQ = new MessageQueue(@".\Private$\BookstoreQueue");
                        else BookstoreQ = MessageQueue.Create(@".\Private$\BookstoreQueue");

                        Message message = new Message();
                        message.Formatter = new BinaryMessageFormatter();
                        message.Body = GenerateSecurityToken(Email, UserId);
                        EmailService.SendEmail(Email, message.Body.ToString());
                        BookstoreQ.ReceiveCompleted += new ReceiveCompletedEventHandler(MsmqQueue_ReciveCompleted);

                        var token = this.GenerateSecurityToken(Email, UserId);

                        return token;
                    }
                    else
                    {
                        con.Close();
                        return null;
                    }
                }
            }
            catch(Exception e)
            {

                throw e;
            }
        }

        public UserPostModel UserRegistration(UserPostModel userPost)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(configuration["ConnectionStrings:BookStore"]))
                {
                    SqlCommand cmd = new SqlCommand("spRegister", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@FullName", userPost.FullName);
                    cmd.Parameters.AddWithValue("@Email", userPost.Email);
                    cmd.Parameters.AddWithValue("@Password", userPost.Password);
                    cmd.Parameters.AddWithValue("@MobileNumber", userPost.MobileNumber);

                    con.Open();
                    var result = cmd.ExecuteNonQuery();
                    con.Close();

                    if (result != 0)
                    {
                        return userPost;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch(Exception ex)
            {

                throw ex;
            }
        }

        public static string EncryptPassword(string Password)
        {
            try
            {
                if (Password == null)
                {
                    return null;
                }
                else
                {
                    byte[] b = Encoding.ASCII.GetBytes(Password);
                    string encrypted = Convert.ToBase64String(b);
                    return encrypted;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string DecryptedPassword(string encryptedPassword)
        {
            byte[] b;
            string decrypted;
            try
            {
                if (encryptedPassword == null)
                {
                    return null;
                }
                else
                {
                    b = Convert.FromBase64String(encryptedPassword);
                    decrypted = Encoding.ASCII.GetString(b);
                    return decrypted;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public string UserResetPassword(UserPasswordModel userPassword, string Email)
        {
            try
            {
                if(userPassword.Password == userPassword.ConfirmPassword)
                {
                    using (SqlConnection con = new SqlConnection(configuration["ConnectionStrings:BookStore"]))
                    {
                        SqlCommand cmd = new SqlCommand("spUserResetPaswrd", con);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Email", Email);
                        cmd.Parameters.AddWithValue("@Password", EncryptPassword(userPassword.Password));

                        con.Open();
                        var result = cmd.ExecuteNonQuery();
                        con.Close();

                        if (result == 0)
                        {
                            return "Congratulations! Your password has been changed successfully";
                        }
                        else
                        {
                            return "Failed to reset your password";
                        }
                    }
                }
                else
                {
                    return "Make sure password are matched";
                }
            }
            catch(Exception e)
            {

                throw e;
            }
        }

        //Token for LogIn
        public string GenerateSecurityToken(string emailID, int userId)
        {
            var SecurityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN"));
            var credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Role,"User"),
                new Claim(ClaimTypes.Email, emailID),
                new Claim("UserId", userId.ToString())
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

        private void MsmqQueue_ReciveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                {
                    MessageQueue queue = (MessageQueue)sender;
                    Message msg = queue.EndReceive(e.AsyncResult);
                    EmailService.SendEmail(e.Message.ToString(), GenerateToken(e.Message.ToString()));
                    queue.BeginReceive();
                }
            }
            catch (MessageQueueException ex)
            {
                if (ex.MessageQueueErrorCode ==
                   MessageQueueErrorCode.AccessDenied)
                {
                    Console.WriteLine("Access is denied. " +
                        "Queue might be a system queue.");
                }
            }
        }

        private string GenerateToken(string email)
        {
            if (email == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Email", email)
                }),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
