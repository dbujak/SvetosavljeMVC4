using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Svetosavlje.Interfaces.Classes
{
    public class PitanjeInfo
    {
        public int ID { get; set; }
        public string Naslov { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int TopicID { get; set; }
        public string Topic { get; set; }
        public string Author { get; set; }

        public PitanjeInfo(int id, string naslov)
        {
            ID = id;
            Naslov = naslov;
        }

        public PitanjeInfo(int id, string naslov, string question, string answer, int topicID, string topic, string author)
        {
            ID = id;
            Naslov = naslov;
            Question = question;
            Answer = answer;
            TopicID = topicID;
            Topic = topic;
            Author = author;
        }

        public PitanjeInfo() { }
    }

    /// <summary>
    /// Represents one Topic under "Pitanja Pastiru"
    /// </summary>
    public class PastirTopic
    {
        public int ID { get; set; }
        public string Title { get; set; }

        public PastirTopic(int id, string title)
        {
            ID = id;
            Title = title;
        }

        public PastirTopic() { }
    }
}
