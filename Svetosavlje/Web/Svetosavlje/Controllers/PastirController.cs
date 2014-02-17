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
    public class PastirController : Controller
    {
        //
        // GET: /Pastir/

        public ActionResult Index()
        {
            PastirModel model = new PastirModel();
            PitanjaPastiru data = new PitanjaPastiru();

            model.PastirTopics = data.GetPastirTopicList();
            return View(model);
        }

        public ActionResult PastirQuestions(int topicID = 0, int rows = 0, bool orderedList = false)
        {
            PastirModel model = new PastirModel();
            PitanjaPastiru data = new PitanjaPastiru();

            model.PastirQuestions = data.GetQuestionList(topicID, rows);
            model.orderedList = orderedList;

            return PartialView("_PastirQuestions", model);
        }

        [OutputCache(Duration = 7200, VaryByParam = "none")] // 7200 sekundi = 2 sata - ucitaj nove podatke svaka 2 sata (ovaj dio se ne bi trebao mijenjati cesto)
        public ActionResult SveTemeSaPitanjima()
        {
            PastirModel model = new PastirModel();
            PitanjaPastiru data = new PitanjaPastiru();

            model.PastirTopics = data.GetPastirTopicList();
            return PartialView("_SveTemeSaPitanjima", model);
        }

        public ActionResult PitanjeOdgovor(int id)
        {
            PastirModel model = new PastirModel();
            PitanjaPastiru data = new PitanjaPastiru();

            model.PastirQuestion = data.GetPastirQuestion(id);
            return View(model);
        }

        public ActionResult OtacSrboljub()
        {
            return View();
        }

        public ActionResult OtacLjubo()
        {
            return View();
        }

        public ActionResult OtacDusan()
        {
            return View();
        }

        public ActionResult OtacRade()
        {
            return View();
        }

        public ActionResult OtacIvan()
        {
            return View();
        }

    }

    public class PitanjaPastiru : IQuestionList, IPastirTopicsList, IPastirQuestion
    {
        private DatabaseProvider _provider = new DatabaseProvider();

        public IList<PitanjeInfo> GetQuestionList(int topicID, int rows)
        {
            return _provider.GetQuestionList(topicID, rows);
        }



        public IList<PastirTopic> GetPastirTopicList()
        {
            return _provider.GetPastirTopicList();
        }


        public PitanjeInfo GetPastirQuestion(int questionID)
        {
            return _provider.GetPastirQuestion(questionID);
        }

    }

}
