using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Svetosavlje.Services
{
    public struct SvastaraProvider
    {
        public string Mjesec(int mjesec)
        {
            switch (mjesec)
            {
                case 1:
                    return "јануар";
                case 2:
                    return "фебруар";
                case 3:
                    return "март";
                case 4:
                    return "април";
                case 5:
                    return "мај";
                case 6:
                    return "јуни";
                case 7:
                    return "јули";
                case 8:
                    return "август";
                case 9:
                    return "септембар";
                case 10:
                    return "октобар";
                case 11:
                    return "новембар";
                case 12:
                    return "децембар";
                default:
                    break;
            }
            return "";
        }

    }
}
