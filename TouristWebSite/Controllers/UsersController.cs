using DAL.DBHelpers;
using Microsoft.AspNet.Identity;
using System;
using System.Web.Mvc;
using TouristWebSite.Models;

namespace TouristWebSite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        public ActionResult Index(string message = "")
        {
            ViewBag.StatusMessage = message;

            AllUsersViewModel model = new AllUsersViewModel()
            {
                Users = UsersDBHelper.GetAll()
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult BlockingStatus(string userId)
        {
            try
            {
                UsersDBHelper.ChangeBlockStatus(userId);
                return RedirectToAction("Index", new { Message = "Зміни були успішно внесені" });
            }
            catch (Exception e)
            {
                return RedirectToRoute(new { controller = "Users", action = "Index" });
            }
        }
    }
}