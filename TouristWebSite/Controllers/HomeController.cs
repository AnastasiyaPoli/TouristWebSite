using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TouristWebSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult News()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Tours()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Helper()
        {
            return View();
        }
    }
}