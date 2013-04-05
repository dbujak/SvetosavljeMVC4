using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Svetosavlje.Data_Layer;
using Svetosavlje.Services;


namespace Svetosavlje.Models
{



    public class ListArhiva
    {
        private DatabaseProvider _provider = new DatabaseProvider();

        public IList<MailListTopicInfo> TopicList(int rows)
        {
            return _provider.TopicList(rows);
        }
    }



    public class Blogs
    {
        private DatabaseProvider _provider = new DatabaseProvider();

        public IList<WPBlogModel> GetNews(int rows)
        {
            return _provider.GetNews(rows);
        }
    }







    public class PitanjaPastiru
    {
        private DatabaseProvider _provider = new DatabaseProvider();

        public IList<PitanjeInfo> QuestionList(int rows)
        {
            return _provider.QuestionList(rows);
        }
    }



    public class SvetiDana
    {
        private DatabaseProvider _provider = new DatabaseProvider();

        public IList<string> GetList(int Mjesec, int Dan)
        {
            return _provider.GetList(Mjesec, Dan);
        }
    }



    public class IzDanaUDan
    {
        // "Mart",  "April",  "Maj",  "Jun",  "Jul",  "Avgust",  "Septembar",  "Oktobar",  "Novembar",  "Decembar",  "Januar",  "Februar" 
        static readonly int[] martOffset = new int[] { 0, 31, 61, 92, 122, 153, 184, 214, 245, 275, 306, 337 };

        private DatabaseProvider _provider = new DatabaseProvider();

        public string GetQuote(int Autor, int Mjesec, int Dan)
        {
            return _provider.GetQuote(Autor, Mjesec, Dan);
        }

        public string GetFastingType(int Mjesec, int Dan)
        {
            return _provider.GetFastingType(Mjesec, Dan);
        }


        // broj dana od 1 marta do Vaskrsa
        public int VaskrsOffset(int Godina)
        {
            int a = (19 * (Godina % 19) + 15) % 30;
            int b = (2 * (Godina % 4) + 4 * (Godina % 7) + 6 * a + 6) % 7;
            return (21 + a + b);
        }


        //
        // Broj dana od Vaskrsa za tekuci godisnji ciklus.
        //
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


        public string GetZachala(int Mjesec, int Dan, int Godina)
        {
            return _provider.GetZachala(Mjesec, Dan, Godina);
        }
    }




    public class Main
    {
        public List<WPBlogModel> Vijesti;
        public List<WPBlogModel> Misija;
        public List<WPBlogModel> Urednistvo;
        public IList<MailListTopicInfo> Lista;
        public IList<PitanjeInfo> Pastir;
        public IList<string> DnevniSveti;
        public string DailyQuote;
        public string DailyReading;
        public string DailyFasting;

        public Main(List<WPBlogModel> v, List<WPBlogModel> m, List<WPBlogModel> u, IList<MailListTopicInfo> l, IList<PitanjeInfo> p, IList<string> s, string q, string r, string f)
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