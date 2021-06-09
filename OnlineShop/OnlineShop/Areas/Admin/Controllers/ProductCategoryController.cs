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
    public class ProductCategoryController : BaseController
    {
        // GET: Admin/ProductCategory
        public ActionResult Index(string searchString, int page=1, int pageSize=7)
        {
            var dao = new ProductCategoryDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(long id)
        {
            var productCategory = new ProductCategoryDao().ViewDetail(id);
            return View(productCategory);
        }
        [HttpPost]
        [HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(ProductCategory productcategory)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductCategoryDao();
                var result = dao.Update(productcategory);
                if (result)
                {
                    return RedirectToAction("Index", "ProductCategory");
                }
                else
                {
                    ModelState.AddModelError("", "Update user Fail!");
                }
            }
            return View("Index");
        }
        [HttpGet]
        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create(ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductCategoryDao();
                long id = dao.Insert(productCategory);
                if (id > 0)
                {
                    return RedirectToAction("Index", "ProductCategory");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm loại sản phẩm thành công");
                }
            }
            return View("Index");
        }
        //Delete
        [HasCredential(RoleID = "DELETE_USER")]
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductCategory productCategory = await db.ProductCategories.FindAsync(id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            return View(productCategory);
        }
        private OnlineShopDBContext db = new OnlineShopDBContext();
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [HasCredential(RoleID = "DELETE_USER")]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            ProductCategory productCategory = await db.ProductCategories.FindAsync(id);
            db.ProductCategories.Remove(productCategory);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        //End_Delete
    }
}