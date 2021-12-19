using HyperGear.Library;
using HyperGear.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HyperGear.Areas.Admin.Controllers
{
    public class FeedBackController : Controller
    {
        private HyperGearEntities db = new HyperGearEntities();
        // GET: Admin/FeedBack
        public ActionResult FeedBack()
        {
            if (Session["HoTen"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            ViewBag.count = db.Feedbacks.Count();
            ViewBag.count1 = db.Feedbacks.Where(n => n.Status == 1).Count();
            ViewBag.count2 = db.Feedbacks.Where(n => n.Status == 2).Count();
            ViewBag.count0 = db.Feedbacks.Where(n => n.Status == 0).Count();
            return View(db.Feedbacks.ToList());
        }

        public ActionResult Details(int id)
        {
            if (Session["HoTen"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            ViewBag.count = db.Feedbacks.Count();
            ViewBag.count1 = db.Feedbacks.Where(n => n.Status == 1).Count();
            ViewBag.count2 = db.Feedbacks.Where(n => n.Status == 2).Count();
            ViewBag.count0 = db.Feedbacks.Where(n => n.Status == 0).Count();
            var details = db.Feedbacks.Find(id);
            return View(details);
        }

        public ActionResult Reply(int id)
        {
            if (Session["HoTen"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            ViewBag.count = db.Feedbacks.Count();
            ViewBag.count1 = db.Feedbacks.Where(n => n.Status == 1).Count();
            ViewBag.count2 = db.Feedbacks.Where(n => n.Status == 2).Count();
            ViewBag.count0 = db.Feedbacks.Where(n => n.Status == 0).Count();
            Feedback details = db.Feedbacks.Find(id);
            return View(details);
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Reply(Feedback fb, FormCollection f)
        {
            if (ModelState.IsValid)
            {
                fb.NgayGui = DateTime.Now;
                fb.Status = 2;
                db.Entry(fb).State = EntityState.Modified;
                db.SaveChanges();
            }
            db.Configuration.ValidateOnSaveEnabled = false;
            //Send Mail
            string mail = System.IO.File.ReadAllText(Server.MapPath("~/template/mail/MailReply.html"));
            string dt = DateTime.Now.ToString();
            mail = mail.Replace("{{Name}}", fb.TenKH);

            mail = mail.Replace("{{Title}}", fb.TieuDe);
            mail = mail.Replace("{{Email}}", fb.Email);
            mail = mail.Replace("{{NoidungKH}}", fb.NoiDung);
            mail = mail.Replace("{{Noidung}}", fb.Reply);
            Random rd = new Random();
            var numrd = rd.Next(1, 1000000).ToString();
            new SendMailOrder().SendMailTo(fb.Email, "Đơn hàng mới từ HYPER GEAR [HYPER" + numrd + "]", mail);
            //save
            return RedirectToAction("FeedBack");
        }

        public ActionResult Delete(int id)
        {
            if (Session["HoTen"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            Feedback fb = db.Feedbacks.Find(id);
            fb.Status = 0;
            db.Entry(fb).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("FeedBack");

        }

        public ActionResult DeleteConfirm(int id)
        {
            if (Session["HoTen"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            Feedback fb = db.Feedbacks.Find(id);
            db.Feedbacks.Remove(fb);
            db.SaveChanges();
            return RedirectToAction("FeedBack");

        }
    }
}