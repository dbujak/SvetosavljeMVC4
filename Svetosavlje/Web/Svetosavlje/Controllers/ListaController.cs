using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;
using Svetosavlje.Interfaces.Classes;
using Svetosavlje.Interfaces.Interfaces;
using Svetosavlje.Services;
using Svetosavlje.Models;

namespace Svetosavlje.Controllers
{
    public class ListaController : Controller
    {
        //
        // GET: /Lista/

        public ActionResult Index(int page = 0)
        {
            int topicsPerPage = Convert.ToInt32(WebConfigurationManager.AppSettings["ListaArhiva_TopicsPerPage"].ToString());

            ListaModel model = new ListaModel();
            ListArhivaData data = new ListArhivaData();

            model.CurrentPage = page;
            model.TotalPages = data.GetTotalTopics() / topicsPerPage;
            model.ListaTopics = data.GetTopicList(page, topicsPerPage);

            return View(model);
        }

        public ActionResult ListaTopics(int rows, int page = 0, bool boolList = false)
        {
            if (rows == 0)
            {
                rows = Convert.ToInt32(WebConfigurationManager.AppSettings["ListaArhiva_TopicsPerPage"].ToString());
            }

            ListaModel model = new ListaModel();
            ListArhivaData data = new ListArhivaData();

            model.boolList = boolList;

            model.CurrentPage = page;
            model.TotalPages = data.GetTotalTopics() / rows;
            model.ListaTopics = data.GetTopicList(page, rows);

            return PartialView("_ListaTopics", model);
        }
    }

    public class ListArhivaData : IListaArhiva
    {
        private DatabaseProvider _provider = new DatabaseProvider();

        public IList<ListaArhiva> GetTopicList(int rows)
        {
            return _provider.GetTopicList(rows);
        }



        public IList<ListaArhiva> GetTopicList(int page, int rows)
        {
            return _provider.GetTopicList(page, rows);
        }

        public int GetTotalTopics()
        {
            return _provider.GetTotalTopics();
        }
    }
}
