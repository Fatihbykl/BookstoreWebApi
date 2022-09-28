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
    public class AdresController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        // Adres ekleme
        public IHttpActionResult Post(AdresModel adres)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using (var context = new BookStoreEntities())
            {
                var musteri = context.Musteri
                                .Where(m => m.musteriNo == adres.MusteriNo)
                                .FirstOrDefault<Musteri>();
                
                if (musteri == null) { return NotFound(); }

                var yeniAdres = context.Adres.Add(new Adres()
                {
                    il = adres.Il,
                    ilce = adres.Ilce,
                    mahalle = adres.Mahalle,
                    no = adres.No,
                    sokak = adres.Sokak,
                    Musteri = musteri
                });
                context.SaveChanges();
                adres.AdresNo = yeniAdres.adresNo;
                logger.Info(adres.AdresNo + " numaralı yeni bir adres oluşturuldu.");
                return CreatedAtRoute("DefaultApi", new
                {
                    id = adres.AdresNo,
                    il = adres.Il
                }, adres);
            }
        }

        public IHttpActionResult Put(AdresModel adres)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using (var context = new BookStoreEntities())
            {
                var degistirilecekAdres = context.Adres
                                        .Where(a => a.adresNo == adres.AdresNo)
                                        .FirstOrDefault<Adres>();

                if(degistirilecekAdres != null)
                {
                    degistirilecekAdres.il = adres.Il;
                    degistirilecekAdres.ilce = adres.Ilce;
                    degistirilecekAdres.mahalle = adres.Mahalle;
                    degistirilecekAdres.no = adres.No;
                    degistirilecekAdres.sokak = adres.Sokak;
                    context.SaveChanges();
                }
                else { return NotFound(); }
                logger.Info(adres.AdresNo + " numaralı adres düzenlendi.");
            }
            return Ok(adres);
        }

        /*
        public IHttpActionResult Delete(int musteriId, int adresId)
        {
            using (var context = new BookStoreEntities())
            {
                var musteri = context.Musteri
                              .Where(m => m.musteriNo == musteriId)
                              .FirstOrDefault<Musteri>();
                var adres = context.Adres
                            .Where(a => a.adresNo == adresId)
                            .FirstOrDefault<Adres>();
                
                if (adres == null) { return NotFound(); }
                musteri.Adres.Remove(adres);
                context.Entry(adres).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
            return Ok();
        }*/
    }
}
