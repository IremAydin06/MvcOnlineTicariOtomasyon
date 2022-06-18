using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult PageError()
        {
            Response.TrySkipIisCustomErrors = true;
            return View("PageError");
        }

        public ActionResult Page401()
        {
            Response.StatusCode = 401;
            Response.TrySkipIisCustomErrors = true;
            return View("Page401");
        }

        public ActionResult Page404()
        {
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true;
            return View("Page404");
        }

        public ActionResult Page500()
        {
            Response.StatusCode = 500;
            Response.TrySkipIisCustomErrors = true;
            return View("Page500");
        }
    }
}