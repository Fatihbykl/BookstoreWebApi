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
    public class YazarTest
    {
        private YazarController controller;

        [TestInitialize]
        public void TestInit()
        {
            controller = new YazarController();
        }

        [TestMethod]
        public void YazarlariGetir()
        {
            IHttpActionResult actionResult = controller.Get();
            var contentResult = actionResult as OkNegotiatedContentResult<IList<YazarModel>>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
        }

        [TestMethod]
        public void YazarGetir()
        {
            IHttpActionResult actionResult = controller.Get(1);
            var contentResult = actionResult as OkNegotiatedContentResult<YazarModel>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.YazarNo);
        }

        [TestMethod]
        public void YazarOlustur()
        {
            Yazar yazar = new Yazar
            {
                yazarAdi = "TestYazarAdı"
            };
            IHttpActionResult actionResult = controller.Post(yazar);
            var contentResult = actionResult as OkNegotiatedContentResult<Yazar>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(yazar, contentResult.Content);
        }

        [TestMethod]
        public void YazarDuzenle()
        {
            YazarModel yazar = new YazarModel
            {
                YazarNo = 1,
                YazarAdi = "Düzenleme Test Adı"
            };
            IHttpActionResult actionResult = controller.Put(yazar);
            var contentResult = actionResult as OkNegotiatedContentResult<YazarModel>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(yazar, contentResult.Content);
        }
    }
}
