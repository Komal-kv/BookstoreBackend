using BusinessLayer.Interfaces;
using CommonLayer.Admin;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class AdminBL : IAdminBL
    {
        private readonly IAdminRL adminRL;
        public AdminBL(IAdminRL adminRL)
        {
            this.adminRL = adminRL;
        }

        public AdminModel AdminLogin(string Email, string Password)
        {
            try
            {
                return this.adminRL.AdminLogin(Email, Password);
            }
            catch(Exception ex)
            {

                throw ex;
            }
        }
    }
}
