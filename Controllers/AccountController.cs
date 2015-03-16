using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webshop.Controllers;
using MySql.Data.MySqlClient;
using webshop.Models;

namespace webshop.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Login/
        protected MySqlConnection dbConnection = new MySqlConnection("Uid=12009261;Pwd=eeyoReepee;Server=meru.hhs.nl;Database=12009261");

        public ActionResult Index()
        {
            return View("Login");
        }

        public ActionResult Login(string username, string password)
        {
            dbConnection.Open();

            MySqlDataReader accountGegevens;
            MySqlCommand checkGegevens = new MySqlCommand("SELECT * FROM Account WHERE account_email = @email AND account_wachtwoord = @password ", dbConnection);
            MySqlParameter emailParam = new MySqlParameter("@email", MySqlDbType.String);
            MySqlParameter passwordParam = new MySqlParameter("@password", MySqlDbType.String);

            emailParam.Value = username;
            passwordParam.Value = password;

            checkGegevens.Parameters.Add(emailParam);
            checkGegevens.Parameters.Add(passwordParam);

            checkGegevens.Prepare();
            accountGegevens = checkGegevens.ExecuteReader();

            if (accountGegevens.HasRows)
            {
                while (accountGegevens.Read())
                {
                    HttpCookie loginCookie = new HttpCookie("loginCookie");
                    loginCookie.Values["email"] = username;
                    loginCookie.Values["voornaam"] = accountGegevens.GetString("account_voornaam");
                    loginCookie.Values["achternaam"] = accountGegevens.GetString("account_achternaam");
                    loginCookie.Values["ID"] = accountGegevens.GetString("account_nummer");
                    loginCookie.Values["privilege"] = accountGegevens.GetString("account_privilege");
                    loginCookie.Expires = DateTime.Now.AddDays(7);
                    Response.Cookies.Set(loginCookie);
                }
                dbConnection.Close();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                dbConnection.Close();
                return RedirectToAction("Index", "Account");
            }

        }

        public void LogOut()
        {
            HttpCookie loginCookie = new HttpCookie("loginCookie");
            HttpCookie cartCookie = new HttpCookie("cartCookie");
            loginCookie.Expires = DateTime.Now.AddHours(-1);
            cartCookie.Expires = DateTime.Now.AddHours(-1);
            Response.Cookies.Set(loginCookie);
            Response.Cookies.Set(cartCookie);
        }

        public ActionResult MyAccount()
        {
            if (Request.Cookies.Get("loginCookie") != null)
            {

                Account user = new Account();
                List<String> order = new List<String>();
                getAccountData(user, Request.Cookies.Get("loginCookie").Values["email"]);
                getOrderData(order, Request.Cookies.Get("loginCookie").Values["ID"]);
                ViewData["orderlist"] = order;
                return View(user);

            }
            else
            {

                return RedirectToAction("Index", "Account");

            }
        }

        public void getOrderData(List<String> list1, string klantnr)
        {
            dbConnection.Open();

            MySqlCommand getOrderData = new MySqlCommand("select * from Factuur where factuur_klant=@klantnr", dbConnection);
            MySqlParameter klantnrParam = new MySqlParameter("@klantnr", MySqlDbType.String);
            MySqlDataReader orderDataReader;

            klantnrParam.Value = Convert.ToInt16(klantnr);

            getOrderData.Parameters.Add(klantnrParam);

            orderDataReader = getOrderData.ExecuteReader();

            while (orderDataReader.Read())
            {
                string factuurnummer = Convert.ToString(orderDataReader.GetInt16("factuur_nummer"));
                string factuurdatum = orderDataReader.GetString("factuur_datum");
                string factuurprijs = Convert.ToString(orderDataReader.GetDouble("factuur_prijs"));

                list1.Add(factuurnummer + ",    Datum: " + factuurdatum + ",    Prijs: €" + factuurprijs );
            }

            dbConnection.Close();
        }

        public void getAccountData(Account user, string email)
        {
            dbConnection.Open();

            string getvnaam = null;
            string getanaam = null;
            string getadres = null;
            string getplaats = null;
            string getpostcode = null;

            MySqlCommand getAccountData = new MySqlCommand("select * from Account where account_email=@email", dbConnection);
            MySqlParameter emailParam = new MySqlParameter("@email", MySqlDbType.String);
            MySqlDataReader accountDataReader;

            emailParam.Value = email;

            getAccountData.Parameters.Add(emailParam);
            accountDataReader = getAccountData.ExecuteReader();

            while (accountDataReader.Read())
            {
                getvnaam = accountDataReader.GetString("account_voornaam");
                getanaam = accountDataReader.GetString("account_achternaam");
                if (accountDataReader.GetString("account_straat") != "nvt")
                {
                    getadres = accountDataReader.GetString("account_straat");
                }
                else
                {
                    getadres = "nvt";

                }
                if (accountDataReader.GetString("account_plaats") != "nvt")
                {
                    getplaats = accountDataReader.GetString("account_plaats");
                }
                else
                {
                    getplaats = "nvt";

                }
                if (accountDataReader.GetString("account_postcode") != "nvt")
                {
                    getpostcode = accountDataReader.GetString("account_postcode");
                }
                else
                {
                    getpostcode = "nvt";

                }
            }

            user.achternaam = getanaam;
            user.voornaam = getvnaam;
            user.straat = getadres;
            user.plaats = getplaats;
            user.postcode = getpostcode;

            dbConnection.Close();
        }

        public ActionResult DeleteOrders()
        {
            dbConnection.Open();
            MySqlCommand removeBestellingData = new MySqlCommand("DELETE from Bestelling where bestelling_klant=@klantnr", dbConnection);
            MySqlParameter klantnrParam = new MySqlParameter("@klantnr", MySqlDbType.Int16);

            klantnrParam.Value = Convert.ToInt16(Request.Cookies.Get("loginCookie").Values["ID"]);

            removeBestellingData.Parameters.Add(klantnrParam);

            removeBestellingData.ExecuteNonQuery();

            LogOut();
            dbConnection.Close();
            return RedirectToAction("Index", "Home");
        }

    }
}
