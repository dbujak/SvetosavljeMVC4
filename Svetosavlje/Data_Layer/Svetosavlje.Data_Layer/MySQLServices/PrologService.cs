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
    public class PrologService : IProlog
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


        public IList<PrologSearchResults> GetPrologSearchResutls(string keyword)
        {
            string strSQL = @"SELECT datum, 'ime' as whereFound, ime as searchField FROM svetosavlje_org.prolog_zitija_utf8 Where ime like @keyword " +
                "union Select datum, 'zitije' as whereFound, zitije as searchField from svetosavlje_org.prolog_zitija_utf8 Where zitije like @keyword " +
                "union select datum, 'pjesma' as whereFound, pjesma as searchField from svetosavlje_org.prolog_utf8 Where pjesma like @keyword " +
                "union select datum, 'rasudjivanje' as whereFound, rasudjivanje as searchField from svetosavlje_org.prolog_utf8 Where rasudjivanje like @keyword " +
                "union select datum, 'sozercanje' as whereFound, sozercanje as searchField from svetosavlje_org.prolog_utf8 Where sozercanje like @keyword " +
                "union select datum, 'besjeda' as whereFound, besjeda as searchField from svetosavlje_org.prolog_utf8 Where besjeda like @keyword";

            IList<Parameter> parameters = new List<Parameter>();
            parameters.Add(new Parameter("@keyword", "%" + keyword + "%"));
            DataTable searchRes = dbConn.GetDataTable(strSQL, dbConnection.Connenction.PitanjaPastiru, parameters);

            IList<PrologSearchResults> returnSearch = new List<PrologSearchResults>();
            foreach (DataRow row in searchRes.Rows)
            {
                if (row["datum"].ToString().Length == 5)
                {
                    PrologSearchResults searchResult = new PrologSearchResults(Convert.ToInt16(row["datum"]), row["whereFound"].ToString(), row["searchField"].ToString());
                    returnSearch.Add(searchResult);
                }
            }
            return returnSearch;
        }
    }
}
