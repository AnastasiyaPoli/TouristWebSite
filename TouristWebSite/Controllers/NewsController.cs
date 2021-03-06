﻿using DAL.DBHelpers;
using System;
using System.Linq;
using System.Web.Mvc;
using TouristWebSite.Helpers;
using TouristWebSite.Models;

namespace TouristWebSite.Controllers
{
    [Authorize(Roles = "Admin")]
    public class NewsController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index(string message = "", string message1 = "")
        {
            var model = new ActiveNewsViewModel()
            {
                ActiveNews = NewsDBHelper.GetActive().OrderByDescending(x => x.Date).ToList(),
                ActiveDiscounts = DiscountsDBHelper.GetActive().OrderByDescending(x => x.StartDate).ToList()
            };

            ViewBag.StatusMessage = message;
            ViewBag.StatusMessage1 = message1;

            return View(model);
        }

        [HttpGet]
        public ActionResult DeleteNew(long itemId)
        {
            try
            {
                NewsDBHelper.Deactivate(itemId);
                return RedirectToRoute(new { controller = "News", action = "Index", message = "Новину було успішно видалено." });
            }
            catch (Exception)
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

                foreach (var user in UsersDBHelper.GetAllActive())
                {
                    if (user.IsSubscribed)
                    {
                        try
                        {
                            string text = model.Name + ": " + model.Content + " ";
                            if (model.Link != null && model.Link != string.Empty)
                            {
                                text += "Детальніше за посиланням: " + model.Link;
                            }

                            EmailSenderHelper.SendEmail(user.Email, "Турфірма \"Формула відпочинку\" має нову новину!", text);
                        }
                        catch (Exception)
                        {

                        }
                    }
                }

                return RedirectToRoute(new { controller = "News", action = "Index", message = "Новину було успішно додано." });
            }
            catch (Exception)
            {
                return RedirectToRoute(new { controller = "News", action = "Index" });
            }
        }

        [HttpGet]
        public ActionResult EditNew(long itemId)
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
            catch (Exception)
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
                    return View(model);
                }

                NewsDBHelper.Edit(model);
                return RedirectToRoute(new { controller = "News", action = "Index", message = "Зміни було успішно збережено." });
            }
            catch (Exception)
            {
                return RedirectToRoute(new { controller = "News", action = "Index" });
            }
        }

        [HttpGet]
        public ActionResult AddDiscount()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddDiscount(DiscountViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                if (model.EndDate <= DateTime.Now)
                {
                    ModelState.AddModelError("EndDate", "Необхідно вказати коректну дату кінця акції.");
                    return View(model);
                }

                DiscountsDBHelper.Add(model);

                foreach (var user in UsersDBHelper.GetAllActive())
                {
                    if (user.IsSubscribed)
                    {
                        try
                        {
                            string text = model.Name + ": " + model.Content + " Акція дійсна до " + model.EndDate + ". ";
                            if (model.Link != null && model.Link != string.Empty)
                            {
                                text += "Детальніше за посиланням: " + model.Link;
                            }

                            EmailSenderHelper.SendEmail(user.Email, "Турфірма \"Формула відпочинку\" має нову акцію!", text);
                        }
                        catch (Exception)
                        {

                        }
                    }
                }

                return RedirectToRoute(new { controller = "News", action = "Index", message1 = "Акцію було успішно додано." });
            }
            catch (Exception)
            {
                return RedirectToRoute(new { controller = "News", action = "Index" });
            }
        }

        [HttpGet]
        public ActionResult EditDiscount(long itemId)
        {
            try
            {
                var discountForEdit = DiscountsDBHelper.GetById(itemId);

                DiscountViewModel model = new DiscountViewModel()
                {
                    Id = discountForEdit.Id,
                    Name = discountForEdit.Name,
                    Content = discountForEdit.Content,
                    EndDate = discountForEdit.EndDate,
                    Link = discountForEdit.Link
                };

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToRoute(new { controller = "News", action = "Index" });
            }
        }

        [HttpPost]
        public ActionResult EditDiscount(DiscountViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.StatusMessage = string.Empty;
                    return View(model);
                }

                if (model.EndDate <= DateTime.Now)
                {
                    ModelState.AddModelError("EndDate", "Необхідно вказати коректну дату кінця акції.");
                    return View(model);
                }

                DiscountsDBHelper.Edit(model);
                return RedirectToRoute(new { controller = "News", action = "Index", message1 = "Зміни було успішно збережено." });
            }
            catch (Exception)
            {
                return RedirectToRoute(new { controller = "News", action = "Index" });
            }
        }
    }
}