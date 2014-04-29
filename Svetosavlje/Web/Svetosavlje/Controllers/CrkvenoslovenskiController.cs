using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Svetosavlje.Controllers
{
    public class CrkvenoslovenskiController : Controller
    {
        //
        // GET: /Crkvenoslovenski/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PesmeNavigacija(int pesma)
        {
            Dictionary<string, string> model = new Dictionary<string, string>();

            model.Add("1", "Тропар први из полуноћнице, глас осми");
            model.Add("2", "Покајни тропари");
            model.Add("3", "О Тебје радујетсја: глас осми");
            model.Add("4", "Дјева днјес: (кондак на Божић), глас трећи");
            model.Add("5", "Стихира по јеванђ. У недељу митара и фарисеја, глас осми");
            model.Add("6", "Из вечерње: Да исправитсја..");
            model.Add("7", "Из Великог повечерја од: Господи сил с нами буди, до Всје упованије моје.");
            model.Add("8", "Слава на стиховње на Велики петак, глас пети");
            model.Add("9", "Благослови душе моја Господа");
            model.Add("10", "Блажен муж..");
            model.Add("11", "Стихира на Господи возвах светом Сергију и Герману, глас осми");
            model.Add("12", "Догматик гл. пети.: В Чермњем мори.");
            model.Add("13", "Свјете тихиј..");
            model.Add("14", "По причешћу: Воскресеније Христово видјеше..");
            model.Add("15", "Канон благовести, песма осма., ирм. 2.");
            model.Add("16", "На јутрењу после осме песме канона: Величит душа моја Господа");
            model.Add("17", "Из јутрење - од: Слава во вишњих Богу и на земљи мир, до: Днјес спасеније мирубист..");
            model.Add("18", "Иже Херувими..");

            ViewBag.Page = pesma;

            if (pesma > 0)
            {
                ViewBag.NazivPesme = model[pesma.ToString()];
            }


            return PartialView("_PesmeNavigacija", model);
        }

        public ActionResult crkvenijezik()
        {
            return View();
        }

        public ActionResult Pesma01()
        {
            return View();
        }

        public ActionResult Pesma02()
        {
            return View();
        }

        public ActionResult Pesma03()
        {
            return View();
        }

        public ActionResult Pesma04()
        {
            return View();
        }

        public ActionResult Pesma05()
        {
            return View();
        }
        public ActionResult Pesma06()
        {
            return View();
        }
        public ActionResult Pesma07()
        {
            return View();
        }
        public ActionResult Pesma08()
        {
            return View();
        }

        public ActionResult Pesma09()
        {
            return View();
        }
        public ActionResult Pesma10()
        {
            return View();
        }
        public ActionResult Pesma11()
        {
            return View();
        }

        public ActionResult Pesma12()
        {
            return View();
        }

        public ActionResult Pesma13()
        {
            return View();
        }

        public ActionResult Pesma14()
        {
            return View();
        }

        public ActionResult Pesma15()
        {
            return View();
        }

        public ActionResult Pesma16()
        {
            return View();
        }

        public ActionResult Pesma17()
        {
            return View();
        }
        public ActionResult Pesma18()
        {
            return View();
        }








    }
}
