using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Svetosavlje.Data_Layer
{
    public class MailListTopicInfo
    {
        public int id;
        public string naziv;
        public long count;
        public string updater;
        public int updaterID;
        public DateTime? datum;

        public MailListTopicInfo(int i, string n, long c, string u, DateTime? d, int updId)
        {
            id = i;
            naziv = n;
            count = c;
            updater = u;
            datum = d;
            updaterID = updId;
        }

    }
}
