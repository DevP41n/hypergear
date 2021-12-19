using HyperGear.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HyperGear.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        // GET: Admin/Order
        private HyperGearEntities db = new HyperGearEntities();
        public ActionResult ListOrder()
        {
            if (Session["HoTen"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            var results = (from od in db.ChiTietDonHangs
                           join o in db.DonDatHangs on od.MaDonHang equals o.MaDonHang

                           group od by new { od.MaDonHang, o } into groupb
                           select new ListOrder()
                           {
                               MaDonHang = groupb.Key.MaDonHang,
                               SAmount = (double)groupb.Sum(m => m.TongTien),
                               DeliveryName = groupb.Key.o.DeliveryName,
                               Tinhtranggiaohang = (int)groupb.Key.o.Tinhtranggiaohang,
                               Ngaydat = (System.DateTime)groupb.Key.o.Ngaydat,
                               Ngaygiao = groupb.Key.o.Ngaygiao,
                           }).OrderByDescending(m => m.MaDonHang);

            return View(results.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (Session["HoTen"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            DonDatHang ddh = db.DonDatHangs.Find(id);
            ViewBag.ctdh = db.ChiTietDonHangs.Where(n => n.MaDonHang == id);
            ViewBag.productOrder = db.SanPhams.ToList();
            return View(ddh);
        }

        public ActionResult UpStatus(int? id)
        {
            if (Session["HoTen"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            DonDatHang ddh = db.DonDatHangs.Find(id);
            ddh.Tinhtranggiaohang = ddh.Tinhtranggiaohang + 1;
            if (ddh.Tinhtranggiaohang == 2)
            {
                ddh.Ngaygiao = DateTime.Now;
            }
            else if (ddh.Tinhtranggiaohang == 3)
            {
                ddh.Dathanhtoan = true;
            }

            db.Entry(ddh).State = EntityState.Modified;
            db.SaveChanges();
            TempData["Message"] = "Cập nhật thành công!";
            return RedirectToAction("ListOrder");
        }

        public ActionResult CancelOrder(int? id)
        {
            if (Session["HoTen"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            DonDatHang ddh = db.DonDatHangs.Find(id);
            if (ddh.Tinhtranggiaohang == 1 || ddh.Tinhtranggiaohang == 2)
            {
                ddh.Tinhtranggiaohang = 0;
            }
            db.Entry(ddh).State = EntityState.Modified;
            
            TempData["Message"] = "Hủy thành công!";

            foreach(var item in db.ChiTietDonHangs.Where(x=>x.MaDonHang == id))
            {
                foreach(var items in db.SanPhams.Where(c=>c.MaSP == item.MaSP))
                {
                    items.Soluongton += item.Soluong;
                }
            }
            db.SaveChanges();

            return RedirectToAction("ListOrder");
        }

    }
}