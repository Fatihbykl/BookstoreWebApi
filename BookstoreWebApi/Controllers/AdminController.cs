using BookstoreWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookstoreWebApi.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        [System.Web.Mvc.HttpPost]
        public ActionResult AddBook(KitapModel kitap, string fiyat)
        {
            System.Diagnostics.Debug.WriteLine("-----" + kitap.Fiyat);
            if (Session["Musteri"] != null)
            {
                var values = Session["Musteri"] as Dictionary<string, string>;
                if (values["Rol"] != "Admin")
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.Unauthorized);
            }
            else { return new HttpStatusCodeResult(System.Net.HttpStatusCode.Unauthorized); }
            var controller = new KitapController();
            controller.Post(kitap);
            ViewBag.Info = "Kitap başarıyla oluşturuldu";
            return View("Index");
        }

        public ActionResult UpdateStock(int id, int stok)
        {
            if (Session["Musteri"] != null)
            {
                var values = Session["Musteri"] as Dictionary<string, string>;
                if (values["Rol"] != "Admin")
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.Unauthorized);
            }
            else { return new HttpStatusCodeResult(System.Net.HttpStatusCode.Unauthorized); }
            var controller = new KitapController();
            controller.Put(id, stok);
            ViewBag.Info = "Stok güncellendi";
            return View("Index");
        }
    }
}