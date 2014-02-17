using System;

namespace Svetosavlje.Interfaces.Classes
{
    public class ListaArhiva
    {
        public int id;
        public string naziv;
        public long count;
        public string updater;
        public int updaterID;
        public DateTime? datum;

        public ListaArhiva(int i, string n, long c, string u, DateTime? d, int updId)
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
