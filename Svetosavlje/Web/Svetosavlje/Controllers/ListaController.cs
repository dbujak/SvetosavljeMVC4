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
            model.ListaTopics = data.GetMessageThreads(page, topicsPerPage);

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
            model.ListaTopics = data.GetMessageThreads(page, rows);

            return PartialView("_ListaTopics", model);
        }

        public ActionResult Topic(int topicID, string strTema)
        {
            ListaModel model = new ListaModel();

            ListArhivaData data = new ListArhivaData();

            model.TopicMessages = data.GetTopicMessages(topicID);
            ViewData["naslovTeme"] = strTema;
            return View(model);
        }
    }

    public class ListArhivaData : IListaArhiva
    {
        private DatabaseProvider _provider = new DatabaseProvider();

        public IList<MessageThread> GetMessageThreads(int rows)
        {
            return _provider.GetMessageThreads(rows);
        }



        public IList<MessageThread> GetMessageThreads(int page, int rows)
        {
            return _provider.GetMessageThreads(page, rows);
        }

        public int GetTotalTopics()
        {
            return _provider.GetTotalTopics();
        }


        public IList<TopicMessage> GetTopicMessages(int topicID)
        {
            return _provider.GetTopicMessages(topicID);
        }
    }
}
