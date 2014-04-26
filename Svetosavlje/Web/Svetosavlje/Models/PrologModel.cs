using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Svetosavlje.Interfaces.Interfaces;
using Svetosavlje.Interfaces.Classes;

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
        public string ZitijaLink { get; set; }
        public IList<string> SearchResults { get; set; }
    }
}