using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Svetosavlje.Interfaces.Interfaces;
using Svetosavlje.Interfaces.Classes;
using Svetosavlje.Data_Layer.Core;
using MySql.Data.MySqlClient;
using System.Data;

namespace Svetosavlje.Data_Layer.MySQLServices
{
    public class FastingType : IFastingType
    {
        private dbConnection dbConn = new dbConnection();

        public string GetFastingType(int Mjesec, int Dan)
        {
            int m = (Mjesec >= 3) ? Mjesec - 3 : Mjesec + 9;
            int do1m = dbConnection.martOffset[m] + Dan - 1;
            
            string strSQL = @"SELECT Tekst FROM IzDanaUDan WHERE (Autor = 999) AND (OfsType = 0) AND (Offset  = " + do1m.ToString() + ") AND (RdType = 0)";

            string fastingType = "";


            DataTable list = dbConn.GetDataTable(strSQL, dbConnection.Connenction.PitanjaPastiru);

            foreach (DataRow row in list.Rows)
            {
                fastingType = row["Tekst"].ToString().Replace("\r\n","");
            }
            
            return fastingType;
        }

    }
}
