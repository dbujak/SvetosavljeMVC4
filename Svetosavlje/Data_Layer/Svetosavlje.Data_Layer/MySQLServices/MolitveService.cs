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
            string strSQL = @"SELECT naziv, redosled FROM molitve_kategorije_utf8 ORDER BY redosled";

            DataTable list = dbConn.GetDataTable(strSQL, dbConnection.Connenction.PitanjaPastiru);

            foreach (DataRow row in list.Rows)
            {
                MolitveKateg oMolitveKateg = new MolitveKateg(row["naziv"].ToString(), Convert.ToInt16(row["redosled"].ToString()));
                returnList.Add(oMolitveKateg);
            }

            return returnList;
        }

        public IList<Molitve> GetMolitveList()
        {
            List<Molitve> returnList = new List<Molitve>();

            //TODO:  TESTIRAJ / izvuci iz baze listu molitvi

            string strSQL = @"SELECT naslov, molitva, kategorija, url_ka_molitvi FROM molitve_kategorije_utf8 ORDER BY kategorija";

            DataTable list = dbConn.GetDataTable(strSQL, dbConnection.Connenction.PitanjaPastiru);

            foreach (DataRow row in list.Rows)
            {
                Molitve oMolitve = new Molitve(row["naslov"].ToString(), row["molitva"].ToString(), Convert.ToInt16(row["kategorija"].ToString()), row["url_ka_molitvi"].ToString());
                returnList.Add(oMolitve);
            }

            return returnList;
        }

    }
}
