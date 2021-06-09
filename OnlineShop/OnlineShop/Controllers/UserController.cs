
using Model.Dao;
using Model.EF;
using OnlineShop.Common;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
namespace OnlineShop.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return Redirect("/");
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.UserName,Encryptor.MD5Hash(model.Password));
                if (result == 1)
                {
                    var user = dao.GetByID(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.UserID = user.ID;
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    Session["UserName"] = userSession.UserName;
                    return Redirect("/");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tài.Vui lòng thử lại!");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khóa!");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng.Vui lòng thử lại!");
                }
                else
                {
                    ModelState.AddModelError("", "Tên người dùng hoặc mật khẩu không đúng. Vui lòng thử lại");
                }
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var EncryptedMd5Pas = Encryptor.MD5Hash(model.Password);
                model.Password = EncryptedMd5Pas;
                if (dao.CheckUserName(model.UserName))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại!");
                }else if (dao.CheckEmail(model.Email))
                {
                    ModelState.AddModelError("", "Email này đã được đang ký!");
                }
                else
                {
                    var user = new User();
                    user.UserName = model.UserName;
                    user.Name = model.Name;
                    user.Password = model.Password;
                    user.Email = model.Email;
                    user.Address = model.Address;
                    user.Phone = model.Phone;
                    user.CreatedDate = DateTime.Now;
                    user.Status = true;
                    dao.Insert(user);
                    ViewBag.Success = "Đăng ký thành công";
                    model = new RegisterModel();
                }
            }
            return View(model);
        }
        
    }
}