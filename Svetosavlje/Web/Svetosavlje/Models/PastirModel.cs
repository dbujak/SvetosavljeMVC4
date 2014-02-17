using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Svetosavlje.Interfaces.Interfaces;
using Svetosavlje.Interfaces.Classes;

namespace Svetosavlje.Models
{
    public class PastirModel
    {
        public IList<PitanjeInfo> PastirQuestions { get; set; }
        public IList<PastirTopic> PastirTopics { get; set; }
        public PitanjeInfo PastirQuestion { get; set; }
        public bool orderedList { get; set; }
    }


}