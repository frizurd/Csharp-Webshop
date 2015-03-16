using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using webshop.Models;

namespace webshop.DatabaseControllers
{
    public class AccessoiresController : DatabaseController
    {

        public List<Artikel> GetAccessoires()
        {
            List<Artikel> artikel = new List<Artikel>();
            try
            {
                conn.Open();

                string selectQuery = "SELECT * FROM Artikel a inner join Merk m on artikel_merk=merk_nummer WHERE artikel_categorie = 7";
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

                    Artikel a = new Artikel { ID = getid, naam = getnaam, omschrijving = getom, toevoegDatum = getdatum, image = getimage, prijs = getprijs, merkNaam = getmerk };

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

        public Artikel getAccessoires(int ID)
        {
            Artikel g = new Artikel();

            try
            {
                conn.Open();

                string selectQuery = "SELECT * FROM Artikel a INNER JOIN Merk m ON artikel_merk=merk_nummer INNER JOIN Categorie c ON artikel_categorie=categorie_nummer INNER JOIN Sport s on artikel_sport=sport_nummer WHERE artikel_nummer = @ID";

                MySqlCommand cmd = new MySqlCommand(selectQuery, conn);
                MySqlParameter artikelID = new MySqlParameter("@ID", MySqlDbType.Int32);

                artikelID.Value = ID;
                cmd.Parameters.Add(artikelID);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    g.ID = dataReader.GetInt32("artikel_nummer");
                    g.naam = dataReader.GetString("artikel_naam");
                    g.merkID = dataReader.GetInt32("merk_nummer");
                    g.merkNaam = dataReader.GetString("merk_naam");
                    g.omschrijving = dataReader.GetString("artikel_omschrijving");
                    g.prijs = dataReader.GetDouble("artikel_prijs");
                    g.image = dataReader.GetString("artikel_image");
                    g.merkImage = dataReader.GetString("merk_image");
                    g.categorieNaam = dataReader.GetString("categorie_naam");
                    g.categorie = dataReader.GetInt32("categorie_nummer");
                    g.sport = dataReader.GetInt32("sport_nummer");
                    g.sportnaam = dataReader.GetString("sport_naam");
                }

            }
            catch (Exception e)
            {
                Console.Write("Ophalen van artikel mislukt " + e);
            }
            finally
            {
                conn.Close();
            }

            return g;
        }


        public List<Artikel> GetSporten(int ID)
        {
            List<Artikel> artikel = new List<Artikel>();

            try
            {
                conn.Open();

                string selectQuery = "SELECT * FROM Artikel a inner join Merk m on artikel_merk=merk_nummer WHERE artikel_sport = @ID";

                MySqlCommand cmd = new MySqlCommand(selectQuery, conn);
                MySqlParameter artikelID = new MySqlParameter("@ID", MySqlDbType.Int32);

                artikelID.Value = ID;
                cmd.Parameters.Add(artikelID);
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
