using BookstoreWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace BookstoreWebApi.Controllers
{
    public class ProfileController : Controller
    {
        public ActionResult Index()
        {
            if (Session["Musteri"] == null)
                return RedirectToAction("Index", "Home");
            var values = Session["Musteri"] as Dictionary<string, string>;
            var controller = new MusterilerController();
            var actionResult = controller.Get(Int32.Parse(values["MusteriNo"]));
            var actionContent = actionResult as OkNegotiatedContentResult<MusteriModel>;
            return View(actionContent.Content);
        }

        public ActionResult AddAddress(string il, string ilce, string sokak, string mahalle, int no)
        {
            if (Session["Musteri"] == null)
                return RedirectToAction("Index", "Home");
            var values = Session["Musteri"] as Dictionary<string, string>;
            var adres = new AdresModel
            {
                Il = il,
                Ilce = ilce,
                Mahalle = mahalle,
                No = no,
                Sokak = sokak,
                MusteriNo = Int32.Parse(values["MusteriNo"])
            };
            var controller = new AdresController();
            controller.Post(adres);
            ViewBag.Info = "Adres başarıyla eklendi";
            return View("Index");
        }

        public ActionResult EditProfile(MusteriModel musteri)
        {
            if (Session["Musteri"] == null)
                return RedirectToAction("Index", "Home");
            var values = Session["Musteri"] as Dictionary<string, string>;
            musteri.MusteriNo = Int32.Parse(values["MusteriNo"]);
            var controller = new MusterilerController();
            controller.Put(musteri);
            ViewBag.Info = "Profil güncellendi.";
            return RedirectToAction("Index", "Home");
        }
    }
}