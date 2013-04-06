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
    public class Quote : IQuote
    {
        private dbConnection dbConn = new dbConnection();

        public string GetQuote(int Autor, int Mjesec, int Dan)
        {
            int m = (Mjesec >= 3) ? Mjesec - 3 : Mjesec + 9;
            int do1m = dbConnection.martOffset[m] + Dan - 1;


            string returnString = "";

            string strSQL = @"SELECT Tekst FROM IzDanaUDan WHERE (Autor = " + Autor.ToString() + ") AND (OfsType = 0) AND (Offset  = " + do1m.ToString() + ") AND (RdType = 0)";

            DataTable list = dbConn.GetDataTable(strSQL, dbConnection.Connenction.PitanjaPastiru);

            foreach (DataRow row in list.Rows)
            {
                returnString += row["Tekst"].ToString();
            }

            return returnString;
        }

    }
}
