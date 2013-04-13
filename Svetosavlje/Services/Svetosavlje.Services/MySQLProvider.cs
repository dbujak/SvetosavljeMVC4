using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Svetosavlje.Data_Layer.Interfaces;
using Svetosavlje.Data_Layer;
using Svetosavlje.Data_Layer.Core;
using Svetosavlje.Data_Layer.MySQLServices;

namespace Svetosavlje.Services
{
    class MySQLProvider : IDataProvider
    {
        

        public IList<MailListTopicInfo> GetTopicList(int rows)
        {
            TopicsList list = new TopicsList();

            return list.GetTopicList(rows);

        }

        public IList<PitanjeInfo> GetQuestionList(int rows)
        {
            QuestionsList list = new QuestionsList();

            return list.GetQuestionList(rows);

        }

        public IList<string> GetList(int Mjesec, int Dan)
        {
            ZitijasList list = new ZitijasList();

            return list.GetList(Mjesec, Dan);
        }

        public string GetQuote(int Autor, int Mjesec, int Dan)
        {
            Quote quote = new Quote();

            return quote.GetQuote(Autor, Mjesec, Dan);
        }

        public string GetFastingType(int Mjesec, int Dan)
        {
            FastingType fast = new FastingType();

            return fast.GetFastingType(Mjesec, Dan);

        }

        public string GetZachala(int Mjesec, int Dan, int Godina)
        {
            CitanjaList list = new CitanjaList();

            return list.GetZachala(Mjesec, Dan, Godina);

        }




        public IList<PastirTopic> GetPastirTopicList()
        {
            PastirTopicsList list = new PastirTopicsList();

            return list.GetPastirTopicList();
        }

    }
}
