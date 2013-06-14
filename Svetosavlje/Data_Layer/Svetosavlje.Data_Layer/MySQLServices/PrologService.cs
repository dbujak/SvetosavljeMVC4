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
    public class PrologService : ISaintNamesList, ISaintNamesAndLivesList, IProlog
    {
        private dbConnection dbConn = new dbConnection();

        public IList<string> GetSaintNamesList(int Mjesec, int Dan)
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


        public IList<Prolog> GetSaintNamesAndLivesList(int Mjesec, int Dan)
        {
            List<Prolog> returnList = new List<Prolog>();

            string strSQL = @"SELECT ime, zitije FROM prolog_zitija_utf8 WHERE (datum = 1" + Mjesec.ToString("D2") + Dan.ToString("D2") + ") ORDER BY br";

            DataTable list = dbConn.GetDataTable(strSQL, dbConnection.Connenction.PitanjaPastiru);

            foreach (DataRow row in list.Rows)
            {
                Prolog prolog = new Prolog(row["ime"].ToString(), row["zitije"].ToString());
                returnList.Add(prolog);
            }

            return returnList;
        }



        public PrologOther GetProlog(int Mjesec, int Dan)
        {
            PrologOther prolog = new PrologOther();

            string strSQL = @"SELECT pjesma, rasudjivanje, sozercanje, besjeda FROM prolog_utf8 WHERE (datum = 1" + Mjesec.ToString("D2") + Dan.ToString("D2") + ")";

            DataTable list = dbConn.GetDataTable(strSQL, dbConnection.Connenction.PitanjaPastiru);

            prolog.Pjesma = list.Rows[0]["pjesma"].ToString();
            prolog.Rasudjivanje = list.Rows[0]["rasudjivanje"].ToString();
            prolog.Sozercanje = list.Rows[0]["sozercanje"].ToString();
            prolog.Besjeda = list.Rows[0]["besjeda"].ToString();


            return prolog;
        }

    }
}
