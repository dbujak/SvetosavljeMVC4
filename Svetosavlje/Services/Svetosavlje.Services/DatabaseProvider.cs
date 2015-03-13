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

        public IList<MessageThread> GetMessageThreads(int rows)
        {
            return _provider.GetMessageThreads(rows);
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



        public DnevnoCitanje GetDnevnoCitanje(int Mjesec, int Dan, int Godina)
        {
            return _provider.GetDnevnoCitanje(Mjesec, Dan, Godina);
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



        public IList<MessageThread> GetMessageThreads(int page, int rows)
        {
            return _provider.GetMessageThreads(page,rows);
        }

        public int GetTotalTopics()
        {
            return _provider.GetTotalTopics();
        }

        public IList<TopicMessage> GetTopicMessages(int topicID)
        {
            return _provider.GetTopicMessages(topicID);
        }

        public IList<PrologSearchResults> GetPrologSearchResutls(string keyword)
        {
            return _provider.GetPrologSearchResutls(keyword);
        }

        public IList<MolitveKateg> GetMolitveKategList()
        {
            return _provider.GetMolitveKategList();
        }

        public IList<Molitva> GetMolitveList()
        {
            return _provider.GetMolitveList();
        }

        public IList<Molitva> GetMolitveList(int nKateg)
        {
            return _provider.GetMolitveList(nKateg);
        }

        public Molitva GetMolitva(int nMolitvaId)
        {
            return _provider.GetMolitva(nMolitvaId);
        }
    }
}
