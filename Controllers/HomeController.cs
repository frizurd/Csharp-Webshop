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
    public class HomeController : Controller
    {
        webshop.DatabaseControllers.HomeController HC = new DatabaseControllers.HomeController();

        public ActionResult Index()
        {
            List<Artikel> artikel = new List<Artikel>();
            artikel = HC.GetWeekaanbiedingen();

            if (Request.Cookies.Get("loginCookie") == null)
            {
                HttpCookie loginCookie = new HttpCookie("loginCookie");
                loginCookie.Values["email"] = "leeg";
                loginCookie.Values["privilege"] = "leeg";
                loginCookie.Expires = DateTime.Now.AddDays(7);
                Response.Cookies.Add(loginCookie);
            }
            if (Request.Cookies.Get("cartCookie") == null)
            {
                HttpCookie cartCookie = new HttpCookie("cartCookie");
                cartCookie.Values["aantal"] = "0";
                cartCookie.Expires = DateTime.Now.AddDays(7);
                Response.Cookies.Add(cartCookie);
            }
            return View(artikel);
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Levertijden()
        {
            return View();
        }

        public ActionResult Betalen()
        {
            return View();
        }

        public ActionResult Retourneren()
        {
            return View();
        }

        public ActionResult Winkel()
        {
            return View();
        }

        public ActionResult Voorwaarden()
        {
            return View();
        }

        public ActionResult Vacatures()
        {
            return View();
        }

        public ActionResult Sitemap()
        {
            return View();
        }

        public ActionResult NieuwsbriefAanmelden(string nb)
        {
            webshop.DatabaseControllers.InschrijvenNieuwsbriefController INC = new DatabaseControllers.InschrijvenNieuwsbriefController();
            Niewsbrief nbe = new Niewsbrief();
            nbe.email = nb;
            INC.InsertNieuwsbrief(nbe);
            return View(nbe);
        }
    }
}
