using DAL.DBHelpers;
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
        /// <summary>
        /// For displaying tours page (default nd after search)
        /// </summary>
        /// <param name="message"> Success message </param>
        /// <param name="search"> Search string </param>
        /// <returns> Tours page </returns>
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

        /// <summary>
        /// For displaying tours page (after filtering)
        /// </summary>
        /// <param name="model"> Active tours view model </param>
        /// <returns> Tours page </returns>
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

        /// <summary>
        /// For searching tours
        /// </summary>
        /// <param name="model">Active view tours model</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Search(ActiveToursViewModel model)
        {
            return RedirectToRoute(new { controller = "Tours", action = "Index", message = "Пошук було успішно виконано.", search = model.Search });
        }

        /// <summary>
        /// For displaying tour details page
        /// </summary>
        /// <param name="itemId"> Tour id </param>
        /// <param name="message"> Success message </param>
        /// <returns> Tour details page </returns>
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
            catch (Exception)
            {
                return RedirectToRoute(new { controller = "Tours", action = "Index" });
            }
        }

        /// <summary>
        /// For deleting tour
        /// </summary>
        /// <param name="itemId"> Tour id </param>
        /// <returns> Redirect to route (Index) </returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Delete(long itemId)
        {
            try
            {
                ToursDBHelper.Deactivate(itemId);
                return RedirectToRoute(new { controller = "Tours", action = "Index", message = "Тур було успішно видалено." });
            }
            catch (Exception)
            {
                return RedirectToRoute(new { controller = "Tours", action = "Index" });
            }
        }

        /// <summary>
        /// For displaying tour add page
        /// </summary>
        /// <returns> Tour add page </returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Add()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {
                return RedirectToRoute(new { controller = "Tours", action = "Index" });
            }
        }

        /// <summary>
        /// For adding new tour
        /// </summary>
        /// <param name="model"> Tour view model </param>
        /// <returns> Redirect to route (Index) </returns>
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
            catch (Exception)
            {
                return RedirectToRoute(new { controller = "Tours", action = "Index" });
            }
        }

        /// <summary>
        /// For displaying tour update page 
        /// </summary>
        /// <param name="itemId"> Tour id </param>
        /// <returns> Tour update page </returns>
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
                    Routes = tour.Routes,
                    Hotel = tour.Hotel,
                    Excursions = tour.Excursions,
                    BackRoutes = tour.BackRoutes,
                    Price = tour.Price,
                    DateStart = tour.DateStart,
                    DateEnd = tour.DateEnd,
                    RouteLink = tour.RouteLink
                };

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToRoute(new { controller = "Tours", action = "Index" });
            }
        }

        /// <summary>
        /// For updating a tour
        /// </summary>
        /// <param name="model"> Tour view model </param>
        /// <returns> Redirect to route (Index) </returns>
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
            catch (Exception)
            {
                return RedirectToRoute(new { controller = "Tours", action = "Index" });
            }
        }

        /// <summary>
        /// For displaying tour booking page
        /// </summary>
        /// <param name="itemId"> Tour id </param>
        /// <returns> Tour booking page </returns>
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
            catch (Exception)
            {
                return RedirectToRoute(new { controller = "Manage", action = "Index" });
            }
        }

        /// <summary>
        /// For booking a tour
        /// </summary>
        /// <param name="model"> Tour booking view model </param>
        /// <returns> Redirect to route (Index) </returns>
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
                EmailSenderHelper.SendEmail(UsersDBHelper.GetById(User.Identity.GetUserId()).Email, "Підтвердження замовлення туру.", "Тур було успішно заброньовано, деталі можна переглянути у прикріпленому документі.", filename);

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
            catch (Exception)
            {
                return RedirectToRoute(new { controller = "Tours", action = "Index" });
            }
        }

        /// <summary>
        /// For displaying tour photos editing page
        /// </summary>
        /// <param name="itemId"> Tour id </param>
        /// <param name="message"> Error message </param>
        /// <param name="successMessage"> Success message </param>
        /// <returns> Tour photos editing page </returns>
        [Authorize(Roles = "Admin")]
        public ActionResult Photos(long itemId, string message = "", string successMessage = "")
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

                if (successMessage != string.Empty)
                {
                    ViewBag.StatusMessage = successMessage;
                }

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToRoute(new { controller = "Tours", action = "Index" });
            }
        }

        /// <summary>
        /// For deleting all tour photos
        /// </summary>
        /// <param name="itemId"> Tour id </param>
        /// <returns> Redirect to route (Photos) </returns>
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
            catch (Exception)
            {
                return RedirectToRoute(new { controller = "Tours", action = "Index" });
            }
        }

        /// <summary>
        /// For deleting one photo
        /// </summary>
        /// <param name="tourId"> Tour id </param>
        /// <param name="photoId"> Photo id </param>
        /// <returns> Redirect to route (Photos) </returns>
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
                        newPath = HostingEnvironment.ApplicationPhysicalPath + "\\Content\\Img\\Tours\\" + tourId + "_" + (i - 1) + ".jpg";
                    }

                    System.IO.File.Move(path, newPath);
                }

                ToursDBHelper.DeletePhoto(tourId);
                return RedirectToRoute(new { controller = "Tours", action = "Photos", itemId = tourId, successMessage = "Фотографію було успішно видалено." });
            }
            catch (Exception)
            {
                return RedirectToRoute(new { controller = "Tours", action = "Index" });
            }
        }

        /// <summary>
        /// For adding new photo
        /// </summary>
        /// <param name="img"> Image view model </param>
        /// <param name="incomeFiles"> List of income files </param>
        /// <returns> Redirect to route (Photos) </returns>
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

        /// <summary>
        /// For adding new comment
        /// </summary>
        /// <param name="model"> Chosen tour view model </param>
        /// <returns> Redirect to route (Details) </returns>
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
            catch (Exception)
            {
                return RedirectToRoute(new { controller = "Tours", action = "Details", itemId = model.ChosenTourId });
            }
        }

        /// <summary>
        /// For deleting comment
        /// </summary>
        /// <param name="itemId"> Comment id </param>
        /// <param name="tourId"> Tour id </param>
        /// <returns> Redirect to route (Details) </returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult DeleteComment(long itemId, long tourId)
        {
            try
            {
                CommentsDBHelper.Deactivate(itemId);
                return RedirectToRoute(new { controller = "Tours", action = "Details", itemId = tourId, message = "Коментар було успішно видалено." });
            }
            catch (Exception)
            {
                return RedirectToRoute(new { controller = "Tours", action = "Index" });
            }
        }

        /// <summary>
        /// For adding tour to favourite
        /// </summary>
        /// <param name="itemId"> Tour id </param>
        /// <returns> Redirect to route (Index) </returns>
        [HttpGet]
        public ActionResult Favourite(long itemId)
        {
            try
            {
                FavouritesDBHelper.Add(User.Identity.GetUserId(), itemId);

                return RedirectToRoute(new { controller = "Tours", action = "Index", message = "Тур було успішно додано до списку улюблених." });
            }
            catch (Exception)
            {
                return RedirectToRoute(new { controller = "Tours", action = "Index" });
            }
        }

        /// <summary>
        /// For removing tour from favourite
        /// </summary>
        /// <param name="itemId"> Tour id </param>
        /// <returns> Redirect to route (Index) </returns>
        [HttpGet]
        public ActionResult RemoveFavourite(long itemId)
        {
            try
            {
                FavouritesDBHelper.Delete(itemId);

                return RedirectToRoute(new { controller = "Tours", action = "Index", message = "Тур було успішно видалено зі списку улюблених." });
            }
            catch (Exception)
            {
                return RedirectToRoute(new { controller = "Tours", action = "Index" });
            }
        }

        /// <summary>
        /// For removing tour from favourite from favourites list page
        /// </summary>
        /// <param name="itemId"> Tour id </param>
        /// <returns> Redirect to route (Manage|FavouriteTours) </returns>
        [HttpGet]
        public ActionResult RemoveFavouriteCab(long itemId)
        {
            try
            {
                FavouritesDBHelper.Delete(itemId);

                return RedirectToRoute(new { controller = "Manage", action = "FavouriteTours", message = "Тур було успішно видалено зі списку улюблених." });
            }
            catch (Exception)
            {
                return RedirectToRoute(new { controller = "Tours", action = "Index" });
            }
        }

        /// <summary>
        /// For adding errors to model
        /// </summary>
        /// <param name="result"> Identity result with errors </param>
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}