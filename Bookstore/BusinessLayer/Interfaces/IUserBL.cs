using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IUserBL
    {
        public UserPostModel UserRegistration(UserPostModel userPost);
        public LoginModel Login(LoginModel login);
        public string UserForgetPassword(string Email);
        public string UserResetPassword(UserPasswordModel userPassword, string Email);
    }
}
