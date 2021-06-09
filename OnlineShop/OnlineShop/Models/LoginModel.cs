using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public class LoginModel
    {
        [Key]
        public string UserName { set; get; }
        public string Password { set; get; }
    }
}