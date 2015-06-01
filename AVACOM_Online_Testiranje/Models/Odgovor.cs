using AVACOM_Online_Testiranje.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AVACOM_Online_Testiranje.Models
{
    public class Odgovor:IEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }

        public string Tekst { get; set; }
        public bool Tacan { get; set; }

        public int PitanjeId { get; set; }
        public Pitanje Pitanje { get; set; }
    }
}