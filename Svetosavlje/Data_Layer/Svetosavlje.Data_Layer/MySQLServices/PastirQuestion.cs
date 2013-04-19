using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Svetosavlje.Data_Layer.Interfaces;
using Svetosavlje.Data_Layer.Core;
using MySql.Data.MySqlClient;
using System.Data;

namespace Svetosavlje.Data_Layer.MySQLServices
{
    public class PastirQuestion : IPastirQuestion
    {
        private dbConnection dbConn = new dbConnection();

        public PitanjeInfo GetPastirQuestion(int questionID)
        {

            PitanjeInfo pitanje;

            string strSQL = @"SELECT p.ID, p.Naslov, p.Pitanje, p.Odgovor, t.ID TemaID, t.Tema, p.Ime FROM pp_pitanja_utf8 p Inner Join pp_teme_utf8 t On p.TemaID=t.ID Where p.ID=" + questionID.ToString() + ";";


            DataTable list = dbConn.GetDataTable(strSQL, dbConnection.Connenction.PitanjaPastiru);

            if (list.Rows.Count > 0)
            {
                DataRow row = list.Rows[0];
                pitanje = new PitanjeInfo(Convert.ToInt32(row["ID"]), row["Naslov"].ToString(),
                        row["Pitanje"].ToString(), row["Odgovor"].ToString(), Convert.ToInt32(row["TemaID"]), row["Tema"].ToString(), row["Ime"].ToString());
            }
            else return null;

            return pitanje;
        }

    }

}
