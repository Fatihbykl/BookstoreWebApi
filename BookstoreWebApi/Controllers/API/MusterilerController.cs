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
    public class MusterilerController : ApiController
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        // Tüm müşterileri getir
        public IHttpActionResult Get()
        {
            IList<MusteriModel> musteriler = null;

            using (var context = new BookStoreEntities())
            {
                musteriler = context.Musteri.Include("Adres")
                                .Select(m => new MusteriModel()
                                {
                                    MusteriNo = m.musteriNo,
                                    Email = m.email,
                                    MusteriAd = m.musteriAd,
                                    MusteriSoyad = m.musteriSoyad,
                                    Rol = m.rol,
                                    Adresler = m.Adres.Select(a => new AdresModel()
                                    {
                                        AdresNo = a.adresNo,
                                        Il = a.il,
                                        Ilce = a.ilce,
                                        Mahalle = a.mahalle,
                                        No = a.no,
                                        Sokak = a.sokak,   
                                    }).ToList<AdresModel>()
                                }).ToList<MusteriModel>();
                if(musteriler == null) { return NotFound(); }
            }
            return Ok(musteriler);
        }
        // Verilen IDli müşteriyi getir
        public IHttpActionResult Get(int id)
        {
            MusteriModel musteri = null;

            using (var context = new BookStoreEntities())
            {
                musteri = context.Musteri.Include("Adres")
                                .Where(q => q.musteriNo == id)
                                .Select(m => new MusteriModel()
                                {
                                    MusteriNo = m.musteriNo,
                                    Email = m.email,
                                    MusteriAd = m.musteriAd,
                                    MusteriSoyad = m.musteriSoyad,
                                    Rol = m.rol,
                                    Adresler = m.Adres.Select(a => new AdresModel()
                                    {
                                        AdresNo = a.adresNo,
                                        Il = a.il,
                                        Ilce = a.ilce,
                                        Mahalle = a.mahalle,
                                        No = a.no,
                                        Sokak = a.sokak,
                                    }).ToList<AdresModel>()
                                }).FirstOrDefault<MusteriModel>();
                if (musteri == null) { return NotFound(); }
            }
            return Ok(musteri);
        }
        // Müşteri ekle
        public IHttpActionResult Post(MusteriOlusturModel bilgiler)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            using (var context = new BookStoreEntities())
            {
                var yeniMusteri = context.Musteri.Add(new Musteri()
                {
                    musteriAd = bilgiler.MusteriAd,
                    musteriSoyad = bilgiler.MusteriSoyad,
                    musteriSifre = bilgiler.MusteriSifre,
                    email = bilgiler.Email,
                    rol = bilgiler.Rol,
                    Adres = new List<Adres>
                    {
                        new Adres
                        {
                            il = bilgiler.Il,
                            ilce = bilgiler.Ilce,
                            mahalle = bilgiler.Mahalle,
                            sokak = bilgiler.Sokak,
                            no = bilgiler.No
                        }
                    }
                });
                context.SaveChanges();
                logger.Info(yeniMusteri.musteriNo + " numaralı yeni bir müşteri eklendi.");
            };
            return Ok(bilgiler);
        }
        // Müşteri güncelle
        public IHttpActionResult Put(MusteriModel musteri)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using (var context = new BookStoreEntities())
            {
                var degistirilecekMusteri = context.Musteri
                                            .Where(m => m.musteriNo == musteri.MusteriNo)
                                            .FirstOrDefault<Musteri>();
                if (degistirilecekMusteri != null)
                {
                    degistirilecekMusteri.musteriAd = musteri.MusteriAd;
                    degistirilecekMusteri.musteriSoyad = musteri.MusteriSoyad;
                    degistirilecekMusteri.email = musteri.Email;
                    context.SaveChanges();
                }
                else { return NotFound(); }
                logger.Info(degistirilecekMusteri.musteriNo + " numaralı müşteri güncellendi.");
            }
            return Ok(musteri);
        }
        /*
        // Müşteri sil
        public IHttpActionResult Delete(int id)
        {
            using (var context = new BookStoreEntities())
            {
                var musteri = context.Musteri
                                .Where(m => m.musteriNo == id)
                                .FirstOrDefault<Musteri>();
                if (musteri != null)
                {
                    context.Entry(musteri).State = System.Data.Entity.EntityState.Deleted;
                    context.SaveChanges();
                }
                else { return NotFound(); }
            }
            return Ok();
        }*/
    }
}
