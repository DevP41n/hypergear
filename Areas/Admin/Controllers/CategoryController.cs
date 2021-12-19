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
    public class CategoryController : Controller
    {

        // GET: Admin/Category
        private HyperGearEntities db = new HyperGearEntities();
        public ActionResult CategoryList()
        {
            if (Session["HoTen"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            return View(db.DanhMucs.OrderByDescending(m => m.MaDM).ToList());
        }

        public ActionResult Create()
        {
            if (Session["HoTen"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            ViewBag.MaMn = new SelectList(db.Menus.ToList().OrderBy(n => n.MaMn), "MaMn", "TenMn");
            return View();
        }

        [HttpPost]
        public ActionResult Create(DanhMuc danhMuc)
        {
            ViewBag.MaMenu = new SelectList(db.Menus.ToList().OrderBy(n => n.MaMn), "MaMn", "TenMn");
            String strSlug = MyString.ToAscii(danhMuc.TenDM);
            danhMuc.Slug = strSlug;
            danhMuc.Created_at = DateTime.Now;
            danhMuc.Updated_at = DateTime.Now;          
            danhMuc.Status = 1;
            db.DanhMucs.Add(danhMuc);
            TempData["Message"] = "Tạo thành công!";
            db.SaveChanges();
            return RedirectToAction("CategoryList");
        }

        public ActionResult Edit(int? id)
        {
            if (Session["HoTen"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            ViewBag.MaMenu = new SelectList(db.Menus.ToList(), "MaMn", "TenMn");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DanhMuc danhMuc = db.DanhMucs.Find(id);
            if (danhMuc == null)
            {
                return HttpNotFound();
            }
            return View(danhMuc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DanhMuc danhMuc)
        {
            ViewBag.MaMn = new SelectList(db.Menus.ToList(), "MaMn", "TenMn");
            if (ModelState.IsValid)
            {
                String strSlug = MyString.ToAscii(danhMuc.TenDM);
                danhMuc.Slug = strSlug;
                danhMuc.Updated_at = DateTime.Now;
                db.Entry(danhMuc).State = EntityState.Modified;
                TempData["Message"] = "Cập nhật thành công!";
                db.SaveChanges();
                return RedirectToAction("CategoryList");
            }
            else
            {
                TempData["Error"] = "Cập nhập không thành công!";
            }
            return View(danhMuc);
        }
        public ActionResult Delete(int id)
        {
            if (Session["HoTen"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            var del = from dele in db.SanPhams
                      where dele.MaDM == id
                      select dele;
            var coundel = del.Count();
            if (coundel == 0)
            {
                DanhMuc danhMuc = db.DanhMucs.Find(id);
                db.DanhMucs.Remove(danhMuc);
                TempData["Message"] = "Xóa thành công!";
                db.SaveChanges();
            }
            else
            {
                TempData["Warning"] = "Không thể xóa vì đang có sản phẩm tồn tại trong danh mục!";
            }
            return RedirectToAction("CategoryList");
        }
    }
}