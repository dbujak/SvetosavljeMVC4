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
    public class PastirTopicsList : IPastirTopicsList
    {
        private dbConnection dbConn = new dbConnection();

        public IList<PastirTopic> GetPastirTopicList()
        {
            IList<PastirTopic> returnList = new List<PastirTopic>();
    
            string strSQL = @"SELECT ID, Tema FROM pp_teme_utf8 Order By ID Desc;";


            DataTable list = dbConn.GetDataTable(strSQL, dbConnection.Connenction.PitanjaPastiru);

            foreach (DataRow row in list.Rows)
            {
                returnList.Add(new PastirTopic(Convert.ToInt32(row["ID"]), row["Tema"].ToString()));
            }

            return returnList;
        }

    }

}
