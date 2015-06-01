using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AVACOM_Online_Testiranje.Models
{
    public class TestOdgovor
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public Test Test { get; set; }
        public int PitanjeId { get; set; }
        public Pitanje Pitanje { get; set; }
        public bool OdgovorTacan { get; set; }
    }
}