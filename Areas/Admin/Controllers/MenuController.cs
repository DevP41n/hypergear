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
    public class MenuController : Controller
    {
        // GET: Admin/Menu
        private HyperGearEntities db = new HyperGearEntities();
        public ActionResult MenuList()
        {
            if (Session["HoTen"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            return View(db.Menus.OrderByDescending(m => m.MaMn).ToList());
        }

        public ActionResult Create()
        {
            if (Session["HoTen"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Create(Menu menu)
        {
            String strSlug = MyString.ToAscii(menu.TenMn);
            menu.Slug = strSlug;
            menu.Created_at = DateTime.Now;
            menu.Updated_at = DateTime.Now;
            menu.Status = 1;
            db.Menus.Add(menu);
            TempData["Message"] = "Tạo thành công!";
            db.SaveChanges();
            return RedirectToAction("MenuList");
        }

        public ActionResult Edit(int? id)
        {
            if (Session["HoTen"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Menu menu)
        {
            if (ModelState.IsValid)
            {
                String strSlug = MyString.ToAscii(menu.TenMn);
                menu.Slug = strSlug;
                menu.Updated_at = DateTime.Now;
                db.Entry(menu).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "Cập nhật thành công!";
                return RedirectToAction("MenuList");
            }
            else
            {
                TempData["Error"] = "Cập nhập không thành công!";
            }
            return View(menu);
        }
        public ActionResult Delete(int id)
        {
            if (Session["HoTen"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            var del = from dele in db.DanhMucs
                      where dele.MaMn == id
                      select dele;
            var coundel = del.Count();
            if (coundel == 0)
            {
                Menu menu = db.Menus.Find(id);
                db.Menus.Remove(menu);
                TempData["Message"] = "Xóa thành công!";
                db.SaveChanges();
            }
            else
            {
                TempData["Warning"] = "Không thể xóa vì đang có danh mục tồn tại trong menu!";
            }
            return RedirectToAction("MenuList");
        }

    }
}