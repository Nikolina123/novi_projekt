using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AVACOM_Online_Testiranje.Areas.User.Models
{
    public class PitanjaDetaljiVM
    {
        public class OdgovorInfo
        {
            public int Id { get; set; }
            public string Odgovor { get; set; }
            public bool IsCorrect { get; set; }
        }

        public class PitanjeOdgovorInfo
        {
            public int Id { get; set; }
            public string Pitanje { get; set; }
            public List<OdgovorInfo> Odgovori { get; set; }
        }

        public List<PitanjeOdgovorInfo> PitanjaOdgovori { get; set; }
    }
}