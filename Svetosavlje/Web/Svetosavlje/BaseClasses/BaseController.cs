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
            if (filterContext != null)
            {
                logger.Error("Exception Message {0}", filterContext.Exception.Message);

                Exception innerEx = filterContext.Exception.InnerException;
                while (innerEx != null)
                {
                    logger.Error("Inner Exception: {0}", innerEx.Message);
                    innerEx = innerEx.InnerException;
                }
                logger.Error("Stack Trace: {0}", filterContext.Exception.StackTrace);
            }
            if (filterContext.ExceptionHandled)
            {
                return;
            }
            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/Shared/Error.cshtml"
            };
            filterContext.ExceptionHandled = true;
        }
    }
}