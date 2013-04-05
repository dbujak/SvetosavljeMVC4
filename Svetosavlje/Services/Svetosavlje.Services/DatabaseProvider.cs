using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Svetosavlje.Services.Interfaces;
using Svetosavlje.Data_Layer;

namespace Svetosavlje.Services
{
    public class DatabaseProvider : IDataProvider
    {
        private IDataProvider _provider = new DummyDataProvider();

        public IList<MailListTopicInfo> TopicList(int rows)
        {
            return _provider.TopicList(rows);
        }




        public IList<PitanjeInfo> QuestionList(int rows)
        {
            return _provider.QuestionList(rows);
        }



        public IList<string> GetList(int Mjesec, int Dan)
        {
            return _provider.GetList(Mjesec, Dan);
        }




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


        public IList<WPBlogModel> GetNews(int rows)
        {
            return _provider.GetNews(rows);
        }
    }
}
