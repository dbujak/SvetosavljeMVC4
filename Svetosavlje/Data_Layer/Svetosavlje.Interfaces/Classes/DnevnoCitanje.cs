using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Svetosavlje.Interfaces.Classes
{
    public class DnevnoCitanje
    {
        public string Vecernje { get; set; }
        public string Jutrenje { get; set; }
        public string LiturgijaJevandjelje { get; set; }
        public string LiturgijaApostoli { get; set; }

        public DnevnoCitanje()
        { }
        public DnevnoCitanje(string vecernje, string jutrenje, string liturgijaJevandjelje, string liturgijaApostoli)
        {
            Vecernje = vecernje;
            Jutrenje = jutrenje;
            LiturgijaJevandjelje = liturgijaJevandjelje;
            LiturgijaApostoli = liturgijaApostoli;
        }
    }
}
