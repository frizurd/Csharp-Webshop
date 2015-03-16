using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using webshop.Models;

namespace webshop.DatabaseControllers
{
    class BeheerderController : DatabaseController
    {
        public void insertArtikel(Artikel artikel)
        {
            MySqlTransaction trans = null;
            try
            {
                conn.Open();
                trans = conn.BeginTransaction();
                string insertString = @"insert into Artikel (artikel_naam, artikel_omschrijving, artikel_merk, artikel_sport, artikel_categorie, artikel_aanbieding, artikel_weekaanbieding, artikel_image, artikel_toevoegdatum, artikel_voorpagina, artikel_prijs, artikel_sex)
                                               values (@naam, @omschr, @merk, @sport, @cat, @aanb, @waanb, @img, @dat, @vpagina, @prijs, @artikelsex)";
                MySqlCommand cmd = new MySqlCommand(insertString, conn);
                MySqlParameter naamParam = new MySqlParameter("@naam", MySqlDbType.VarChar);
                MySqlParameter omschrParam = new MySqlParameter("@omschr", MySqlDbType.Text);
                MySqlParameter merkParam = new MySqlParameter("@merk", MySqlDbType.Int32);
                MySqlParameter sportParam = new MySqlParameter("@sport", MySqlDbType.Int32);
                MySqlParameter catParam = new MySqlParameter("@cat", MySqlDbType.Int32);
                MySqlParameter aanbParam = new MySqlParameter("@aanb", MySqlDbType.Int32);
                MySqlParameter waanbParam = new MySqlParameter("@waanb", MySqlDbType.Int32);
                MySqlParameter imgParam = new MySqlParameter("@img", MySqlDbType.VarChar);
                MySqlParameter datParam = new MySqlParameter("@dat", MySqlDbType.VarChar);
                MySqlParameter vpaginaParam = new MySqlParameter("@vpagina", MySqlDbType.Int32);
                MySqlParameter prijsParam = new MySqlParameter("@prijs", MySqlDbType.Double);
                MySqlParameter sexParam = new MySqlParameter("@artikelsex", MySqlDbType.VarChar);

                naamParam.Value = artikel.naam;
                omschrParam.Value = artikel.omschrijving;
                merkParam.Value = artikel.merkID;
                sportParam.Value = artikel.sport;
                catParam.Value = artikel.categorie;
                aanbParam.Value = artikel.aanbieding;
                waanbParam.Value = artikel.wAanbieding;
                imgParam.Value = artikel.image;
                datParam.Value = artikel.toevoegDatum;
                vpaginaParam.Value = artikel.voorPagina;
                prijsParam.Value = artikel.prijs;
                sexParam.Value = artikel.geslacht;

                cmd.Parameters.Add(naamParam);
                cmd.Parameters.Add(omschrParam);
                cmd.Parameters.Add(merkParam);
                cmd.Parameters.Add(sportParam);
                cmd.Parameters.Add(catParam);
                cmd.Parameters.Add(aanbParam);
                cmd.Parameters.Add(waanbParam);
                cmd.Parameters.Add(imgParam);
                cmd.Parameters.Add(datParam);
                cmd.Parameters.Add(vpaginaParam);
                cmd.Parameters.Add(prijsParam);
                cmd.Parameters.Add(sexParam);

                cmd.Prepare();

                cmd.ExecuteNonQuery();

                trans.Commit();
            }
            catch (Exception e)
            {
                trans.Rollback();
                artikel.naam = "ERROR" + e + "\n\n";
                Console.Write("Artikel is niet toegevoegd: " + e);
            }
            finally
            {
                conn.Close();
            }

        }

