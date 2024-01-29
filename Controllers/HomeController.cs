using kutse.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                case 1: ViewBag.Message += "Head uus aasta! 1 Jaanuar"; break;
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
                db.Guests.Add(guest);
                db.SaveChanges();
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
        
        GuestContext db = new GuestContext();
        [Authorize]

        public ActionResult Guests()
        {
            IEnumerable<Guest> guests = db.Guests;
            return View(guests);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Guest guest)
        {
            db.Guests.Add(guest);
            db.SaveChanges();
            return RedirectToAction("Guests");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Guest g = db.Guests.Find(id);
            if(g == null)
            {
                return HttpNotFound();
            }
            return View(g);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Guest g = db.Guests.Find(id);
            if(g==null)
            {
                return HttpNotFound();
            }
            db.Guests.Remove(g);
            db.SaveChanges();
            return RedirectToAction("Guests");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            Guest g = db.Guests.Find(id);
            if (g == null)
            {
                return HttpNotFound();
            }
            return View(g);
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult EditConfirmed(Guest guest)
        {
            db.Entry(guest).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Guests");
        }

        [HttpGet]
        public ActionResult Accept()
        {
            IEnumerable<Guest> guests = db.Guests.Where(g => g.WillAttend == true);
            return View(guests);
        }



        HolidayContext HDdb = new HolidayContext();
        [Authorize]

        public ActionResult Holidays()
        {
            IEnumerable<Holiday> holidays = HDdb.Holidays;
            return View(holidays);

        }

        [HttpGet]
        public ActionResult HDCreate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult HDCreate(Holiday holiday)
        {
            HDdb.Holidays.Add(holiday);
            HDdb.SaveChanges();
            return RedirectToAction("Holidays");
        }

        [HttpGet]
        public ActionResult HDDelete(int id)
        {
            Holiday h = HDdb.Holidays.Find(id);
            if (h == null)
            {
                return HttpNotFound();
            }
            return View(h);
        }

        [HttpPost, ActionName("HDDelete")]
        public ActionResult HDDeleteConfirmed(int id)
        {
            Holiday h = HDdb.Holidays.Find(id);
            if (h == null)
            {
                return HttpNotFound();
            }
            HDdb.Holidays.Remove(h);
            HDdb.SaveChanges();
            return RedirectToAction("Holidays");
        }

        [HttpGet]
        public ActionResult HDEdit(int? id)
        {
            Holiday h = HDdb.Holidays.Find(id);
            if (h == null)
            {
                return HttpNotFound();
            }
            return View(h);
        }

        [HttpPost, ActionName("HDEdit")]
        public ActionResult HDEditConfirmed(Holiday holiday)
        {
            HDdb.Entry(holiday).State = EntityState.Modified;
            HDdb.SaveChanges();
            return RedirectToAction("Holidays");
        }
    }
}