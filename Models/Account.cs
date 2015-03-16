using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace webshop.Models
{
    public class Account
    {
        public int ID {get; set;}

        [Required(ErrorMessage = "Dit Veld is verplicht!")]
        public String voornaam { get; set; }

        [Required(ErrorMessage = "Dit veld is verplicht")]
        public String achternaam { get; set; }

        public String straat { get; set; }

        public String plaats { get; set; }

        public String postcode { get; set; }

        [Required(ErrorMessage = "Dit Veld is verplicht!")]
        public String wachtwoord { get; set; }

        [Required(ErrorMessage = "Dit Veld is verplicht!")]
        public String email { get; set; }

        public String geboorteDatum { get; set; }

        public String telnummer { get; set; }

        public int nieuwsbrief { get; set; }

        public int privilege { get; set; }
    }
}