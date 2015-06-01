using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AVACOM_Online_Testiranje.Models
{
    public class TestOblast
    {
        public int TestId { get; set; }
        public Test Test { get; set; }
        public int OblastId { get; set; }
        public Oblast Oblast { get; set; }
    }
}