using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using BookstoreWebApi.Controllers;
using BookstoreWebApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BookstoreWebApi.Tests
{
    [TestClass]
    public class KitapTest
    {
        private KitapController controller;
        
        [TestInitialize]
        public void TestInit()
        {
            controller = new KitapController();
        }

        [TestMethod]
        public void KitaplariGetir()
        {
            IHttpActionResult actionResult = controller.Get();
            var contentResult = actionResult as OkNegotiatedContentResult<IList<KitapModel>>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
        }

        [TestMethod]
        public void KitapGetir()
        {
            IHttpActionResult actionResult = controller.Get(1);
            var contentResult = actionResult as OkNegotiatedContentResult<KitapModel>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.KitapNo);
        }

        [TestMethod]
        public void KitapOlustur()
        {
            KitapModel kitap = new KitapModel
            {
                BasimTarihi = "Ocak 1999",
                Baslik = "Kitap Oluşturma Test",
                Fiyat = 19.99,
                SayfaSayisi = 55,
                StokSayisi = 75,
                Tur = "Test",
                YazarNo = 2
            };
            IHttpActionResult actionResult = controller.Post(kitap);
            var contentResult = actionResult as OkNegotiatedContentResult<KitapModel>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(kitap, contentResult.Content);
        }

        [TestMethod]
        public void StokGuncelle()
        {
            IHttpActionResult actionResult = controller.Put(1, 100);
            var contentResult = actionResult as OkNegotiatedContentResult<int>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(100, contentResult.Content);
        }

        [TestMethod]
        public void KitapDuzenle()
        {
            KitapModel kitap = new KitapModel
            {
                KitapNo = 2,
                BasimTarihi = "Mart 2003",
                Baslik = "Kitap Düzenleme Test",
                Fiyat = 39.99,
                SayfaSayisi = 120,
                StokSayisi = 120,
                Tur = "Test",
                YazarNo = 2
            };
            IHttpActionResult actionResult = controller.Put(kitap);
            var contentResult = actionResult as OkNegotiatedContentResult<KitapModel>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(kitap, contentResult.Content);
        }
    }
}
