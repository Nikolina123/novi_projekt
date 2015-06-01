using AVACOM_Online_Testiranje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AVACOM_Online_Testiranje.Areas.User.Models
{
    public class TestStatistikaVM
    {
        public class OblastInfo
        {
            public int Id { get; set; }
            public string Naziv { get; set; }
        }
        public class TestoviInfo
        {
            public int Id { get; set; }
            public DateTime VrijemePocetka { get; set; }
            public DateTime VrijemeZavrsetka { get; set; }
            public List<OblastInfo> Oblasti { get; set; }
            public string BrojTacnihOdgovora { get; set; }
            public float Rezultat { get; set; }
        }

        public List<TestoviInfo> Testovi { get; set; }
    }
}