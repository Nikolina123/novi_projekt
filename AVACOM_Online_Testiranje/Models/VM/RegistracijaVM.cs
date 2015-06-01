using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AVACOM_Online_Testiranje.Models.VM
{
    public class RegistracijaVM
    {
        [Required]
        public string Ime { get; set; }
        [Required]
        public string Prezime { get; set; }
        [Required]
        public string Email { get; set; }
        public bool Admin { get; set; }
        [Required]
        [Index("Index", 1, IsUnique = true)]
        public string KorisnickoIme { get; set; }
        [Required]
        public string Lozinka { get; set; }
        public bool Aktivan { get; set; }
    }
}