using HyperGear.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.Data.Entity;

namespace HyperGear.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        // GET: Home
        private HyperGearEntities db = new HyperGearEntities();
        public ActionResult Index()
        {
            var list = db.SanPhams.Where(m => m.Status == 1)
                .Take(8).OrderByDescending(m=>m.Created_at).ToList();
            return View(list);
        }

        public ActionResult Menu()
        {
            var result = db.DanhMucs;
            ViewBag.MainMenu = db.Menus;
            return View("_MainMenu",result);
        }

        public ActionResult Contact()
        {
            ViewBag.Cont = db.LienHes.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(Feedback fb)
        {
            ViewBag.Cont = db.LienHes.ToList();
            if (ModelState.IsValid)
            {
                fb.NgayNhan = DateTime.Now;
                fb.Status = 1;
                db.Feedbacks.Add(fb);
                db.SaveChanges();

                return RedirectToAction("Contact");

            }
            return View(fb);
        }

        //Danh sách sản phẩm (Menu)
        public ActionResult LstMenu(string id, int? page)
        {

            int pageSize = 8;
            int pageNum = (page ?? 1);
            Menu menu = db.Menus.Where(v => v.Slug == id).First();
            ViewBag.Mn = menu.TenMn;
            var sp = db.SanPhams.Where(x => x.DanhMuc.Menu.Slug == id && x.Status == 1).ToList();
            ViewBag.id = id;
            return View(sp.ToPagedList(pageNum, pageSize));
        }

        public ActionResult LstDM(string id, int ? page)
        {
            int pageSize = 8;
            int pageNum = (page ?? 1);
            DanhMuc dm = db.DanhMucs.Where(n => n.Slug == id).First();
            ViewBag.DM = dm.TenDM;
            var sp = db.SanPhams.Where(x => x.DanhMuc.Slug == id && x.Status ==1).ToList();
            ViewBag.id = id;
            return View(sp.ToPagedList(pageNum, pageSize));
        }

        public ActionResult Details (string id)
        {
            var product = db.SanPhams
                        .Where(m => m.Slug == id && m.Status == 1).First();
            return View(product);
        }
        [ChildActionOnly]       
        public ActionResult _SearchParital()
        {
            return PartialView();
        }

        
        public ActionResult Search(string keyword, int ? page)
        {
            int pageSize = 8;
            int pageNum = (page ?? 1);
            var sp = from s in db.SanPhams select s;
            if (!String.IsNullOrEmpty(keyword))
            {
                sp = sp.Where(s => s.TenSP.Contains(keyword) || 
                s.DanhMuc.TenDM.Contains(keyword) || 
                s.DanhMuc.Menu.TenMn.Contains(keyword)|| 
                keyword == null);
            }
            ViewBag.kw = keyword;
            var spp = sp.OrderBy(m => m.MaSP);
            return View(spp.ToPagedList(pageNum, pageSize));
        }
        [ChildActionOnly]
        public ActionResult _ViewOrder()
        {
            return PartialView();
        }

        public ActionResult TrackOrder (int id)
        {
            var mak = Convert.ToInt32(Session["MaKH"]);
            if (mak != id)
            {
                TempData["Warning"] = "Không đúng tài khoản của bạn!";
                return RedirectToAction("Index", "Home");
            }
            var results = (from od in db.ChiTietDonHangs
                           join o in db.DonDatHangs on od.MaDonHang equals o.MaDonHang
                           where o.MaKH == id
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

        public ActionResult ViewTrackOrder (int? id)
        {
            DonDatHang ddh = db.DonDatHangs.Find(id);
            var mak = Convert.ToInt32(Session["MaKH"]);
            if (mak != ddh.MaKH)
             {
                TempData["Warning"] = "Không đúng đơn hàng của bạn!";
                return RedirectToAction("Index", "Home");
             }
            ViewBag.ctdh = db.ChiTietDonHangs.Where(n => n.MaDonHang == id);
            ViewBag.productOrder = db.SanPhams.ToList();
            return View(ddh);
        }

        public ActionResult CancelTOrder (int id, string strURL)
        {
            DonDatHang ddh = db.DonDatHangs.Find(id);
            var mak = Convert.ToInt32(Session["MaKH"]);
            if (mak != ddh.MaKH)
            {
                TempData["Warning"] = "Không đúng đơn hàng của bạn!";
                return RedirectToAction("Index", "Home");
            }
            if (ddh.Tinhtranggiaohang==1)
            {
                foreach (var item in db.ChiTietDonHangs.Where(x => x.MaDonHang == id))
                {
                    foreach (var items in db.SanPhams.Where(c => c.MaSP == item.MaSP))
                    {
                        items.Soluongton += item.Soluong;
                    }
                }
                ddh.Tinhtranggiaohang = 0;
            }
            else
            {
                TempData["Error"] = "Không thể hủy đơn hàng trong tình trạng đang giao!";
                return Redirect(strURL);
            }
            db.Entry(ddh).State = EntityState.Modified;
            db.SaveChanges();
            TempData["Message"] = "Hủy đơn hàng thành công!";
            return Redirect(strURL);
        }
    }
}