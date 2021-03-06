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
    public class CartController : Controller
    {
        private const string CartSession = "CartSession"; //Hang so khong the thay doi
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if(cart!= null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }
        //Xoa gio hang---
        public JsonResult DeleteAll()
        {
            Session[CartSession] = null;
            return Json(new { 
                status= true
            });
        }
        public JsonResult Delete(long id)
        {
            var sessionCart = (List<CartItem>)Session[CartSession];
            sessionCart.RemoveAll(x => x.Product.ID == id);
            Session[CartSession] = sessionCart;
            return Json(new { 
                status = true
            });
        }
        //End------------
        //Cap nhat gio hang-----------------------
        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CartSession];
            foreach(var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ID == item.Product.ID);
                if(jsonItem!= null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status= true
            });
        }
        //End-------
        //Xu ly gio hang---------
        public ActionResult AddItem(long productId, int quantity)
        {
            var product = new ProductDao().ViewDetail(productId);
            var cart = Session[CartSession];
            if(cart != null)
            {
                var list = (List<CartItem>)cart;
                if(list.Exists(x=> x.Product.ID== productId)) //Neu list chua productId thi them moi Quantity(so luong)
                {
                    foreach(var item in list)
                    {
                        if(item.Product.ID== productId)
                        {
                            item.Quantity += quantity; //Cong them so luong
                        }
                    }
                }
                else
                {
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                //Gan vao Session
                Session[CartSession] = list;
            }
            else
            {
                //Tao moi doi tuong cart item
                var item = new CartItem();
                item.Product = product;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                //Gan vao Session
                Session[CartSession] = list;
            }
            return RedirectToAction("Index");
        }
        //End--------------------
        //Thanh toan---------
        [HttpGet]
        public ActionResult Payment()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }
        [HttpPost]
        public ActionResult Payment(string shipName, string mobile, string address, string email)
        {
            var order = new Order();
            order.CreatedDate = DateTime.Now;
            order.ShipName = shipName;
            order.ShipMobile = mobile;
            order.ShipAddress = address;
            order.ShipEmail = email;

            try
            {
                var id = new OrderDao().Insert(order);
                var cart = (List<CartItem>)Session[CartSession];
                var detailDao = new OrderDetailDao();
                decimal total = 0;
                foreach(var item in cart)
                {
                    var orderDetail = new OrderDetail();
                    orderDetail.ProductID = item.Product.ID;
                    orderDetail.OrderID = id;
                    orderDetail.Price = item.Product.Price;
                    orderDetail.Quantity = item.Quantity;
                    detailDao.Insert(orderDetail);

                    total += (item.Product.Price.GetValueOrDefault(0) * item.Quantity);
                }
                //Send mail-------
                //string content = System.IO.File.ReadAllText(Server.MapPath("~/Assets/Client/EmailTemplate/EmailOrder.html"));

                //content = content.Replace("{{CustomerName}}", shipName);
                //content = content.Replace("{{Phone}}", mobile);
                //content = content.Replace("{{Email}}", email);
                //content = content.Replace("{{Address}}", address);
                //content = content.Replace("{{Total}}", total.ToString("N0"));
                //var fromEmail = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
                //var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                //new MailHelper().SendMail(fromEmail, "Đơn hàng mới từ OnlineShop", content);
                //new MailHelper().SendMail(toEmail, "Đơn hàng mới từ OnlineShop", content);
                //End SendMail----
            }
            catch (Exception ex)
            {
                return Redirect("/loi-thanh-toan");
            }

            return Redirect("/hoan-thanh");
        }
        //End----------------
        //Thanh toan thanh cong---
        public ActionResult OrderSuccess()
        {
            return View();
        }
        //End
    }
}