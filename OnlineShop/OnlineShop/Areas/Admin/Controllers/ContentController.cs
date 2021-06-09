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
    public class ContentController : BaseController
    {
        // GET: Admin/Content
        public ActionResult Index(string searchString, int page= 1, int pageSize=5)
        {
            var dao = new ContentDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }
        public ActionResult Edit(int id)
        {
            var content = new ContentDao().ViewDetail(id);
            SetViewBag();
            return View(content);
        }
        [HttpPost]
        public ActionResult Edit(Content content)
        {
            if (ModelState.IsValid)
            {
                var dao = new ContentDao();
                var result = dao.Update(content);
                if (result)
                {
                    return RedirectToAction("Index", "Content");
                }
                else
                {
                    ModelState.AddModelError("", "Sửa thông tin không thành công");
                }
            }
           
            return View("Index");
        }
        [HttpPost]
        public ActionResult Create( Content content)
        {
            if (ModelState.IsValid)
            {
                var dao = new ContentDao();
                long id = dao.Insert(content);
                if (id > 0)
                {
                    return RedirectToAction("Index", "Content");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm Content thất bại");
                }
            }
            return View("Index");
        }
        //select box
        public void SetViewBag(long? selectedId = null)
        {
            var dao = new CategoryDao();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Content content = await db.Contents.FindAsync(id);
            if (content == null)
            {
                return HttpNotFound();
            }
            
            return View(content);
        }
        private OnlineShopDBContext db = new OnlineShopDBContext();
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Content content = await db.Contents.FindAsync(id);
            db.Contents.Remove(content);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}