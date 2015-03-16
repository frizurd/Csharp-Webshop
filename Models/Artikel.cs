using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace webshop.Models
{
    public class Artikel
    {
        public int ID { get; set; }

        public String naam { get; set; }

        public String omschrijving { get; set; }

        public double prijs { get; set; }

        public int sport { get; set; }

        public String sportnaam { get; set; }

        public int categorie { get; set; }

        public String categorieNaam { get; set; }

        public List<Maat> maten { get; set; }

        public String grootte { get; set; }

        public int aanbieding { get; set; }

        public int wAanbieding { get; set; }

        public int voorPagina { get; set; }

        public int toevoegDatum { get; set; }

        public String image { get; set; }

        public String merkImage { get; set; }

        public int merkID { get; set; }

        public String merkNaam { get; set; }

        public string geslacht { get; set; }
    }
}
