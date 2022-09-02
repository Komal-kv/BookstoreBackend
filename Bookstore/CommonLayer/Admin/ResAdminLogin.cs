using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Admin
{
    public class ResAdminLogin
    {
        public int AdminId { get; set; }
        public string FullName { get; set; }
        public string EmailId { get; set; }
        public long MobileNumber { get; set; }
        public string Address { get; set; }
        public string Token { get; set; }
    }
}