        public List<Artikel> GetArtikelen()
        {
            List<Artikel> artikelen = new List<Artikel>();
            try
            {
                conn.Open();

                string selectQuery = "SELECT artikel_nummer, merk_naam, artikel_naam FROM Artikel a INNER JOIN Merk m ON artikel_merk=merk_nummer";
                MySqlCommand cmd = new MySqlCommand(selectQuery, conn);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    int getid = dataReader.GetInt32("artikel_nummer");
                    string getmerk = dataReader.GetString("merk_naam");
                    string getnaam = dataReader.GetString("artikel_naam");

                    Artikel a = new Artikel { ID = getid, merkNaam = getmerk, naam = getnaam };

                    artikelen.Add(a);
                }
            }
            catch (Exception e)
            {
                Artikel a = new Artikel { ID = 1, merkNaam = "test", naam = "test" };
            }
            finally
            {
                conn.Close();
            }
            return artikelen;
        }

        public Artikel getArtikel(int ID)
        {
            Artikel g = new Artikel();
            Maat m = new Maat();

            List<Maat> maten = new List<Maat>();

            try
            {
                conn.Open();

                string selectQuery = "SELECT * FROM Artikel a INNER JOIN Merk m ON artikel_merk=merk_nummer INNER JOIN Categorie c ON artikel_categorie=categorie_nummer INNER JOIN Maat ma ON artikel_nummer=maat_artikel WHERE artikel_nummer = @ID";

                MySqlCommand cmd = new MySqlCommand(selectQuery, conn);
                MySqlParameter artikelID = new MySqlParameter("@ID", MySqlDbType.Int32);

                artikelID.Value = ID;
                cmd.Parameters.Add(artikelID);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    g.ID = dataReader.GetInt32("artikel_nummer");
                    g.naam = dataReader.GetString("artikel_naam");
                    g.merkNaam = dataReader.GetString("merk_naam");
                    g.merkID = dataReader.GetInt32("artikel_merk");
                    g.omschrijving = dataReader.GetString("artikel_omschrijving");
                    g.prijs = dataReader.GetDouble("artikel_prijs");
                    g.image = dataReader.GetString("artikel_image");
                    g.merkImage = dataReader.GetString("merk_image");
                    g.sport = dataReader.GetInt32("artikel_sport");
                    g.categorieNaam = dataReader.GetString("categorie_naam");
                    g.categorie = dataReader.GetInt32("artikel_categorie");
                    g.toevoegDatum = dataReader.GetInt32("artikel_toevoegdatum");


                    m.grootte = dataReader.GetString("maat_grootte");
                    maten.Add(m);
                }


                g.maten = maten;

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

        public void verwijderArtikel(int ID)
        {
            MySqlTransaction trans = null;
            try
            {
                conn.Open();
                trans = conn.BeginTransaction();
                string deleteString = @"DELETE FROM Artikel WHERE artikel_nummer = @ID";
                MySqlCommand cmd = new MySqlCommand(deleteString, conn);
                MySqlParameter idParam = new MySqlParameter("@ID", MySqlDbType.Int32);

                idParam.Value = ID;

                cmd.Parameters.Add(idParam);

                cmd.Prepare();

                cmd.ExecuteNonQuery();

                trans.Commit();
            }
            catch (Exception)
            {
                trans.Rollback();
            }
            finally
            {
                conn.Close();
            }

        }

        public int getLaatsteID()
        {
            int lastid = 0;

            try
            {
                conn.Open();

                string selectQuery = "SELECT max(artikel_nummer) AS laatsteID FROM Artikel";

                MySqlCommand cmd = new MySqlCommand(selectQuery, conn);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    lastid = dataReader.GetInt32("laatsteID");
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

            return lastid;
        }

        public void insertMaten(Artikel artikel, int id)
        {
            MySqlTransaction trans = null;
            try
            {
                conn.Open();
                trans = conn.BeginTransaction();
                string insertString = @"INSERT INTO Maat (maat_grootte, maat_artikel) VALUES (@maat, @artikelnr)";


                MySqlCommand cmd = new MySqlCommand(insertString, conn);
                MySqlParameter maatParam = new MySqlParameter("@maat", MySqlDbType.VarChar);
                MySqlParameter artikelParam = new MySqlParameter("@artikelnr", MySqlDbType.Int32);

                maatParam.Value = artikel.grootte;
                artikelParam.Value = id;

                cmd.Parameters.Add(maatParam);
                cmd.Parameters.Add(artikelParam);

                cmd.Prepare();

                cmd.ExecuteNonQuery();

                trans.Commit();
            }
            catch (Exception e)
            {
                trans.Rollback();
                artikel.naam = "ERROR" + e + "\n\n";
                Console.Write("Artikel is niet toegevoegd: " + id + " /// " + e);
            }
            finally
            {
                conn.Close();
            }

        }

        public void wijzigArtikel(Artikel artikel)
        {
            MySqlTransaction trans = null;
            try
            {
                conn.Open();
                trans = conn.BeginTransaction();


                string insertString = @"UPDATE Artikel SET artikel_naam = @naam, artikel_omschrijving = @omschr, artikel_merk = @merk, artikel_sport = @sport, artikel_categorie = @cat, artikel_aanbieding = @aanb, artikel_weekaanbieding = @waanb, artikel_image = @img, artikel_toevoegdatum = @dat, artikel_voorpagina = @vpagina, artikel_prijs = @prijs, artikel_sex = @artikelsex WHERE artikel_nummer=@id";

                MySqlCommand cmd = new MySqlCommand(insertString, conn);
                MySqlParameter idParam = new MySqlParameter("@id", MySqlDbType.Int32);
                MySqlParameter naamParam = new MySqlParameter("@naam", MySqlDbType.VarChar);
                MySqlParameter omschrParam = new MySqlParameter("@omschr", MySqlDbType.Text);
                MySqlParameter merkParam = new MySqlParameter("@merk", MySqlDbType.Int32);
                MySqlParameter sportParam = new MySqlParameter("@sport", MySqlDbType.Int32);
                MySqlParameter catParam = new MySqlParameter("@cat", MySqlDbType.Int32);
                MySqlParameter aanbParam = new MySqlParameter("@aanb", MySqlDbType.Int32);
                MySqlParameter waanbParam = new MySqlParameter("@waanb", MySqlDbType.Int32);
                MySqlParameter imgParam = new MySqlParameter("@img", MySqlDbType.VarChar);
                MySqlParameter datParam = new MySqlParameter("@dat", MySqlDbType.VarChar);
                MySqlParameter vpaginaParam = new MySqlParameter("@vpagina", MySqlDbType.Int32);
                MySqlParameter prijsParam = new MySqlParameter("@prijs", MySqlDbType.Double);
                MySqlParameter sexParam = new MySqlParameter("@artikelsex", MySqlDbType.VarChar);

                idParam.Value = artikel.ID;
                naamParam.Value = artikel.naam;
                omschrParam.Value = artikel.omschrijving;
                merkParam.Value = artikel.merkID;
                sportParam.Value = artikel.sport;
                catParam.Value = artikel.categorie;
                aanbParam.Value = artikel.aanbieding;
                waanbParam.Value = artikel.wAanbieding;
                imgParam.Value = artikel.image;
                datParam.Value = artikel.toevoegDatum;
                vpaginaParam.Value = artikel.voorPagina;
                prijsParam.Value = artikel.prijs;
                sexParam.Value = artikel.geslacht;

                cmd.Parameters.Add(idParam);
                cmd.Parameters.Add(naamParam);
                cmd.Parameters.Add(omschrParam);
                cmd.Parameters.Add(merkParam);
                cmd.Parameters.Add(sportParam);
                cmd.Parameters.Add(catParam);
                cmd.Parameters.Add(aanbParam);
                cmd.Parameters.Add(waanbParam);
                cmd.Parameters.Add(imgParam);
                cmd.Parameters.Add(datParam);
                cmd.Parameters.Add(vpaginaParam);
                cmd.Parameters.Add(prijsParam);
                cmd.Parameters.Add(sexParam);

                cmd.Prepare();

                cmd.ExecuteNonQuery();

                trans.Commit();
            }
            catch (Exception e)
            {
                trans.Rollback();
                artikel.naam = "ERROR" + e + "\n\n";
            }
            finally
            {
                conn.Close();
            }

        }

        public List<Sport> GetSporten()
        {
            List<Sport> Listsport = new List<Sport>();
            try
            {
                conn.Open();

                string selectQuery = "SELECT * from Sport";
                MySqlCommand cmd = new MySqlCommand(selectQuery, conn);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    int getID = dataReader.GetInt32("sport_nummer");
                    string getSport = dataReader.GetString("sport_naam");

                    Sport s = new Sport { ID = getID, naam = getSport };

                    Listsport.Add(s);
                }
            }
            catch (Exception)
            {
                Sport a = new Sport { ID = 1, naam = "test" };
            }
            finally
            {
                conn.Close();
            }
            return Listsport;
        }

        public void InsertSport(Sport sport)
        {
            MySqlTransaction trans = null;
            try
            {
                conn.Open();
                trans = conn.BeginTransaction();
                string insertString = @"insert into Sport (sport_naam)
                                               values (@naam)";
                MySqlCommand cmd = new MySqlCommand(insertString, conn);
                MySqlParameter naamParam = new MySqlParameter("@naam", MySqlDbType.VarChar);


                naamParam.Value = sport.naam;

                cmd.Parameters.Add(naamParam);

                cmd.Prepare();

                cmd.ExecuteNonQuery();

                trans.Commit();
            }
            catch (Exception e)
            {
                trans.Rollback();
                sport.naam = "ERROR" + e + "\n\n";
                Console.Write("Sport is niet toegevoegd: " + e);
            }
            finally
            {
                conn.Close();
            }
        }

        public void VerwijderSport(int ID)
        {
            MySqlTransaction trans = null;
            try
            {
                conn.Open();
                trans = conn.BeginTransaction();
                string deleteString = @"DELETE FROM Sport WHERE sport_nummer = @ID";
                MySqlCommand cmd = new MySqlCommand(deleteString, conn);
                MySqlParameter idParam = new MySqlParameter("@ID", MySqlDbType.Int32);

                idParam.Value = ID;

                cmd.Parameters.Add(idParam);

                cmd.Prepare();

                cmd.ExecuteNonQuery();

                trans.Commit();
            }
            catch (Exception)
            {
                trans.Rollback();
            }
            finally
            {
                conn.Close();
            }
        }

        public void CategorieToevoegen(Categorie categorie)
        {
            MySqlTransaction trans = null;

            try
            {
                conn.Open();
                trans = conn.BeginTransaction();
                string insertString = @"insert into Categorie (categorie_naam) values (@naam)";

                MySqlCommand cmd = new MySqlCommand(insertString, conn);
                MySqlParameter naamParam = new MySqlParameter("@naam", MySqlDbType.VarChar);

                naamParam.Value = categorie.naam;

                cmd.Parameters.Add(naamParam);

                cmd.Prepare();

                cmd.ExecuteNonQuery();

                trans.Commit();
            }
            catch (Exception e)
            {
                trans.Rollback();
                categorie.naam = "ERROR" + e + "\n\n";
                Console.Write("Categorie is niet toegevoegd: " + e);
            }
            finally
            {
                conn.Close();
            }
        }

        public List<Categorie> GetCategorieen()
        {
            List<Categorie> categorieen = new List<Categorie>();
            try
            {
                conn.Open();

                string selectQuery = "SELECT categorie_naam, categorie_nummer FROM Categorie;";
                MySqlCommand cmd = new MySqlCommand(selectQuery, conn);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    string getnaam = dataReader.GetString("categorie_naam");
                    int getid = dataReader.GetInt16("categorie_nummer");

                    Categorie c = new Categorie { naam = getnaam, ID = getid };

                    categorieen.Add(c);
                }
            }
            catch (Exception e)
            {
                Categorie c = new Categorie { naam = "test" };
            }
            finally
            {
                conn.Close();
            }
            return categorieen;
        }

