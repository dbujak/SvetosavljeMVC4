using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Svetosavlje.Interfaces.Classes
{
    public class DnevnoCitanje
    {
        public string CompactVersion { get; set; }
        public string FullVersion { get; set; }

        public DnevnoCitanje()
        { }
        public DnevnoCitanje(string compactVersion, string fullVersion)
        {
            CompactVersion = compactVersion;
            FullVersion = fullVersion;
        }
    }
}
