using AVACOM_Online_Testiranje.Areas.User.Models;
using AVACOM_Online_Testiranje.DAL;
using AVACOM_Online_Testiranje.Helper;
using AVACOM_Online_Testiranje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AVACOM_Online_Testiranje.Areas.User.Controllers
{
    public class TestController : Controller
    {
        MojContext db = new MojContext();
        private static List<TestOdgovor> aktivniTest { get; set; }
        private static List<KorisnikOdgovor> KorisnikOdgovori { get; set; }
        private static List<PitanjeOdgovorVM.PitanjeInfo> svaPitanja { get; set; }
        private static Test trenutniTest { get; set; }

        // GET: /User/Test/
        public ActionResult Index()
        {
            if (TempData["error"] != null)
            {
                ModelState.AddModelError("", "Niste odabrali niti jednu oblast!");
            }

            MojContext db = new MojContext();

            List<Oblast> model = db.Oblasti.ToList();

            return View(model);
        }

        public ActionResult Test(string[] odabir)
        {
            if (odabir == null)
            {
                TempData["error"] = true;
                return RedirectToAction("Index");
            }

            int brojOblasti = odabir.Length;
            int brojPitanjaPoOblasti = 10 / brojOblasti;
            int ostalo = 10 - brojPitanjaPoOblasti * brojOblasti;

            int[] pitanjaIzOb = new int[brojOblasti];

            for (int i = 0; i < brojOblasti; i++)
            {
                pitanjaIzOb[i] = brojPitanjaPoOblasti;
                if (i <= ostalo - 1)
                {
                    pitanjaIzOb[i] += 1;
                }
            }

            trenutniTest = new Test();
            trenutniTest.IsDeleted = false;
            trenutniTest.KorisnikId = LogiraniKorisnik.Id;
            trenutniTest.Rezultat = 0;
            trenutniTest.VrijemePocetka = DateTime.Now;
            trenutniTest.VrijemeZavrsetka = DateTime.Now;

            List<TestOblast> tob = new List<TestOblast>();

            int[] listaOblasti = new int[brojOblasti];

            for (int i = 0; i < brojOblasti; i++)
            {
                listaOblasti[i] = int.Parse(odabir[i]);
                TestOblast to = new TestOblast
                {
                    TestId = trenutniTest.Id,
                    OblastId = int.Parse(odabir[i])
                };
                tob.Add(to);
            }

            db.TestOblast.AddRange(tob);
            db.Testovi.Add(trenutniTest);
            db.SaveChanges();

            List<PitanjeOdgovorVM.PitanjeInfo>[] listaPitanjaPoOblastima = new List<PitanjeOdgovorVM.PitanjeInfo>[brojOblasti];
            svaPitanja = new List<PitanjeOdgovorVM.PitanjeInfo>();

            int trenutnaOblast;
            for (int i = 0; i < brojOblasti; i++)
            {
                trenutnaOblast = listaOblasti[i];

                listaPitanjaPoOblastima[i] = db.Pitanja
                    .Where(b => b.OblastId == trenutnaOblast)
                    .OrderBy(a => Guid.NewGuid()).Take(pitanjaIzOb[i])
                    .Select(c => new PitanjeOdgovorVM.PitanjeInfo
                    {
                        Id = c.Id,
                        Pitanje = c.Tekst,
                        Bod = c.Bod,
                        Vrsta = c.VrstaPitanja.Naziv,
                        PitanjeOrder = 1,
                        Odgovori = db.Odgovori
                        .Where(d => d.PitanjeId == c.Id)
                        .Select(e => new PitanjeOdgovorVM.OdgovorInfo
                        {
                            Id = e.Id,
                            Odgovor = e.Tekst,
                            IsCorrect = e.Tacan
                        }).ToList()
                    }).ToList();
                svaPitanja.AddRange(listaPitanjaPoOblastima[i]);
            }

            List<PitanjeOdgovorVM.PitanjeInfo> randomPitanja = ShuffleList<PitanjeOdgovorVM.PitanjeInfo>(svaPitanja);
            svaPitanja = randomPitanja;

            PitanjeOdgovorVM.PitanjeInfo model = svaPitanja[0];

            aktivniTest = new List<TestOdgovor>();
            KorisnikOdgovori = new List<KorisnikOdgovor>();

            return View(model);
        }

        private List<E> ShuffleList<E>(List<E> inputList)
        {
            List<E> randomList = new List<E>();

            Random r = new Random();
            int randomIndex = 0;
            while (inputList.Count > 0)
            {
                randomIndex = r.Next(0, inputList.Count); //Choose a random object in the list
                randomList.Add(inputList[randomIndex]); //add it to the new, random list
                inputList.RemoveAt(randomIndex); //remove to avoid duplicates
            }

            return randomList; //return the new random list
        }

        public ActionResult Next(int next, int[] oznaceniOdgovori)
        {
            PitanjeOdgovorVM.PitanjeInfo model;
            if (oznaceniOdgovori != null)
            {
                model = svaPitanja[next];
                model.PitanjeOrder = next + 1;

                //if(next <= aktivniTest.Count)
                //{
                //    int trazi = svaPitanja[next-1].Id;
                //    TestOdgovor nadji = db.TestOdgovori.SingleOrDefault(a => a.PitanjeId == trazi && a.TestId == trenutniTest.Id);
                //    //TestOdgovor brisi = db.TestOdgovori.Find(nadji);
                //    aktivniTest.Remove(nadji);
                //    db.TestOdgovori.Remove(nadji);
                //}

                TestOdgovor pit = new TestOdgovor
                {
                    TestId = trenutniTest.Id,
                    PitanjeId = svaPitanja[next - 1].Id,
                    OdgovorTacan = false
                };

                aktivniTest.Add(pit);
                db.TestOdgovori.Add(pit);
                db.SaveChanges();

                if (oznaceniOdgovori != null)
                {
                    foreach (int item in oznaceniOdgovori)
                    {
                        KorisnikOdgovor ko = new KorisnikOdgovor
                        {
                            OdgovorId = item,
                            TestOdgovorId = pit.Id
                        };
                        KorisnikOdgovori.Add(ko);
                    }

                    db.KorisnikOdgovori.AddRange(KorisnikOdgovori);
                    db.SaveChanges();
                }
            }
            else
            {
                ModelState.AddModelError("", "Odabrati barem jedan odgovor!");
                model = svaPitanja[next-1];
                model.PitanjeOrder = next;
            }

            return View("Test", model);
        }

        public ActionResult Back(int back)
        {
            PitanjeOdgovorVM.PitanjeInfo model = svaPitanja[back - 2];
            model.PitanjeOrder = back - 1;

            return View("Test", model);
        }

        public ActionResult Rezultat(int pitanje, int[] oznaceniOdgovori)
        {
            PitanjeOdgovorVM.PitanjeInfo kraj = svaPitanja[pitanje - 1];

            if (oznaceniOdgovori != null)
            {
                Test t = db.Testovi.SingleOrDefault(c => c.Id == trenutniTest.Id);
                t.VrijemeZavrsetka = DateTime.Now;

                TestOdgovor pit = new TestOdgovor
                {
                    TestId = trenutniTest.Id,
                    PitanjeId = kraj.Id,
                    OdgovorTacan = false
                };


                db.TestOdgovori.Add(pit);
                db.SaveChanges();

                if (oznaceniOdgovori != null)
                {
                    foreach (int item in oznaceniOdgovori)
                    {
                        KorisnikOdgovor ko = new KorisnikOdgovor
                        {
                            OdgovorId = item,
                            TestOdgovorId = pit.Id
                        };
                        KorisnikOdgovori.Add(ko);
                    }
                }
                aktivniTest = db.TestOdgovori.Where(v => v.TestId == trenutniTest.Id).ToList();

                db.KorisnikOdgovori.AddRange(KorisnikOdgovori);
                db.SaveChanges();

                int brojOdg = 0;
                int brojPog = 0;

                for (int i = 0; i < svaPitanja.Count; i++)
                {
                    for (int j = 0; j < svaPitanja[i].Odgovori.Count; j++)
                    {
                        for (int k = 0; k < KorisnikOdgovori.Count; k++)
                        {
                            if (svaPitanja[i].Vrsta == "MC-MA")
                            {
                                if (brojOdg == 0)
                                {
                                    int l = svaPitanja[i].Id;
                                    brojOdg = db.Odgovori.Where(m => m.PitanjeId == l && m.Tacan == true).Count();
                                }

                                if(svaPitanja[i].Odgovori[j].Id == KorisnikOdgovori[k].OdgovorId && svaPitanja[i].Odgovori[j].IsCorrect)
                                {
                                    brojPog++;
                                }

                                if (brojOdg == brojPog)
                                    aktivniTest[i].OdgovorTacan = true;
                                 
                            }
                            else if (svaPitanja[i].Odgovori[j].Id == KorisnikOdgovori[k].OdgovorId && svaPitanja[i].Odgovori[j].IsCorrect)
                            {
                                aktivniTest[i].OdgovorTacan = true;
                            }
                        }

                    }
                                            brojOdg = 0;
                                            brojPog = 0;
                }

                db.SaveChanges();
                float ukupnoBodova = db.TestOdgovori.Where(r => r.TestId == trenutniTest.Id).Sum(z => z.Pitanje.Bod);
                float? osvojenoBodova = db.TestOdgovori.Where(c => c.OdgovorTacan == true && c.TestId == trenutniTest.Id).Sum(d => (int?)d.Pitanje.Bod);
                float bodovi = 0;
                if (osvojenoBodova != null)
                    bodovi = (float)osvojenoBodova;
                float model = (bodovi / ukupnoBodova) * 100;

                t.Rezultat = model;
                db.SaveChanges();

                return View("Rezultat", model);
            }
            else
            {
                ModelState.AddModelError("", "Odabrati barem jedan odgovor!");
                return View("Test", kraj);
            }
        }
    }
}