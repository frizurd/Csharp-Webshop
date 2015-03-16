using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;

using webshop.Models;
using webshop.DatabaseControllers;

namespace webshop.Controllers
{
    public class KledingController : Controller
    {
        webshop.DatabaseControllers.KledingController KC = new DatabaseControllers.KledingController();

        public ActionResult Index()
        {
            List<Artikel> artikel = new List<Artikel>();
            artikel = KC.GetArtikel();
            return View(artikel);

        }

        public ActionResult Artikel(int ID)
        {
            Models.Artikel ar = new Models.Artikel();
            ar = KC.getArtikel(ID);
            ar.maten = KC.GetMaten(ID);
            return View(ar);
        }

        public ActionResult Categorie(int ID)
        {
            List<Artikel> artikel = new List<Artikel>();
            artikel = KC.GetCategorieArtikelen(ID);
            return View(artikel);
        }

        public ActionResult Mannen()
        {
            List<Artikel> artikel = new List<Artikel>();
            artikel = KC.GetMannenArtikelen();
            return View(artikel);

        }

        public ActionResult Vrouwen()
        {
            List<Artikel> artikel = new List<Artikel>();
            artikel = KC.GetVrouwenArtikelen();
            return View(artikel);

        }

    }
}