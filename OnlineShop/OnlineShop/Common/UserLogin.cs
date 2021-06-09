using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Common
{
    [Serializable]
    public class UserLogin
    {
        public long UserID { set; get; }
        public string UserName { set; get; }
        public string GroupID { set; get; }
        public string Name { set; get; }
        public string Email { set; get; }
        public string Address { set; get; }
        public string Phone { set; get; }
        
    }
}