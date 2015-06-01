using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AVACOM_Online_Testiranje.Areas.User.Models
{
    public class PitanjeOdgovorVM
    {
        public class OdgovorInfo
        {
            public int Id { get; set; }
            public string Odgovor { get; set; }
            public bool IsCorrect { get; set; }
            public bool IsSelected { get; set; }
        }

        public class PitanjeInfo
        {
            public int Id { get; set; }
            public string Pitanje { get; set; }
            public int Bod { get; set; }
            public string Vrsta { get; set; }
            public List<OdgovorInfo> Odgovori { get; set; }
            public int PitanjeOrder { get; set; }
        }

        public List<PitanjeInfo> Pitanja { get; set; }
    }
}