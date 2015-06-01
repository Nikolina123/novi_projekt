using AVACOM_Online_Testiranje.DAL;
using AVACOM_Online_Testiranje.Helper;
using AVACOM_Online_Testiranje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AVACOM_Online_Testiranje.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Provjera (string usr , string pass)
        {
            MojContext db = new MojContext();

            Korisnik k = db.Korisnici.SingleOrDefault(a => a.KorisnickoIme == usr && a.Lozinka == pass);


            if(k == null)
            {
                ModelState.AddModelError("", "Pogresni pristupni podaci");
                return View("Index");
            }

            if (k.Admin)
                return RedirectToAction("Index", "Home", new { Area = "Admin" });
            else
            {
                LogiraniKorisnik.Id = k.Id;
                LogiraniKorisnik.Ime = k.Ime;
                LogiraniKorisnik.Prezime = k.Prezime;

                return RedirectToAction("Index", "Home", new { Area = "User" });
            }
        }


	}
}