using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL
    {
        public UserPostModel UserRegistration(UserPostModel userPost);
        public LoginModel Login(LoginModel login);
        public string UserForgetPassword(string Email);
        public string UserResetPassword(UserPasswordModel userPassword, string Email);
    }
}
