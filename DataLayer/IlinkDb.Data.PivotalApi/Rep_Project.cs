using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Web;

using IlinkDb.Entity;
using AppCommon;

namespace IlinkDb.Data.PivotalApi
{
    public partial class RepositoryPivotal : IRepository
    {

        public Project ProjectGet(long id)
        {
            throw new NotImplementedException();
            string logMsg = "RepositoryPivotal/ProjectGet";

            try
            { }
            catch (Exception ex)
            { Logging.LogError(logMsg + ", EXCEPTION: " + ex.Message, ex); }
        }

        public Project ProjectSave(Project project)
        {
            throw new NotImplementedException();
            string logMsg = "RepositoryPivotal/ProjectSave";

            try
            { }
            catch (Exception ex)
            { Logging.LogError(logMsg + ", EXCEPTION: " + ex.Message, ex); }
        }

        public bool ProjectDelete(Project project)
        {
            throw new NotImplementedException();
            string logMsg = "RepositoryPivotal/ProjectDelete";

            try
            { }
            catch (Exception ex)
            { Logging.LogError(logMsg + ", EXCEPTION: " + ex.Message, ex); }
        }

        public IQueryable<Project> ProjectList()
        {
            IQueryable<Project> retVal = new List<Project>().AsQueryable();

            string logMsg = "RepositoryPivotal/ProjectList";

            try
            {
                PivotApiGetRequest getRequest = new PivotApiGetRequest("projects");
                PivotApiResponse response = Common.GetXmlFromPivotApi(getRequest);

                if (response.Success)
                {
                    XDocument doc = XDocument.Parse(response.Xml);

                    var list = from node in doc.Descendants("project")
                               select new Project(node);

                    retVal = list.AsQueryable<Project>();
                }
                else
                { Logging.LogError(logMsg + string.Format(", ERROR Code: {0}, Message: {1}", 
                    response.StatusCode, response.ErrorMessage)); }
            }
            catch (Exception ex)
            { Logging.LogError(logMsg + ", EXCEPTION: " + ex.Message, ex); }

            return retVal;
        }
    }
}
