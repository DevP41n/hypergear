using HyperGear.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HyperGear.Controllers
{
    public class ShopController : Controller
    {
        private HyperGearEntities db = new HyperGearEntities();
        // GET: Shop
        public ActionResult ShopFull()
        {
            ViewBag.Menu = db.Menus.ToList();
            ViewBag.DanhMuc = db.DanhMucs.ToList();
            return View(db.SanPhams.Where(m => m.Status == 1)
                .OrderByDescending(m => m.MaSP));
        }
    }
}