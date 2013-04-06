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
    public class TopicsList : ITopicList
    {
        private dbConnection dbConn = new dbConnection();

        public IList<MailListTopicInfo> GetTopicList(int rows)
        {
            IList<MailListTopicInfo> returnList = new List<MailListTopicInfo>();
            string strSQL = @"SELECT topics.ID, topics.naziv, count(messages.temaID) AS cnt, users.ime, topics.updated, users.email, topics.last_user
                  FROM topics 
                     INNER JOIN users  ON topics.last_user = users.ID 
                     INNER JOIN messages ON topics.ID = messages.temaID
                  GROUP BY messages.temaID 
                  ORDER BY topics.updated DESC ";
            strSQL += "LIMIT " + rows.ToString();

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
                MailListTopicInfo ti = new MailListTopicInfo(msgId, title, count, user, updated, updId);
                returnList.Add(ti);
            }

            return returnList;
        }

    }
}
