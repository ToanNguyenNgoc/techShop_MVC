using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    public class RegisterModel
    {
        [Key]
        public long ID { set; get; }
        [Required(ErrorMessage ="Vui lòng nhập tên đăng nhập!")]
        public string UserName { set; get; }
        [Required(ErrorMessage ="Vui lòng nhập mật khẩu!")]
        public string Password { set; get; }
        [Compare("Password",ErrorMessage ="Mật khẩu nhập lại không đúng!")]
        public string ConfirmPassword { set; get; }
        [Required(ErrorMessage ="Vui lòng nhập đủ họ và tên")]
        public string Name { set; get; }
        [Required(ErrorMessage ="Vui lòng nhập địa chỉ!")]
        public string Address { set; get; }
        [Required(ErrorMessage ="Vui lòng nhập Email!")]
        public string Email { set; get; }
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại!")]
        public string Phone { set; get; }
    }
}