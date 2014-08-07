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
    public class PrologController : BaseController
    {


        // implement caching http://stackoverflow.com/questions/19054455/how-to-use-outputcache-for-a-specific-argument-only
        public ActionResult Index(string dtProlog, string txtSearchKeyword)
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

            if (txtSearchKeyword != null && txtSearchKeyword != "")
            {
                foreach (Prolog p in model.Sveti)
                {
                    p.ImeSveca = highlightSearchText(p.ImeSveca, txtSearchKeyword);
                    p.ZitijaSveca = highlightSearchText(p.ZitijaSveca, txtSearchKeyword);
                }

                prolog.Pjesma = highlightSearchText(prolog.Pjesma, txtSearchKeyword);
                prolog.Rasudjivanje = highlightSearchText(prolog.Rasudjivanje, txtSearchKeyword);
                prolog.Sozercanje = highlightSearchText(prolog.Sozercanje, txtSearchKeyword);
                prolog.Besjeda = highlightSearchText(prolog.Besjeda, txtSearchKeyword);
            }

            model.Pjesma = prolog.Pjesma;
            model.Rasudjivanje = prolog.Rasudjivanje;
            model.Sozercanje = prolog.Sozercanje;
            model.Besjeda = prolog.Besjeda;
            model.Datum = dtDate.Day.ToString() + "/" + dtDate.Month.ToString() + "/" + dtDate.Year.ToString();
            model.Dan = dtDate.Day.ToString() + ".";
            SvastaraProvider svastara;
            model.Mjesec = svastara.Mjesec(dtDate.Month);
            model.ZitijaLink = dtDate.ToString("MMdd");

            return View(model);
        }

        public ActionResult SearchResults(string txtSearchKeyword)
        {
            PrologModel model = new PrologModel();
            SvetiDana data = new SvetiDana();

            if (txtSearchKeyword.Length >= 3)
            {
                model.SearchResults = data.GetPrologSearchResutls(txtSearchKeyword);
                model.SearchKeyword = txtSearchKeyword;
            }
            else
            {
                return View(model);
            }

            SvastaraProvider svastara;

            foreach (PrologSearchResults res in model.SearchResults)
            {
                res.DatumDisplay = res.Datum.Day.ToString() + ". " + svastara.Mjesec(res.Datum.Month);

                res.Tekst = highlightSearchText(res.Tekst, txtSearchKeyword);
            }
            return View(model);
        }

        private string highlightSearchText(string text, string search)
        {
            CompareInfo ci = CultureInfo.CurrentCulture.CompareInfo;
            IList<int> pozicije = new List<int>();
            bool more = true;
            int location = -1;

            while (more)
            {
                location++;
                location = ci.IndexOf(text, search, location, CompareOptions.IgnoreCase);
                if (location > -1)
                {
                    pozicije.Add(location);
                }
                else
                {
                    more = false;
                }
            }

            for (int i = pozicije.Count - 1; i >= 0; i--)
            {
                text = text.Insert(pozicije[i] + search.Length, "</span>");
                text = text.Insert(pozicije[i], "<span class='searchHiglight'>");
            }

            return text;
        }

    }

    public class SvetiDana : ISaintNamesList, ISaintNamesAndLivesList, IProlog
    {
        private IDataProvider _provider = Factory.GetDatabaseProvider();

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
            return _provider.GetProlog(Mjesec, Dan);
        }



        public IList<PrologSearchResults> GetPrologSearchResutls(string keyword)
        {
            return _provider.GetPrologSearchResutls(keyword);
        }
    }
}
