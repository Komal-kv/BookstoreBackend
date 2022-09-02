using CommonLayer.Admin;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IAdminRL
    {
        public AdminModel AdminLogin(string Email, string Password);
    }
}
