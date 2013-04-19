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
    public class QuestionsList : IQuestionList
    {
        private dbConnection dbConn = new dbConnection();

        public IList<PitanjeInfo> GetQuestionList(int topicID, int rows)
        {
            IList<PitanjeInfo> returnList = new List<PitanjeInfo>();
    
            string strSQL = "";

            if (topicID == 0)
            {
                strSQL = @"SET NAMES utf8;
                  SELECT ID, NaslovPitanja
                  FROM pp_pitanja_utf8 
                  WHERE (StanjeID=3) 
                  ORDER BY RedniBroj DESC ";
            }
            else
            {
                strSQL = @"SET NAMES utf8;
                  SELECT ID, NaslovPitanja
                  FROM pp_pitanja_utf8 
                  WHERE (StanjeID=3 And TemaID=" + topicID.ToString() + @") 
                  ORDER BY RedniBroj DESC ";
            }

            if (rows > 0) strSQL += "LIMIT " + rows.ToString();

            DataTable list = dbConn.GetDataTable(strSQL, dbConnection.Connenction.PitanjaPastiru);

            foreach (DataRow row in list.Rows)
            {
                returnList.Add(new PitanjeInfo(Convert.ToInt32(row["ID"]), row["NaslovPitanja"].ToString()));
            }

            return returnList;
        }

    }
}
