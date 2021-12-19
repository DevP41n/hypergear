using HyperGear.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace HyperGear.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: Admin/User
        private HyperGearEntities db = new HyperGearEntities();

        public ActionResult ListUserAdmin()
        {
            if (Session["HoTen"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            return View(db.Admins.OrderByDescending(m => m.Id).ToList());
        }

        public ActionResult AdminRegister()
        {
            if (Session["HoTen"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminRegister(Models.Admin admin)
        {
            if (ModelState.IsValid)
            {
                var check = db.Admins.FirstOrDefault(s => s.Username == admin.Username);
                if (check == null)
                {
                    admin.Password = MyString.ToMD5(admin.Password);
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.Admins.Add(admin);
                    db.SaveChanges();
                    TempData["Message"] = "Tạo thành công!";
                    return RedirectToAction("ListUserAdmin");
                }
                else
                {
                    TempData["Warning"] = "Tên tài khoản đã tồn tại!";
                    return View();
                }
            }
            return View();


        }

        public ActionResult Delete(int id)
        {
            if (Session["HoTen"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            Models.Admin admin = db.Admins.Find(id);
            db.Admins.Remove(admin);
            TempData["Message"] = "Xóa thành công!";
            db.SaveChanges();
            return RedirectToAction("ListUserAdmin");
        }

        public ActionResult ListCustomer()
        {
            if (Session["HoTen"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            return View(db.KhachHangs.OrderByDescending(m => m.MaKH).ToList());
        }
    }
}