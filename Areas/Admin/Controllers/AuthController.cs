using HyperGear.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HyperGear.Areas.Admin.Controllers
{
    public class AuthController : Controller
    {
        private HyperGearEntities db = new HyperGearEntities();
        // GET: Admin/Auth
        public ActionResult Login()
        {
            if (Session["HoTen"] != null)
            {
                return RedirectToAction("Dashboard", "Admin");
            }
            else
            {
                return View();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                var f_password = MyString.ToMD5(password);
                var data = db.Admins.Where(s => s.Username.Equals(username) && s.Password.Equals(f_password)).ToList();

                if (data.Count() > 0)
                {

                    //add session
                    Session["HoTen"] = data.FirstOrDefault().HoTen;
                    Session["Username"] = data.FirstOrDefault().Username;
                    Session["Id"] = data.FirstOrDefault().Id;
                    var ten = Convert.ToString(Session["HoTen"]);
                    TempData["Message"] = "Đăng nhập thành công!</br>" + "Xin chào " + ten;
                    return RedirectToAction("Dashboard", "Admin");
                }
                else
                {
                    TempData["Warning"] = "Sai Thông tin đăng nhập vui lòng nhập lại!";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }


        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}