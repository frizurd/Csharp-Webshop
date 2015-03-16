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
    public class ZoekenController : Controller
    {
        webshop.DatabaseControllers.ZoekController ZC = new DatabaseControllers.ZoekController();

        public ActionResult Index(String query)
        {
            List<Artikel> artikel = new List<Artikel>();
            artikel = ZC.ZoekResults(query);
            return View(artikel);
        }

    }
}
