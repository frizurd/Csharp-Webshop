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
    public class MerkenController : Controller
    {
        webshop.DatabaseControllers.MerkenController MC = new DatabaseControllers.MerkenController();

        public ActionResult Index()
        {
            List<Artikel> artikel = new List<Artikel>();
            artikel = MC.GetMerken();
            return View(artikel);
        }

        public ActionResult alles(int ID)
        {
            return View(MC.getMerken(ID));
        }
    }
}
