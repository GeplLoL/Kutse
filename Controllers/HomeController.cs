using kutse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
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