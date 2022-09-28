using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Web;
using BookstoreWebApi.Controllers;
using BookstoreWebApi.Models;
using System.Web.Http;
using System.Web.Http.Results;

namespace BookstoreWebApi.Tests
{
    [TestClass]
    public class AdresTest
    {
        private AdresController controller;

        [TestInitialize]
        public void TestInit()
        {
            controller = new AdresController();
        }

        [TestMethod]
        public void AdresEkle()
        {
            AdresModel adres = new AdresModel
            {
                Il = "IlTest",
                Ilce = "IlceTest",
                Mahalle = "MahalleTest",
                MusteriNo = 1,
                No = 42,
                Sokak = "SokakTest"
            };
            IHttpActionResult actionResult = controller.Post(adres);
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<AdresModel>;
            Assert.IsNotNull(createdResult);
            Assert.IsNotNull(createdResult.RouteValues["id"]);
            Assert.AreEqual("DefaultApi", createdResult.RouteName);
            Assert.AreEqual("IlTest", createdResult.RouteValues["il"]);
        }

        [TestMethod]
        public void AdresDuzenle()
        {
            AdresModel adres = new AdresModel
            {
                AdresNo = 1002,
                Il = "IlTestDüzenleme",
                Ilce = "IlceTestDüzenleme",
                Mahalle = "MahalleTestDüzenleme",
                MusteriNo = 2,
                No = 32,
                Sokak = "SokakTestDüzenleme"
            };
            IHttpActionResult actionResult = controller.Put(adres);
            var contentResult = actionResult as OkNegotiatedContentResult<AdresModel>;
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(adres, contentResult.Content);
        }
    }
}
