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
        private void NoteInitialize()
        {
            //
        }

        public Note NoteGet(Story story, long noteId)
        {
            Note retVal = null;

            string logMsg = "Rep_Note/NoteGet";

            try
            {
                string action = string.Format("projects/{0}/stories/{1}/notes/{2}", story.ProjectId, story.Id, noteId);
                GetRequest getRequest = new GetRequest(action);
                ApiResponse response = PivotApi.Get(getRequest);

                if (response.Success)
                {
                    XDocument doc = XDocument.Parse(response.Xml);

                    var query = from node in doc.Descendants("note")
                                select new Note(node);

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

        public Note NoteSave(Story story, Note note)
        {
            Note retVal = null;

            string logMsg = "Rep_Note/NoteSave";

            string postAction = string.Format("projects/{0}/stories/{1}/notes",
                story.ProjectId, story.Id);
            PostRequest postRequest = new PostRequest(postAction);

            postRequest.XmlDoc = note.XmlDoc;

            ApiResponse response;
            if (note.Id > 0)
            { response = PivotApi.Put(postRequest); }
            else
            { response = PivotApi.Post(postRequest); }

            if (response.Success)
            {
                XDocument doc = XDocument.Parse(response.Xml);
                XElement node = doc.Descendants("note").FirstOrDefault();

                retVal = new Note(node);
            }
            else
            {
                Logging.LogError(logMsg + string.Format(", ERROR Code: {0}, Message: {1}",
                  response.StatusCode, response.ErrorMessage));
            }
            return retVal;
        }

        public bool NoteDelete(Note note)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Note> NoteList(Story story)
        {
            throw new NotImplementedException();
        }
    }
}
