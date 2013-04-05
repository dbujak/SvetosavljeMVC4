using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Svetosavlje.Services.Interfaces;
using Svetosavlje.Data_Layer;

namespace Svetosavlje.Services
{
    class MySQLProvider : IDataProvider
    {
        #region IDataProvider Members

        public IList<MailListTopicInfo> TopicList(int rows)
        {
            throw new NotImplementedException();
            //            mySqlConn.Open();

            //            MySqlCommand cmd = mySqlConn.CreateCommand();
            //            cmd.CommandType = System.Data.CommandType.Text;
            //            cmd.CommandText =
            //                @"SELECT topics.ID, topics.naziv, count(messages.temaID) AS cnt, users.ime, topics.updated, users.email, topics.last_user
            //                  FROM topics 
            //                     INNER JOIN users  ON topics.last_user = users.ID 
            //                     INNER JOIN messages ON topics.ID = messages.temaID
            //                  GROUP BY messages.temaID 
            //                  ORDER BY topics.updated DESC ";
            //            string limit = "LIMIT " + rows.ToString();
            //            cmd.CommandText += limit;


            //            List<MailListTopicInfo> topicList = new List<MailListTopicInfo>(33);

            //            MySqlDataReader dr = cmd.ExecuteReader();
            //            // SELECT topics.ID, topics.naziv, count(messages.temaID) AS cnt, users.ime, topics.updated
            //            while (dr.Read())
            //            {
            //                int msgId = dr.GetInt32(0);
            //                string title = dr.GetString(1);
            //                if (string.IsNullOrEmpty(title))
            //                    title = "(unknown title)";
            //                int count = dr.GetInt32(2);
            //                string user = dr.GetString(3);
            //                if (string.IsNullOrEmpty(user))
            //                    user = dr.GetString(5);
            //                DateTime updated = dr.GetDateTime(4);
            //                int updId = dr.GetInt32(6);
            //                MailListTopicInfo ti = new MailListTopicInfo(msgId, title, count, user, updated, updId);
            //                topicList.Add(ti);
            //            }

            //            dr.Close();

            //            mySqlConn.Close();

            //            return topicList;

        }

        #endregion

        #region IDataProvider Members


        public IList<PitanjeInfo> QuestionList(int rows)
        {
            throw new NotImplementedException();
//            mySqlConn.Open();

//            MySqlCommand setNames = mySqlConn.CreateCommand();
//            setNames.CommandType = System.Data.CommandType.Text;
//            setNames.CommandText = "SET NAMES utf8";
//            setNames.ExecuteNonQuery();

//            List<PitanjeInfo> questionList = new List<PitanjeInfo>();

//            MySqlCommand cmd = mySqlConn.CreateCommand();
//            cmd.CommandType = System.Data.CommandType.Text;
//            cmd.CommandText =
//                @"SELECT RedniBroj, NaslovPitanja
//                  FROM pp_pitanja_utf8 
//                  WHERE (StanjeID=3) 
//                  ORDER BY RedniBroj DESC ";
//            string limit = "LIMIT " + rows.ToString();
//            cmd.CommandText += limit;

//            MySqlDataReader dr = cmd.ExecuteReader();   // SELECT RedniBroj, NaslovPitanja
//            while (dr.Read())
//            {
//                questionList.Add(new PitanjeInfo(dr.GetInt32(0), dr.GetString(1)));
//            }
//            dr.Close();

//            mySqlConn.Close();

//            return questionList;

        }

        #endregion



        #region IDataProvider Members


        public IList<string> GetList(int Mjesec, int Dan)
        {
            throw new NotImplementedException();
            //mySqlConn.Open();

            //MySqlCommand cmd = mySqlConn.CreateCommand();
            //cmd.CommandType = System.Data.CommandType.Text;
            //cmd.CommandText = @"SELECT ime FROM prolog_zitija_utf8 WHERE (datum = 1" + Mjesec.ToString("D2") + Dan.ToString("D2") + ") ORDER BY br";

            //List<string> topicList = new List<string>(33);

            //MySqlDataReader dr = cmd.ExecuteReader();
            //while (dr.Read())
            //{
            //    topicList.Add(dr.GetString(0));
            //}
            //dr.Close();
            //mySqlConn.Close();

            //return topicList;
        }

        #endregion

        #region IDataProvider Members


