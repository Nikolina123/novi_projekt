using AVACOM_Online_Testiranje.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AVACOM_Online_Testiranje.Models
{
    public class Test:IEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public int KorisnikId { get;set; }
        public Korisnik Korisnik { get; set; }
        public DateTime VrijemePocetka { get; set; }
        public DateTime VrijemeZavrsetka { get; set; }
        public float Rezultat { get; set; }


    }
}