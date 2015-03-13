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
    public class MolitveService : IMolitve
    {
        private dbConnection dbConn = new dbConnection();

        public IList<MolitveKateg> GetMolitveKategList()
        {
            List<MolitveKateg> returnList = new List<MolitveKateg>();

            //TODO: TESTIRAJ / izvuci iz baze listu kategorija molitvi
            //string strSQL = @"SELECT ID, naziv, redosled FROM molitve_kategorije_utf8 ORDER BY redosled";
            string strSQL = @"SELECT mk.ID, mk.naziv, mk.redosled, count(m.ID) as nrMolitvi 
                                    FROM molitve_kategorije_utf8 mk, molitve_utf8 m 
                                where mk.ID = m.kategorija group by mk.ID, mk.naziv, mk.redosled order by mk.redosled;";

            DataTable list = dbConn.GetDataTable(strSQL, dbConnection.Connenction.PitanjaPastiru);

            foreach (DataRow row in list.Rows)
            {
                MolitveKateg oMolitveKateg = new MolitveKateg(Convert.ToInt16(row["ID"].ToString()), row["naziv"].ToString(), Convert.ToInt16(row["redosled"].ToString()), 
                                                              Convert.ToInt32(row["nrMolitvi"].ToString()));
                returnList.Add(oMolitveKateg);
            }

            return returnList;
        }

        public IList<Molitva> GetMolitveList()
        {
            List<Molitva> returnList = new List<Molitva>();

            //TODO:  TESTIRAJ / izvuci iz baze listu molitvi

            string strSQL = @"SELECT m.ID, m.naslov, m.molitva, m.kategorija, m.url_ka_molitvi FROM molitve_utf8 m, molitve_kategorije_utf8 mk where m.kategorija = mk.ID ORDER BY mk.redosled, m.ID;";

            DataTable list = dbConn.GetDataTable(strSQL, dbConnection.Connenction.PitanjaPastiru);

            foreach (DataRow row in list.Rows)
            {
                Molitva oMolitva = new Molitva(Convert.ToInt32(row["ID"].ToString()), row["naslov"].ToString(), row["molitva"].ToString(), Convert.ToInt16(row["kategorija"].ToString()), row["url_ka_molitvi"].ToString());
                returnList.Add(oMolitva);
            }

            return returnList;
        }

        public IList<Molitva> GetMolitveList(int nKateg)
        {
            List<Molitva> returnList = new List<Molitva>();

            //TODO:  TESTIRAJ / izvuci iz baze listu molitvi

            string strSQL = @"SELECT m.ID, m.naslov, m.molitva, m.kategorija, m.url_ka_molitvi 
                                     FROM molitve_utf8 m, molitve_kategorije_utf8 mk 
                                where m.kategorija = "+ nKateg +" ORDER BY mk.redosled, m.ID;";

            DataTable list = dbConn.GetDataTable(strSQL, dbConnection.Connenction.PitanjaPastiru);

            foreach (DataRow row in list.Rows)
            {
                Molitva oMolitva = new Molitva(Convert.ToInt32(row["ID"].ToString()), row["naslov"].ToString(), row["molitva"].ToString(), Convert.ToInt16(row["kategorija"].ToString()), row["url_ka_molitvi"].ToString());
                returnList.Add(oMolitva);
            }

            return returnList;
        }

        public Molitva GetMolitva(int nMolitvaId)
        {
            string strSQL = @"SELECT m.ID, m.naslov, m.molitva, m.kategorija, m.url_ka_molitvi 
                                     FROM molitve_utf8 m
                                where m.ID = "+ nMolitvaId +" ;";
            DataTable list = dbConn.GetDataTable(strSQL, dbConnection.Connenction.PitanjaPastiru);
            DataRow row = list.Rows[0];
            Molitva oMolitva = new Molitva(Convert.ToInt32(row["ID"].ToString()), row["naslov"].ToString(), row["molitva"].ToString(), Convert.ToInt16(row["kategorija"].ToString()), row["url_ka_molitvi"].ToString());
            return oMolitva;
        }
    }
}
