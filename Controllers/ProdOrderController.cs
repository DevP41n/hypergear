using HyperGear.Library;
using HyperGear.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace HyperGear.Controllers
{
    public class ProdOrderController : Controller
    {
        HyperGearEntities db = new HyperGearEntities();
        // GET: ProdOrder
        public List<Order> OrderCart()
        {
            List<Order> lstOrder = Session["Cart"] as List<Order>;
            if (lstOrder == null)
            {
                lstOrder = new List<Order>();
                Session["Cart"] = lstOrder;
            }
            return lstOrder;
        }
        public ActionResult AddCart(int oMaSP, string strURL)
        {
            List<Order> lstOrder = OrderCart();
            Order ordsp = lstOrder.Find(n => n.oMaSP == oMaSP);
            SanPham sp = db.SanPhams.Find(oMaSP);
            if (ordsp == null)
            {
                ordsp = new Order(oMaSP);
                var them1 = Convert.ToInt32(Request.Form["soluong"]);
                if (sp.Soluongton < them1)
                {
                    TempData["Warning"] = "Số lượng sản phẩm không đủ!";
                    return Redirect(strURL);
                }

                ordsp.oSoluong = Convert.ToInt32(Request.Form["soluong"]);
                if (ordsp.oSoluong == 0)
                {
                    ordsp.oSoluong = 1;
                }
                lstOrder.Add(ordsp);
                TempData["Message"] = "Đã thêm vào giỏ hàng!";
                return Redirect(strURL);

            }
            else
            {
                var them = Convert.ToInt32(Request.Form["soluong"]);
                if (sp.Soluongton - ordsp.oSoluong < them)
                {
                    TempData["Warning"] = "Số lượng sản phẩm không đủ!";
                    return Redirect(strURL);
                }
                
                if (them > 0)
                {
                    ordsp.oSoluong += them;
                }
                else
                {
                    ordsp.oSoluong++;
                }

                TempData["Message"] = "Đã thêm vào giỏ hàng!";
                return Redirect(strURL);
            }
        }

        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<Order> lstOrder = Session["Cart"] as List<Order>;
            if (lstOrder != null)
            {
                iTongSoLuong = lstOrder.Sum(n => n.oSoluong);
            }
            return iTongSoLuong;
        }

        private double TongTien()
        {
            double iTongTien = 0;
            List<Order> lstOrder = Session["Cart"] as List<Order>;
            if (lstOrder != null)
            {
                iTongTien = lstOrder.Sum(n => n.oThanhTien);
            }
            return iTongTien;
        }

        public ActionResult GioHang()
        {
            List<Order> lstOrder = OrderCart();
            if (lstOrder.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(lstOrder);
        }


        public ActionResult GiohangPartial()
        {
            List<Order> lstOrder = OrderCart();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.list = lstOrder.Count();
            return PartialView(lstOrder);
        }

        public ActionResult XoaGioHang(int oMaSP, string strURL)
        {
            List<Order> lstOrder = OrderCart();
            Order ordsp = lstOrder.SingleOrDefault(n => n.oMaSP == oMaSP);
            if (ordsp != null)
            {
                lstOrder.RemoveAll(n => n.oMaSP == oMaSP);
                TempData["Message"] = "Đã xóa sản phẩm!";
                return Redirect(strURL);
            }
            if (lstOrder.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return Redirect(strURL);
        }

        public ActionResult CapnhatGiohang(int oMaSP, FormCollection f)
        {
            List<Order> lstOrder = OrderCart();
            Order ordsp = lstOrder.SingleOrDefault(n => n.oMaSP == oMaSP);
            SanPham sp = db.SanPhams.Find(oMaSP);
            if(sp.Soluongton == ordsp.oSoluong)
            {
                TempData["Warning"] = "Số lượng sản phẩm không đủ!";
                return RedirectToAction("GioHang");
            }
            if (ordsp != null)
            {
                ordsp.oSoluong ++;
            }
            return RedirectToAction("GioHang");
        }

        public ActionResult Tru(int oMaSP, FormCollection f)
        {
            List<Order> lstOrder = OrderCart();
            Order ordsp = lstOrder.SingleOrDefault(n => n.oMaSP == oMaSP);
            if (ordsp != null)
            {
                if(ordsp.oSoluong ==1 )
                {
                    TempData["Warning"] = "Số lượng không thể bằng 0!";
                    return RedirectToAction("GioHang");
                }
                ordsp.oSoluong--;
            }
            return RedirectToAction("GioHang");
        }


        public ActionResult XoatatcaGiohang()
        {
            List<Order> lstOrder = OrderCart();
            lstOrder.Clear();
            TempData["Message"] = "Đã xóa giỏ hàng!";
            return RedirectToAction("Index", "Home");
        }

        public ActionResult DatHang()
        {
            if (Session["TenCus"] == null)
            {
                return RedirectToAction("CusLogin", "Account");
            }
            if (Session["Cart"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            string id = Convert.ToString(Session["EmailCus"]);
            ViewBag.Custom = db.KhachHangs.Where(m => m.Email == id).First();
            List<Order> lstOrder = OrderCart();
            if (lstOrder.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(lstOrder);
        }

        [HttpPost]
        public ActionResult DatHang(FormCollection f)
        {
            DonDatHang ddh = new DonDatHang();
            List<Order> lstOrder = OrderCart();
            int str = Convert.ToInt32(Session["MaKH"]);
            ddh.MaKH = str;
            ddh.Ngaydat = DateTime.Now;
            ddh.DeliveryName = Request.Form["ten"];
            ddh.DeliveryPhone = Request.Form["sdt"];
            ddh.DeliveryEmail = Request.Form["email"];
            ddh.DeliveryAdd = Request.Form["dc"];
            ddh.Tinhtranggiaohang = 1;
            ddh.Dathanhtoan = false;
            db.DonDatHangs.Add(ddh);
            db.SaveChanges();
            decimal Amount = 0;
            foreach (var item in lstOrder)
            {
                ChiTietDonHang ctdh = new ChiTietDonHang();
                ctdh.MaDonHang = ddh.MaDonHang;
                ctdh.MaSP = item.oMaSP;
                ctdh.Soluong = item.oSoluong;
                ctdh.Dongia = (decimal)item.oGia;
                ctdh.TongTien = (decimal)item.oThanhTien;
                Amount += (decimal)item.oThanhTien;
                db.ChiTietDonHangs.Add(ctdh);
                foreach(var items in db.SanPhams.Where(x=> x.MaSP == item.oMaSP))
                {
                    items.Soluongton -= item.oSoluong;
                }
            }
            //save
            db.SaveChanges();
            //Send Mail
            string mail = System.IO.File.ReadAllText(Server.MapPath("~/template/mail/MailOrder.html"));
            string dt = DateTime.Now.ToString();
            mail = mail.Replace("{{Name}}", Request.Form["ten"]);
            mail = mail.Replace("{{Phone}}", Request.Form["sdt"]);
            mail = mail.Replace("{{Email}}", Request.Form["email"]);
            mail = mail.Replace("{{Address}}", Request.Form["dc"]);
            mail = mail.Replace("{{Amount}}", Amount.ToString("N0"));
            mail = mail.Replace("{{date}}", dt);
            var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();
            Random rd = new Random();
            var numrd = rd.Next(1, 1000000).ToString();
            new SendMailOrder().SendMailTo(Request.Form["email"], "Đơn hàng mới từ HYPER GEAR [HYPER" + numrd + "]", mail);
            
            Session.Remove("Cart");
            return View("xacnhan");
            //return RedirectToAction("PaymentWithPaypal", "Payment");
        }

        public ActionResult xacnhan()
        {
            return View();
        }

        public ActionResult ThanhToan()
        {
            if (Session["TenCus"] == null)
            {
                return RedirectToAction("CusLogin", "Account");
            }
            if (Session["Cart"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            string id = Convert.ToString(Session["EmailCus"]);
            ViewBag.Custom = db.KhachHangs.Where(m => m.Email == id).First();
            List<Order> lstOrder = OrderCart();
            if (lstOrder.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(lstOrder);
        }

        [HttpPost]
        public ActionResult ThanhToan(FormCollection f)
        {
            TempData["tenkh"] = Request.Form["ten"];
            TempData["sdt"] = Request.Form["sdt"];
            TempData["mail"] = Request.Form["email"];
            TempData["diac"] = Request.Form["dc"];
            return RedirectToAction("PaymentWithPaypal", "Payment");
        }
    }
}