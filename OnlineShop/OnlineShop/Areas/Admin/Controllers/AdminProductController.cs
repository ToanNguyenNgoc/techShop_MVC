using System.Web;
using System.Web.Mvc;
using Model.EF;
using Model.Dao;
using OnlineShop.Common;
using PagedList;
using PagedList.Mvc;
using System.Threading.Tasks;
using System.Net;
using System;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class AdminProductController : BaseController
    {
        // GET: Admin/AdminProduct
        public ActionResult Index(string searchString, int page=1, int pageSize=5)
        {
            var dao = new ProductDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        //Create-------
        [HttpGet]
        [HasCredential(RoleID ="ADD_USER")]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }
        [HttpPost]
        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductDao();
                long id = dao.Insert(product);
                if (id > 0)
                {
                    return RedirectToAction("Index", "AdminProduct");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm sản phẩm thành công");
                }
            }
            
            return View("Index");
        }
        //End Create---
        //Update-------
        [HasCredential(RoleID ="EDIT_USER")]
        public ActionResult Edit(long id)
        {
            SetViewBag();
            var product = new ProductDao().ViewDetail(id);
            return View(product);
        }
        [HttpPost]
        [HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductDao();
                var result = dao.Update(product);
                if (result)
                {
                    return RedirectToAction("Index", "AdminProduct");
                }
                else
                {
                    ModelState.AddModelError("", "Update product Fail!");
                }
            }
            return View("Index");
        }
        //EndUpdate----
        //Delete Product---
        [HasCredential(RoleID ="DELETE_USER")]
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        private OnlineShopDBContext db = new OnlineShopDBContext();
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [HasCredential(RoleID = "DELETE_USER")]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            Product product = await db.Products.FindAsync(id);
            db.Products.Remove(product);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        //End Product------
        //Select----
        public void SetViewBag(long? selectedId= null)
        {
            var dao = new ProductCategoryDao();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }
        //End Select----
    }
}