using DAL.DBHelpers;
using System.Web.Mvc;
using TouristWebSite.Models;

namespace TouristWebSite.Controllers
{
    public class NewsController : Controller
    {
        public ActionResult Index()
        {
            var model = new ActiveNewsViewModel() {ActiveNews = NewsDBHelper.GetActive()};

            return View(model);
        }
    }
}