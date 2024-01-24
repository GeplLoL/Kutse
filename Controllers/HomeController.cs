using kutse.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace kutse.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            int hour = DateTime.Now.Hour;
            //ViewBag.Greeting = hour < 10 ? "Tere hommikust" : "Tere päevast";//hour<10? = if(hour < 10) / : = else
            if(hour < 10)
            {
                ViewBag.Greeting = "Tere hommikust";
            }
            else if(hour > 10)
            {
                ViewBag.Greeting = "Tere päevast";
            }
            else if(hour > 17)
            {
                ViewBag.Greeting = "Tere õhtust";
            }
            else if(hour > 21 && hour < 4)
            {
                ViewBag.Greeting = "Head ööd";
            }
            return View();
        }

        public ActionResult kutse()
        {
            int hour = DateTime.Now.Hour;
            //ViewBag.Greeting = hour<10? "Tere hommikust" : "Tere päevast";//hour<10? = if(hour < 10) / : = else
            if (hour < 10)
            {
                ViewBag.Greeting = "Tere hommikust";
            }
            else if (hour > 10)
            {
                ViewBag.Greeting = "Tere päevast";
            }
            else if (hour > 17)
            {
                ViewBag.Greeting = "Tere õhtust";
            }
            else if (hour > 21 && hour < 4)
            {
                ViewBag.Greeting = "Head ööd";
            }

            int month = DateTime.Now.Month;
            switch (month)
            {
                case 1: ViewBag.Message += "Uus aasta! 1 Jaanuar"; break;
                case 2: ViewBag.Message += "Iseseisvuspäev! 24 Veebruar"; break;
                case 3: ViewBag.Message += "Naistepäev! 8 Märts"; break;
                case 4: ViewBag.Message += "Naljapäev! 1 Aprill"; break;
                case 5: ViewBag.Message += "Kevadpüha! 1 Mai"; break;
                case 6: ViewBag.Message += "Jaanipäev! 24 Juuni"; break;
                case 7: ViewBag.Message += "Rahvusvaheline malepäev! 4 Juuli"; break;
                case 8: ViewBag.Message += "Taasiseseisvumispäev! 20 August"; break;
                case 9: ViewBag.Message += "Teadmistepäev! 1 September"; break;
                case 10: ViewBag.Message += "Omavalitsuspäev! 1 Oktoober"; break;
                case 11: ViewBag.Message += "Isadepäev! 10 November"; break;
                case 12: ViewBag.Message += "Jõulud! 24-26 Detsembrid"; break;
            }
            ViewBag.Month = "month" + month + ".jpg";

            return View();
        }

        [HttpGet]
        public ActionResult ankeet()
        {
            return View();
        }

        [HttpPost]
        public ViewResult ankeet(Guest guest)
        {
            E_mail(guest);
            if (ModelState.IsValid)
            {
                return View("Thanks",guest);
            }
            else
            {
                return View();
            }
        }

        public void E_mail(Guest guest)
        {
            try
            {
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.SmtpPort = 587;
                WebMail.EnableSsl = true;
                WebMail.UserName = "nepridumalnazvaniepocht@gmail.com";
                WebMail.Password = "rnlt mfvn ftjb usxu";
                WebMail.From = "nepridumalnazvaniepocht@gmail.com";
                WebMail.Send(guest.Email, "Vastus kutsele", guest.Name + " vastas " + ((guest.WillAttend ?? false) ? "tuleb peole " : "ei tule peole"));
                ViewBag.Message = "Kiri on saatnud!";
            }
            catch
            {
                ViewBag.Message = "Mul on kahju! Ei saa kirja saada!!!";
            }


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