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
    public class AdminOrderController : BaseController
    {
        // GET: Admin/AdminOrder
        public ActionResult Index(string searchString, int page = 1, int pageSize=5)
        {
            var dao = new OrderDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
    }
}