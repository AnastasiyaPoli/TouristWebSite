﻿using DAL.DBHelpers;
using DAL.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        [HttpGet]
        public ActionResult Index(string message = "", string search = "")
        {
            ViewBag.StatusMessage = message;
            search = search.ToLower();

            var tours = ToursDBHelper.GetActive();
            List<TourPlace> tourPlaces = new List<TourPlace>();

            DateTime max = DateTime.Now;
            long maxPrice = 0;

            if (tours.Count > 0)
            {
                max = tours[0].DateEnd;
            }

            foreach (var tour in tours)
            {
                if (tour.DateEnd > max)
                {
                    max = tour.DateEnd;
                }

                if (tour.Price > maxPrice)
                {
                    maxPrice = tour.Price;
                }

                var places = tour.Place.Split(',').ToList();
                foreach (var place in places)
                {
                    if (tourPlaces.FirstOrDefault(x => x.Place == place) == null)
                    {
                        tourPlaces.Add(new TourPlace()
                        {
                            Place = place,
                            IsChosen = false
                        });
                    }
                }
            }

            var model = new ActiveToursViewModel()
            {
                ActiveTours = tours
                    .Where(x =>
                         x.Name.ToLower().Contains(search) ||
                         x.Description.ToLower().Contains(search) ||
                         x.Place.ToLower().Contains(search))
                    .ToList(),
                Favourites = FavouritesDBHelper.GetForUser(User.Identity.GetUserId()),
                Search = string.Empty,
                TourPlaces = tourPlaces,
                DateFrom = DateTime.Now,
                DateTo = max,
                PriceTo = maxPrice
            };

            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Index(ActiveToursViewModel model)
        {
            var tours = ToursDBHelper.GetActive();
            List<Tour> rezultTours = new List<Tour>();

            foreach (var tour in tours)
            {
                if (!(tour.DateStart >= model.DateFrom && tour.DateEnd <= model.DateTo && tour.Price <= model.PriceTo))
                {
                    continue;
                }

                foreach (var place in model.TourPlaces)
                {
                    var lowerPlace = place.Place.ToLower();
                    if (place.IsChosen && tour.Place.ToLower().Contains(lowerPlace))
                    {
                        rezultTours.Add(tour);
                        break;
                    }
                }
            }

            var newModel = new ActiveToursViewModel()
            {
                ActiveTours = rezultTours,
                Favourites = FavouritesDBHelper.GetForUser(User.Identity.GetUserId()),
                Search = string.Empty,
                TourPlaces = model.TourPlaces,
                DateTo = model.DateTo,
                DateFrom = model.DateFrom,
                PriceTo = model.PriceTo
            };

            ViewBag.StatusMessage = "Фільтр було успішно застосовано.";
            return View(newModel);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Search(ActiveToursViewModel model)
        {
            return RedirectToRoute(new { controller = "Tours", action = "Index", message = "Пошук було успішно виконано.", search = model.Search });
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Details(long itemId, string message)
        {
            try
            {
                List<CommentViewModel> list = new List<CommentViewModel>();

                foreach (var item in CommentsDBHelper.GetActiveForTour(itemId))
                {
                    string mark = "";

                    switch (item.NumberMark)
                    {
                        case 5:
                            {
                                mark = "Ідеально";
                            }
                            break;

                        case 4:
                            {
                                mark = "Добре";
                            }
                            break;

                        case 3:
                            {
                                mark = "Задовільно";
                            }
                            break;

                        case 2:
                            {
                                mark = "Погано";
                            }
                            break;

                        case 1:
                            {
                                mark = "Жахливо";
                            }
                            break;

                        default:
                            {
                                mark = "Не обрана";
                            }
                            break;
                    }

                    list.Add(new CommentViewModel()
                    {
                        Id = item.Id,
                        User = item.User,
                        Text = item.Text,
                        Mark = mark,
                        WasBooked = BookedToursDBHelper.Check(item.User.Id, itemId)
                    });
                }

                var model = new ChosenTourViewModel()
                {
                    ChosenTour = ToursDBHelper.GetById(itemId),
                    ChosenTourId = itemId,
                    Comments = list
                };

                ViewBag.StatusMessage = message;

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
                return RedirectToRoute(new { controller = "Tours", action = "Index", message = "Тур було успішно видалено." });
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

                if (model.DateStart <= DateTime.Now)
                {
                    ModelState.AddModelError("DateStart", "Дата початку туру повинна бути пізнішою за сьогоднішню дату.");
                    return View(model);
                }

                ToursDBHelper.Add(model);
                return RedirectToRoute(new { controller = "Tours", action = "Index", message = "Тур було успішно додано." });
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
                return RedirectToRoute(new { controller = "Tours", action = "Index", message = "Зміни було успішно збережено." });
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

                //recommendations
                var bookedTour = ToursDBHelper.GetById(model.TourId);
                var userId = User.Identity.GetUserId();
                var tours = SimilarToursHelper.FindSimilarTours(bookedTour, userId);

                if (tours.Count > 0)
                {
                    string filenameRecommend = PDFGeneratorHelper.GeneratePDFRecommend(tours, userId);
                    EmailSenderHelper.SendEmail(UsersDBHelper.GetById(User.Identity.GetUserId()).Email, "Рекомендації до туру.", "У цьому документі Ви знайдете рекомендації турів, приємного перегляду!", filenameRecommend);
                }

                return RedirectToAction("Index", new { Message = "Тур було успішно заброньовано. Документ про замовлення надіслано на електронну пошту." });
            }
            catch (Exception e)
            {
                return RedirectToRoute(new { controller = "Tours", action = "Index" });
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Photos(long itemId, string message = "", string successMessage = "", string errorMessage = "")
        {
            try
            {
                var chosenTour = ToursDBHelper.GetById(itemId);

                ImageViewModel model = new ImageViewModel()
                {
                    Tour = chosenTour,
                    TourId = chosenTour.Id
                };

                if (message != string.Empty)
                {
                    string[] Errors = new string[1];
                    Errors[0] = message;
                    var rez = new IdentityResult(Errors);
                    AddErrors(rez);
                }

                if (successMessage != String.Empty)
                {
                    ViewBag.StatusMessage = successMessage;
                }

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
                return RedirectToRoute(new { controller = "Tours", action = "Photos", itemId = itemId, successMessage = "Всі фотографії було успішно видалено." });
            }
            catch (Exception e)
            {
                return RedirectToRoute(new { controller = "Tours", action = "Index" });
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeletePhoto(long tourId, long photoId)
        {
            try
            {
                var tour = ToursDBHelper.GetById(tourId);
                var deletePath = "";
                var path = "";
                var newPath = "";

                if (photoId == 0)
                {
                    deletePath = HostingEnvironment.ApplicationPhysicalPath + "\\Content\\Img\\Tours\\" + tourId + ".jpg";
                }
                else
                {
                    deletePath = HostingEnvironment.ApplicationPhysicalPath + "\\Content\\Img\\Tours\\" + tourId + "_" + photoId + ".jpg";

                }

                if (System.IO.File.Exists(deletePath))
                {
                    System.IO.File.Delete(deletePath);
                }


                for (long i = photoId + 1; i < tour.NumberOfPhotos; i++)
                {
                    path = HostingEnvironment.ApplicationPhysicalPath + "\\Content\\Img\\Tours\\" + tourId + "_" + i + ".jpg";

                    if (i == 1)
                    {
                        newPath = HostingEnvironment.ApplicationPhysicalPath + "\\Content\\Img\\Tours\\" + tourId + ".jpg";

                    }
                    else
                    {
                        newPath = HostingEnvironment.ApplicationPhysicalPath + "\\Content\\Img\\Tours\\" + tourId + "_" + (i-1) + ".jpg";
                    }

                    System.IO.File.Move(path, newPath);
                }

                ToursDBHelper.DeletePhoto(tourId);
                return RedirectToRoute(new { controller = "Tours", action = "Photos", itemId = tourId, successMessage = "Фотографію було успішно видалено." });
            }
            catch (Exception e)
            {
                return RedirectToRoute(new { controller = "Tours", action = "Index" });
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AddPhoto(ImageViewModel img, IEnumerable<HttpPostedFileBase> incomeFiles)
        {
            if (ModelState.IsValid)
            {
                if (incomeFiles != null && incomeFiles.Count() != 0 && incomeFiles.First() != null)
                {
                    var files = incomeFiles.ToList();

                    var tour = ToursDBHelper.GetById(img.TourId);

                    if (ToursDBHelper.GetById(img.TourId).NumberOfPhotos == 0)
                    {
                        files[0].SaveAs(HttpContext.Server.MapPath("~/Content/Img/Tours/" + img.TourId + ".jpg"));
                        img.ImagePath = files[0].FileName;
                        files.RemoveAt(0);
                    }

                    var i = tour.NumberOfPhotos;
                    if (i == 0)
                    {
                        i++;
                    }
                    foreach (var file in files)
                    {
                        file.SaveAs(HttpContext.Server.MapPath("~/Content/Img/Tours/" + img.TourId + "_" + (i++) + ".jpg"));
                        img.ImagePath = file.FileName;
                    }
                }
                else
                {
                    return RedirectToRoute(new { controller = "Tours", action = "Photos", itemId = img.TourId, message = "Завантажте фотографії спочатку." });
                }

                ToursDBHelper.AddNewPhotos(img.TourId, incomeFiles.Count());
                return RedirectToRoute(new { controller = "Tours", action = "Photos", itemId = img.TourId, successMessage = "Фотографії було успішно збережено." });
            }
            return View(img);
        }

        public ActionResult AddComment(ChosenTourViewModel model)
        {
            try
            {
                CommentsDBHelper.Add(model.Text, User.Identity.GetUserId(), model.ChosenTourId, model.Mark);

                // if current user has already booked this tour and ratet it highly
                if ((model.Mark == "Ідеально" || model.Mark == "Добре")
                    && BookedToursDBHelper.GetBookedToursForUser(User.Identity.GetUserId())
                        .FirstOrDefault(x => x.TourId == model.ChosenTourId) != null)
                {
                    var bookedTour = ToursDBHelper.GetById(model.ChosenTourId);
                    var userId = User.Identity.GetUserId();
                    var tours = SimilarToursHelper.FindSimilarTours(bookedTour, userId);

                    if (tours.Count > 0)
                    {
                        string filenameRecommend = PDFGeneratorHelper.GeneratePDFRecommend(tours, userId);
                        EmailSenderHelper.SendEmail(UsersDBHelper.GetById(User.Identity.GetUserId()).Email, "Рекомендації до туру.", "У цьому документі Ви знайдете рекомендації турів, приємного перегляду!", filenameRecommend);
                    }
                }

                return RedirectToRoute(new { controller = "Tours", action = "Details", itemId = model.ChosenTourId, message = "Коментар було успішно збережено." });
            }
            catch (Exception e)
            {
                return RedirectToRoute(new { controller = "Tours", action = "Details", itemId = model.ChosenTourId });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult DeleteComment(long itemId, long tourId)
        {
            try
            {
                CommentsDBHelper.Deactivate(itemId);
                return RedirectToRoute(new { controller = "Tours", action = "Details", itemId = tourId, message = "Коментар було успішно видалено." });
            }
            catch (Exception e)
            {
                return RedirectToRoute(new { controller = "Tours", action = "Index" });
            }
        }

        [HttpGet]
        public ActionResult Favourite(long itemId)
        {
            try
            {
                FavouritesDBHelper.Add(User.Identity.GetUserId(), itemId);

                return RedirectToRoute(new { controller = "Tours", action = "Index", message = "Тур було успішно додано до списку улюблених." });
            }
            catch (Exception e)
            {
                return RedirectToRoute(new { controller = "Tours", action = "Index" });
            }
        }

        [HttpGet]
        public ActionResult RemoveFavourite(long itemId)
        {
            try
            {
                FavouritesDBHelper.Delete(itemId);

                return RedirectToRoute(new { controller = "Tours", action = "Index", message = "Тур було успішно видалено зі списку улюблених." });
            }
            catch (Exception e)
            {
                return RedirectToRoute(new { controller = "Tours", action = "Index" });
            }
        }

        [HttpGet]
        public ActionResult RemoveFavouriteCab(long itemId)
        {
            try
            {
                FavouritesDBHelper.Delete(itemId);

                return RedirectToRoute(new { controller = "Manage", action = "FavouriteTours", message = "Тур було успішно видалено зі списку улюблених." });
            }
            catch (Exception e)
            {
                return RedirectToRoute(new { controller = "Tours", action = "Index" });
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}