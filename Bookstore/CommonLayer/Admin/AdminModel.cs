using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Admin
{
    public class AdminModel
    {
        //[Required]
        public string EmailId { get; set; }
        //[Required]
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
