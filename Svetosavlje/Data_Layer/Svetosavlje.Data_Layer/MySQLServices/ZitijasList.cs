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
    public class ZitijasList : IGetList
    {        
        private dbConnection dbConn = new dbConnection();

        public IList<string> GetList(int Mjesec, int Dan)
        {
            List<string> returnList = new List<string>();

            string strSQL = @"SELECT ime FROM prolog_zitija_utf8 WHERE (datum = 1" + Mjesec.ToString("D2") + Dan.ToString("D2") + ") ORDER BY br";

            DataTable list = dbConn.GetDataTable(strSQL, dbConnection.Connenction.PitanjaPastiru);

            foreach (DataRow row in list.Rows)
            {
                returnList.Add(row["ime"].ToString());
            }

            return returnList;

        }

    }
}
