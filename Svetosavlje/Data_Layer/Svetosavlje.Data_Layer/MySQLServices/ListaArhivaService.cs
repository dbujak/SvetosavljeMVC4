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
    public class ListaArhivaService : IListaArhiva
    {
        private dbConnection dbConn = new dbConnection();

        public IList<ListaArhiva> GetTopicList(int rows)
        {
            return GetTopicList(0, rows);
        }



        public IList<ListaArhiva> GetTopicList(int page, int rows)
        {
            IList<ListaArhiva> returnList = new List<ListaArhiva>();
            string strSQL = @"SELECT topics.ID, topics.naziv, count(messages.temaID) AS cnt, users.ime, topics.updated, users.email, topics.last_user
                  FROM topics 
                     INNER JOIN users  ON topics.last_user = users.ID 
                     INNER JOIN messages ON topics.ID = messages.temaID
                  GROUP BY messages.temaID 
                  ORDER BY topics.updated DESC ";
            strSQL += "LIMIT " + (page * rows).ToString() + "," + rows.ToString();

            DataTable list = dbConn.GetDataTable(strSQL, dbConnection.Connenction.ListaArhiva);

            foreach (DataRow row in list.Rows)
            {
                int msgId = Convert.ToInt32(row["ID"]);
                string title = row["naziv"].ToString();
                if (string.IsNullOrEmpty(title))
                    title = "(unknown title)";
                int count = Convert.ToInt32(row["cnt"]);
                string user = row["ime"].ToString();
                if (string.IsNullOrEmpty(user))
                    user = row["email"].ToString();
                DateTime updated = Convert.ToDateTime(row["updated"]);
                int updId = Convert.ToInt32(row["last_user"]);
                ListaArhiva ti = new ListaArhiva(msgId, title, count, user, updated, updId);
                returnList.Add(ti);
            }

            return returnList;
        }

        public int GetTotalTopics()
        {
            string strSQL = @"SELECT count(topics.ID) as cnt FROM topics";

            return Convert.ToInt32(dbConn.GetScalar(strSQL, dbConnection.Connenction.ListaArhiva));
        }
    }
}
