using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using mitredeneme.Models.Entity;

namespace mitredeneme.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        dbdenemeEntities db = new dbdenemeEntities();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(mitredeneme.Models.Entity.TBL_kisi userModel)
        {
            var userDetails = db.TBL_kisi.Where(x => x.kullaniciAdi==userModel.kullaniciAdi && x.sifre == userModel.sifre).FirstOrDefault();
            if (userDetails == null)
            {
                userModel.LoginErrorMessage = "Yanlış Kullanıcı Adı veya Şifre.";
                return View("Index", userModel);
            }
            else
            {
                Session["id"] = userDetails.id;
                Session["name"] = userDetails.isim;
                Session["username"] = userDetails.kullaniciAdi;
                return RedirectToAction("Index", "Attack");
            }
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(TBL_kisi p1)
        {
            if(db.TBL_kisi.Any( x => x.kullaniciAdi == p1.kullaniciAdi))
            {
                ViewBag.DuplicateMessage = "Bu Kullanıcı Adı Zaten Var.";
                return View();
            }
            if (db.TBL_kisi.Any(x => x.email == p1.email))
            {
                ViewBag.DuplicateMessage = "Bu E-Posta Zaten Kayıtlı.";
                return View();
            }

            db.TBL_kisi.Add(p1);
            db.SaveChanges();
            ViewBag.SuccessMessage = "Kayıt Başarılı.";
            return View();
        }


        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {

            return View(db.TBL_kisi.Where(x => x.id == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(int id, TBL_kisi user )
        {
            try
            {
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}