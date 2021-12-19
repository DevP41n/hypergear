using HyperGear.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data.Entity;

namespace HyperGear.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private HyperGearEntities db = new HyperGearEntities();
        // GET: Admin/Product
        public ActionResult List()
        {
            ViewBag.trash = db.SanPhams.Where(m => m.Status == 0).Count();
            
            if (Session["Id"] != null)
            {
                return View(db.SanPhams.OrderByDescending(m => m.MaSP).ToList());
            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }            
        }

        public ActionResult Create()
        {
            if (Session["HoTen"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            ViewBag.MaDM = new SelectList(db.DanhMucs.ToList().OrderBy(n => n.MaDM), "MaDM", "TenDM");
            return View();
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SanPham sanPham)
        {
            ViewBag.MaDM = new SelectList(db.DanhMucs.ToList().OrderBy(n => n.MaDM), "MaDM", "TenDM");
            if(ModelState.IsValid)
            {
                Random rd = new Random();
                var numrd = rd.Next(1, 100).ToString();
                String strSlug = MyString.ToAscii(sanPham.TenSP) + numrd + sanPham.MaSP;
                sanPham.Slug = strSlug;
                sanPham.Created_at = DateTime.Now;
                sanPham.Updated_at = DateTime.Now;
                sanPham.Status = 1;
                //Upload File
                var file = Request.Files["Anh"];
                if (file != null && file.ContentLength >0)
                {
                    String filename = strSlug + file.FileName.Substring(file.FileName.LastIndexOf("."));
                    sanPham.Anh = filename;
                    String StrPath = Path.Combine(Server.MapPath("~/images/product/"),filename);
                    file.SaveAs(StrPath);
                } 
                db.SanPhams.Add(sanPham);
                TempData["Message"] = "Tạo thành công!";
                db.SaveChanges();
                return RedirectToAction("List");
            }
            return View(sanPham);
        }

        public ActionResult Edit(int ?id)
        {
            if (Session["HoTen"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            ViewBag.MaDMuc = new SelectList(db.DanhMucs.ToList(), "MaDM", "TenDM");
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham ==null)
            {
                return RedirectToAction("List", "Product");
            }
            return View(sanPham);
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SanPham sanPham)
        {
            ViewBag.MaDMuc = new SelectList(db.DanhMucs.ToList(), "MaDM", "TenDM");
            if (ModelState.IsValid)
            {
                Random rd = new Random();
                var numrd = rd.Next(1, 100).ToString();
                String strSlug = MyString.ToAscii(sanPham.TenSP) +numrd + sanPham.MaSP;
                sanPham.Slug = strSlug;
                sanPham.Updated_at = DateTime.Now;
                //Upload File
                var file = Request.Files["Anh"];
                if (file != null && file.ContentLength > 0)
                {
                    String filename = strSlug + file.FileName.Substring(file.FileName.LastIndexOf("."));
                    sanPham.Anh = filename;
                    String StrPath = Path.Combine(Server.MapPath("~/images/product/"));
                    file.SaveAs(Path.Combine(StrPath, filename));
                }
                db.Entry(sanPham).State = EntityState.Modified;
                TempData["Message"] = "Cập nhật thành công!";
                db.SaveChanges();
                return RedirectToAction("List");
            }
            else
            {
                TempData["Error"] = "Cập nhập không thành công!";
            }
            return View(sanPham);
        }

        [HttpPost]
        public JsonResult changeStatus(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            sanPham.Status = (sanPham.Status == 1) ? 2 : 1;
            //if (mProduct.Status==1)
            //{
            //    mProduct.Status = 2;
            //}
            //else
            //{
            //    mProduct.Status = 1;
            //}

            sanPham.Updated_at = DateTime.Now;
            sanPham.Updated_by = (Session["Username"].ToString());
            db.Entry(sanPham).State = EntityState.Modified;
            db.SaveChanges();
            return Json(new { Status = sanPham.Status });
        }
        public ActionResult DelToTrash(int? id)
        {
            if (Session["HoTen"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            SanPham sanPham = db.SanPhams.Find(id);
            sanPham.Status = 0;

            sanPham.Updated_at = DateTime.Now;
            sanPham.Updated_by = (Session["Username"].ToString());
            db.Entry(sanPham).State = EntityState.Modified;
            TempData["Message"] = "Đã chuyển vào thùng rác!";
            db.SaveChanges();
            return RedirectToAction("List");
        }

        public ActionResult Undo(int? id)
        {
            if (Session["HoTen"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            SanPham sanPham = db.SanPhams.Find(id);
            sanPham.Status = 2;

            sanPham.Updated_at = DateTime.Now;
            sanPham.Updated_by = (Session["Username"].ToString());
            db.Entry(sanPham).State = EntityState.Modified;
            TempData["Message"] = "Khôi phục thành công!";
            db.SaveChanges();
            return RedirectToAction("List");
        }


        // POST: Admin/Product/Delete/5
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["HoTen"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
            TempData["Message"] = "Xóa thành công!";
            db.SaveChanges();
            return RedirectToAction("List");
        }

    }
}