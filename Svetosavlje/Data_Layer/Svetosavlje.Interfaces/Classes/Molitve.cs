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
        public int nBrojMolitvi { get; set; }

       public MolitveKateg(int IDkategMolitvi, string kategMolitvi, int redKategMolitvi, int nrMolitvi)
        {
            sKategMolitvi = kategMolitvi;
            nRedKategMolitvi = redKategMolitvi;
            nkategMolitvi = IDkategMolitvi;
            nBrojMolitvi = nrMolitvi;
        }

    }

    public class Molitva
    {
        public int nIdMolitva { get; set; }
        public string sNaslovMolitve { get; set; }
        public string sMolitva { get; set; }
        public int nKategMolitve { get; set; }
        //public readonly MolitveKateg oKategMolitvi;
        public string sURLuBiblioteci { get; set; }



        public Molitva(int IdMolitva, string naslovMolitve, string molitve, int idKategMolitve, string URLuBiblioteci)
        {
            nIdMolitva = IdMolitva;
            sNaslovMolitve = naslovMolitve;
            sMolitva = molitve;
            nKategMolitve = idKategMolitve;
            //sKategMolitvi = kategMolitvi;
            //oKategMolitvi = new MolitveKateg(kategMolitvi, redKategMolitvi);
            sURLuBiblioteci = URLuBiblioteci;
        }

    }
}
