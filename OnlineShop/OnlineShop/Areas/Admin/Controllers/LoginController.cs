using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShop.Areas.Admin.Models;
using Model.Dao;
using OnlineShop.Common;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.PassWord), true);
                if (result==1)
                {
                    var user = dao.GetByID(model.UserName);
                    var userSession = new UserLogin();
                    //
                    userSession.UserName = user.UserName;
                    userSession.Name = user.Name;
                    userSession.Address = user.Address;
                    userSession.Email = user.Email;
                    userSession.Phone = user.Phone;
                    
                    //
                    userSession.UserID = user.ID;
                    userSession.GroupID = user.GroupID;

                    var listCredentials = dao.GetListCredential(model.UserName);

                    Session.Add(CommonConstants.SESSION_CREDENTIALS, listCredentials); //goi session phan quyen
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    //
                    Session["UserName"] = userSession.UserName;
                    Session["Name"] = userSession.Name;
                    Session["Address"] = userSession.Address;
                    Session["Email"] = userSession.Email;
                    Session["GroupID"] = user.GroupID;
                    Session["Phone"] = userSession.Phone;
                    //
                    
                    return RedirectToAction("Index", "Home");
                }else if (result == 0)
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
                }else if (result == -3)
                {
                    ModelState.AddModelError("", "Bạn không có quyền truy cập trang này!");
                }
                else
                {
                    ModelState.AddModelError("", "Tên người dùng hoặc mật khẩu không đúng. Vui lòng thử lại");
                }
            }
            return View("Index");
        }
    }
}