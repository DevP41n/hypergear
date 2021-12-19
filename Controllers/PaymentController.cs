using HyperGear.Library;
using HyperGear.Models;
using PayPal;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace HyperGear.Controllers
{
    public class PaymentController : Controller
    {
        HyperGearEntities db = new HyperGearEntities();
        // GET: Payment
        public ActionResult Index()
        {
            return View();
        }

        private PayPal.Api.Payment payment;
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }
        private Payment CreatePayment(APIContext apicontext, string redirectURl)
        {

            var ItemLIst = new ItemList() { items = new List<PayPal.Api.Item>() };

            List<Models.Order> lstOrder = Session["Cart"] as List<Models.Order>;
            double tygia = 23300;
            double tien = 0;
            foreach (var item in lstOrder) {
                var thanhtien = Math.Round(item.oThanhTien / tygia,2);
                tien += Convert.ToDouble(thanhtien);
            ItemLIst.items.Add(new PayPal.Api.Item()
            {
                name = item.oTenSP,
                currency = "USD",
                price = Math.Round(item.oGia / tygia, 2).ToString(),
                quantity = item.oSoluong.ToString(),
                sku = "sku"
            });

            }
            var payer = new Payer()
            {
                payment_method = "paypal"
            };
            // Configure Redirect Urls here with RedirectUrls object  
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectURl + "&Cancel=true",
                return_url = redirectURl
            };
            // Adding Tax, shipping and Subtotal details  
            var details = new Details()
            {
                tax = "0",
                shipping = "0",
                subtotal = tien.ToString()
            };
            //Final amount with details  
            var amount = new Amount()
            {
                currency = "USD",
                total = tien.ToString(), // Total must be equal to sum of tax, shipping and subtotal.  
                details = details
            };
            var transactionList = new List<Transaction>();
            // Adding description about the transaction  
            transactionList.Add(new Transaction()
            {
                description = "Transaction description",
                invoice_number = Convert.ToString((new Random()).Next(100000000)), //Generate an Invoice No  
                amount = amount,
                item_list = ItemLIst
            });
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            // Create a payment using a APIContext  
            return this.payment.Create(apicontext);

        }
        public ActionResult PaymentWithPaypal(string Cancel = null)
        {
            //getting the apiContext
            APIContext apicontext = PaypalConfiguration.GetAPIContext();
            try
            {

                string PayerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(PayerId))
                {
                    string baseURi = Request.Url.Scheme + "://" + Request.Url.Authority +
                                     "/Payment/PaymentWithPaypal?";

                    var Guid = Convert.ToString((new Random()).Next(100000000));
                    var createdPayment = this.CreatePayment(apicontext, baseURi + "guid=" + Guid);

                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectURL = string.Empty;

                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;

                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            paypalRedirectURL = lnk.href;
                        }


                    }
                    Session.Add(Guid, createdPayment.id);
                    return Redirect(paypalRedirectURL);
                }
                else
                {
                    var guid = Request.Params["guid"];
                    var executedPaymnt = ExecutePayment(apicontext, PayerId, Session[guid] as string);

                    if (executedPaymnt.state.ToString().ToLower() != "approved")
                    {
                        return View("FailureView");
                    }

                }
            }
            catch (PayPalException)
            {
                return View("FailureView");


                //throw;
            }

            DonDatHang ddh = new DonDatHang();
            List<Models.Order> lstOrder = Session["Cart"] as List<Models.Order>;
            int str = Convert.ToInt32(Session["MaKH"]);
            ddh.MaKH = str;
            ddh.Ngaydat = DateTime.Now;
            ddh.DeliveryName = TempData["tenkh"].ToString();
            ddh.DeliveryPhone = TempData["sdt"].ToString();
            ddh.DeliveryEmail = TempData["mail"].ToString();
            ddh.DeliveryAdd = TempData["diac"].ToString();
            ddh.Tinhtranggiaohang = 1;
            ddh.Dathanhtoan = true;
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
                foreach (var items in db.SanPhams.Where(x => x.MaSP == item.oMaSP))
                {
                    items.Soluongton -= item.oSoluong;
                }
            }
            //save
            db.SaveChanges();
            //Send Mail
            string mail = System.IO.File.ReadAllText(Server.MapPath("~/template/mail/MailOrder.html"));
            string dt = DateTime.Now.ToString();
            mail = mail.Replace("{{Name}}", TempData["tenkh"].ToString());
            mail = mail.Replace("{{Phone}}", TempData["sdt"].ToString());
            mail = mail.Replace("{{Email}}", TempData["mail"].ToString());
            mail = mail.Replace("{{Address}}", TempData["diac"].ToString());
            mail = mail.Replace("{{Amount}}", Amount.ToString("N0"));
            mail = mail.Replace("{{date}}", dt);
            var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();
            Random rd = new Random();
            var numrd = rd.Next(1, 1000000).ToString();
            new SendMailOrder().SendMailTo(TempData["mail"].ToString(), "Đơn hàng mới từ HYPER GEAR [HYPER" + numrd + "]", mail);

            
            Session.Remove("Cart");
            return RedirectToAction("xacnhan", "ProdOrder");
        }
        
    }
}