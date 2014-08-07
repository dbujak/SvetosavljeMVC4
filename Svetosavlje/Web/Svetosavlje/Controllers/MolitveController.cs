using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Xml.Linq;
using Svetosavlje.Models;
using Svetosavlje.Services;
using Svetosavlje.Interfaces.Interfaces;
using Svetosavlje.Interfaces.Classes;
using Svetosavlje.BaseClasses;

namespace Svetosavlje.Controllers
{
    public class MolitveController : BaseController
    {
        //
        private IDataProvider _provider = Factory.GetDatabaseProvider();

        public ActionResult Index()
        {
            MolitveModel Mmodel = new MolitveModel();

            Mmodel.KategorijeMolitvi = GetMolitveKateg();
            Mmodel.Molitve = GetMolitveList();
            
            return View(Mmodel);
        }

        public ActionResult PrikazMolitve()
        {
            return View();
        }

        public IList<MolitveKateg> GetMolitveKateg()
        {
            return _provider.GetMolitveKategList();
        }

        public IList<Molitve> GetMolitveList()
        {
            return _provider.GetMolitveList();
        }

    }
}
