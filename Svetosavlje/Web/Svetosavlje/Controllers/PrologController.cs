using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Xml.Linq;
using Svetosavlje.Models;
using Svetosavlje.Data_Layer;
using Svetosavlje.Services;
using Svetosavlje.Data_Layer.Interfaces;

namespace Svetosavlje.Controllers
{
    public class PrologController : Controller
    {
        //
        // GET: /PrologOther/

        public ActionResult Index()
        {
            PrologModel model = new PrologModel();
            SvetiDana data = new SvetiDana();

            DateTime today = DateTime.Today.AddDays(-13);     // Julian date
            model.Sveti = data.GetSaintNamesAndLivesList(today.Month, today.Day);
            PrologOther prolog = new PrologOther();
            prolog = data.GetProlog(today.Month, today.Day);

            model.Pjesma = prolog.Pjesma;
            model.Rasudjivanje = prolog.Rasudjivanje;
            model.Sozercanje = prolog.Sozercanje;
            model.Besjeda = prolog.Besjeda;

            return View(model);
        }

    }

    public class SvetiDana : ISaintNamesList, ISaintNamesAndLivesList, IProlog
    {
        private DatabaseProvider _provider = new DatabaseProvider();

        public IList<string> GetSaintNamesList(int Mjesec, int Dan)
        {
            return _provider.GetSaintNamesList(Mjesec, Dan);
        }



      

        public IList<Prolog> GetSaintNamesAndLivesList(int Mjesec, int Dan)
        {
            return _provider.GetSaintNamesAndLivesList(Mjesec, Dan);
        }


        public PrologOther GetProlog(int Mjesec, int Dan)
        {
            return _provider.GetProlog(Mjesec,Dan);
        }

  
    }
}
