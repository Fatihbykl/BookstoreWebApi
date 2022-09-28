using BookstoreWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace BookstoreWebApi.Controllers
{
    
    public class OrderController : Controller
    {
        public bool AddToCart(int id)
        {
            if (Session["Musteri"] == null)
                return false;

            try
            {
                var controller = new KitapController();
                var actionResult = controller.Get(id);
                var actionContent = actionResult as OkNegotiatedContentResult<KitapModel>;
                if (actionContent.Content != null && Session["Sepet"] != null)
                {
                    var sepet = Session["Sepet"] as List<int>;
                    sepet.Add(id);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return false;
            }
            return true;
        }

        public bool RemoveFromCart(int id)
        {
            if (Session["Musteri"] == null)
                return false;

            try
            {
                if (Session["Sepet"] != null)
                {
                    var sepet = Session["Sepet"] as List<int>;
                    sepet.Remove(id);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return false;
            }
            return true;
        }

        public ActionResult Cart()
        {
            if (Session["Musteri"] == null)
                return RedirectToAction("Login", "User");

            Dictionary<KitapModel, int> kitaplar = new Dictionary<KitapModel, int>();
            ICollection<AdresModel> adresler = new List<AdresModel>();
            Nullable<double> toplamFiyat = 0;
            if (Session["Sepet"] != null)
            {
                var controller = new KitapController();
                var sepet = Session["Sepet"] as List<int>;
                foreach (var i in sepet)
                {
                    var actionResult = controller.Get(i);
                    var actionContent = actionResult as OkNegotiatedContentResult<KitapModel>;
                    toplamFiyat += actionContent.Content.Fiyat;
                    if (kitaplar.TryGetValue(actionContent.Content, out var value))
                    {
                        kitaplar[actionContent.Content] = value + 1;
                    }
                    else
                    {
                        kitaplar.Add(actionContent.Content, 1);
                    }
                    System.Diagnostics.Debug.WriteLine(kitaplar[actionContent.Content]);
                }
                ViewBag.Total = toplamFiyat;
                if (kitaplar.Count == 0) {
                    ViewBag.Empty = "Sepetinizde kitap bulunmamaktadır";
                }
            }
            Dictionary<string, string> values = Session["Musteri"] as Dictionary<string, string>;
            var controllerM = new MusterilerController();
            var actionResultM = controllerM.Get(Int32.Parse(values["MusteriNo"]));
            var actionContentM = actionResultM as OkNegotiatedContentResult<MusteriModel>;
            adresler = actionContentM.Content.Adresler;
            
            return View(new OrderModel { Adresler = adresler, Kitaplar = kitaplar });
        }

        public ActionResult CreateOrder(List<int> Kitaplar, int AdresNo)
        {
            Dictionary<string, string> musteri = Session["Musteri"] as Dictionary<string, string>;
            SiparisOlusturmaModel siparis = new SiparisOlusturmaModel()
            {
                SiparisTarihi = DateTime.Now,
                SiparisVeren = Int32.Parse(musteri["MusteriNo"]),
                TeslimatAdresi = AdresNo,
                SiparisIcerigi = Kitaplar.Select(k => new SiparisIcerigiModel
                {
                    KitapNo = k
                }).ToList<SiparisIcerigiModel>()
            };
            var controller = new SiparisController();
            controller.Post(siparis);
            var sepet = Session["Sepet"] as List<int>;
            sepet.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult MyOrders()
        {
            if (Session["Musteri"] == null)
                return RedirectToAction("Login", "User");

            Dictionary<string, string> musteri = Session["Musteri"] as Dictionary<string, string>;

            var controller = new SiparisController();
            var actionResult = controller.Get(Int32.Parse(musteri["MusteriNo"]));
            var actionContent = actionResult as OkNegotiatedContentResult<IList<SiparisModel>>;
            return View(actionContent.Content);
        }
    }
}