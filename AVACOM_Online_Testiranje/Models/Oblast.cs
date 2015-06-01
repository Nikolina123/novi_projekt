using AVACOM_Online_Testiranje.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AVACOM_Online_Testiranje.Models
{
    public class Oblast:IEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }

        public string Naziv { get; set; }

        
    }
}