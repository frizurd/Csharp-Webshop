using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using webshop.Models;

namespace webshop.Controllers
{
    public class RegisterController : Controller
    {
        //
        // GET: /Register/
        protected MySqlConnection dbConnection = new MySqlConnection("Uid=12009261;Pwd=eeyoReepee;Server=meru.hhs.nl;Database=12009261");

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register(Account user)
        {
            if (ModelState.IsValid)
            {
                RegisterData(user);
                return RedirectToAction("Login", "Account", new { username = user.email, password = user.wachtwoord });
            }
            else
            {
                return View("~/Views/Account/Login.cshtml");
            }
        }

        public void RegisterData(Account user)
        {
            dbConnection.Open();

            MySqlCommand insertGegevens = new MySqlCommand("INSERT INTO Account(account_voornaam, account_achternaam, account_email, account_wachtwoord, account_straat, account_geboortedatum, account_postcode, account_telnummer, account_plaats) VALUES(@voornaam, @achternaam, @username, @password, @straat, @gebdat, @postcode, @telnr, @plaats)", dbConnection);
            MySqlParameter vnaamParam = new MySqlParameter("@voornaam", MySqlDbType.String);
            MySqlParameter anaamParam = new MySqlParameter("@achternaam", MySqlDbType.String);
            MySqlParameter plaatsParam = new MySqlParameter("@plaats", MySqlDbType.String);
            MySqlParameter usernameParam = new MySqlParameter("@username", MySqlDbType.String);
            MySqlParameter passwordParam = new MySqlParameter("@password", MySqlDbType.String);
            MySqlParameter straatParam = new MySqlParameter("@straat", MySqlDbType.String);
            MySqlParameter gebdatParam = new MySqlParameter("@gebdat", MySqlDbType.String);
            MySqlParameter postcodeParam = new MySqlParameter("@postcode", MySqlDbType.String);
            MySqlParameter telnrParam = new MySqlParameter("@telnr", MySqlDbType.String);

            vnaamParam.Value = user.voornaam;
            anaamParam.Value = user.achternaam;
            plaatsParam.Value = user.plaats;
            usernameParam.Value = user.email;
            passwordParam.Value = user.wachtwoord;
            straatParam.Value = user.straat;
            gebdatParam.Value = user.geboorteDatum;
            postcodeParam.Value = user.postcode;
            telnrParam.Value = user.telnummer;

            insertGegevens.Parameters.Add(vnaamParam);
            insertGegevens.Parameters.Add(anaamParam);
            insertGegevens.Parameters.Add(plaatsParam);
            insertGegevens.Parameters.Add(usernameParam);
            insertGegevens.Parameters.Add(passwordParam);
            insertGegevens.Parameters.Add(straatParam);
            insertGegevens.Parameters.Add(gebdatParam);
            insertGegevens.Parameters.Add(postcodeParam);
            insertGegevens.Parameters.Add(telnrParam);

            insertGegevens.Prepare();

            try
            {
                insertGegevens.ExecuteNonQuery();
            }
            catch
            {
                RedirectToAction("Index","Account");
            }
            finally
            {
                dbConnection.Close();
            }
        }

    }
}
