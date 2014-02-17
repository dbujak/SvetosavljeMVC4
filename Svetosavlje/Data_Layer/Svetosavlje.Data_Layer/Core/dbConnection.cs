using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MySql.Data;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace Svetosavlje.Data_Layer.Core
{
    class dbConnection
    {
        public Connenction Connection { get; set; }

        // "Mart",  "April",  "Maj",  "Jun",  "Jul",  "Avgust",  "Septembar",  "Oktobar",  "Novembar",  "Decembar",  "Januar",  "Februar" 
        public static readonly int[] martOffset = new int[] { 0, 31, 61, 92, 122, 153, 184, 214, 245, 275, 306, 337 };

        public int PokretniOffset(int Mjesec, int Dan, int Godina)
        {
            int offs = -1000;       // Vaskrs uvijek pada između 22. marta i 25. aprila

            int m = (Mjesec >= 3) ? Mjesec - 3 : Mjesec + 9;
            int do1m = martOffset[m] + Dan - 1;             // Dana Od 1-og Marta

            int vog = VaskrsOffset(Godina);        // broj dana od 1 marta do Vaskrsa
            int vpg = VaskrsOffset(Godina - 1);      // broj dana od 1 marta do Vaskrsa prosle godine

            int nedMitFar = vog + 365 - 70 + (((Godina % 4) == 0) ? 1 : 0);   // Nedjelja o Mitaru i Fariseju ove godine

            if (do1m >= nedMitFar)
            {                       //<Mjesec,Dan> je posle [Nedjelje o Mitaru i Fariseju] (a prije 1-og Marta), znaci pripada ovogodisnjem krugu ctenija
                offs = do1m - nedMitFar - 70;   // = do1m - vog - 365;
            }
            else if (do1m >= 306)
            {                       //<Mjesec,Dan> je izmedju [1. Jan] i (Nedjelje o Mitaru i Fariseju), znaci pripada proslogodisnjem krugu ctenija
                offs = do1m - vpg;
            }
            else
            {                       //<Mjesec,Dan> je posle [1-og Marta], znaci pripada ovogodisnjem krugu ctenija
                offs = do1m - vog;
            }

            return offs;
        }

        // broj dana od 1 marta do Vaskrsa
        public int VaskrsOffset(int Godina)
        {
            int a = (19 * (Godina % 19) + 15) % 30;
            int b = (2 * (Godina % 4) + 4 * (Godina % 7) + 6 * a + 6) % 7;
            return (21 + a + b);
        }

        public enum Connenction
        {
            ListaArhiva,
            Blogs,
            PitanjaPastiru,
            SvetiDana,
            IzDanaUDan
        }

        public DataTable GetDataTable(string strSQL, Connenction connection)
        {
            this.Connection = connection;

            return GetDataTable(strSQL);
        }

        public DataTable GetDataTable(string strSQL)
        {

            DataTable dataSet = new DataTable();

            using (MySqlConnection con =  MySqlConn())
            {
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(strSQL, con);
                dataAdapter.Fill(dataSet);
            }

            return dataSet;
        
        }

        public object GetScalar(string strSQL, Connenction connection)
        {
            this.Connection = connection;
            object scalar = null;

            using (MySqlConnection con = MySqlConn())
            {
                MySqlCommand cmd = new MySqlCommand(strSQL, con);
                con.Open();
                scalar = cmd.ExecuteScalar();
                con.Close();
            }

            return scalar;
        }

        private MySqlConnection MySqlConn()
        {
            string name = "";

            switch (this.Connection)
            { 
                case Connenction.ListaArhiva:
                    name = "ListaArhiva";
                    break;
                case Connenction.Blogs:
                    name = "Blogs";
                    break;
                case Connenction.PitanjaPastiru:
                    name = "PitanjaPastiru";
                    break;
                case Connenction.SvetiDana:
                    name = "SvetiDana";
                    break;
                case Connenction.IzDanaUDan:
                    name = "IzDanaUDan";
                    break;
                default:
                    break;
            }

            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];

            MySqlConnection newconn = new MySqlConnection(settings.ConnectionString);
            return newconn;
        }
    }


}
