using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using AppCommon;

namespace IlinkDb.WebRole.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            string logMsg = "HomeController/Index";

            Logging.LogDebug(logMsg);
            return View();
        }

    }
}
