using CommonLayer.Admin;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IAdminBL
    {
        public AdminModel AdminLogin(string Email, string Password);
    }
}
