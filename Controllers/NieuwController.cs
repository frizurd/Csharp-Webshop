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
    public class NieuwController : Controller
    {
        webshop.DatabaseControllers.NieuwController NC = new DatabaseControllers.NieuwController();

        public ActionResult Index()
        {
            List<Artikel> artikel = new List<Artikel>();
            artikel = NC.GetNieuweArtikelen();
            return View(artikel);
        }

    }
}
