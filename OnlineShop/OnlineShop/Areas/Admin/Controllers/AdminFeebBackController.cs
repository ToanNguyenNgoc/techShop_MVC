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

namespace OnlineShop.Areas.Admin.Controllers
{
    public class AdminFeebBackController : BaseController
    {
        // GET: Admin/AdminFeebBack
        public ActionResult Index(string searchString, int page=1, int pageSize= 8)
        {
            var dao = new FeebBackDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        public async Task<ActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = await db.Feedbacks.FindAsync(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }
        private OnlineShopDBContext db = new OnlineShopDBContext();
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DetailConfirmed(int id)
        {
            Feedback feedback = await db.Feedbacks.FindAsync(id);
            db.Feedbacks.Remove(feedback);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}