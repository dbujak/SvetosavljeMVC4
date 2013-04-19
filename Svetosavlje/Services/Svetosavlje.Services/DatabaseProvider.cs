using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Svetosavlje.Data_Layer.Interfaces;
using Svetosavlje.Data_Layer;

namespace Svetosavlje.Services
{
    public class DatabaseProvider : IDataProvider
    {
        private IDataProvider _provider = new MySQLProvider();

        public IList<MailListTopicInfo> GetTopicList(int rows)
        {
            return _provider.GetTopicList(rows);
        }




        public IList<PitanjeInfo> GetQuestionList(int topicID, int rows)
        {
            return _provider.GetQuestionList(topicID, rows);
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




        public IList<PastirTopic> GetPastirTopicList()
        {
            return _provider.GetPastirTopicList();
        }



        public PitanjeInfo GetPastirQuestion(int questionID)
        {
            return _provider.GetPastirQuestion(questionID);
        }

    }
}
