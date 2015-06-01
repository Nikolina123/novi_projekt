using AVACOM_Online_Testiranje.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AVACOM_Online_Testiranje.Models
{
    public class Korisnik : IEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }

        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Email { get; set; }
        public bool Admin { get; set; }
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public bool Aktivan { get; set; }
    }
}