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
    public class UserController : BaseController
    {
        // GET: Admin/User
        [HasCredential(RoleID = "VIEW_USER")]
        public ActionResult Index(string searchString, int page=1, int pageSize= 3)
        {
            var dao = new UserDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        [HttpGet]
        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }
        [HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(int id)
        {
            var user = new UserDao().ViewDetail(id);
            SetViewBag();
            return View(user);
        }
        [HttpPost]
        [HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();

                //var encryptedMd5Pas = Encryptor.MD5Hash(user.Password);
                //user.Password = encryptedMd5Pas;

                var result = dao.Update(user);
                if (result)
                {
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Update user Fail!");
                }
            }
            return View("Index");

        }
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new UserDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }

        [HttpPost]
        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var EncryptedMd5Pas = Encryptor.MD5Hash(user.Password);
                user.Password = EncryptedMd5Pas;
                long id = dao.Insert(user);
                if (id > 0)
                {
                    SetAlert("Thêm người dùng thành công!", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "THêm người dùng không thành công!");
                }
            }
            return View("Index");

        }
        public async Task<ActionResult> Delete(int? id)
        {
            if(id== null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if(user== null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        private OnlineShopDBContext db = new OnlineShopDBContext();
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            User user = await db.Users.FindAsync(id);
            db.Users.Remove(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        //List select user---
        public void SetViewBag(string selectedId= null)
        {
            var dao = new UserGroupDao();
            ViewBag.GroupID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }
        //End----------------

    }
}