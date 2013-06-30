using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using IlinkDb.Entity;

namespace IlinkDb.WebRole.Controllers
{
    public class StoryBuddyController : Controller
    {
        //
        // GET: /StoryBuddy/

        public ActionResult List()
        {
            Story story = new Story();
            return View(story);
        }

    }
}
