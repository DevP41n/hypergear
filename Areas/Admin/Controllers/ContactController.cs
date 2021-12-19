using HyperGear.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HyperGear.Areas.Admin.Controllers
{
    public class ContactController : Controller
    {
        private HyperGearEntities db = new HyperGearEntities();
        // GET: Admin/Contact
        public ActionResult Contact()
        {
            if (Session["HoTen"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            return View(db.LienHes.OrderByDescending(m => m.MaLH).ToList());
        }

        public ActionResult Create()
        {
            if (Session["HoTen"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            return View();
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LienHe lh)
        {

            if (ModelState.IsValid)
            {

                lh.Created_at = DateTime.Now;
                lh.Updated_at = DateTime.Now;
                lh.Status = 1;
                //Upload File
                db.LienHes.Add(lh);
                TempData["Message"] = "Tạo thành công!";
                db.SaveChanges();
                return RedirectToAction("Contact");
            }
            return View(lh);
        }

        public ActionResult Edit(int? id)
        {
            if (Session["HoTen"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            if (id == null)
            {
                return RedirectToAction("Contact", "Contact");
            }
            LienHe lh = db.LienHes.Find(id);
            if (lh == null)
            {
                return HttpNotFound();
            }

            return View(lh);
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LienHe lh)
        {
            if (ModelState.IsValid)
            {
                lh.Updated_at = DateTime.Now;
                db.Entry(lh).State = EntityState.Modified;                
                db.SaveChanges();
                TempData["Message"] = "Cập nhật thành công!";
                return RedirectToAction("Contact","Contact");
            }
            else
            {
                TempData["Error"] = "Cập nhập không thành công!";
            }
            return View(lh);
        }
        public ActionResult Delete(int id)
        {
            if (Session["HoTen"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            LienHe lh = db.LienHes.Find(id);
            db.LienHes.Remove(lh);
            TempData["Message"] = "Xóa thành công!";
            db.SaveChanges();

            return RedirectToAction("Contact");
        }

    }
}