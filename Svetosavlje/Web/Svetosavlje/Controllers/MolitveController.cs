using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Svetosavlje.Controllers
{
    public class MolitveController : Controller
    {
        //
        // GET: /Molitve/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PrikazMolitve()
        {
            return View();
        }

    }
}
