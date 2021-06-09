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
using OnlineShop.Models;
using System.Web.Script.Serialization;
using System.Configuration;
using Common;
using System.IO;
namespace OnlineShop.Controllers
{
    public class WishlistController : Controller
    {
        private const string WishlistSession = "WishlistSession";
        // GET: Wishlist
        public ActionResult Index()
        {
            var wishlist = Session[WishlistSession];
            var list = new List<WishlistItem>();
            if (wishlist != null)
            {
                list = (List<WishlistItem>)wishlist;
            }
            return View(list);
        }
        public ActionResult AddItem(long productId, int quantity)
        {
            var product = new ProductDao().ViewDetail(productId);
            var wishlist = Session[WishlistSession];
            if (wishlist != null)
            {
                var list = (List<WishlistItem>)wishlist;
                if(list.Exists(x=> x.Product.ID == productId))
                {
                    foreach(var item in list)
                    {
                        item.Quantity += quantity;
                    }
                }
                else
                {
                    var item = new WishlistItem();
                    item.Product = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                Session[WishlistSession] = list;
            }
            else
            {
                var item = new WishlistItem();
                item.Product = product;
                item.Quantity = quantity;
                var list = new List<WishlistItem>();
                list.Add(item);
                Session[WishlistSession] = list;
            }
            return RedirectToAction("Index");
        }
        //Delate item in Wishlish
        public JsonResult Delete(long id)
        {
            var sessionWishlist = (List<WishlistItem>)Session[WishlistSession];
            sessionWishlist.RemoveAll(x => x.Product.ID == id);
            Session[WishlistSession] = sessionWishlist;
            return Json(new { 
                status= true
            });
        }
        //End--------------------
    }
}