        public void CategorieVerwijderen(int ID)
        {
            MySqlTransaction trans = null;

            try
            {
                conn.Open();
                trans = conn.BeginTransaction();
                //string deleteString = @"delete from Categorie where cat_naam = @ID";

                MySqlCommand cmd = new MySqlCommand("delete from Categorie WHERE categorie_nummer = @ID", conn);
                MySqlParameter idParam = new MySqlParameter("@ID", MySqlDbType.Int16);

                idParam.Value = ID;

                cmd.Parameters.Add(idParam);

                cmd.Prepare();

                cmd.ExecuteNonQuery();

                trans.Commit();
            }
            catch (Exception e)
            {
                trans.Rollback();
                //categorie.naam = "ERROR" + e + "\n\n";
                Console.Write("Categorie is niet verwijderd: " + e);
            }
            finally
            {
                conn.Close();
            }
        }

        public void MerkToevoegen(Merk merk)
        {
            MySqlTransaction trans = null;

            try
            {
                conn.Open();
                trans = conn.BeginTransaction();
                string insertString = @"insert into Merk (merk_naam, merk_image) values (@naam, @image)";

                MySqlCommand cmd = new MySqlCommand(insertString, conn);
                MySqlParameter naamParam = new MySqlParameter("@naam", MySqlDbType.VarChar);
                MySqlParameter imageParam = new MySqlParameter("@image", MySqlDbType.VarChar);

                naamParam.Value = merk.naam;
                imageParam.Value = merk.image;

                cmd.Parameters.Add(naamParam);
                cmd.Parameters.Add(imageParam);

                cmd.Prepare();

                cmd.ExecuteNonQuery();

                trans.Commit();
            }
            catch (Exception e)
            {
                trans.Rollback();
                merk.naam = "ERROR" + e + "\n\n";
                Console.Write("Merk is niet toegevoegd: " + e);
            }
            finally
            {
                conn.Close();
            }


        }

