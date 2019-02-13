using DAL.DBHelpers;
using System;
using System.Web.Mvc;
using TouristWebSite.Models;

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
                return RedirectToRoute(new {controller = "Helper", action = "Index"});
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
                return RedirectToAction("Index", new {Message = "Зміни було успішно внесено."});
            }
            catch (Exception e)
            {
                return RedirectToRoute(new {controller = "Helper", action = "Index"});
            }
        }

        [HttpGet]
        public ActionResult TourConstruct()
        {
            try
            {
                CountriesViewModel model = new CountriesViewModel()
                {
                    Countries = CountriesDBHelper.GetCountries(),
                    Transports = TransportsDBHelper.GetTransports(),
                    DestinationCountries = DestinationCountriesDBHelper.GetCountries(),
                };

                return View(model);
            }
            catch (Exception e)
            {
                return RedirectToRoute(new {controller = "Helper", action = "Index"});
            }
        }

        [HttpPost]
        public ActionResult TourConstruct(CountriesViewModel model)
        {
            try
            { 
                return View(model);
            }
            catch (Exception e)
            {
                return RedirectToRoute(new {controller = "Helper", action = "Index"});
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
        public JsonResult GetRoutes(long leavePointId)
        {
            var temp = RoutesDBHelper.GetRoutesForLeavePoint(leavePointId);

            foreach (var item in temp)
            {
                item.Name = $"{item.Name} ({item.Start.ToShortDateString()}, {item.Start.ToShortTimeString()} - {item.End.ToShortDateString()}, {item.End.ToShortTimeString()})";
            }

            return Json(temp, JsonRequestBehavior.AllowGet);
        }
    }
}