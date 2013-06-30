using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Web.Http;
using System.Net.Http;

using IlinkDb.Entity;
using IlinkDb.Business;

using AppCommon;

namespace IlinkDb.Service.Controllers
{
    public class StoryController : ApiController
    {
        [AcceptVerbs("GET")]
        public HttpResponseMessage Get(long storyId)
        {
            HttpResponseMessage retVal = new HttpResponseMessage();

            string logMsg = "StoryController/Get";

            try
            {
                StoryManager mgr = new StoryManager();
                Story story = mgr.Get(storyId);

                Logging.LogDebug(logMsg + string.Format(" Story: {0}", story));

                retVal = Request.CreateResponse(HttpStatusCode.OK, story);
            }
            catch (Exception ex)
            {
                Logging.LogError(logMsg + ", EXCEPTION: " + ex.Message, ex);
                retVal.StatusCode = HttpStatusCode.InternalServerError;
                retVal.Content = new StringContent(ex.Message);
            }
            return retVal;
        }

        [AcceptVerbs("GET")]
        public HttpResponseMessage List()
        {
            HttpResponseMessage retVal = new HttpResponseMessage();

            string logMsg = "StoryController/List";

            try
            {
                StoryManager mgr = new StoryManager();
                List<Story> storys = mgr.List();

                Logging.LogDebug(logMsg + string.Format(" Story Count: {0}",
                    storys.Count()));

                retVal = Request.CreateResponse(HttpStatusCode.OK, storys);
            }
            catch (Exception ex)
            {
                Logging.LogError(logMsg + ", EXCEPTION: " + ex.Message, ex);
                retVal.StatusCode = HttpStatusCode.InternalServerError;
                retVal.Content = new StringContent(ex.Message);
            }
            return retVal;
        }

        [AcceptVerbs("GET")]
        public HttpResponseMessage ListForLabel(string label)
        {
            HttpResponseMessage retVal = new HttpResponseMessage();

            string logMsg = "ListForLabel/List";

            try
            {
                string workLabel = CleanLabel(label);
                StoryManager mgr = new StoryManager();
                List<Story> storys = mgr.ListForLabel(workLabel);

                Logging.LogDebug(logMsg + string.Format(" for Label: {0} Story Count: {1}",
                    label, storys.Count()));

                retVal = Request.CreateResponse(HttpStatusCode.OK, storys);
            }
            catch (Exception ex)
            {
                Logging.LogError(logMsg + ", EXCEPTION: " + ex.Message, ex);
                retVal.StatusCode = HttpStatusCode.InternalServerError;
                retVal.Content = new StringContent(ex.Message);
            }
            return retVal;
        }

        [AcceptVerbs("POST")]
        public HttpResponseMessage Post(Story story)
        {
            return Put(story, "");
        }

        [AcceptVerbs("POST")]
        public HttpResponseMessage Post(Story story, string label)
        {
            return Put(story, label);
        }

        [AcceptVerbs("PUT")]
        public HttpResponseMessage Put(Story story)
        {
            return Put(story, "");
        }

        [AcceptVerbs("PUT")]
        public HttpResponseMessage Put(Story story, string label)
        {
            HttpResponseMessage retVal = Request.CreateResponse(HttpStatusCode.OK, story);

            string logMsg = "StoryController/Put";

            try
            {
                StoryManager mgr = new StoryManager();


                if (!string.IsNullOrEmpty(label))
                {
                    string tempLabel = CleanLabel(label);
                    if (string.IsNullOrEmpty(story.Labels))
                    { story.Labels = tempLabel; }
                    else
                    {
                        if (!story.Labels.ToLower().Contains(tempLabel.ToLower()))
                        { story.Labels += tempLabel; }
                    }
                }

                Story updatedStory = mgr.Save(story);

                Logging.LogDebug(logMsg + string.Format(" Story: {0}", updatedStory));

                retVal = Request.CreateResponse(HttpStatusCode.OK, updatedStory);

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

        [AcceptVerbs("DELETE")]
        public HttpResponseMessage Delete(Story story)
        {
            HttpResponseMessage retVal = Request.CreateResponse(HttpStatusCode.OK, story);

            string logMsg = "StoryController/Delete";

            try
            {
                StoryManager mgr = new StoryManager();

                bool success = mgr.Delete(story);

                string delMsg = "Success deleting";
                if (!success)
                { delMsg = "Failure deleting"; }

                Logging.LogDebug(logMsg + string.Format(" {0} Story: {1}", delMsg, story));

                retVal = Request.CreateResponse(HttpStatusCode.OK, success);

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

        private string CleanLabel(string label)
        {
            string retVal = label.Replace("/", "_").Replace(" ", "");
            return retVal;
        }
    }
}
