using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IlinkDb.Data;
using IlinkDb.Entity;

namespace IlinkDb.Business
{
    public class NoteManager
    {
        private IRepository _db;

        public NoteManager()
            : this(false)
        {
        }

        public NoteManager(bool initializeData)
        {
            _db = Factory.Current.Repository;
            if (initializeData)
            { _db.Initialize(); }
        }

        public Note Get(Story story, long noteId)
        {
            return _db.NoteGet(story, noteId);
        }

        public Note Save(Story story, Note note)
        {
            return _db.NoteSave(story, note);
        }

        public bool Delete(Note note)
        {
            return _db.NoteDelete(note);
        }

        public List<Note> List(Story story)
        {
            return _db.NoteList(story).ToList();
        }

    }
}
