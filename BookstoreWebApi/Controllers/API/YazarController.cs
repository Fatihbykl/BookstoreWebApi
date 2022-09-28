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
    public class YazarController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public IHttpActionResult Get()
        {
            IList<YazarModel> yazarlar = null;

            using (var context = new BookStoreEntities())
            {
                yazarlar = context.Yazar
                            .Select(y => new YazarModel()
                            {
                                YazarNo = y.yazarNo,
                                YazarAdi = y.yazarAdi,
                                Kitaplar = y.Kitap.Select(k => new KitapModel()
                                {
                                    BasimTarihi = k.basimTarihi,
                                    Baslik = k.baslik,
                                    Fiyat = k.fiyat,
                                    KitapNo = k.kitapNo,
                                    SayfaSayisi = k.sayfaSayisi,
                                    StokSayisi = k.stokSayisi,
                                    Tur = k.tur,
                                    YazarNo = k.yazarNo
                                }).ToList<KitapModel>()
                            }).ToList<YazarModel>();
                if (yazarlar == null) { return NotFound(); }
            }
            return Ok(yazarlar);
        }

        public IHttpActionResult Get(int id)
        {
            YazarModel yazar = null;
            using (var context = new BookStoreEntities())
            {
                yazar = context.Yazar
                            .Where(y => y.yazarNo == id)
                            .Select(y => new YazarModel()
                            {
                                YazarNo = y.yazarNo,
                                YazarAdi = y.yazarAdi,
                                Kitaplar = y.Kitap.Select(k => new KitapModel()
                                {
                                    BasimTarihi = k.basimTarihi,
                                    Baslik = k.baslik,
                                    Fiyat = k.fiyat,
                                    KitapNo = k.kitapNo,
                                    SayfaSayisi = k.sayfaSayisi,
                                    StokSayisi = k.stokSayisi,
                                    Tur = k.tur,
                                    YazarNo = k.yazarNo
                                }).ToList<KitapModel>()
                            }).FirstOrDefault<YazarModel>();
                if (yazar == null) { return NotFound(); }
            }
            return Ok(yazar);
        }

        public IHttpActionResult Post(Yazar yazar)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using (var context = new BookStoreEntities())
            {
                var yeniYazar = context.Yazar.Add(new Yazar()
                {
                    yazarAdi = yazar.yazarAdi
                });
                context.SaveChanges();
                logger.Info(yeniYazar.yazarNo + " numaralı yeni bir yazar oluşturuldu.");
            }
            return Ok(yazar);
        }

        public IHttpActionResult Put(YazarModel yazar)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using (var context = new BookStoreEntities())
            {
                var degistirilecekYazar = context.Yazar
                                            .Where(y => y.yazarNo == yazar.YazarNo)
                                            .FirstOrDefault<Yazar>();
                if (degistirilecekYazar != null)
                {
                    degistirilecekYazar.yazarAdi = yazar.YazarAdi;
                    context.SaveChanges();
                }
                else { return NotFound(); }
                logger.Info(degistirilecekYazar.yazarNo + " numaralı yazar güncellendi.");
            }
            return Ok(yazar);
        }
    }
}