        public string GetQuote(int Autor, int Mjesec, int Dan)
        {
            throw new NotImplementedException();
            //mySqlConn.Open();

            //int m = (Mjesec >= 3) ? Mjesec - 3 : Mjesec + 9;
            //int do1m = martOffset[m] + Dan - 1;

            //MySqlCommand cmd = mySqlConn.CreateCommand();
            //cmd.CommandType = System.Data.CommandType.Text;
            //cmd.CommandText = @"SELECT Tekst FROM IzDanaUDan WHERE (Autor = " + Autor.ToString() + ") AND (OfsType = 0) AND (Offset  = " + do1m.ToString() + ") AND (RdType = 0)";

            //string quote = "";

            //MySqlDataReader dr = cmd.ExecuteReader();
            //if (dr.Read())
            //{
            //    quote = dr.GetString(0);
            //}
            //dr.Close();
            //mySqlConn.Close();

            //return quote;

        }

        #endregion

        #region IDataProvider Members


        public string GetFastingType(int Mjesec, int Dan)
        {
            throw new NotImplementedException();
            //mySqlConn.Open();

            //int m = (Mjesec >= 3) ? Mjesec - 3 : Mjesec + 9;
            //int do1m = martOffset[m] + Dan - 1;

            //MySqlCommand cmd = mySqlConn.CreateCommand();
            //cmd.CommandType = System.Data.CommandType.Text;
            //cmd.CommandText = @"SELECT Tekst FROM IzDanaUDan WHERE (Autor = 999) AND (OfsType = 0) AND (Offset  = " + do1m.ToString() + ") AND (RdType = 0)";

            //string fastingType = "";

            //MySqlDataReader dr = cmd.ExecuteReader();
            //if (dr.Read())
            //{
            //    fastingType = dr.GetString(0);
            //}
            //dr.Close();
            //mySqlConn.Close();

            //return fastingType;

        }

        #endregion

        #region IDataProvider Members


        public string GetZachala(int Mjesec, int Dan, int Godina)
        {
            throw new NotImplementedException();
            //int offs = PokretniOffset(Mjesec, Dan, Godina);       // Vaskrs uvijek pada između 22. marta i 25. aprila

            //mySqlConn.Open();

            //MySqlCommand cmd = mySqlConn.CreateCommand();
            //cmd.CommandType = System.Data.CommandType.Text;
            //cmd.CommandText = @"SELECT RdType, Tekst FROM IzDanaUDan WHERE (Autor = 0) AND (OfsType = 1) AND (Offset  = " + offs.ToString() + ") ORDER BY RdType";

            //string ctenije = "";

            //MySqlDataReader dr = cmd.ExecuteReader();
            //while (dr.Read())
            //{
            //    int rdType = dr.GetInt16(0);
            //    string quote = dr.GetString(1);

            //    if ((rdType == 1) && !string.IsNullOrEmpty(quote)) ctenije += "На вечерњи:<br />" + quote;
            //    else if ((rdType == 2) && !string.IsNullOrEmpty(quote)) ctenije += "На јутрењу:<br />" + quote;
            //    else if ((rdType == 3) && !string.IsNullOrEmpty(quote)) ctenije += "На Св. Литургији, Еванђеље:<br />" + quote;
            //    else if ((rdType == 4) && !string.IsNullOrEmpty(quote)) ctenije += "На Св. Литургији, Апостол:<br />" + quote;
            //}
            //dr.Close();
            //mySqlConn.Close();

            //return ctenije;

        }

        #endregion

        #region IDataProvider Members


        public IList<WPBlogModel> GetNews(int rows)
        {
            throw new NotImplementedException();
//            mySqlConn.Open();

//            MySqlCommand cmd = mySqlConn.CreateCommand();
//            cmd.CommandType = System.Data.CommandType.Text;
//            cmd.CommandText =
//                @"SELECT post_content, post_title, guid 
//                  FROM wp_28_posts 
//                  WHERE post_status = 'publish' 
//                  ORDER BY ID DESC ";
//            string limit = "LIMIT " + rows.ToString();
//            cmd.CommandText += limit;

//            List<WPBlogModel> newsList = new List<WPBlogModel>(rows);

//            MySqlDataReader dr = cmd.ExecuteReader();
//            while (dr.Read())
//            {
//                string content = dr.GetString(0);
//                if (string.IsNullOrEmpty(content))
//                    content = "(unknown content)";
//                string title = dr.GetString(1);
//                if (string.IsNullOrEmpty(title))
//                    title = "(unknown title)";
//                string link = dr.GetString(2);
//                if (string.IsNullOrEmpty(link))
//                    link = "(unknown link)";
//                WPBlogModel v = new WPBlogModel(new HtmlString(title), new HtmlString(content), link);
//                newsList.Add(v);
//            }

//            dr.Close();

//            mySqlConn.Close();

//            return newsList;

        }

        #endregion
    }
}
