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
    public class PastirModel
    {
        public IList<PitanjeInfo> PastirQuestions { get; set; }
        public IList<PastirTopic> PastirTopics { get; set; }
        public PitanjeInfo PastirQuestion { get; set; }
        public bool orderedList { get; set; }
    }


    public class PitanjaPastiru : IQuestionList, IPastirTopicsList, IPastirQuestion
    {
        private DatabaseProvider _provider = new DatabaseProvider();

        public IList<PitanjeInfo> GetQuestionList(int topicID, int rows)
        {
            return _provider.GetQuestionList(topicID, rows);
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