using DAL.DBHelpers;
using System;
using System.Web.Mvc;
using TouristWebSite.Models;

namespace TouristWebSite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ToursController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            try
            {
                var model = new ActiveToursViewModel()
                {
                    ActiveTours = ToursDBHelper.GetActive(),
                };

                return View(model);
            }
            catch (Exception e)
            {
                return RedirectToRoute(new { controller = "Tours", action = "Index" });
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Details(int itemId)
        {
            try
            {
                var model = new ChosenTourViewModel()
                {
                    ChosenTour = ToursDBHelper.GetTour(itemId),
                };

                return View(model);
            }
            catch (Exception e)
            {
                return RedirectToRoute(new { controller = "Tours", action = "Index" });
            }
        }

        [HttpGet]
        public ActionResult Delete(int itemId)
        {
            try
            {
                ToursDBHelper.Deactivate(itemId);
                return RedirectToRoute(new { controller = "Tours", action = "Index" });
            }
            catch (Exception e)
            {
                return RedirectToRoute(new { controller = "Tours", action = "Index" });
            }
        }
    }
}