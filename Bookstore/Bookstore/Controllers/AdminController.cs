using BusinessLayer.Interfaces;
using CommonLayer.Admin;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Bookstore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminBL adminBL;
        public AdminController(IAdminBL adminBL)
        {
            this.adminBL = adminBL;
        }

        [HttpPost("LogIn")]
        public ActionResult AdminLogin(string Email, string Password)
        {
            try
            {
                var main = this.adminBL.AdminLogin(Email, Password);

                if (main != null)
                {
                    return Ok(new { success = true, message = "LogIn sucessfully", data = main });
                }
                else
                {
                    return BadRequest(new { success = false, message = "Faild to Login" });
                }
            }
            catch(Exception ex)
            {

                throw ex;
            }
        }
    }
}
