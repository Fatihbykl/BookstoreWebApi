using BookstoreWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;

namespace BookstoreWebApi.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Login()
        {
            if (Session["Musteri"] != null)
                return RedirectToAction("Index", "Home");

            ViewBag.Title = "Giriş Yap";
            return View();
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Login(string email, string sifre)
        {
            if (Session["Musteri"] != null)
                return RedirectToAction("Index", "Home");

            var hashedPassword = getHash(sifre);
            using (var context = new BookStoreEntities())
            {
                var user = context.Musteri
                                  .Where(m => m.email == email && m.musteriSifre == hashedPassword)
                                  .FirstOrDefault<Musteri>();
                if (user != null)
                {
                    var data = new Dictionary<string, string>();
                    data["MusteriAd"] = user.musteriAd;
                    data["MusteriSoyad"] = user.musteriSoyad;
                    data["MusteriEmail"] = user.email;
                    data["MusteriNo"] = user.musteriNo.ToString();
                    data["Rol"] = user.rol;
                    Session.Add("Musteri", data);
                    Session.Add("Sepet", new List<int>());
                }
                else
                {
                    // user not found
                    ViewBag.Error = "Bu bilgilere ait bir kullanıcı bulunamadı";
                    return View();
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            if (Session["Musteri"] != null)
                return RedirectToAction("Index", "Home");

            ViewBag.Title = "Üye Ol";
            return View();
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Register(string ad, string soyad, string email, string sifre, string il, string ilce, string sokak, string mahalle, int no)
        {
            if (Session["Musteri"] != null)
                return RedirectToAction("Index", "Home");

            using (var context = new BookStoreEntities())
            {
                var user = context.Musteri
                                  .Where(m => m.email == email)
                                  .FirstOrDefault<Musteri>();
                if (user == null)
                {
                    var model = new MusteriOlusturModel
                    {
                        Email = email,
                        Il = il,
                        Ilce = ilce,
                        Mahalle = mahalle,
                        MusteriAd = ad,
                        MusteriSoyad = soyad,
                        No = no,
                        Sokak = sokak,
                        Rol = "Musteri",
                        MusteriSifre = getHash(sifre)
                    };
                    var controller = new MusterilerController();
                    controller.Post(model);
                }
                else
                {
                    ViewBag.Error = "Bu email kullanılıyor";
                    return View();
                }
            }
            return RedirectToAction("Login", "User");
        }

        public ActionResult Logout()
        {
            if (Session["Musteri"] != null)
                Session.Remove("Musteri");
            return RedirectToAction("Index", "Home");
        }
        private static string getHash(string text)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
