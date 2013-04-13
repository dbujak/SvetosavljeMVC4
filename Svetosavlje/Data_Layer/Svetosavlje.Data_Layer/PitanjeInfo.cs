using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Svetosavlje.Data_Layer
{
    public class PitanjeInfo
    {
        public int Broj;
        //        public HtmlString Naslov;
        public string Naslov;

        public PitanjeInfo(int b, /*HtmlString*/string n)
        {
            Broj = b;
            Naslov = n;
        }

    }

    /// <summary>
    /// Represents one Topic under "Pitanja Pastiru"
    /// </summary>
    public class PastirTopic
    {
        public int ID { get; set; }
        public string Title { get; set; }

        public PastirTopic(int id, string title)
        {
            ID = id;
            Title = title;
        }
    }
}
