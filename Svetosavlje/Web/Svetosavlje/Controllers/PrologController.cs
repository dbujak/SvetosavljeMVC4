using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Xml.Linq;
using Svetosavlje.Models;
using Svetosavlje.Services;
using Svetosavlje.Interfaces.Interfaces;
using Svetosavlje.Interfaces.Classes;

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
            model.Dan = dtDate.Day.ToString() + ".";
            model.Mjesec = this.Mjesec(dtDate.Month);

            return View(model);
        }

        private string Mjesec(int mjesec)
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
