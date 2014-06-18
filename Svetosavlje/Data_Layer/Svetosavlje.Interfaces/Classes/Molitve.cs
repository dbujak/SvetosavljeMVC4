using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Svetosavlje.Interfaces.Classes
{
    public class MolitveKateg
    {
        public int nkategMolitvi { get; set; }
        public string sKategMolitvi { get; set; }
        public int nRedKategMolitvi { get; set; }

       public MolitveKateg(int IDkategMolitvi, string kategMolitvi, int redKategMolitvi)
        {
            sKategMolitvi = kategMolitvi;
            nRedKategMolitvi = redKategMolitvi;
            nkategMolitvi = IDkategMolitvi;
        }

    }

    public class Molitve
    {
        public string sNaslovMolitve { get; set; }
        public string sMolitve { get; set; }
        public int nKategMolitvi { get; set; }
        //public readonly MolitveKateg oKategMolitvi;
        public string sURLuBiblioteci { get; set; }

        public Molitve(string naslovMolitve, string molitve, int idKategMolitvi, string URLuBiblioteci)
        {
            sNaslovMolitve = naslovMolitve;
            sMolitve = molitve;
            nKategMolitvi = idKategMolitvi;
            //sKategMolitvi = kategMolitvi;
            //oKategMolitvi = new MolitveKateg(kategMolitvi, redKategMolitvi);
            sURLuBiblioteci = URLuBiblioteci;
        }

    }
}
