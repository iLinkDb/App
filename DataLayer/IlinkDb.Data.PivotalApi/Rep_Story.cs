using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using IlinkDb.Entity;
using AppCommon;

namespace IlinkDb.Data.PivotalApi
{
    public partial class RepositoryPivotal : IRepository
    {

        public Story StoryGet(long projectId, long storyId)
        {
            Story retVal = null;

            string logMsg = "Rep_Story/StoryGet";

            try
            {
                string action = string.Format("projects/{0}/stories/{1}", projectId, storyId);
                GetRequest getRequest = new GetRequest(action);
                ApiResponse response = PivotApi.Get(getRequest);

                if (response.Success)
                {
                    XDocument doc = XDocument.Parse(response.Xml);

                    var query = from node in doc.Descendants("story")
                                select new Story(node);

                    retVal = query.First();
                }
                else
                {
                    Logging.LogError(logMsg + string.Format(", ERROR Code: {0}, Message: {1}",
                      response.StatusCode, response.ErrorMessage));
                }
            }
            catch (Exception ex)
            { Logging.LogError(logMsg + ", EXCEPTION: " + ex.Message, ex); }

            return retVal;
        }

        public Story StorySave(Story story)
        {
            Story retVal = null;

            string logMsg = "Rep_Story/StorySave";

            string postAction;
            if (story.Id > 0)
            { postAction = string.Format("projects/{0}/stories/{1}", story.ProjectId, story.Id); }
            else
            { postAction = string.Format("projects/{0}/stories", story.ProjectId); }

            PostRequest postRequest = new PostRequest(postAction);

            postRequest.XmlDoc = story.XmlDoc;

            ApiResponse response;
            if (story.Id > 0)
            { response = PivotApi.Put(postRequest); }
            else
            { response = PivotApi.Post(postRequest); }

            if (response.Success)
            {
                XDocument doc = XDocument.Parse(response.Xml);
                XElement node = doc.Descendants("story").FirstOrDefault();

                retVal = new Story(node);
            }
            else
            {
                Logging.LogError(logMsg + string.Format(", ERROR Code: {0}, Message: {1}",
                  response.StatusCode, response.ErrorMessage));
            }
            return retVal;
        }

        public bool StoryDelete(Story story)
        {
            throw new NotImplementedException();
            string logMsg = "Rep_Story/StoryDelete";

            try
            { }
            catch (Exception ex)
            { Logging.LogError(logMsg + ", EXCEPTION: " + ex.Message, ex); }
        }

        public IQueryable<Story> StoryList(long projectId)
        {
            IQueryable<Story> retVal = new List<Story>().AsQueryable();

            string logMsg = "Rep_Story/StoryList";

            try
            {
                string action = string.Format("projects/{0}/stories", projectId);
                GetRequest getRequest = new GetRequest(action);
                ApiResponse response = PivotApi.Get(getRequest);

                if (response.Success)
                {
                    XDocument doc = XDocument.Parse(response.Xml);

                    var list = from node in doc.Descendants("story")
                               select new Story(node);

                    retVal = list.AsQueryable<Story>();
                }
                else
                {
                    Logging.LogError(logMsg + string.Format(", ERROR Code: {0}, Message: {1}",
                      response.StatusCode, response.ErrorMessage));
                }
            }
            catch (Exception ex)
            { Logging.LogError(logMsg + ", EXCEPTION: " + ex.Message, ex); }

            return retVal;
        }

        public IQueryable<Story> StoryListForLabel(long projectId, string label)
        {
            IQueryable<Story> retVal = new List<Story>().AsQueryable();

            string logMsg = "Rep_Story/StoryListForLabel";

            try
            {
                string action = string.Format("projects/{0}/stories", projectId);
                string parameters = string.Format("&filter=label:{0}", label);
                GetRequest getRequest = new GetRequest(action, parameters);
                ApiResponse response = PivotApi.Get(getRequest);

                if (response.Success)
                {
                    XDocument doc = XDocument.Parse(response.Xml);

                    var list = from node in doc.Descendants("story")
                               select new Story(node);

                    retVal = list.AsQueryable<Story>();
                }
                else
                {
                    Logging.LogError(logMsg + string.Format(", ERROR Code: {0}, Message: {1}",
                      response.StatusCode, response.ErrorMessage));
                }
            }
            catch (Exception ex)
            { Logging.LogError(logMsg + ", EXCEPTION: " + ex.Message, ex); }

            return retVal;
        }

    }
}
