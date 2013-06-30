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
        public Note NoteGet(Story story, long noteId)
        {
            throw new NotImplementedException();
        }

        public Note NoteSave(Story story, Note newItem)
        {
            throw new NotImplementedException();
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
