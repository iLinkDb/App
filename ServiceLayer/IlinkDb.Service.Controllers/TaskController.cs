﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Web.Http;
using System.Net.Http;

using IlinkDb.Entity;
using IlinkDb.Business;

using AppCommon;

namespace IlinkDb.Service.Controllers
{
    public class TaskController : ApiController
    {
        [AcceptVerbs("POST")]
        public HttpResponseMessage Post(Task task)
        {
            return Put(task);
        }

        [AcceptVerbs("PUT")]
        public HttpResponseMessage Put(Task task)
        {
            HttpResponseMessage retVal = Request.CreateResponse(HttpStatusCode.OK, task);

            string logMsg = "TaskController/Put";

            try
            {
                if (task.StoryId > 0)
                {
                    StoryManager mgrStory = new StoryManager();
                    Story story = mgrStory.Get(task.StoryId);

                    TaskManager mgrTask = new TaskManager();

                    Task updatedTask = mgrTask.Save(story, task);

                    Logging.LogDebug(logMsg + string.Format(" Task: {0}", updatedTask));

                    retVal = Request.CreateResponse(HttpStatusCode.OK, updatedTask);
                }
                else
                { Logging.LogError(logMsg + ", ERROR StoryId must be > 0"); }
                //string uri = Url.Route(null, new { id = timesheet.Id }); // Where is the modified timesheet?
                //response.Headers.Location = new Uri(Request.RequestUri, uri);

            }
            catch (Exception ex)
            {
                Logging.LogError(logMsg + ", EXCEPTION: " + ex.Message, ex);
                retVal.StatusCode = HttpStatusCode.InternalServerError;
                retVal.Content = new StringContent(ex.Message);
            }
            return retVal;

        }


    }
}
