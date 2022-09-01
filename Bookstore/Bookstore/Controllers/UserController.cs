using BusinessLayer.Interfaces;
using CommonLayer.Model;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Bookstore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        [HttpPost("Register")]
        public ActionResult UserRegisteration(UserPostModel userPost)
        {
            try
            {
                var user = this.userBL.UserRegistration(userPost);
                if(user != null)
                {
                    return this.Ok(new { success = true, message = "Registration Successfull", data = user });
                }
                return this.BadRequest(new { success = false, message = "Email Already Exits", data = user });

            }
            catch(Exception e)
            {

                throw e;
            }
        }

        [HttpPost("LogIn")]
        public ActionResult UserLogin(LoginModel login)
        {
            try
            {
                var user = this.userBL.Login(login);
                if (user != null)
                {
                    return this.Ok(new { success = true, message = "LogIn Successfull", data = user });
                }
                return this.BadRequest(new { success = false, message = "LogIn Failed", data = user });

                
            }
            catch(Exception e)
            {

                throw e;
            }
        }

        [HttpPost("ForgotPassword")]
        public IActionResult UserForgotPassword(string Email)
        {
            try
            {
                var password = this.userBL.UserForgetPassword(Email);
                if (password != null)
                {
                    return this.Ok(new { success = true, message = "Mail has sent Successfull", data = password });
                }
                return this.BadRequest(new { success = false, message = "Enter the valid email"});

            }
            catch(Exception ex)
            {

                throw ex;
            }
        }

        [HttpPut("ResetPassword")]
        public ActionResult UserResetPassword(UserPasswordModel userPassword,string Email)
        {
            try
            {
                var password = this.userBL.UserResetPassword(userPassword, Email);
                if (password != null)
                {
                    return this.Ok(new { success = true, message = "Reset Password has been Successfull" });
                }
                return this.BadRequest(new { success = false, message = "Reset Password failed" });

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
