using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Svetosavlje.Interfaces.Interfaces;
using Svetosavlje.Interfaces.Classes;

namespace Svetosavlje.Models
{
    public class ListaModel
    {
        public IList<MessageThread> ListaTopics;
        public IList<TopicMessage> TopicMessages;
        public int CurrentPage;
        public int TotalPages;
        public bool boolList;

        
    }
}