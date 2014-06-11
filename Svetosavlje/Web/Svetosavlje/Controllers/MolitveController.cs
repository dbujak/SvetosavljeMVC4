using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Svetosavlje.Models;
using Svetosavlje.Services;
using Svetosavlje.Interfaces.Interfaces;
using Svetosavlje.Interfaces.Classes;

namespace Svetosavlje.Controllers
{
    public class MolitveController : Controller
    {
        //
        // GET: /Molitve/

        public ActionResult Index()
        {
            MolitveModel Mmodel = new MolitveModel();


            return View(Mmodel);
        }

        public ActionResult PrikazMolitve()
        {
            return View();
        }

    }
}
