using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }
        public LoginModel Login(LoginModel login)
        {
            try
            {
                return this.userRL.Login(login);
            }
            catch(Exception ex)
            {

                throw ex;
            }
        }

        public string UserForgetPassword(string Email)
        {
            return this.userRL.UserForgetPassword(Email);
        }

        public UserPostModel UserRegistration(UserPostModel userPost)
        {
            try
            {
                return this.userRL.UserRegistration(userPost);
            }
            catch(Exception ex)
            {

                throw ex;
            }
        }

        public string UserResetPassword(UserPasswordModel userPassword, string Email)
        {
            return this.userRL.UserResetPassword(userPassword, Email);
        }
    }
}
