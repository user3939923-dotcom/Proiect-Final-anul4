using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoPro.Models
{
    public class RaportMasa
    {
        public int NumarMasa { get; set; }
        public string Zona { get; set; }
        public int NrComenzi { get; set; }
        public decimal TotalAchitat { get; set; }
    }
}