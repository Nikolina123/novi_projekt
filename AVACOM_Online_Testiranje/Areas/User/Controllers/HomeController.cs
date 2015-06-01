using AVACOM_Online_Testiranje.DAL;
using AVACOM_Online_Testiranje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using AVACOM_Online_Testiranje.Areas.User.Models;
using AVACOM_Online_Testiranje.Helper;

namespace AVACOM_Online_Testiranje.Areas.User.Controllers
{
    public class HomeController : Controller
    {
        MojContext db = new MojContext();


        //
        // GET: /User/User/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Testovi()
        {
            MojContext db = new MojContext();

            List<TestStatistikaVM.TestoviInfo> testovi = db.Testovi
                .Where(c => c.KorisnikId == LogiraniKorisnik.Id)
                .Select(a => new TestStatistikaVM.TestoviInfo 
                {
                    Id = a.Id,
                    VrijemePocetka = a.VrijemePocetka,
                    VrijemeZavrsetka = a.VrijemeZavrsetka,
                    Rezultat = a.Rezultat,
                    BrojTacnihOdgovora = db.TestOdgovori
                    .Where(d => d.TestId == a.Id && d.OdgovorTacan == true).Count().ToString(),
                    Oblasti = db.TestOblast.Where(b => b.TestId == a.Id)
                    .Select(c => new TestStatistikaVM.OblastInfo
                    {
                        Id = c.Oblast.Id,
                        Naziv = c.Oblast.Naziv
                    }).ToList()
                }).ToList();

            TestStatistikaVM model = new TestStatistikaVM
            {
                Testovi = testovi
            };

            return View(model);
        }

	}
}