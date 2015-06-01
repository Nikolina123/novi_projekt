using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AVACOM_Online_Testiranje.Models
{
    public class KorisnikOdgovor
    {
        public int Id { get; set; }
        public int OdgovorId { get; set; }
        public int TestOdgovorId { get; set; }
        public TestOdgovor TestOdgovor { get; set; }
    }
}