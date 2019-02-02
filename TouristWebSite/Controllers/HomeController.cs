using System.Web;
using System.Web.Mvc;

namespace TouristWebSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contacts()
        {
            return View();
        }

        public ActionResult Create(TouristWebSite.Models.Image img, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    file.SaveAs(HttpContext.Server.MapPath("~/Content/Img/")
                                + file.FileName);
                    img.ImagePath = file.FileName;
                }
                return RedirectToAction("Index"); 
            }
            return View(img);
        }
    }
}