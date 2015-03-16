using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using webshop.Models;

namespace webshop.DatabaseControllers
{
    public class ZoekController : DatabaseController
    {

        public List<Artikel> ZoekResults(String query)
        {
            List<Artikel> artikelen = new List<Artikel>();

            try
            {
                conn.Open();

                string selectQuery = "SELECT * FROM Artikel a INNER JOIN Merk m ON artikel_merk=merk_nummer INNER JOIN Categorie c ON artikel_categorie=categorie_nummer WHERE artikel_naam like @ZOEK or merk_naam like @ZOEK";

                MySqlCommand cmd = new MySqlCommand(selectQuery, conn);
                MySqlParameter ZoekQuery = new MySqlParameter("@ZOEK", MySqlDbType.String);

                ZoekQuery.Value = "%" + query + "%";
                cmd.Parameters.Add(ZoekQuery);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    int getid = dataReader.GetInt32("artikel_nummer");
                    string getnaam = dataReader.GetString("artikel_naam");
                    string getom = dataReader.GetString("artikel_omschrijving");
                    int getdatum = dataReader.GetInt32("artikel_toevoegdatum");
                    string getimage = dataReader.GetString("artikel_image");
                    double getprijs = dataReader.GetDouble("artikel_prijs");
                    string getmerk = dataReader.GetString("merk_naam");

                    Artikel a = new Artikel { ID = getid, naam = getnaam, omschrijving = getom, toevoegDatum = getdatum, image = getimage, prijs = getprijs, merkNaam = getmerk };

                    artikelen.Add(a);
                }

            }
            catch (Exception e)
            {
                Artikel a = new Artikel { ID = 1, naam = "1", omschrijving = "1", toevoegDatum = 1, image = "1", prijs = 1 };
            }
            finally
            {
                conn.Close();
            }

            return artikelen;
        }

    }
}
