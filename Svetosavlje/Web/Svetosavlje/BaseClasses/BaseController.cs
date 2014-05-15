using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog;

namespace Svetosavlje.BaseClasses
{
    public class BaseController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        protected override void OnException(ExceptionContext filterContext)
        {
            // log error message
            logger.Error(filterContext.Exception.Message);
            logger.Error("Inner Exception: {0}", filterContext.Exception.InnerException.Message);
            logger.Error("Stack Trace: {0}", filterContext.Exception.StackTrace);
            //if (filterContext.ExceptionHandled)
            //{
            //    return;
            //}
            //filterContext.Result = new ViewResult
            //{
            //    ViewName = "~/Views/Shared/Error.aspx"
            //};
            //filterContext.ExceptionHandled = true;
        }
    }
}