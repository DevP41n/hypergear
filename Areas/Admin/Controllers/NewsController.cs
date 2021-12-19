using HyperGear.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HyperGear.Areas.Admin.Controllers
{
    public class NewsController : Controller
    {
        // GET: Admin/News
        private HyperGearEntities db = new HyperGearEntities();
        public ActionResult ListNews()
        {
            if (Session["HoTen"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            var news = db.TinTucs.OrderByDescending(m => m.MaTT).ToList();
            return View(news);

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
        public ActionResult Create(TinTuc tintuc)
        {
            if (ModelState.IsValid)
            {
                Random rd = new Random();
                var numrd = rd.Next(1, 100).ToString();
                string userslug = MyString.ToAscii(Session["Username"].ToString()) + tintuc.MaTT + numrd;
                String strSlug = MyString.ToAscii(tintuc.TieuDe);
                tintuc.Slug = strSlug;
                tintuc.Created_by = Session["Username"].ToString();
                tintuc.Created_at = DateTime.Now;
                tintuc.Status = 1;
                var file = Request.Files["Anh"];
                if (file != null && file.ContentLength > 0)
                {
                    String filename = userslug + file.FileName.Substring(file.FileName.LastIndexOf("."));
                    tintuc.Anh = filename;
                    String StrPath = Path.Combine(Server.MapPath("~/images/news/"), filename);
                    file.SaveAs(StrPath);
                }
                db.TinTucs.Add(tintuc);
                db.SaveChanges();
                TempData["Message"] = "Tạo thành công!";
                return RedirectToAction("ListNews");
            }
            return View(tintuc);
        }

        public ActionResult Edit(int? id)
        {
            if (Session["HoTen"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            TinTuc tintuc = db.TinTucs.Find(id);
            if (tintuc == null)
            {
                return RedirectToAction("ListNews");
            }
            return View(tintuc);
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TinTuc tintuc)
        {
            if (ModelState.IsValid)
            {
                Random rd = new Random();
                var numrd = rd.Next(1, 100).ToString();
                string userslug = MyString.ToAscii(Session["Username"].ToString()) + tintuc.MaTT + numrd;
                String strSlug = MyString.ToAscii(tintuc.TieuDe);
                tintuc.Slug = strSlug;
                tintuc.Updated_by = Session["Username"].ToString();
                tintuc.Updated_at = DateTime.Now;
                tintuc.Status = 1;
                var file = Request.Files["Anh"];
                if (file != null && file.ContentLength > 0)
                {
                    String filename = userslug + file.FileName.Substring(file.FileName.LastIndexOf("."));
                    tintuc.Anh = filename;
                    String StrPath = Path.Combine(Server.MapPath("~/images/news/"), filename);
                    file.SaveAs(StrPath);
                }

                db.Entry(tintuc).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "Cập nhật thành công!";
                return RedirectToAction("ListNews");
            }
            else
            {
                TempData["Error"] = "Cập nhập không thành công!";
            }
            return View(tintuc);
        }

        public ActionResult Delete(int? id)
        {
            if (Session["HoTen"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            TinTuc tinTuc = db.TinTucs.Find(id);
            db.TinTucs.Remove(tinTuc);
            db.SaveChanges();
            TempData["Message"] = "Xóa thành công!";
            return RedirectToAction("ListNews");
        }


    }
}