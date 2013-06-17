using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

using IlinkDb.Entity;
using AppCommon;

namespace IlinkDb.Data.PivotalApi
{
    public partial class RepositoryPivotal : IRepository
    {

        public Task GetTask(long id)
        {
            throw new NotImplementedException();
        }

        public Task Save(Task task)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Task task)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Task> List(Story story)
        {
            throw new NotImplementedException();
        }

        public Task Add(Story story, Task task)
        {
            Task retVal = null;

            string logMsg = "Rep_Task/Add";

            string postAction = string.Format("projects/{0}/stories/{1}/tasks",
                story.ProjectId, story.Id);
            PostRequest postRequest = new PostRequest(postAction);

            postRequest.XmlDoc = task.XmlDoc;

            ApiResponse response = PivotApi.Post(postRequest);

            if (response.Success)
            {
                XDocument doc = XDocument.Parse(response.Xml);
                XElement node = doc.Descendants("task").FirstOrDefault();

                retVal = new Task(node);
            }
            else
            {
                Logging.LogError(logMsg + string.Format(", ERROR Code: {0}, Message: {1}",
                  response.StatusCode, response.ErrorMessage));
            }
            return retVal;
        }

    }
}
