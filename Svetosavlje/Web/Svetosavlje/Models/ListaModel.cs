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
        public IList<ListaArhiva> ListaTopics;
        public int CurrentPage;
        public int TotalPages;
    }
}