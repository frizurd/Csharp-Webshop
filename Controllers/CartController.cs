using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using webshop.Models;

namespace webshop.Controllers
{
    public class CartController : Controller
    {
        protected MySqlConnection dbConnection = new MySqlConnection("Uid=12009261;Pwd=eeyoReepee;Server=meru.hhs.nl;Database=12009261");
        protected MySqlConnection dbConnection2 = new MySqlConnection("Uid=12009261;Pwd=eeyoReepee;Server=meru.hhs.nl;Database=12009261");
        //
        // GET: /Cart/

        public ActionResult Index()
        {
            List<Artikel> articleList = new List<Artikel>();

            GetAddedArticles(articleList);

            return View("MyCart", articleList);
        }

        public void AddArticleCount()
        {
            HttpCookie cartCookie = new HttpCookie("cartCookie");
            int tempInt = Convert.ToInt16(Request.Cookies.Get("cartCookie").Values["aantal"]) + 1;
            cartCookie.Values["aantal"] = Convert.ToString(tempInt);
            Response.Cookies.Set(cartCookie);
        }

        public ActionResult AddArticleToCart(int artikelnr, double prijs)
        {
            dbConnection.Open();

            Artikel article = new Artikel();
            MySqlCommand insertArticleData = new MySqlCommand("INSERT INTO Bestelling(bestelling_datum, bestelling_klant, bestelling_artikel, bestelling_artikelaantal, bestelling_prijs) VALUES(@datum, @klantnr, @artikelnr, @artikelaantal, @prijs)", dbConnection);
            MySqlParameter artikelnrParam = new MySqlParameter("@artikelnr", MySqlDbType.Int16);
            MySqlParameter klantnrParam = new MySqlParameter("@klantnr", MySqlDbType.Int16);
            MySqlParameter artikelaantalParam = new MySqlParameter("@artikelaantal", MySqlDbType.Int16);
            MySqlParameter datumParam = new MySqlParameter("@datum", MySqlDbType.String);
            MySqlParameter prijsParam = new MySqlParameter("@prijs", MySqlDbType.Double);

            artikelnrParam.Value = artikelnr;
            klantnrParam.Value = Convert.ToInt16(Request.Cookies.Get("loginCookie").Values["ID"]);
            artikelaantalParam.Value = 1;
            datumParam.Value = DateTime.Now.ToString();
            prijsParam.Value = prijs;

            insertArticleData.Parameters.Add(artikelaantalParam);
            insertArticleData.Parameters.Add(klantnrParam);
            insertArticleData.Parameters.Add(artikelnrParam);
            insertArticleData.Parameters.Add(datumParam);
            insertArticleData.Parameters.Add(prijsParam);

            try
            {
                insertArticleData.ExecuteNonQuery();
            }
            catch
            {

                //ARTIKEL IS AL TOEGEVOEGD PAGINA!//
            }
            dbConnection.Close();

            AddArticleCount();

            return RedirectToAction("Index", "Cart");

        }

        public void GetAddedArticles(List<Artikel> list1)
        {

            dbConnection.Open();

            MySqlCommand getCartData = new MySqlCommand("select * from Bestelling join Artikel on bestelling_artikel = artikel_nummer where bestelling_klant = @klantnr;", dbConnection);
            MySqlParameter klantnrParam = new MySqlParameter("@klantnr", MySqlDbType.Int16);
            MySqlDataReader cartDataReader;

            klantnrParam.Value = Request.Cookies.Get("loginCookie").Values["ID"];

            getCartData.Parameters.Add(klantnrParam);
            cartDataReader = getCartData.ExecuteReader();


            while (cartDataReader.Read())
            {
                list1.Add(GetArticleData(cartDataReader.GetInt16("bestelling_artikel")));
            }

            dbConnection.Close();

            RedirectToAction("Index", "Cart");

        }

