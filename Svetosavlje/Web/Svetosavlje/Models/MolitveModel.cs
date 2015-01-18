using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Svetosavlje.Interfaces.Interfaces;
using Svetosavlje.Interfaces.Classes;


namespace Svetosavlje.Models
{
    public class MolitveModel
    {
        public IList<MolitveKateg> KategorijeMolitvi { get; set; }
        public IList<Molitva> Molitve { get; set; }
    }
}