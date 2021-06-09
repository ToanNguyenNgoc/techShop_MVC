using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Areas.Admin.Models
{
    public class LoginModel
    {
        public string UserName { set; get; }
        public string PassWord { set; get; }
        public string RememberMe { set; get; }
    }
}