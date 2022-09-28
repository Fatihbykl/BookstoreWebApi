using BookstoreWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NLog;

namespace BookstoreWebApi.Controllers
{
    [Authorize]
    public class SiparisController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public IHttpActionResult Get()
        {
            IList<SiparisModel> siparisler = null;

            using (var context = new BookStoreEntities())
            {
                siparisler = context.Siparis
                             .Include("SiparisIcerigi")
                             .Include("Adres")
                             .Include("Musteri")
                             .Select(s => new SiparisModel()
                             {
                                 SiparisNo = s.siparisNo,
                                 SiparisTarihi = s.siparisTarihi,
                                 siparisTutari = s.siparisTutari,
                                 SiparisVeren = new MusteriModel()
                                 {
                                     MusteriNo = s.Musteri.musteriNo,
                                     Email = s.Musteri.email,
                                     MusteriAd = s.Musteri.musteriAd
                                 },
                                 TeslimatAdresi = new AdresModel()
                                 {
                                     AdresNo = s.Adres.adresNo,
                                     Il = s.Adres.il,
                                     Ilce = s.Adres.ilce,
                                     Mahalle = s.Adres.mahalle,
                                     Sokak = s.Adres.sokak,
                                     No = s.Adres.no,
                                     MusteriNo = s.Adres.musteriNo
                                 },
                                 SiparisIcerigi = s.SiparisIcerigi.Select(i => new SiparisIcerigiModel()
                                 {
                                     ID = i.ID,
                                     KitapNo = i.kitapNo,
                                     KitapBaslik = i.Kitap.baslik,
                                     KitapFiyat = i.Kitap.fiyat
                                 }).ToList<SiparisIcerigiModel>()
                             }).ToList<SiparisModel>();
                if (siparisler == null) { return NotFound(); }
            }
            return Ok(siparisler);
        }

        public IHttpActionResult Get(int id)
        {
            IList<SiparisModel> siparisler = null;
            using (var context = new BookStoreEntities())
            {
                siparisler = context.Siparis
                                         .Include("SiparisIcerigi")
                                         .Include("Adres")
                                         .Include("Musteri")
                                         .Where(s => s.Musteri.musteriNo == id)
                                         .Select(s => new SiparisModel()
                                         {
                                             SiparisNo = s.siparisNo,
                                             SiparisTarihi = s.siparisTarihi,
                                             siparisTutari = s.siparisTutari,
                                             SiparisVeren = new MusteriModel()
                                             {
                                                 MusteriNo = s.Musteri.musteriNo,
                                                 Email = s.Musteri.email,
                                                 MusteriAd = s.Musteri.musteriAd,
                                                 Rol = s.Musteri.rol,
                                                 MusteriSoyad = s.Musteri.musteriSoyad
                                             },
                                             TeslimatAdresi = new AdresModel()
                                             {
                                                 AdresNo = s.Adres.adresNo,
                                                 Il = s.Adres.il,
                                                 Ilce = s.Adres.ilce,
                                                 Mahalle = s.Adres.mahalle,
                                                 Sokak = s.Adres.sokak,
                                                 No = s.Adres.no,
                                                 MusteriNo = s.Adres.musteriNo
                                             },
                                             SiparisIcerigi = s.SiparisIcerigi.Select(i => new SiparisIcerigiModel()
                                             {
                                                 ID = i.ID,
                                                 KitapNo = i.kitapNo,
                                                 KitapBaslik = i.Kitap.baslik,
                                                 KitapFiyat = i.Kitap.fiyat
                                             }).ToList<SiparisIcerigiModel>()
                                         }).ToList<SiparisModel>();
                if (siparisler == null) { return NotFound(); }
            }
            return Ok(siparisler);
        }

        public IHttpActionResult Post(SiparisOlusturmaModel siparis)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            Nullable<double> total = 0;
            using (var context = new BookStoreEntities())
            {
                foreach (var i in siparis.SiparisIcerigi)
                {
                    var kitap = context.Kitap
                                .Where(k => k.kitapNo == i.KitapNo)
                                .FirstOrDefault<Kitap>();

                    if (kitap != null && kitap.stokSayisi > 0) { kitap.stokSayisi -= 1; }
                    else { return BadRequest("Book not found or out of stock."); }
                    total += kitap.fiyat;
                }

                var yeniSiparis = context.Siparis.Add(new Siparis()
                {
                    siparisTarihi = DateTime.Now,
                    siparisVeren = siparis.SiparisVeren,
                    teslimatAdresi = siparis.TeslimatAdresi,
                    siparisTutari = total,
                    SiparisIcerigi = siparis.SiparisIcerigi.Select(s => new SiparisIcerigi()
                    {
                        ID = s.ID,
                        kitapNo = s.KitapNo
                    }).ToList<SiparisIcerigi>()
                });
                context.SaveChanges();
                logger.Info(yeniSiparis.siparisNo + " numaralı yeni bir sipariş oluşturuldu.");
            }
            return Ok(siparis);
        }
    }
}
