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
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult ProductCategory()
        {
            var model = new ProductCategoryDao().ListAll();
            return PartialView(model);
        }
        //Search
        public JsonResult ListName(string q)
        {
            var data = new ProductDao().ListName(q);
            return Json(new { 
                data= data,
                status= true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Search(string keyword, int page=1, int pageSize = 10)
        {
            int totalRecord = 0;
            var model = new ProductDao().Search(keyword, ref totalRecord, page, pageSize);

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;
            ViewBag.Keyword = keyword;
            int maxPage = 5;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;

            return View(model);
        }
        //------
        public ActionResult Category(long id, int page = 1, int pageSize=10)
        {
            var category = new CategoryDao().ViewDetail(id);
            ViewBag.Category = category;
            int totalRecord = 0;
            var model = new ProductDao().ListByCategoryId(id,ref totalRecord, page, pageSize);
            //_______
            ViewBag.Total = totalRecord;
            ViewBag.Page = page;
            int maxPage = 5;
            int totalPage = 0;
            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;
            //________
            return View(model);
        }
        public ActionResult Detail(long id)
        {
            var product = new ProductDao().ViewDetail(id);
            ViewBag.Category = new ProductCategoryDao().ViewDetail(product.CategoryID.Value);
            ViewBag.RecommenProduct = new ProductDao().ListRecommenProduct(id);
            return View(product);
        }
    }
}