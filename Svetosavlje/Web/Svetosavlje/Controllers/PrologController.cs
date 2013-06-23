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

        public ActionResult Index(string dtProlog)
        {
            PrologModel model = new PrologModel();
            SvetiDana data = new SvetiDana();
            DateTime dtDate;

            if (!DateTime.TryParse(dtProlog, out dtDate))
            {
                dtDate = DateTime.Today.AddDays(-13);
            }

            model.Sveti = data.GetSaintNamesAndLivesList(dtDate.Month, dtDate.Day);
            PrologOther prolog = new PrologOther();
            prolog = data.GetProlog(dtDate.Month, dtDate.Day);

            model.Pjesma = prolog.Pjesma;
            model.Rasudjivanje = prolog.Rasudjivanje;
            model.Sozercanje = prolog.Sozercanje;
            model.Besjeda = prolog.Besjeda;
            model.Datum = dtDate.Day.ToString() + "/" + dtDate.Month.ToString() + "/" + dtDate.Year.ToString();

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
