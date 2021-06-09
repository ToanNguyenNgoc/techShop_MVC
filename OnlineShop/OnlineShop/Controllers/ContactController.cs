using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using Model.Dao;
using OnlineShop.Common;
using PagedList;
using PagedList.Mvc;
using System.Threading.Tasks;
using System.Net;

namespace OnlineShop.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            var model = new ContactDao().GetActiveContact();
            return View(model);
        }
        [HttpPost]
        public JsonResult Send(string name, string mobile, string address, string email, string content)
        {
            var feedback = new Feedback();
            feedback.Name = name;
            feedback.Phone = mobile;
            feedback.Address = address;
            feedback.Email = email;
            feedback.Content = content;
            feedback.CreatedDate = DateTime.Now;

            var id = new ContactDao().InsertFeedBack(feedback);
            if (id > 0)
            {
                return Json(new { 
                    status= true
                });
            }
            else
            {
                return Json(new { 
                    status= false
                });
            }
        }
    }
}