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

        public IList<PitanjeInfo> GetQuestionList(int rows)
        {
            IList<PitanjeInfo> returnList = new List<PitanjeInfo>();
    
            string strSQL = @"SET NAMES utf8;
                  SELECT RedniBroj, NaslovPitanja
                  FROM pp_pitanja_utf8 
                  WHERE (StanjeID=3) 
                  ORDER BY RedniBroj DESC ";
            strSQL += "LIMIT " + rows.ToString();

            DataTable list = dbConn.GetDataTable(strSQL, dbConnection.Connenction.PitanjaPastiru);

            foreach (DataRow row in list.Rows)
            {
                returnList.Add(new PitanjeInfo(Convert.ToInt32(row["RedniBroj"]), row["NaslovPitanja"].ToString()));
            }

            return returnList;
        }

    }
}
