using DAL.DBHelpers;
using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TouristWebSite.Models;
using TouristWebSite.Helpers;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace TouristWebSite.Controllers
{
    [Authorize]
    public class HelperController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index(string message = "")
        {
            ViewBag.StatusMessage = message;

            return View();
        }

        [HttpGet]
        public ActionResult AdditionalInfo(string itemId)
        {
            try
            {
                var userForEdit = UsersDBHelper.GetById(itemId);
                AdditionalInfoViewModel model = new AdditionalInfoViewModel()
                {
                    Id = userForEdit.Id,
                    TypeOfStudies = userForEdit.TypeOfStudies,
                    StudiesDescription = userForEdit.StudiesDescription,
                    TypeOfWork = userForEdit.TypeOfWork,
                    WorkDescription = userForEdit.WorkDescription,
                    SportsDescription = userForEdit.SportsDescription,
                    MusicDescription = userForEdit.MusicDescription,
                    FilmsDescription = userForEdit.FilmsDescription,
                    BooksDescription = userForEdit.BooksDescription,
                    HobbiesDescription = userForEdit.HobbiesDescription,
                    PetsDescription = userForEdit.PetsDescription,
                    OtherInfo = userForEdit.OtherInfo
                };

                return View(model);
            }
            catch (Exception e)
            {
                return RedirectToRoute(new { controller = "Helper", action = "Index" });
            }
        }

        [HttpPost]
        public ActionResult AdditionalInfo(AdditionalInfoViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                UsersDBHelper.UpdateUserAdditional(model);
                return RedirectToAction("Index", new { Message = "Зміни було успішно внесено." });
            }
            catch (Exception e)
            {
                return RedirectToRoute(new { controller = "Helper", action = "Index" });
            }
        }

        [HttpGet]
        public ActionResult TourConstruct()
        {
            try
            {
                ConstructViewModel model = new ConstructViewModel()
                {
                    Countries = CountriesDBHelper.GetCountries(),
                    Transports = TransportsDBHelper.GetTransports(),
                    DestinationCountries = DestinationCountriesDBHelper.GetCountries(),
                    PeopleCount = 1
                };

                return View(model);
            }
            catch (Exception e)
            {
                return RedirectToRoute(new { controller = "Helper", action = "Index" });
            }
        }

        [HttpPost]
        public ActionResult TourConstruct(ConstructViewModel model)
        {
            try
            {
                long? ex1 = 0;
                long? ex2 = 0;
                long? ex3 = 0;
                long? ex4 = 0;
                long? ex5 = 0;

                if (model.ExcursionsCount > 0)
                {
                    ex1 = model.Excursion1;
                }

                if (model.ExcursionsCount > 1)
                {
                    ex2 = model.Excursion2;
                }

                if (model.ExcursionsCount > 2)
                {
                    ex3 = model.Excursion3;
                }

                if (model.ExcursionsCount > 3)
                {
                    ex4 = model.Excursion4;
                }

                if (model.ExcursionsCount > 4)
                {
                    ex5 = model.Excursion5;
                }

                model.Price = PriceCounterHelper.CountPrice(model.Route, model.Class == "Бізнес", model.BackRoute, model.BackClass == "Бізнес", model.Hotel, model.HotelClass == "Люкс", (long)ex1, (long)ex2, (long)ex3, (long)ex4, (long)ex5, model.PeopleCount);
                long newId = ConstructedToursDBHelper.Add(model, User.Identity.GetUserId());
                string filename = PDFGeneratorHelper.GenerateConstructPDF(model, newId);
                EmailSenderHelper.SendEmail(UsersDBHelper.GetById(User.Identity.GetUserId()).Email, "Підтвердження бронювання туру.", "Тур було успішно заброньовано, деталі можна переглянути у прикріпленому документі.", filename);
                return RedirectToAction("Index", new { Message = "Тур було успішно сконструйовано. Документ про бронювання відправлено на електронну пошту." });
            }
            catch (Exception e)
            {
                return RedirectToRoute(new { controller = "Helper", action = "Index" });
            }
        }

        [HttpGet]
        public JsonResult GetCities(long id)
        {
            return Json(CitiesDBHelper.GetCitiesForCountry(id), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetLeavePoints(long cityId, long transportId)
        {
            return Json(LeavePointDBHelper.GetLeavePointsByCityAndTransport(cityId, transportId), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetDestinationCities(long id)
        {
            return Json(DestinationCitiesDBHelper.GetCitiesForCountry(id), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetDestinationPoints(long cityId, long transportId)
        {
            return Json(DestinationPointDBHelper.GetDestinationPointsByCityAndTransport(cityId, transportId), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetRoutes(long leavePointId, long destinationPointId)
        {
            var temp = RoutesDBHelper.GetRoutes(leavePointId, destinationPointId);

            foreach (var item in temp)
            {
                item.Name = $"{item.Name} ({item.Start.ToShortDateString()}, {item.Start.ToShortTimeString()} - {item.End.ToShortDateString()}, {item.End.ToShortTimeString()})";
            }

            return Json(temp, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetHotels(long destinationCityId)
        {
            var temp = HotelsDBHelper.GetHotels(destinationCityId);

            foreach (var item in temp)
            {
                item.Name = $"{item.Name} ({item.PriceStandart}грн./{item.PriceLux}грн.)";
            }

            return Json(temp, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetExcursions(long hotelId)
        {
            return Json(ExcursionsDBHelper.GetExcursions(hotelId), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetBackRoutes(long leavePointId, long destinationPointId)
        {
            var temp = BackRoutesDBHelper.GetBackRoutes(leavePointId, destinationPointId);

            foreach (var item in temp)
            {
                item.Name = $"{item.Name} ({item.Start.ToShortDateString()}, {item.Start.ToShortTimeString()} - {item.End.ToShortDateString()}, {item.End.ToShortTimeString()})";
            }

            return Json(temp, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public long GetPrice(long routeId, bool isBusiness, long backRouteId, bool isBackBusiness, long hotelId, bool isLux, long ex1, long ex2, long ex3, long ex4, long ex5, long peopleCount)
        {
            return PriceCounterHelper.CountPrice(routeId, isBusiness, backRouteId, isBackBusiness, hotelId, isLux, ex1, ex2, ex3, ex4, ex5, peopleCount);
        }

        [HttpGet]
        public ActionResult HelpConstruct()
        {
            try
            {
                var currentUser = UsersDBHelper.GetById(User.Identity.GetUserId());
                var usersFromSamePlace = UsersDBHelper.GetAllActive()
                    .Where(x => x.Id != currentUser.Id &&
                                x.Country == currentUser.Country &&
                                x.City == currentUser.City).ToList();

                List<ApplicationUser> sameUsers = new List<ApplicationUser>();

                foreach (var user in usersFromSamePlace)
                {
                    var counter = 0;

                    if (user.DateOfBirth != null && currentUser.DateOfBirth != null)
                    {
                        if (Math.Abs((user.DateOfBirth - currentUser.DateOfBirth).Value.TotalDays / 365) <= 5)
                        {
                            counter++;
                        }
                    }

                    if (user.MaritalStatus != null && currentUser.MaritalStatus != null &&
                        user.MaritalStatus == currentUser.MaritalStatus)
                    {
                        counter++;
                    }

                    if (user.Profession != null && currentUser.Profession != null &&
                        user.Profession == currentUser.Profession)
                    {
                        counter++;
                    }

                    if (user.TypeOfStudies != null && currentUser.TypeOfStudies != null &&
                        user.TypeOfStudies == currentUser.TypeOfStudies)
                    {
                        counter++;
                    }

                    if (user.TypeOfWork != null && currentUser.TypeOfWork != null &&
                        user.TypeOfWork == currentUser.TypeOfWork)
                    {
                        counter++;
                    }

                    if (user.StudiesDescription != null && currentUser.StudiesDescription != null &&
                        CountMatches(user.StudiesDescription, currentUser.StudiesDescription) >= 5)
                    {
                        counter++;
                    }

                    if (user.WorkDescription != null && currentUser.WorkDescription != null &&
                        CountMatches(user.WorkDescription, currentUser.WorkDescription) >= 5)
                    {
                        counter++;
                    }

                    if (user.SportsDescription != null && currentUser.SportsDescription != null &&
                        CountMatches(user.SportsDescription, currentUser.SportsDescription) >= 5)
                    {
                        counter++;
                    }

                    if (user.MusicDescription != null && currentUser.MusicDescription != null &&
                        CountMatches(user.MusicDescription, currentUser.MusicDescription) >= 5)
                    {
                        counter++;
                    }

                    if (user.FilmsDescription != null && currentUser.FilmsDescription != null &&
                        CountMatches(user.FilmsDescription, currentUser.FilmsDescription) >= 5)
                    {
                        counter++;
                    }

                    if (user.BooksDescription != null && currentUser.BooksDescription != null &&
                        CountMatches(user.BooksDescription, currentUser.BooksDescription) >= 5)
                    {
                        counter++;
                    }

                    if (user.HobbiesDescription != null && currentUser.HobbiesDescription != null &&
                        CountMatches(user.HobbiesDescription, currentUser.HobbiesDescription) >= 5)
                    {
                        counter++;
                    }

                    if (user.PetsDescription != null && currentUser.PetsDescription != null &&
                        CountMatches(user.PetsDescription, currentUser.PetsDescription) >= 5)
                    {
                        counter++;
                    }

                    if (counter >= 5)
                    {
                        sameUsers.Add(user);
                    }
                }

                // TODO CHANGE
                return RedirectToRoute(new { controller = "Helper", action = "Index" });
            }
            catch (Exception e)
            {
                return RedirectToRoute(new { controller = "Helper", action = "Index" });
            }
        }

        [HttpGet]
        public ActionResult RateConstruct()
        {
            try
            {
                var constructedTours = ConstructedToursDBHelper.GetByUserId(User.Identity.GetUserId());
                ListConstructRateViewModels list = new ListConstructRateViewModels()
                {
                    ConstructRateViewModels = new List<ConstructRateViewModel>()
                };

                foreach (var tour in constructedTours)
                {
                    var route = RoutesDBHelper.GetRouteById(tour.RouteId);
                    var backRoute = BackRoutesDBHelper.GetBackRouteById(tour.BackRouteId);

                    string content = string.Empty;

                    if (tour.ExcursionsCount > 0)
                    {
                        content += ExcursionsDBHelper.GetExcursionById(tour.Excursion1Id).Name + ", ";
                    }

                    if (tour.ExcursionsCount > 1)
                    {
                        content += ExcursionsDBHelper.GetExcursionById(tour.Excursion2Id).Name + ", ";
                    }

                    if (tour.ExcursionsCount > 2)
                    {
                        content += ExcursionsDBHelper.GetExcursionById(tour.Excursion3Id).Name + ", ";
                    }

                    if (tour.ExcursionsCount > 3)
                    {
                        content += ExcursionsDBHelper.GetExcursionById(tour.Excursion4Id).Name + ", ";
                    }

                    if (tour.ExcursionsCount > 4)
                    {
                        content += ExcursionsDBHelper.GetExcursionById(tour.Excursion5Id).Name + ", ";
                    }

                    if (content != string.Empty)
                    {
                        content = content.Substring(0, content.Length - 2);
                    }

                    list.ConstructRateViewModels.Add(new ConstructRateViewModel()
                    {
                        Id = tour.Id,
                        DateStart = route.Start,
                        DateEnd = route.End,
                        PeopleCount = tour.PeopleCount,
                        Price = tour.Price,
                        Country = CountriesDBHelper.GetCountryById(tour.CountryId).Name,
                        City = CitiesDBHelper.GetCityById(tour.CityId).Name,
                        Transport = TransportsDBHelper.GetTransportById(tour.TransportId).Name,
                        LeavePoint = LeavePointDBHelper.GetLeavePointById(tour.LeavePointId).Name,
                        DestinationCountry = DestinationCountriesDBHelper.GetCountryById(tour.DestinationCountryId).Name,
                        DestinationCity = DestinationCitiesDBHelper.GetCityById(tour.DestinationCityId).Name,
                        DestinationPoint = DestinationPointDBHelper.GetDestinationPointById(tour.DestinationPointId).Name,
                        Route = route.Name + "(" + route.Start.ToShortDateString() + ", " + route.Start.ToShortTimeString() + " - " + route.End.ToShortDateString() + ", " + route.End.ToShortTimeString() + ")",
                        Class = tour.Class,
                        Hotel = HotelsDBHelper.GetHotelById(tour.HotelId).Name,
                        HotelClass = tour.HotelClass,
                        Excursions = content,
                        BackRoute = backRoute.Name + "(" + backRoute.Start.ToShortDateString() + ", " + backRoute.Start.ToShortTimeString() + " - " + backRoute.End.ToShortDateString() + ", " + backRoute.End.ToShortTimeString() + ")",
                        BackClass = tour.BackClass,
                        Comment = tour.Comment,
                        Mark = tour.Mark
                    });
                }

                return View(list);
            }
            catch (Exception e)
            {
                return RedirectToRoute(new { controller = "Helper", action = "Index" });
            }
        }

        [HttpPost]
        public ActionResult AddComment(string comment, string mark, long id)
        {
            try
            {
                ConstructedToursDBHelper.AddComment(comment, mark, id);

                return RedirectToRoute(new { controller = "Helper", action = "RateConstruct" });
            }
            catch (Exception e)
            {
                return RedirectToRoute(new { controller = "Helper", action = "RateConstruct" });
            }
        }

        private int CountMatches(string string1, string string2)
        {
            string1 = string1.ToLower();
            string2 = string2.ToLower();

            List<string> notMatches = new List<string>()
                {"та", "і", "також", "тільки", "люблю", "не", "отож", "саме", "навіть", "дуже", "у", "на", "в", "під", "", " ", "з", "із"};

            List<string> stringList1 = string1.Split(',', ' ', '.').ToList();
            List<string> stringList2 = string2.Split(',', ' ', '.').ToList();

            List<string> result = stringList1.Intersect(stringList2).Except(notMatches).ToList();
            return result.Count;
        }

    }
}