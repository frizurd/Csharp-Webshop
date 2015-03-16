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
    public class SidebarController : Controller
    {
        webshop.DatabaseControllers.SidebarMethController SMC = new DatabaseControllers.SidebarMethController();

        [ChildActionOnly]
        public ActionResult Categorie()
        {
            List<SidebarItem> item = new List<SidebarItem>();
            item = SMC.GetCategorie();

            return PartialView("_categorieSidebar", item);
        }

        [ChildActionOnly]
        public ActionResult Sport()
        {
            List<SidebarItem> item = new List<SidebarItem>();
            item = SMC.GetSporten();

            return PartialView("_sportSidebar", item);
        }

        [ChildActionOnly]
        public ActionResult Merk()
        {
            List<SidebarItem> item = new List<SidebarItem>();
            item = SMC.GetMerken();

            return PartialView("_merkenSidebar", item);
        }

    }
}
