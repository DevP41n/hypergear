using HyperGear.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HyperGear.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        private HyperGearEntities db = new HyperGearEntities();
        // GET: Admin/Admin
        public ActionResult Dashboard()
        {
            if (Session["HoTen"] != null)
            {
                ViewBag.order = db.DonDatHangs.Count();
                ViewBag.sold = db.DonDatHangs.Where(m => m.Dathanhtoan == true).Count();
                ViewBag.proceeds = db.ChiTietDonHangs.Where(m => m.DonDatHang.Dathanhtoan == true).Sum(m=>m.TongTien);
                ViewBag.product = db.SanPhams.Count();
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
        }

    }
}