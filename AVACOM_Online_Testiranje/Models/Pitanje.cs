using AVACOM_Online_Testiranje.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AVACOM_Online_Testiranje.Models
{
    public class Pitanje : IEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }

        public string Tekst { get; set; }
        public int Bod { get; set; }

        public int VrstaPitanjaId { get; set; }
        public VrstaPitanja VrstaPitanja { get; set; }

        public int OblastId { get; set; }
        public Oblast Oblast { get; set; }
    }
}