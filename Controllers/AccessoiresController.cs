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
    public class AccessoiresController : Controller
    {

        webshop.DatabaseControllers.AccessoiresController AC = new DatabaseControllers.AccessoiresController();

        public ActionResult Index()
        {
            List<Artikel> artikel = new List<Artikel>();
            artikel = AC.GetAccessoires();
            return View(artikel);

        }

        public ActionResult Artikel(int ID)
        {
            Models.Artikel ar = new Models.Artikel();
            ar = AC.getAccessoires(ID);
            return View(ar);
        }

        public ActionResult Sport(int ID)
        {
            List<Artikel> artikel = new List<Artikel>();
            artikel = AC.GetSporten(ID);
            return View(artikel);
        }

    }
}
