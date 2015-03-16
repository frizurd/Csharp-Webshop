using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using webshop.Models;

namespace webshop.DatabaseControllers
{
    public class MerkenController : DatabaseController
    {

        public List<Artikel> GetMerken()
        {
            List<Artikel> artikel = new List<Artikel>();
            try
            {
                conn.Open();

                string selectQuery = "SELECT * FROM Merk";
                MySqlCommand cmd = new MySqlCommand(selectQuery, conn);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    int getmerknr = dataReader.GetInt32("merk_nummer");
                    string getmerknaam = dataReader.GetString("merk_naam");
                    string getmerkimg = dataReader.GetString("merk_image");

                    Artikel a = new Artikel { merkNaam = getmerknaam, merkImage = getmerkimg, merkID = getmerknr };

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

        public List<Artikel> getMerken(int ID)
        {
            List<Artikel> artikelen = new List<Artikel>();

            try
            {
                conn.Open();

                string selectQuery = "SELECT * FROM Artikel a INNER JOIN Merk m ON artikel_merk=merk_nummer INNER JOIN Categorie c ON artikel_categorie=categorie_nummer WHERE merk_nummer = @ID";

                MySqlCommand cmd = new MySqlCommand(selectQuery, conn);
                MySqlParameter merkID = new MySqlParameter("@ID", MySqlDbType.Int32);

                merkID.Value = ID;
                cmd.Parameters.Add(merkID);
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
