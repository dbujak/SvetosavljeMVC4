using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Svetosavlje.Data_Layer;

namespace Svetosavlje.Models
{
    public class PrologModel
    {
        public IList<Prolog> Sveti { get; set; }
        public string Pjesma { get; set; }
        public string Rasudjivanje { get; set; }
        public string Sozercanje { get; set; }
        public string Besjeda { get; set; }
        public string Datum { get; set; } // so I can format it to Serbian date - dd/mm/yyyy
        public string Dan { get; set; }
        public string Mjesec { get; set; }
    }
}