        public List<Merk> GetMerken()
        {
            List<Merk> merken = new List<Merk>();
            try
            {
                conn.Open();

                string selectQuery = "SELECT * FROM Merk;";
                MySqlCommand cmd = new MySqlCommand(selectQuery, conn);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    int getnr = dataReader.GetInt16("merk_nummer");
                    string getnaam = dataReader.GetString("merk_naam");
                    string getimg = dataReader.GetString("merk_image");

                    Merk m = new Merk { ID = getnr, naam = getnaam };

                    merken.Add(m);
                }
            }
            catch (Exception e)
            {
                Merk m = new Merk { naam = "test" };
            }
            finally
            {
                conn.Close();
            }
            return merken;
        }

        public void MerkVerwijderen(int ID)
        {
            MySqlTransaction trans = null;

            try
            {
                conn.Open();
                trans = conn.BeginTransaction();
                string deleteString = @"delete from Merk where merk_nummer = @id";

                MySqlCommand cmd = new MySqlCommand(deleteString, conn);
                MySqlParameter naamParam = new MySqlParameter("@id", MySqlDbType.VarChar);

                naamParam.Value = ID;

                cmd.Parameters.Add(naamParam);

                cmd.Prepare();

                cmd.ExecuteNonQuery();

                trans.Commit();
            }
            catch (Exception e)
            {
                trans.Rollback();
                //merk.naam = "ERROR" + e + "\n\n";
                Console.Write("Merk is niet verwijderd: " + e);
            }
            finally
            {
                conn.Close();
            }
        }

    }
}
