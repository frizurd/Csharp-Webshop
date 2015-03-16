using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using webshop.Models;

namespace webshop.DatabaseControllers
{
    public class InschrijvenNieuwsbriefController : DatabaseController
    {
        //
        // GET: /InschrijvenNieuwsbrief/

        public void InsertNieuwsbrief(Niewsbrief nb)
        {
            //MySqlTransaction trans = null;
            try
            {
                conn.Open();
                //trans = conn.BeginTransaction();
                MySqlCommand cmd = new MySqlCommand("INSERT INTO Nieuwsbrief(nieuwsbrief_email) VALUES (@mailadres)", conn);
                MySqlParameter mailParam = new MySqlParameter("@mailadres", MySqlDbType.String);


                mailParam.Value = nb.email;

                cmd.Parameters.Add(mailParam);

                cmd.Prepare();

                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                //trans.Rollback();
                //nb.Nieuwsbrieven = "ERROR" + e + "\n\n";
                Console.Write("Nieuwsbrief is niet toegevoegd: " + e);
            }
            finally
            {
                conn.Close();
            }
        }

    }
}
