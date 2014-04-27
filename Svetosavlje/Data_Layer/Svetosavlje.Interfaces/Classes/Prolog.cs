using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Svetosavlje.Interfaces.Classes
{
    public class PrologOther
    {
        public string Pjesma { get; set; }
        public string Rasudjivanje { get; set; }
        public string Sozercanje { get; set; }
        public string Besjeda { get; set; }
    }
    public class Prolog
    {
        public string ImeSveca { get; set; }
        public string ZitijaSveca { get; set; }

        public Prolog(string ime, string zitija)
        {
            ImeSveca = ime;
            ZitijaSveca = zitija;
        }
    }
    public class PrologSearchResults
    {
        public string DatumDisplay { get; set; }
        public DateTime Datum { get; set; }
        public string Naslov { get; set; }
        public string Tekst { get; set; }
        public bool OznaciPretragu { get; set; }

        public PrologSearchResults(int datum, string naslov, string tekst)
        {
            string d = datum.ToString();
            string day = d.Substring(3, 2);
            string month = d.Substring(1, 2);
            if (day == "29")
            {
                Datum = Convert.ToDateTime(month + "/" + day + "/2012"); // format datum as mm/dd/yyyy, 2012 was leap year
            }
            else
            {
                Datum = Convert.ToDateTime(month + "/" + day + "/" + DateTime.Now.Year.ToString()); // format datum as mm/dd/yyyy, 2012 was leap year
            }
            
            Naslov = naslov;
            Tekst = tekst;
        }
    }
}
