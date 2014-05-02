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
    public class CitanjaList : IZachala
    {
        private dbConnection dbConn = new dbConnection();

        public DnevnoCitanje GetDnevnoCitanje(int Mjesec, int Dan, int Godina)
        {
            int offs = dbConn.PokretniOffset(Mjesec, Dan, Godina);       // Vaskrs uvijek pada između 22. marta i 25. aprila

            string strSQL = @"SELECT RdType, Tekst FROM IzDanaUDan WHERE (Autor = 0) AND (OfsType = 1) AND (Offset  = " + offs.ToString() + ") ORDER BY RdType";


            DataTable list = dbConn.GetDataTable(strSQL, dbConnection.Connenction.PitanjaPastiru);

            DnevnoCitanje citanje = new DnevnoCitanje();
            foreach (DataRow row in list.Rows)
            {
                int rdType = Convert.ToInt32(row["RdType"]);
                string quote = row["Tekst"].ToString();
                if ((rdType == 1) && !string.IsNullOrEmpty(quote)) citanje.Vecernje = quote;
                else if ((rdType == 2) && !string.IsNullOrEmpty(quote)) citanje.Jutrenje = quote;
                else if ((rdType == 3) && !string.IsNullOrEmpty(quote)) citanje.LiturgijaJevandjelje = quote;
                else if ((rdType == 4) && !string.IsNullOrEmpty(quote)) citanje.LiturgijaApostoli = quote;
            }

            return citanje;
        }

    }
}
