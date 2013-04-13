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
        public IList<PitanjeInfo> LatestQuestions { get; set; }
        public IList<PastirTopic> Topics { get; set; }
    }


    public class PitanjaPastiru : IQuestionList, IPastirTopicsList
    {
        private DatabaseProvider _provider = new DatabaseProvider();

        public IList<PitanjeInfo> GetQuestionList(int rows)
        {
            return _provider.GetQuestionList(rows);
        }



        public IList<PastirTopic> GetPastirTopicList()
        {
            return _provider.GetPastirTopicList();
        }

    }
}