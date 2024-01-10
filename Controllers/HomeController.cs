using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kutse.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult kutse()
        {
            int hour = DateTime.Now.Hour;
            ViewBag.Greeting = hour<10? "Tere hommikust" : "Tere päevast";//hour<10? = if(hour < 10) / : = else
            ViewBag.Message = "Ootan sind oma peole. Tule kindlasti! Ootan sind!";
            return View();
        }

        [HttpGet]
        public ActionResult ankeet()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Teie rakenduse kirjelduse leht.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Teie kontaktileht.";

            return View();
        }
    }
}