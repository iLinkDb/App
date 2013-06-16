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

        public Story StoryGet(long id)
        {
            throw new NotImplementedException();
            string logMsg = "RepositoryPivotal/StoryGet";

            try
            { }
            catch (Exception ex)
            { Logging.LogError(logMsg + ", EXCEPTION: " + ex.Message, ex); }
        }

        public Story StorySave(Story story)
        {
            throw new NotImplementedException();
            string logMsg = "RepositoryPivotal/StorySave";

            try
            { }
            catch (Exception ex)
            { Logging.LogError(logMsg + ", EXCEPTION: " + ex.Message, ex); }
        }

        public bool StoryDelete(Story story)
        {
            throw new NotImplementedException();
            string logMsg = "RepositoryPivotal/StoryDelete";

            try
            { }
            catch (Exception ex)
            { Logging.LogError(logMsg + ", EXCEPTION: " + ex.Message, ex); }
        }

        public IQueryable<Story> StoryList(long projectId)
        {
            IQueryable<Story> retVal = new List<Story>().AsQueryable();

            string logMsg = "RepositoryPivotal/StoryList";

            try
            {
                string action = string.Format("projects/{0}/stories", projectId);
                PivotApiGetRequest getRequest = new PivotApiGetRequest(action);
                PivotApiResponse response = Common.GetXmlFromPivotApi(getRequest);

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
