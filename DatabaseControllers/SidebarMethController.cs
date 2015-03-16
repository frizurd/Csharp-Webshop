using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using webshop.Models;

namespace webshop.DatabaseControllers
{
    class SidebarMethController : DatabaseController
    {
        public List<SidebarItem> GetCategorie()
        {
            List<SidebarItem> item = new List<SidebarItem>();
            try
            {
                conn.Open();

                string selectQuery = "SELECT * FROM Categorie WHERE categorie_nummer in (select artikel_categorie from Artikel) and categorie_naam != 'Accessoires' ORDER BY categorie_naam ASC";
                MySqlCommand cmd = new MySqlCommand(selectQuery, conn);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    int getid = dataReader.GetInt32("categorie_nummer");
                    string getnaam = dataReader.GetString("categorie_naam");

                    SidebarItem a = new SidebarItem { ID = getid, naam = getnaam };

                    item.Add(a);
                }
            }
            catch (Exception e)
            {
                SidebarItem a = new SidebarItem { ID = 1, naam = "Test" };
            }
            finally
            {
                conn.Close();
            }
            return item;
        }


        public List<SidebarItem> GetSporten()
        {
            List<SidebarItem> item = new List<SidebarItem>();
            try
            {
                conn.Open();

                string selectQuery = "SELECT * FROM Sport WHERE sport_nummer in (select artikel_sport from Artikel) ORDER BY sport_naam ASC";
                MySqlCommand cmd = new MySqlCommand(selectQuery, conn);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    int getid = dataReader.GetInt32("sport_nummer");
                    string getnaam = dataReader.GetString("sport_naam");

                    SidebarItem a = new SidebarItem { ID = getid, naam = getnaam };

                    item.Add(a);
                }
            }
            catch (Exception e)
            {
                SidebarItem a = new SidebarItem { ID = 1, naam = "Test" };
            }
            finally
            {
                conn.Close();
            }
            return item;
        }


        public List<SidebarItem> GetMerken()
        {
            List<SidebarItem> item = new List<SidebarItem>();
            try
            {
                conn.Open();

                string selectQuery = "SELECT * FROM Merk WHERE merk_nummer in (select artikel_merk from Artikel) ORDER BY merk_naam ASC";
                MySqlCommand cmd = new MySqlCommand(selectQuery, conn);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    int getid = dataReader.GetInt32("merk_nummer");
                    string getnaam = dataReader.GetString("merk_naam");

                    SidebarItem a = new SidebarItem { ID = getid, naam = getnaam };

                    item.Add(a);
                }
            }
            catch (Exception e)
            {
                SidebarItem a = new SidebarItem { ID = 1, naam = "Test" };
            }
            finally
            {
                conn.Close();
            }
            return item;
        }


    }
}
