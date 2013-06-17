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
            throw new NotImplementedException();
        }

        public Note Add(Story story, Note note)
        {
            Note retVal = null;

            string logMsg = "Rep_Note/Add";

            string postAction = string.Format("projects/{0}/stories/{1}/notes",
                story.ProjectId, story.Id);
            PostRequest postRequest = new PostRequest(postAction);

            postRequest.XmlDoc = note.XmlDoc;

            ApiResponse response = PivotApi.Post(postRequest);

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

        public Note NoteGet(long id)
        {
            throw new NotImplementedException();
        }

        public Note NoteSave(Note note)
        {
            throw new NotImplementedException();
        }

        public bool NoteDelete(Note note)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Note> NoteList()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Note> NoteListForPath(string path)
        {
            throw new NotImplementedException();
        }

    }
}
