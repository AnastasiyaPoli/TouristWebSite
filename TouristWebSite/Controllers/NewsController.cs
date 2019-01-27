using DAL.DBHelpers;
using System.Web.Mvc;
using TouristWebSite.Models;

namespace TouristWebSite.Controllers
{
    public class NewsController : Controller
    {
        public ActionResult Index()
        {
            var model = new ActiveNewsViewModel()
            {
                ActiveNews = NewsDBHelper.GetActive(),
                ActiveDiscounts = DiscountsDBHelper.GetActive()
            };

            return View(model);
        }

        public ActionResult DeleteNew(int itemId)
        {
            NewsDBHelper.Deactivate(itemId);
            return RedirectToRoute(new { controller = "News", action = "Index" });
        }
    }
}