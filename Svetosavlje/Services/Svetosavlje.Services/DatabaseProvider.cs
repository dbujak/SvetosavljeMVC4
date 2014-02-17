using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Svetosavlje.Interfaces.Interfaces;
using Svetosavlje.Interfaces.Classes;
using Svetosavlje.Data_Layer;

namespace Svetosavlje.Services
{
    public class DatabaseProvider : IDataProvider
    {
        private IDataProvider _provider = new MySQLProvider();
        //private IDataProvider _provider = new DummyDatabaseProvider();

        public IList<ListaArhiva> GetTopicList(int rows)
        {
            return _provider.GetTopicList(rows);
        }




        public IList<PitanjeInfo> GetQuestionList(int topicID, int rows)
        {
            return _provider.GetQuestionList(topicID, rows);
        }



        public IList<string> GetSaintNamesList(int Mjesec, int Dan)
        {
            return _provider.GetSaintNamesList(Mjesec, Dan);
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


        public IList<Prolog> GetSaintNamesAndLivesList(int Mjesec, int Dan)
        {
            return _provider.GetSaintNamesAndLivesList(Mjesec, Dan);
        }

        public PrologOther GetProlog(int Mjesec, int Dan)
        {
            return _provider.GetProlog(Mjesec, Dan);
        }



        public IList<ListaArhiva> GetTopicList(int page, int rows)
        {
            return _provider.GetTopicList(page,rows);
        }

        public int GetTotalTopics()
        {
            return _provider.GetTotalTopics();
        }
    }
}
