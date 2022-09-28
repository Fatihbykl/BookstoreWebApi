using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using BookstoreWebApi.Controllers;
using BookstoreWebApi.Models;

namespace BookstoreWebApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            var controller = new KitapController();
            var response = controller.Get();
            var contentResult = response as OkNegotiatedContentResult<IList<KitapModel>>;
            return View(contentResult.Content);
        }

        
    }
}
