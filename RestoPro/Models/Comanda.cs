using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoPro.Models
{
    public class Comanda
    {
        public int IdComanda { get; set; }
        public int IdMasa { get; set; }
        public int IdProdus { get; set; }
        public DateTime DataComanda { get; set; }
        public int Cantitate { get; set; }
        public string StatusPlata { get; set; }

        // Proprietăți de afișare (JOIN)
        public string NumarMasa { get; set; }
        public string DenumireProdus { get; set; }
        public decimal PretProdus { get; set; }
    }
}
