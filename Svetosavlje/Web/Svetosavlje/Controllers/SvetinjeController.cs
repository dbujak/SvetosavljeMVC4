using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Svetosavlje.Controllers
{
    public class SvetinjeController : Controller
    {
        //
        // GET: /Svetinje/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Crkve()
        {
            return View();
        }

        public ActionResult Manastiri()
        {
            return View();
        }

        public ActionResult Mosti()
        {
            return View();
        }

        public ActionResult Svetinje()
        {
            return View();
        }

        public ActionResult Hodocasca()
        {
            return View();
        }

        public ActionResult RukaSvSave()
        {
            return View();
        }

        public ActionResult RukaSvJovanaKrstitelja()
        {
            return View();
        }

        public ActionResult MostiSvPetraKoriskog()
        {
            return View();
        }

        public ActionResult MostiSvKraljaMilutina()
        {
            return View();
        }


        public ActionResult MostiSvPetke()
        {
            return View();
        }

        public ActionResult MostiSvArsenijaSremca()
        {
            return View();
        }

        public ActionResult MostiSvApostolaLuke()
        {
            return View();
        }

        public ActionResult SvKatarinaSinaj()
        {
            return View();
        }

        public ActionResult SvKatarinaSinajFotos()
        {
            return View();
        }

        public ActionResult Kosovo1()
        {
            return View();
        }

        public ActionResult Kosovo2()
        {
            return View();
        }

        public ActionResult Kosovo3()
        {
            return View();
        }

        public ActionResult Kosovo4()
        {
            return View();
        }

        public ActionResult Kosovo5()
        {
            return View();
        }

        public ActionResult KosovoNavigacija(int strana)
        {
            Dictionary<string, string> model = new Dictionary<string, string>();

            model.Add("1", "1");
            model.Add("2", "2");
            model.Add("3", "3");
            model.Add("4", "4");
            model.Add("5", "5");

            ViewBag.Strana = strana;

            if (strana > 0)
            {
                ViewBag.NazivPesme = model[strana.ToString()];
            }


            return PartialView("_KosovoNavigacija", model);
        }

        public ActionResult KosovoFotografije()
        {
            return View();
        }

        public ActionResult Jerusalim1()
        {
            return View();
        }

        public ActionResult Jerusalim2()
        {
            return View();
        }

        public ActionResult Jerusalim3()
        {
            return View();
        }

        public ActionResult Jerusalim4()
        {
            return View();
        }

        public ActionResult Jerusalim5()
        {
            return View();
        }


        public ActionResult JerusalimNavigacija(int strana)
        {
            Dictionary<string, string> model = new Dictionary<string, string>();

            model.Add("1", "1");
            model.Add("2", "2");
            model.Add("3", "3");
            model.Add("4", "4");
            model.Add("5", "5");

            ViewBag.Strana = strana;

            if (strana > 0)
            {
                ViewBag.NazivPesme = model[strana.ToString()];
            }


            return PartialView("_JerusalimNavigacija", model);
        }

        public ActionResult JerusalimFotografije()
        {
            return View();
        }

        public ActionResult RenoviranjeSinajskeBiblioteke()
        {
            return View();
        }

        public ActionResult PutovanjeUSvetuGoru()
        {
            return View();
        }

        public ActionResult Plac()
        {
            return View();
        }

        
    }
}
