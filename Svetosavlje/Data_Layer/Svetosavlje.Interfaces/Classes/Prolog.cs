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
}
