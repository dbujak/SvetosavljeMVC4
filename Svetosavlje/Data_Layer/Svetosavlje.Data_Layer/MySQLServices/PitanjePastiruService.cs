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
    public class PitanjePastiruService : IQuestionList, IPastirTopicsList, IPastirQuestion
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

        public PitanjeInfo GetPastirQuestion(int questionID)
        {

            PitanjeInfo pitanje;

            string strSQL = @"SELECT p.ID, p.NaslovPitanja, p.Pitanje, p.Odgovor, t.ID TemaID, t.Tema, p.Ime FROM pp_pitanja_utf8 p Inner Join pp_teme_utf8 t On p.TemaID=t.ID Where p.ID=" + questionID.ToString() + ";";


            DataTable list = dbConn.GetDataTable(strSQL, dbConnection.Connenction.PitanjaPastiru);

            if (list.Rows.Count > 0)
            {
                DataRow row = list.Rows[0];
                pitanje = new PitanjeInfo(Convert.ToInt32(row["ID"]), row["NaslovPitanja"].ToString(),
                        row["Pitanje"].ToString(), row["Odgovor"].ToString(), Convert.ToInt32(row["TemaID"]), row["Tema"].ToString(), row["Ime"].ToString());
            }
            else return null;

            return pitanje;
        }

    }
}
