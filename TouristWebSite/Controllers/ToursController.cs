using DAL.DBHelpers;
using Microsoft.AspNet.Identity;
using System;
using System.IO;
using System.Web.Hosting;
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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Add()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                return RedirectToRoute(new { controller = "Tours", action = "Index" });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Add(TourViewModel model)
        {
            try
            {
                if (model.DateStart > model.DateEnd)
                {
                    ModelState.AddModelError("DateEnd", "Дата початку туру не може бути меншою, ніж дата закінчення туру.");
                    return View(model);
                }

                ToursDBHelper.Add(model);
                return RedirectToRoute(new { controller = "Tours", action = "Index" });
            }
            catch (Exception e)
            {
                return RedirectToRoute(new { controller = "Tours", action = "Index" });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Update(long itemId)
        {
            try
            {
                var tour = ToursDBHelper.GetById(itemId);

                TourViewModel model = new TourViewModel()
                {
                    Id = itemId,
                    Name = tour.Name,
                    Place = tour.Place,
                    Description = tour.Description,
                    Price = tour.Price,
                    DateStart = tour.DateStart,
                    DateEnd = tour.DateEnd,
                    RouteLink = tour.RouteLink
                };

                return View(model);
            }
            catch (Exception e)
            {
                return RedirectToRoute(new { controller = "Tours", action = "Index" });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Update(TourViewModel model)
        {
            try
            {
                if (model.DateStart > model.DateEnd)
                {
                    ModelState.AddModelError("DateEnd", "Дата початку туру не може бути меншою, ніж дата закінчення туру.");
                    return View(model);
                }

                ToursDBHelper.Update(model);
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

        [Authorize(Roles = "Admin")]
        public ActionResult Photos(long itemId)
        {
            try
            {
                var chosenTour = ToursDBHelper.GetById(itemId);

                ImageViewModel model = new ImageViewModel()
                {
                    Tour = chosenTour
                };

                return View(model);
            }
            catch (Exception e)
            {
                return RedirectToRoute(new { controller = "Tours", action = "Index" });
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteAll(long itemId)
        {
            try
            {
                var tour = ToursDBHelper.GetById(itemId);

                var path = HostingEnvironment.ApplicationPhysicalPath + "\\Content\\Img\\Tours\\" + itemId + ".jpg";
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

                for (int i = 1; i <= tour.NumberOfPhotos; i++)
                {
                    path = HostingEnvironment.ApplicationPhysicalPath + "\\Content\\Img\\Tours\\" + itemId + "_" + i + ".jpg";
                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                }

                ToursDBHelper.DeleteAllPhotos(itemId);
                return RedirectToRoute(new { controller = "Tours", action = "Index", message = "Зміни було успішно внесено." });
            }
            catch (Exception e)
            {
                return RedirectToRoute(new { controller = "Tours", action = "Index" });
            }
        }
    }
}