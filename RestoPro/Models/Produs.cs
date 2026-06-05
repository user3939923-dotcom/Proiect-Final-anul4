using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestoPro.Models
{
    public class Produs
    {
        public int IdProdus { get; set; }
        public string Denumire { get; set; }
        public string Categorie { get; set; }
        public decimal Pret { get; set; }
        public bool Disponibil { get; set; }
    }
}