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
}
