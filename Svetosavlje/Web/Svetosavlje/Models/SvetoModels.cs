using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Svetosavlje.Data_Layer;

namespace Svetosavlje.Models
{



    public class Main
    {
        public IList<WPBlogModel> Vijesti;
        public IList<WPBlogModel> Misija;
        public IList<WPBlogModel> Urednistvo;
        public IList<MailListTopicInfo> Lista;
        public IList<PitanjeInfo> Pastir;
        public IList<string> DnevniSveti;
        public string DailyQuote;
        public string DailyReading;
        public string DailyFasting;

        public Main(IList<WPBlogModel> v, IList<WPBlogModel> m, IList<WPBlogModel> u, IList<MailListTopicInfo> l, IList<PitanjeInfo> p, IList<string> s, string q, string r, string f)
        {
            Vijesti = v;
            Misija = m;
            Urednistvo = u;
            Lista = l;
            Pastir = p;
            DnevniSveti = s;
            DailyQuote = q;
            DailyReading = r;
            DailyFasting = f;
        }
    }

}