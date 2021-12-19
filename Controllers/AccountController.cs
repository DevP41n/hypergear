using BotDetect.Web.Mvc;
using Facebook;
using HyperGear.Library;
using HyperGear.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HyperGear.Controllers
{
    public class AccountController : Controller
    {
        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }


        private HyperGearEntities db = new HyperGearEntities();
        // GET: Account
        public ActionResult CusLogin()
        {
            if (Session["TenCus"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CusLogin(string Taikhoan, string Matkhau)
        {
            if (ModelState.IsValid)
            {
                var f_password = MyString.ToMD5(Matkhau);
                var data = db.KhachHangs.Where(s => s.Taikhoan.Equals(Taikhoan) && s.Matkhau.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["TenKH"] = data;
                    Session["TenCus"] = data.FirstOrDefault().HoTen;
                    Session["EmailCus"] = data.FirstOrDefault().Email;
                    Session["MaKH"] = data.FirstOrDefault().MaKH;
                    Session["SDT"] = data.FirstOrDefault().DienthoaiKH;
                    TempData["Message"] = "Đăng nhập thành công";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Warning"] = "Sai tài khoản hoặc mật khẩu vui lòng nhập lại!";
                    return RedirectToAction("CusLogin");
                }
            }
            return View();
        }


        public ActionResult CusRegister()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [CaptchaValidationActionFilter("CaptchaCode", "regCaptcha", "Sai mã xác nhận vui lòng nhập lại!")]
        public ActionResult CusRegister(KhachHang _user)
        {
            if (ModelState.IsValid)
            {
                var check = db.KhachHangs.FirstOrDefault(s => s.Email == _user.Email);
                if (check == null)
                {

                    _user.Matkhau = MyString.ToMD5(_user.Matkhau);
                    _user.Ngaysinh = DateTime.Now;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.KhachHangs.Add(_user);
                    TempData["Message"] = "Bạn đã tạo tài khoản thành công!";
                    db.SaveChanges();
                    return RedirectToAction("CusLogin");
                }
                else
                {
                    ViewBag.error = "Email này đã đăng ký bằng tài khoản khác, vui lòng nhập email khác!";
                    return View();
                }


            }
            return View();
        }

        public ActionResult CusLogout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LoginWithFB()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email",
            });

            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });


            var accessToken = result.access_token;
            if (!string.IsNullOrEmpty(accessToken))
            {
                fb.AccessToken = accessToken;
                // Get the user's information, like email,name etc
                dynamic me = fb.Get("me?fields=name,id,email");
                string email = me.email;
                string userName = me.email;
                string hoten = me.name;



                var user = new KhachHang();
                user.Email = email;
                user.Taikhoan = userName;
                user.HoTen = hoten;
                user.Matkhau = "@Tien123";
                user.ConfirmPassword = "@Tien123";
                var resultInsert = new KhachHang().InsertForFacebook(user);
                if (resultInsert > 0)
                {
                    Session["MaKH"] = resultInsert;
                    Session["TenCus"] = user.HoTen;
                    Session["EmailCus"] = user.Email;
                }
                TempData["Message"] = "Đăng nhập thành công";
            }
            return Redirect("/");
        }

        public ActionResult Forgot()
        {
            return View();
        }

        public ActionResult SearchForgot(FormCollection f)
        {
            string mailkh = Request.Form["email"];
            KhachHang kh = db.KhachHangs.Where(x => x.Email == mailkh).SingleOrDefault();
            if (kh == null)
            {
                TempData["Warning"] = "Email không tồn tại! </br>Vui lòng nhập đúng!";
                return RedirectToAction("Forgot");
            }
            var resultInsert = new KhachHang().InsertForgot(kh);
            if (resultInsert > 0)
            {
                var id = resultInsert;
                KhachHang khachHang = db.KhachHangs.Find(id);
                Random rd = new Random();
                var numrd = rd.Next(1, 1000).ToString();
                string mk = "@Hypergear" + numrd;
                khachHang.Matkhau = MyString.ToMD5(mk);
                khachHang.ConfirmPassword = khachHang.Matkhau;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.Entry(khachHang).State = EntityState.Modified;
                db.SaveChanges();
                // mail forgot
                string mail = System.IO.File.ReadAllText(Server.MapPath("~/template/mail/Forgot.html"));
                string dt = DateTime.Now.ToString();
                mail = mail.Replace("{{Name}}", khachHang.HoTen);
                mail = mail.Replace("{{Email}}", kh.Email);
                mail = mail.Replace("{{Pass}}", mk);
                mail = mail.Replace("{{date}}", dt);
                //send
                var num = rd.Next(1, 1000000).ToString();
                new SendMailOrder().SendMailTo(khachHang.Email, "Thông tin mới từ HYPER GEAR [HYPER" + num + "]", mail);
                TempData["Message"] = "Gửi thành công!";
                return RedirectToAction("CusLogin");
            }
            return View(kh);
        }
        //public ActionResult ForgotEmail(int id)
        //{
        //    KhachHang khachHang = db.KhachHangs.Find(id);
        //    if (khachHang == null)
        //    {
        //        return RedirectToAction("Forgot", "Account");
        //    }
        //    return View(khachHang);
        //}

        //[HttpPost, ValidateInput(false)]
        //public ActionResult ForgotEmail(KhachHang kh)
        //{
        //    Random rd = new Random();
        //    var numrd = rd.Next(1, 1000).ToString();
        //    string mk = "@Hypergear" + numrd;
        //    kh.Matkhau = MyString.ToMD5(mk);
        //    kh.ConfirmPassword = kh.Matkhau;
        //    db.Configuration.ValidateOnSaveEnabled = false;
        //    db.Entry(kh).State = EntityState.Modified;
        //    db.SaveChanges();
        //    // mail forgot
        //    string mail = System.IO.File.ReadAllText(Server.MapPath("~/template/mail/Forgot.html"));
        //    string dt = DateTime.Now.ToString();
        //    mail = mail.Replace("{{Name}}", kh.HoTen);
        //    mail = mail.Replace("{{Email}}", kh.Email);
        //    mail = mail.Replace("{{Pass}}", mk);
        //    mail = mail.Replace("{{date}}", dt);
        //    //send
        //    var num = rd.Next(1, 1000000).ToString();
        //    new SendMailOrder().SendMailTo(kh.Email, "Thông tin mới từ HYPER GEAR [HYPER" + num + "]", mail);
        //    TempData["Message"] = "Gửi thành công!";
        //    return RedirectToAction("CusLogin");
        //}

        public ActionResult ProfileAccount(int? id)
        {
            var mak = Convert.ToInt32(Session["MaKH"]);
            if (mak != id)
            {
                TempData["Warning"] = "Không đúng tài khoản của bạn!";
                return RedirectToAction("Index", "Home");
            }
            var kh = db.KhachHangs.Where(x => x.MaKH == id).FirstOrDefault();
            return View(kh);
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult ProfileAccount(KhachHang kh)
        {
            db.Configuration.ValidateOnSaveEnabled = false;
            db.Entry(kh).State = EntityState.Modified;
            db.SaveChanges();
            TempData["Message"] = "Cập nhật thành công!";
            string url = "/ProfileAccount/" + kh.MaKH;
            return RedirectToAction(url);
        }
        public ActionResult ChangePass (int? id)
        {
            var mak = Convert.ToInt32(Session["MaKH"]);
            if (mak != id)
            {
                TempData["Warning"] = "Không đúng tài khoản của bạn!";
                return RedirectToAction("Index", "Home");
            }
            var kh = db.KhachHangs.Where(x => x.MaKH == id).FirstOrDefault();
            return View(kh);
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePass(KhachHang kh,FormCollection f)
        {
            var mkc = Request.Form["mkcu"];
            var mkcuu = MyString.ToMD5(mkc);
            if (kh.Matkhau != mkcuu)
            {
                TempData["Warning"] = "Sai mật khẩu cũ!";
                string urla = "/ChangePass/" + kh.MaKH;
                return RedirectToAction(urla);
            }
            if(Request.Form["mkmoi"] != Request.Form["xnmk"])
            {
                TempData["Warning"] = "Mật khẩu không khớp!";
                string urlaz = "/ChangePass/" + kh.MaKH;
                return RedirectToAction(urlaz);
            }
            var matkhau = Request.Form["mkmoi"];
            kh.Matkhau = MyString.ToMD5(matkhau);
            kh.ConfirmPassword = MyString.ToMD5(matkhau);
            db.Configuration.ValidateOnSaveEnabled = false;
            db.Entry(kh).State = EntityState.Modified;
            db.SaveChanges();
            TempData["Message"] = "Đổi mật khẩu thành công!";
            string url = "/ProfileAccount/" + kh.MaKH;
            return RedirectToAction(url);
        }
    }
}