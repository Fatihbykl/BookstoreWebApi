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
    public class KitapController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public IHttpActionResult Get()
        {
            IList<KitapModel> kitaplar = null;

            using (var context = new BookStoreEntities())
            {
                kitaplar = context.Kitap
                            .Include("Yazar")
                            .Select(k => new KitapModel()
                            {
                                BasimTarihi = k.basimTarihi,
                                Baslik = k.baslik,
                                Fiyat = k.fiyat,
                                KitapNo = k.kitapNo,
                                SayfaSayisi = k.sayfaSayisi,
                                StokSayisi = k.stokSayisi,
                                Tur = k.tur,
                                gorselURL = k.gorselURL,
                                Yazar = new YazarModel()
                                {
                                    YazarNo = k.Yazar.yazarNo,
                                    YazarAdi = k.Yazar.yazarAdi
                                }
                            }).ToList<KitapModel>();
                if (kitaplar == null) { return NotFound(); }
            }
            return Ok(kitaplar);
        }

        public IHttpActionResult Get(int id)
        {
            KitapModel kitap = null;
            using (var context = new BookStoreEntities())
            {
                kitap = context.Kitap
                                    .Include("Yazar")
                                    .Where(k => k.kitapNo == id)
                                    .Select(k => new KitapModel()
                                    {
                                        BasimTarihi = k.basimTarihi,
                                        Baslik = k.baslik,
                                        Fiyat = k.fiyat,
                                        KitapNo = k.kitapNo,
                                        SayfaSayisi = k.sayfaSayisi,
                                        StokSayisi = k.stokSayisi,
                                        Tur = k.tur,
                                        gorselURL = k.gorselURL,
                                        Yazar = new YazarModel()
                                        {
                                            YazarNo = k.Yazar.yazarNo,
                                            YazarAdi = k.Yazar.yazarAdi
                                        }
                                    }).FirstOrDefault<KitapModel>();
                if (kitap == null) { return NotFound(); }
            }
            return Ok(kitap);
        }

        public IHttpActionResult Post(KitapModel kitap)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using (var context = new BookStoreEntities())
            {
                var yazar = context.Yazar
                            .Where(y => y.yazarNo == kitap.YazarNo)
                            .FirstOrDefault();

                if (yazar == null) { return NotFound(); }

                var yeniKitap = context.Kitap.Add(new Kitap()
                {
                    basimTarihi = kitap.BasimTarihi,
                    baslik = kitap.Baslik,
                    fiyat = kitap.Fiyat,
                    sayfaSayisi = kitap.SayfaSayisi,
                    stokSayisi = kitap.StokSayisi,
                    tur = kitap.Tur,
                    gorselURL = kitap.gorselURL,
                    Yazar = yazar
                });
                context.SaveChanges();
                logger.Info(yeniKitap.kitapNo + " numaralı yeni bir kitap oluşturuldu.");
            }
            return Ok(kitap);
        }

        public IHttpActionResult Put(int id, int stok)
        {
            if (stok < 0)
                return BadRequest("Stok can not be less than 0!");
            Nullable<int> eskiStok = 0;
            using (var context = new BookStoreEntities())
            {
                var kitap = context.Kitap
                            .Where(k => k.kitapNo == id)
                            .FirstOrDefault<Kitap>();
                if (kitap != null)
                {
                    eskiStok = kitap.stokSayisi;
                    kitap.stokSayisi = stok;
                    context.SaveChanges();
                }
                else { return NotFound(); }
                logger.Info(id + " numaralı kitabın stok sayısı güncellendi. " + eskiStok + " -> " + stok);
            }
            return Ok(stok);
        }

        public IHttpActionResult Put(KitapModel kitap)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); ;

            using (var context = new BookStoreEntities())
            {
                var degistirilecekKitap = context.Kitap
                                          .Where(k => k.kitapNo == kitap.KitapNo)
                                          .FirstOrDefault<Kitap>();

                if (degistirilecekKitap != null)
                {
                    degistirilecekKitap.fiyat = kitap.Fiyat;
                    degistirilecekKitap.basimTarihi = kitap.BasimTarihi;
                    degistirilecekKitap.baslik = kitap.Baslik;
                    degistirilecekKitap.sayfaSayisi = kitap.SayfaSayisi;
                    degistirilecekKitap.stokSayisi = kitap.StokSayisi;
                    degistirilecekKitap.yazarNo = kitap.YazarNo;
                    degistirilecekKitap.tur = kitap.Tur;
                    degistirilecekKitap.gorselURL = kitap.gorselURL;
                    context.SaveChanges();
                }
                else { return NotFound(); }
                logger.Info(degistirilecekKitap.kitapNo + " numaralı kitap bilgileri güncellendi.");
            }
            return Ok(kitap);
        }
    }
}
