using AVACOM_Online_Testiranje.DAL;
using AVACOM_Online_Testiranje.Models;
using AVACOM_Online_Testiranje.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AVACOM_Online_Testiranje.Controllers
{
    public class RegistracijaController : Controller
    {
        //
        // GET: /Registracija/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Snimi(RegistracijaVM r)
        {
            if (!ModelState.IsValid)
                return View("Index");

            MojContext db = new MojContext();

            Korisnik postoji = db.Korisnici.FirstOrDefault(x => x.KorisnickoIme == r.KorisnickoIme);

            if(postoji != null)
            {
                ModelState.AddModelError("", "Korisnicko ime je zauzeto");
                return View("Index");
            }

            Korisnik k = new Korisnik
            {
                Ime = r.Ime,
                Prezime = r.Prezime,
                Email = r.Email,
                Admin = false,
                Aktivan = true,
                KorisnickoIme = r.KorisnickoIme,
                Lozinka = r.Lozinka
            };

            db.Korisnici.Add(k);
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
	}
}