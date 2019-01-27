using DAL.DBHelpers;
using System;
using System.Web.Mvc;
using TouristWebSite.Models;

namespace TouristWebSite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class NewsController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            try
            {
                var model = new ActiveNewsViewModel()
                {
                    ActiveNews = NewsDBHelper.GetActive(),
                    ActiveDiscounts = DiscountsDBHelper.GetActive()
                };

                return View(model);
            }
            catch (Exception e)
            {
                return RedirectToRoute(new { controller = "News", action = "Index" });
            }
        }

        [HttpGet]
        public ActionResult DeleteNew(int itemId)
        {
            try
            {
                NewsDBHelper.Deactivate(itemId);
                return RedirectToRoute(new { controller = "News", action = "Index" });
            }
            catch (Exception e)
            {
                return RedirectToRoute(new { controller = "News", action = "Index" });
            }
        }

        [HttpGet]
        public ActionResult AddNew()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNew(NewViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                NewsDBHelper.Add(model);
                return RedirectToRoute(new { controller = "News", action = "Index" });
            }
            catch (Exception e)
            {
                return RedirectToRoute(new { controller = "News", action = "Index" });
            }
        }

        [HttpGet]
        public ActionResult EditNew(int itemId)
        {
            try
            {
                var newForEdit = NewsDBHelper.GetById(itemId);

                NewViewModel model = new NewViewModel()
                {
                    Id = newForEdit.Id,
                    Name = newForEdit.Name,
                    Content = newForEdit.Content,
                    Link = newForEdit.Link
                };

                return View(model);
            }
            catch (Exception e)
            {
                return RedirectToRoute(new { controller = "News", action = "Index" });
            }
        }

        [HttpPost]
        public ActionResult EditNew(NewViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.StatusMessage = string.Empty;
                    return View(model);
                }

                NewsDBHelper.Edit(model);
                return RedirectToRoute(new { controller = "News", action = "Index" });
            }
            catch (Exception e)
            {
                return RedirectToRoute(new { controller = "News", action = "Index" });
            }
        }
    }
}