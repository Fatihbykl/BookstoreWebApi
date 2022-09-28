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
    public class SiparisTest
    {
        private SiparisController controller;

        [TestInitialize]
        public void TestInit()
        {
            controller = new SiparisController();
        }

        [TestMethod]
        public void SiparisleriGetir()
        {
            IHttpActionResult actionResult = controller.Get();
            var contentResult = actionResult as OkNegotiatedContentResult<IList<SiparisModel>>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
        }

        [TestMethod]
        public void SiparisGetir()
        {
            IHttpActionResult actionResult = controller.Get(1);
            var contentResult = actionResult as OkNegotiatedContentResult<IList<SiparisModel>>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
        }

        [TestMethod]
        public void SiparisOlustur()
        {
            SiparisOlusturmaModel siparis = new SiparisOlusturmaModel
            {
                SiparisTarihi = DateTime.Now,
                SiparisVeren = 1,
                TeslimatAdresi = 1,
                SiparisIcerigi = new List<SiparisIcerigiModel>()
                {
                    new SiparisIcerigiModel
                    {
                        KitapNo = 1
                    },
                    new SiparisIcerigiModel
                    {
                        KitapNo = 2
                    }
                }
            };
            IHttpActionResult actionResult = controller.Post(siparis);
            var contentResult = actionResult as OkNegotiatedContentResult<SiparisOlusturmaModel>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(siparis, contentResult.Content);
        }
    }
}