        public Artikel GetArticleData(int artikelNummer)
        {
            dbConnection2.Open();
            Artikel article = new Artikel();

            MySqlCommand getArticleData = new MySqlCommand("SELECT * FROM Artikel a INNER JOIN Merk m ON a.artikel_merk=m.merk_nummer WHERE artikel_nummer = @artikelnr", dbConnection2);
            MySqlParameter articlenrParam = new MySqlParameter("@artikelnr", MySqlDbType.Int16);
            MySqlDataReader articleDataReader;

            articlenrParam.Value = artikelNummer;

            getArticleData.Parameters.Add(articlenrParam);
            articleDataReader = getArticleData.ExecuteReader();

            while (articleDataReader.Read())
            {
                article.naam = articleDataReader.GetString("artikel_naam");
                article.ID = articleDataReader.GetInt16("artikel_nummer");
                article.image = articleDataReader.GetString("artikel_image");
                article.merkID = articleDataReader.GetInt32("merk_nummer");
                article.merkNaam = articleDataReader.GetString("merk_naam");
                article.prijs = articleDataReader.GetInt32("artikel_prijs");
            }
            dbConnection2.Close();
            return article;
        }

        public ActionResult MakeOrder()
        {
            dbConnection.Open();

            MySqlCommand getOrderData = new MySqlCommand("SELECT SUM(bestelling_prijs) as total from Bestelling where bestelling_klant = @klantnr", dbConnection);
            MySqlParameter orderParam = new MySqlParameter("@klantnr", MySqlDbType.Int16);
            MySqlDataReader orderDataReader;

            orderParam.Value = Request.Cookies.Get("loginCookie").Values["ID"];

            getOrderData.Parameters.Add(orderParam);
            orderDataReader = getOrderData.ExecuteReader();

            double totalPrice = 0;

            while (orderDataReader.Read())
            {
                totalPrice = orderDataReader.GetDouble("total");
            }
            dbConnection.Close();

            dbConnection.Open();

            MySqlCommand insertOrderData = new MySqlCommand("Insert INTO Factuur (factuur_klant, factuur_datum, factuur_prijs) VALUES (@klant, @datum, @prijs)", dbConnection);
            
            MySqlParameter klantParam = new MySqlParameter("@klant", MySqlDbType.Int16);
            MySqlParameter datumParam = new MySqlParameter("@datum", MySqlDbType.VarChar);
            MySqlParameter prijsParam = new MySqlParameter("@prijs", MySqlDbType.Double);

            klantParam.Value = Request.Cookies.Get("loginCookie").Values["ID"];
            datumParam.Value = Convert.ToString(DateTime.Now);
            prijsParam.Value = totalPrice;

            insertOrderData.Parameters.Add(klantParam);
            insertOrderData.Parameters.Add(datumParam);
            insertOrderData.Parameters.Add(prijsParam);

            insertOrderData.ExecuteNonQuery();

            return RedirectToAction("MyAccount", "Account");
        }

        public ActionResult DeleteItem(int articleNr)
        {
            dbConnection.Open();

            MySqlCommand removeArticleData = new MySqlCommand("DELETE FROM Bestelling WHERE bestelling_artikel = @artikelnr AND bestelling_klant = @klantnr", dbConnection);
            MySqlParameter klantnrParam = new MySqlParameter("@klantnr", MySqlDbType.Int16);
            MySqlParameter artikelnrParam = new MySqlParameter("@artikelnr", MySqlDbType.Int16);

            klantnrParam.Value = Request.Cookies.Get("loginCookie").Values["ID"];
            artikelnrParam.Value = articleNr;

            removeArticleData.Parameters.Add(klantnrParam);
            removeArticleData.Parameters.Add(artikelnrParam);

            removeArticleData.ExecuteNonQuery();

            dbConnection.Close();


            return RedirectToAction("Index", "cart");
        }

        public ActionResult FinalizeOrder()
        {
            dbConnection.Open();

            MySqlCommand getOrderData = new MySqlCommand("SELECT SUM(bestelling_prijs) as total from Bestelling where bestelling_klant = @klantnr", dbConnection);
            MySqlParameter orderParam = new MySqlParameter("@klantnr", MySqlDbType.Int16);
            MySqlDataReader orderDataReader;

            orderParam.Value = Request.Cookies.Get("loginCookie").Values["ID"];

            getOrderData.Parameters.Add(orderParam);
            orderDataReader = getOrderData.ExecuteReader();

            double totalPrice = 0;

            while (orderDataReader.Read())
            {
                totalPrice = orderDataReader.GetDouble("total");               
            }

            OrderPrijs orderPrijs = new OrderPrijs() { prijs = totalPrice};

            dbConnection.Close();
            return View(orderPrijs);

        }
    }
}
