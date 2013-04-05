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
}
