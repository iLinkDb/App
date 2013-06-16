using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IlinkDb.Entity;

namespace IlinkDb.Data.DynamoDb
{
    public partial class RepositoryDynamoDb : IRepository
    {

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


        public Note AddNote(long projectId, Note note)
        {
            throw new NotImplementedException();
        }
    }
}
