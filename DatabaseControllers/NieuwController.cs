using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using webshop.Models;

namespace webshop.DatabaseControllers
{
    public class NieuwController : DatabaseController
    {

        public List<Artikel> GetNieuweArtikelen()
        {
            List<Artikel> artikel = new List<Artikel>();
            try
            {
                conn.Open();

                string selectQuery = "SELECT * FROM Artikel a inner join Merk m on artikel_merk=merk_nummer ORDER BY artikel_toevoegdatum DESC";
                MySqlCommand cmd = new MySqlCommand(selectQuery, conn);
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
                    int getcategory = dataReader.GetInt32("artikel_categorie");

                    Artikel a = new Artikel { ID = getid, naam = getnaam, omschrijving = getom, toevoegDatum = getdatum, image = getimage, prijs = getprijs, merkNaam = getmerk, categorie = getcategory };

                    artikel.Add(a);
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
            return artikel;
        }

    }
}
