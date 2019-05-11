using DAL.DBHelpers;
using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TouristWebSite.Models;
using TouristWebSite.Helpers;
using System.Collections.Generic;
using System.Security.Cryptography;
using DAL.Models;

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
                return RedirectToAction("Index", new { Message = "Зміни було успішно збережено." });
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
                ConstructedTour incomeTour = (ConstructedTour)TempData["tour"];
                ConstructViewModel model = null;

                if (incomeTour == null)
                {
                    model = new ConstructViewModel()
                    {
                        Countries = CountriesDBHelper.GetCountries(),
                        Transports = TransportsDBHelper.GetTransports(),
                        DestinationCountries = DestinationCountriesDBHelper.GetCountries(),
                        PeopleCount = 1,
                        Type = "Not recommended"
                    };
                }
                else
                {
                    var route = RoutesDBHelper.GetRouteById(incomeTour.RouteId);
                    var city = CitiesDBHelper.GetCityById(route.LeavePoint.CityId);
                    var transports = TransportsDBHelper.GetTransports();
                    var transport = transports.FirstOrDefault(x => x.Id == route.LeavePoint.TransportId);
                    transports.Remove(transport);
                    transports.Insert(0, transport);
                    var backRoute = BackRoutesDBHelper.GetBackRouteById(incomeTour.BackRouteId);
                    var destinationCity = DestinationCitiesDBHelper.GetCityById(route.DestinationPoint.DestinationCityId);
                    var excursions = TourExcusionsDBHelper.GetByTourId(incomeTour.Id);

                    long? ex1 = 0;
                    long? ex2 = 0;
                    long? ex3 = 0;
                    long? ex4 = 0;
                    long? ex5 = 0;

                    if (excursions.Count > 0)
                    {
                        ex1 = excursions[0].ExcursionId;
                    }

                    if (excursions.Count > 1)
                    {
                        ex2 = excursions[1].ExcursionId;
                    }

                    if (excursions.Count > 2)
                    {
                        ex3 = excursions[2].ExcursionId;
                    }

                    if (excursions.Count > 3)
                    {
                        ex4 = excursions[3].ExcursionId;
                    }

                    if (excursions.Count > 4)
                    {
                        ex5 = excursions[4].ExcursionId;
                    }

                    model = new ConstructViewModel()
                    {
                        Countries = CountriesDBHelper.GetCountries(),
                        Transports = transports,
                        DestinationCountries = DestinationCountriesDBHelper.GetCountries(),
                        Country = CountriesDBHelper.GetCountryById(city.CountryId).Id,
                        City = city.Id,
                        LeavePoint = route.LeavePointId,
                        DestinationCountry = DestinationCountriesDBHelper.GetCountryById(destinationCity.DestinationCountryId).Id,
                        DestinationCity = destinationCity.Id,
                        DestinationPoint = route.DestinationPointId,
                        Route = route.Id,
                        Class = incomeTour.Class,
                        Hotel = incomeTour.HotelId,
                        HotelClass = incomeTour.HotelClass,
                        PeopleCount = incomeTour.PeopleCount,
                        ExcursionsCount = excursions.Count,
                        Excursion1 = ex1,
                        Excursion2 = ex2,
                        Excursion3 = ex3,
                        Excursion4 = ex4,
                        Excursion5 = ex5,
                        BackRoute = incomeTour.BackRouteId,
                        BackClass = incomeTour.BackClass,
                        Type = "Recommended"
                    };
                }

                return View(model);

            }
            catch (Exception e)
            {
                return RedirectToRoute(new { controller = "Helper", action = "Index" });
            }
        }

        [HttpGet]
        public ActionResult RecommendationsConstruct(long itemId)
        {
            var tour = RecommendationsDBHelper.GetRecommendationById(itemId).ConstructedTour;
            TempData["tour"] = tour;
            return RedirectToAction("TourConstruct");
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
        public JsonResult GetCities(long CountryHint, long CityHint)
        {
            var cities = CitiesDBHelper.GetCitiesForCountry(CountryHint);

            if (CityHint != 0)
            {
                var city = cities.FirstOrDefault(x => x.Id == CityHint);
                cities.Remove(city);
                cities.Insert(0, city);
            }

            return Json(cities, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetLeavePoints(long cityId, long transportId, long LeavePointHint)
        {
            var leavePoints = LeavePointDBHelper.GetLeavePointsByCityAndTransport(cityId, transportId);

            if (LeavePointHint != 0)
            {
                var leavePoint = leavePoints.FirstOrDefault(x => x.Id == LeavePointHint);
                leavePoints.Remove(leavePoint);
                leavePoints.Insert(0, leavePoint);
            }

            return Json(leavePoints, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetDestinationCities(long id, long DestinationCityHint)
        {
            var destinationCities = DestinationCitiesDBHelper.GetCitiesForCountry(id);

            if (DestinationCityHint != 0)
            {
                var destinationCity = destinationCities.FirstOrDefault(x => x.Id == DestinationCityHint);
                destinationCities.Remove(destinationCity);
                destinationCities.Insert(0, destinationCity);
            }

            return Json(destinationCities, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetDestinationPoints(long cityId, long transportId, long DestinationPointHint)
        {
            var destinationPoints = DestinationPointDBHelper.GetDestinationPointsByCityAndTransport(cityId, transportId);

            if (DestinationPointHint != 0)
            {
                var destinationPoint = destinationPoints.FirstOrDefault(x => x.Id == DestinationPointHint);
                destinationPoints.Remove(destinationPoint);
                destinationPoints.Insert(0, destinationPoint);
            }

            return Json(destinationPoints, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetRoutes(long leavePointId, long destinationPointId, long RouteHint)
        {
            var routes = RoutesDBHelper.GetRoutes(leavePointId, destinationPointId);

            if (RouteHint != 0)
            {
                var route = routes.FirstOrDefault(x => x.Id == RouteHint);
                routes.Remove(route);
                routes.Insert(0, route);
            }

            foreach (var item in routes)
            {
                item.Name = $"{item.Name} ({item.Start.ToShortDateString()}, {item.Start.ToShortTimeString()} - {item.End.ToShortDateString()}, {item.End.ToShortTimeString()})";
            }

            return Json(routes, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetHotels(long destinationCityId, long HotelHint)
        {
            var hotels = HotelsDBHelper.GetHotels(destinationCityId);

            if (HotelHint != 0)
            {
                var hotel = hotels.FirstOrDefault(x => x.Id == HotelHint);
                hotels.Remove(hotel);
                hotels.Insert(0, hotel);
            }

            foreach (var item in hotels)
            {
                item.Name = $"{item.Name} ({item.PriceStandart}грн./{item.PriceLux}грн.)";
            }

            return Json(hotels, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetExcursions(long hotelId, long ExcursionHint)
        {
            var excursions = ExcursionsDBHelper.GetExcursions(hotelId);

            if (ExcursionHint != 0)
            {
                var excursion = excursions.FirstOrDefault(x => x.Id == ExcursionHint);
                excursions.Remove(excursion);
                excursions.Insert(0, excursion);
            }

            return Json(excursions, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetBackRoutes(long leavePointId, long destinationPointId, long BackRouteHint)
        {
            var backRoutes = BackRoutesDBHelper.GetBackRoutes(leavePointId, destinationPointId);

            if (BackRouteHint != 0)
            {
                var backRoute = backRoutes.FirstOrDefault(x => x.Id == BackRouteHint);
                backRoutes.Remove(backRoute);
                backRoutes.Insert(0, backRoute);
            }

            foreach (var item in backRoutes)
            {
                item.Name = $"{item.Name} ({item.Start.ToShortDateString()}, {item.Start.ToShortTimeString()} - {item.End.ToShortDateString()}, {item.End.ToShortTimeString()})";
            }

            return Json(backRoutes, JsonRequestBehavior.AllowGet);
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

                var tours = ConstructedToursDBHelper.GetAll()
                    .Where(x => (sameUsers.FirstOrDefault(y => y.Id == x.ApplicationUserId)) != null)
                    .ToList();

                foreach (var tour in tours)
                {
                    if (tour.NumberMark == 0)
                    {
                        tour.NumberMark = 3;
                    }

                    if (tour.Comment == null)
                    {
                        tour.Comment = "";
                    }
                };

                var bestTours = tours.OrderByDescending(x => x.NumberMark)
                    .ThenByDescending(y => y.Comment.Length)
                    .Take(3)
                    .ToList();

                RecommendationsDBHelper.SaveRecommendations(bestTours, User.Identity.GetUserId());

                return RedirectToAction("Recommendations", new { noRecommend = (bestTours.Count == 0) });
            }
            catch (Exception e)
            {
                return RedirectToRoute(new { controller = "Helper", action = "Index" });
            }
        }

        [HttpGet]
        public ActionResult Recommendations(bool noRecommend = false)
        {
            try
            {
                var recommendations = RecommendationsDBHelper.GetRecommendationsForUser(User.Identity.GetUserId());
                ListConstructRateViewModels list = new ListConstructRateViewModels()
                {
                    ConstructRateViewModels = new List<ConstructRateViewModel>()
                };

                foreach (var recommendation in recommendations)
                {
                    string content = string.Empty;
                    var excusions = TourExcusionsDBHelper.GetByTourId(recommendation.ConstructedTourId);

                    foreach (var excursion in excusions)
                    {
                        var realExcursion = ExcursionsDBHelper.GetExcursionById(excursion.ExcursionId);
                        content += realExcursion.Name + ", ";
                    }

                    if (content != string.Empty)
                    {
                        content = content.Substring(0, content.Length - 2);
                    }

                    var route = RoutesDBHelper.GetRouteById(recommendation.ConstructedTour.RouteId);
                    var backRoute = BackRoutesDBHelper.GetBackRouteById(recommendation.ConstructedTour.BackRouteId);
                    var city = CitiesDBHelper.GetCityById(route.LeavePoint.CityId);
                    var destinationCity = DestinationCitiesDBHelper.GetCityById(route.DestinationPoint.DestinationCityId);

                    string mark = "";

                    switch (recommendation.ConstructedTour.NumberMark)
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

                    list.ConstructRateViewModels.Add(new ConstructRateViewModel()
                    {
                        Id = recommendation.Id,
                        DateStart = route.Start,
                        DateEnd = backRoute.End,
                        PeopleCount = recommendation.ConstructedTour.PeopleCount,
                        Price = recommendation.ConstructedTour.Price,
                        Country = city.Country.Name,
                        City = city.Name,
                        Transport = TransportsDBHelper.GetTransportById(route.LeavePoint.TransportId).Name,
                        LeavePoint = route.LeavePoint.Name,
                        DestinationCountry = destinationCity.DestinationCountry.Name,
                        DestinationCity = destinationCity.Name,
                        DestinationPoint = route.DestinationPoint.Name,
                        Route = route.Name + "(" + route.Start.ToShortDateString() + ", " + route.Start.ToShortTimeString() + " - " + route.End.ToShortDateString() + ", " + route.End.ToShortTimeString() + ")",
                        Class = recommendation.ConstructedTour.Class,
                        Hotel = HotelsDBHelper.GetHotelById(recommendation.ConstructedTour.HotelId).Name,
                        HotelClass = recommendation.ConstructedTour.HotelClass,
                        Excursions = content,
                        BackRoute = backRoute.Name + "(" + backRoute.Start.ToShortDateString() + ", " + backRoute.Start.ToShortTimeString() + " - " + backRoute.End.ToShortDateString() + ", " + backRoute.End.ToShortTimeString() + ")",
                        BackClass = recommendation.ConstructedTour.BackClass,
                        Comment = recommendation.ConstructedTour.Comment,
                        Mark = mark
                    });
                }

                if (recommendations.Count == 0)
                {
                    if (noRecommend == true)
                    {
                        ViewBag.noRecommendations = "На жаль, на основі наявної інформації жодних рекомендацій не може бути надано. Необхідно доповнити та розширити загальну і додаткову інформацію про себе або зачекати, доки у системі з'явиться більша кількість користувачів.";
                    }
                    else
                    {
                        ViewBag.noRecommendations = "Рекомендацій ще не було надано. Перейдіть за ";
                        ViewBag.endNoRecommendations = " для надання рекомендацій.";
                    }
                }

                return View(list);
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
                    string content = string.Empty;
                    var excusions = TourExcusionsDBHelper.GetByTourId(tour.Id);

                    foreach (var excursion in excusions)
                    {
                        var realExcursion = ExcursionsDBHelper.GetExcursionById(excursion.ExcursionId);
                        content += realExcursion.Name + ", ";
                    }

                    if (content != string.Empty)
                    {
                        content = content.Substring(0, content.Length - 2);
                    }

                    var route = RoutesDBHelper.GetRouteById(tour.RouteId);
                    var backRoute = BackRoutesDBHelper.GetBackRouteById(tour.BackRouteId);
                    var city = CitiesDBHelper.GetCityById(route.LeavePoint.CityId);
                    var destinationCity = DestinationCitiesDBHelper.GetCityById(route.DestinationPoint.DestinationCityId);

                    string mark = "";

                    switch (tour.NumberMark)
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

                    list.ConstructRateViewModels.Add(new ConstructRateViewModel()
                    {
                        Id = tour.Id,
                        DateStart = route.Start,
                        DateEnd = backRoute.End,
                        PeopleCount = tour.PeopleCount,
                        Price = tour.Price,
                        Country = city.Country.Name,
                        City = city.Name,
                        Transport = TransportsDBHelper.GetTransportById(route.LeavePoint.TransportId).Name,
                        LeavePoint = route.LeavePoint.Name,
                        DestinationCountry = destinationCity.DestinationCountry.Name,
                        DestinationCity = destinationCity.Name,
                        DestinationPoint = route.DestinationPoint.Name,
                        Route = tour.Route.Name + "(" + route.Start.ToShortDateString() + ", " + route.Start.ToShortTimeString() + " - " + route.End.ToShortDateString() + ", " + route.End.ToShortTimeString() + ")",
                        Class = tour.Class,
                        Hotel = tour.Hotel.Name,
                        HotelClass = tour.HotelClass,
                        Excursions = content,
                        BackRoute = backRoute.Name + "(" + backRoute.Start.ToShortDateString() + ", " + backRoute.Start.ToShortTimeString() + " - " + backRoute.End.ToShortDateString() + ", " + backRoute.End.ToShortTimeString() + ")",
                        BackClass = tour.BackClass,
                        Comment = tour.Comment,
                        Mark = mark
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