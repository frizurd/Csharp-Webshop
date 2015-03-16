using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;

using webshop.DatabaseControllers;
using webshop.Models;

namespace webshop.Controllers
{
    public class BeheerderController : Controller
    {
        webshop.DatabaseControllers.BeheerderController BC = new DatabaseControllers.BeheerderController();
        static int idmeegeven = 0;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Artikelen()
        {
            List<Artikel> artikelen = new List<Artikel>();
            artikelen = BC.GetArtikelen();

            return View(artikelen);
        }

        public ActionResult ArtikelToevoegen()
        {
            return View();
        }

        public ActionResult ArtikelToegevoegd(Artikel artikel)
        {
            if (ModelState.IsValid)
            {
                BC.insertArtikel(artikel);
                int ID = BC.getLaatsteID();
                BC.insertMaten(artikel, ID);
                return View(artikel);
            }
            else
            {
                return View("ArtikelToevoegen", artikel);
            }
        }

        public ActionResult ArtikelGewijzigd(Artikel artikel)
        {
            if (ModelState.IsValid)
            {
                artikel.ID = idmeegeven;
                BC.wijzigArtikel(artikel);
                return View(artikel);
            }
            else
            {
                return View("ArtikelWijzigen", artikel);
            }
        }

        public ActionResult ArtikelWijzigen(int ID)
        {
            Artikel artikel = new Artikel();
            artikel = BC.getArtikel(ID);
            idmeegeven = ID;
            return View(artikel);
        }

        public ActionResult ArtikelVerwijderen(int ID)
        {
            BC.verwijderArtikel(ID);
            return View(ID);
        }

        public ActionResult Sporten()
        {
            List<Sport> sport = new List<Sport>();
            sport = BC.GetSporten();

            return View(sport);
        }

        public ActionResult SportToevoegen()
        {
            return View();
        }

        public ActionResult SportToegevoegd(Sport s)
        {
            webshop.DatabaseControllers.BeheerderController bc = new DatabaseControllers.BeheerderController();
            if (ModelState.IsValid)
            {
                bc.InsertSport(s);
                return View(s);
            }
            else
            {
                return View("Index", s);
            }
        }

        public ActionResult SportVerwijderen(int ID)
        {
            BC.VerwijderSport(ID);
            return View();
        }

        public ActionResult Categorieen()
        {
            List<Categorie> categorieen = new List<Categorie>();
            categorieen = BC.GetCategorieen();

            return View(categorieen);
        }

        public ActionResult CategorieToevoegen()
        {
            return View();
        }

        public ActionResult CategorieToegevoegd(Categorie categorie)
        {
            if (ModelState.IsValid)
            {
                BC.CategorieToevoegen(categorie);
                return View(categorie);
            }
            else
            {
                return View("CategorieToevoegen", categorie);
            }
        }

        public ActionResult CategorieVerwijderen(int ID)
        {
            BC.CategorieVerwijderen(ID);
            return View();
        }


        public ActionResult Merken()
        {
            List<Merk> merk = new List<Merk>();
            merk = BC.GetMerken();

            return View(merk);
        }

        public ActionResult MerkToevoegen()
        {
            return View();
        }

        public ActionResult MerkToegevoegd(Merk merk)
        {

            if (ModelState.IsValid)
            {
                BC.MerkToevoegen(merk);
                return View(merk);
            }
            else
            {
                return View("MerkenToevoegen", merk);
            }
        }

        public ActionResult MerkVerwijderen(int ID)
        {
            BC.MerkVerwijderen(ID);

            return View();
        }

    }
}
