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
    public class UserProfileController : Controller
    {
        // GET: Admin/UserProfile
        public ActionResult Index()
        {
            return View();
        }
    }
}