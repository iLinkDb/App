﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IlinkDb.WebRole.Controllers
{
    public class BpController : Controller
    {
        //
        // GET: /Bp/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Icons()
        {
            return View();
        }
		
      public ActionResult SelectArea(string id)
      {
         if (string.IsNullOrEmpty(id))
         {
            return View("Index");
         }
         else
         {
            return View(id);
         }
      }
		
    }
}
