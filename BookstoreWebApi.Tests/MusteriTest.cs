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
    public class MusteriTest
    {
        private MusterilerController controller;

        [TestInitialize]
        public void TestInit()
        {
            controller = new MusterilerController();
        }

        [TestMethod]
        public void MusterileriGetir()
        {
            IHttpActionResult actionResult = controller.Get();
            var contentResult = actionResult as OkNegotiatedContentResult<IList<MusteriModel>>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
        }

        [TestMethod]
        public void MusteriGetir()
        {
            IHttpActionResult actionResult = controller.Get(1);
            var contentResult = actionResult as OkNegotiatedContentResult<MusteriModel>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.MusteriNo);
        }

        [TestMethod]
        public void MusteriEkle()
        {
            MusteriOlusturModel musteri = new MusteriOlusturModel
            {
                Email = "test@outlook.com",
                Il = "Sakarya",
                Ilce = "Adapazarı",
                Mahalle = "TestMahalle",
                Sokak = "TestSokak",
                MusteriAd = "TestAd",
                MusteriSoyad = "TestSoyad",
                MusteriSifre = "123456",
                No = 123
            };
            IHttpActionResult actionResult = controller.Post(musteri);
            var contentResult = actionResult as OkNegotiatedContentResult<MusteriOlusturModel>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(musteri, contentResult.Content);
        }

        [TestMethod]
        public void MusteriDuzenle()
        {
            MusteriModel musteri = new MusteriModel
            {
                Email = "TestDuzenle@outlook.com",
                MusteriAd = "TestDuzenle",
                MusteriSoyad = "TestSoyad",
                MusteriNo = 2
            };
            IHttpActionResult actionResult = controller.Put(musteri);
            var contentResult = actionResult as OkNegotiatedContentResult<MusteriModel>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(musteri, contentResult.Content);
        }
    }
}
