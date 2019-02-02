using DAL.DBHelpers;
using Microsoft.AspNet.Identity;
using System;
using System.Web.Mvc;
using TouristWebSite.Helpers;
using TouristWebSite.Models;

namespace TouristWebSite.Controllers
{
    [Authorize]
    public class ToursController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index(string message = "")
        {
            ViewBag.StatusMessage = message;

            var model = new ActiveToursViewModel()
            {
                ActiveTours = ToursDBHelper.GetActive(),
            };

            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Details(long itemId)
        {
            try
            {
                var model = new ChosenTourViewModel()
                {
                    ChosenTour = ToursDBHelper.GetById(itemId),
                };

                return View(model);
            }
            catch (Exception e)
            {
                return RedirectToRoute(new { controller = "Tours", action = "Index" });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Delete(long itemId)
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

        public ActionResult TourBooking(long itemId)
        {
            try
            {
                var chosenTour = ToursDBHelper.GetById(itemId);

                TourBookingViewModel model = new TourBookingViewModel()
                {
                    TourId = chosenTour.Id,
                    Price = chosenTour.Price,
                    PeopleCount = 1,
                    Description = "Введіть деталі для бронювання туру \"" + chosenTour.Name + "\" (" + chosenTour.Place + "). Дати: " + chosenTour.DateStart.ToShortDateString() + " - " + chosenTour.DateEnd.ToShortDateString() + "."
                };

                return View(model);
            }
            catch (Exception e)
            {
                return RedirectToRoute(new { controller = "Manage", action = "Index" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TourBooking(TourBookingViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var newId = BookedToursDBHelper.BookTour(model, User.Identity.GetUserId());
                string filename = PDFGeneratorHelper.GeneratePDF(ToursDBHelper.GetById(model.TourId), model.PeopleCount, model.Comment, newId);
                EmailSenderHelper.SendEmail(UsersDBHelper.GetById(User.Identity.GetUserId()).Email, "Підтвердження бронювання туру.", "Тур було успішно заброньовано, деталі можна переглянути у прикріпленому документі.", filename);
                return RedirectToAction("Index", new { Message = "Тур було успішно заброньовано. Документ про бронювання відправлено на електронну пошту." });
            }
            catch (Exception e)
            {
                return RedirectToRoute(new { controller = "Tours", action = "Index" });
            }
        }
    }
}