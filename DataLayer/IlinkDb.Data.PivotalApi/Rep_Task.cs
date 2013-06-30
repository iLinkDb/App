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
        private void TaskInitialize()
        {
            //
        }

        public Task TaskGet(Story story, long taskId)
        {
            Task retVal = null;

            string logMsg = "Rep_Task/TaskGet";

            try
            {
                string action = string.Format("projects/{0}/stories/{1}/tasks/{2}", story.ProjectId, story.Id, taskId);
                GetRequest getRequest = new GetRequest(action);
                ApiResponse response = PivotApi.Get(getRequest);

                if (response.Success)
                {
                    XDocument doc = XDocument.Parse(response.Xml);

                    var query = from node in doc.Descendants("task")
                                select new Task(node);

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

        public bool TaskDelete(Task task)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Task> TaskList(Story story)
        {
            IQueryable<Task> retVal = new List<Task>().AsQueryable();

            string logMsg = "Rep_Story/TaskList";

            try
            {
                string action = string.Format("projects/{0}/stories/{1}/tasks", story.ProjectId, story.Id);
                GetRequest getRequest = new GetRequest(action);
                ApiResponse response = PivotApi.Get(getRequest);

                if (response.Success)
                {
                    XDocument doc = XDocument.Parse(response.Xml);

                    var list = from node in doc.Descendants("task")
                               select new Task(node);

                    retVal = list.AsQueryable<Task>();
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

        public Task TaskSave(Story story, Task task)
        {
            Task retVal = null;

            string logMsg = "Rep_Task/TaskSave";

            string postAction;
            if (task.Id > 0)
            {
                postAction = string.Format("projects/{0}/stories/{1}/tasks/{2}",
                    story.ProjectId, story.Id, task.Id);
            }
            else
            {
                postAction = string.Format("projects/{0}/stories/{1}/tasks",
                    story.ProjectId, story.Id);
            }

            PostRequest postRequest = new PostRequest(postAction);

            postRequest.XmlDoc = task.XmlDoc;

            ApiResponse response;
            if (task.Id > 0)
            { response = PivotApi.Put(postRequest); }
            else
            { response = PivotApi.Post(postRequest); }

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
