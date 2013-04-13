using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Svetosavlje.Data_Layer;
using Svetosavlje.Services;
using Svetosavlje.Data_Layer.Interfaces;

namespace Svetosavlje.Models
{



    public class ListArhiva : ITopicList
    {
        private DatabaseProvider _provider = new DatabaseProvider();

        public IList<MailListTopicInfo> GetTopicList(int rows)
        {
            return _provider.GetTopicList(rows);
        }

    }

    public class Blogs : IBlogProvider
    {
        private BlogProvider _provider = new BlogProvider();


        public IList<WPBlogModel> GetNews()
        {
            return _provider.GetNews();
        }

        public IList<WPBlogModel> GetEditorNews()
        {
            return _provider.GetEditorNews();
        }

        public IList<WPBlogModel> GetMissionNews()
        {
            return _provider.GetMissionNews();
        }
    }



    public class SvetiDana : IGetList
    {
        private DatabaseProvider _provider = new DatabaseProvider();

        public IList<string> GetList(int Mjesec, int Dan)
        {
            return _provider.GetList(Mjesec, Dan);
        }

    }

    public class IzDanaUDan : IQuote, IFastingType, IZachala
    {

        private DatabaseProvider _provider = new DatabaseProvider();

        public string GetQuote(int Autor, int Mjesec, int Dan)
        {
            return _provider.GetQuote(Autor, Mjesec, Dan);
        }

        public string GetFastingType(int Mjesec, int Dan)
        {
            return _provider.GetFastingType(Mjesec, Dan);
        }

        public string GetZachala(int Mjesec, int Dan, int Godina)
        {
            return _provider.GetZachala(Mjesec, Dan, Godina);
        }

    }




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