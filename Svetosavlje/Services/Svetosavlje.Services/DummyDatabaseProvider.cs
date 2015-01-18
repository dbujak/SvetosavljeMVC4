using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Svetosavlje.Interfaces.Interfaces;
using Svetosavlje.Interfaces.Classes;
using Svetosavlje.Data_Layer;
using System.Web;

namespace Svetosavlje.Services
{
    class DummyDatabaseProvider : IDataProvider
    {

        public IList<MessageThread> GetMessageThreads(int rows)
        {
            IList<MessageThread> topicList = new List<MessageThread>(rows);

            for (int i = 1; i <= rows; i++ )
            {
                int msgId = i;
                string title = "Title " + i.ToString();
                int count = i;
                string user = "User " + i.ToString();
                DateTime updated = DateTime.Now;
                int updId = i;
                MessageThread ti = new MessageThread(msgId, title, count, user, updated, updId);
                topicList.Add(ti);
            }

            return topicList;        
        }



        public IList<PitanjeInfo> GetQuestionList(int topicID, int rows)
        {

            IList<PitanjeInfo> questionList = new List<PitanjeInfo>(rows);

            for (int i = 1; i <= rows; i++)
            {
                questionList.Add(new PitanjeInfo(i, "Naslov " + i.ToString()));
            }

            return questionList;
        }



        public IList<string> GetSaintNamesList(int Mjesec, int Dan)
        {

            List<string> topicList = new List<string>();

            topicList.Add("Светац 1");
            topicList.Add("Светац 2");
            topicList.Add("Светац 3");
            topicList.Add("Светац 4");
            
            return topicList;
        }




        public string GetQuote(int Autor, int Mjesec, int Dan)
        {

            string quote = "Поука светих отаца за данашњи дан " + DateTime.Now.ToShortDateString();

            return quote;

        }


        public string GetFastingType(int Mjesec, int Dan)
        {
            return "Данас се пости на води";
        }



        public DnevnoCitanje GetDnevnoCitanje(int Mjesec, int Dan, int Godina)
        {
            DnevnoCitanje citanje = new DnevnoCitanje();

            citanje.CompactVersion = "Сажета верзија за главну страницу";
            citanje.FullVersion = "Пуна верзија за страницу Дневно читање";

            return citanje;
        }






        public IList<PastirTopic> GetPastirTopicList()
        {
            IList<PastirTopic> list = new List<PastirTopic>();

            list.Add(new PastirTopic(1, "Topic 1"));
            list.Add(new PastirTopic(2, "Topic 2"));
            list.Add(new PastirTopic(3, "Topic 3"));
            list.Add(new PastirTopic(4, "Topic 4"));

            return list;
        }



        public PitanjeInfo GetPastirQuestion(int questionID)
        {
            string question = "";
            string answer = "";
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < 100; i++)
            {
                str.Append("blah ");
            }

            question = "Dear Brothers and Sisters, <p>" + str.ToString();
            answer = "Dear Brother, <p>" + str.ToString();

            PitanjeInfo pitanje = new PitanjeInfo(0,"Sample Question", question, answer, 0, "Sample Topic", "Some Author");

            return pitanje;
        }



        public IList<Prolog> GetSaintNamesAndLivesList(int Mjesec, int Dan)
        {
            List<Prolog> ret = new List<Prolog>();

            for (int i = 0; i < 5; i++)
            {
                Prolog prolog = new Prolog("Sv. " + i.ToString(), "Zitija za Sv. " + i.ToString());
            }
            return ret;
        }



        public PrologOther GetProlog(int Mjesec, int Dan)
        {
            PrologOther other = new PrologOther();
            other.Pjesma = "Pjesma";
            other.Rasudjivanje = "Rasudjivanje";
            other.Sozercanje = "Sozercanje";
            other.Besjeda = "Besjeda";

            return other;
        }



        public IList<MessageThread> GetMessageThreads(int page, int rows)
        {
            return GetMessageThreads(rows);
        }

        public int GetTotalTopics()
        {
            return 333;
        }

        public IList<TopicMessage> GetTopicMessages(int topicID)
        {
            IList<TopicMessage> topics = new List<TopicMessage>();

            for (int i = 0; i < 10; i++)
            {
                TopicMessage msg = new TopicMessage(i, i, "name " + i.ToString(), DateTime.Now, "html Text of the mssg " + i.ToString(), null, i, "hdr " + i.ToString());
                topics.Add(msg);
            }
            return topics;
        }

        public IList<PrologSearchResults> GetPrologSearchResutls(string keyword)
        {
            throw new NotImplementedException();
        }


        public IList<Molitva> GetMolitveList()
        {
            throw new NotImplementedException();
        }

        public IList<Molitva> GetMolitveList(int nKateg)
        {
            throw new NotImplementedException();
        }

        public IList<MolitveKateg> GetMolitveKategList()
        {
            throw new NotImplementedException();
        }
    }
}
