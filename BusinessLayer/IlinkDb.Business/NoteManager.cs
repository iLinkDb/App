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

        public Note Get(long id)
        {
            return _db.NoteGet(id);
        }

        public Note AddNote(Story story, Note note)
        {
            return _db.Add(story, note);
        }

        //public Note Save(Note note)
        //{
        //    return _db.NoteSave(note);
        //}

        public bool Delete(Note note)
        {
            return _db.NoteDelete(note);
        }

        public List<Note> List()
        {
            return _db.NoteList().ToList();
        }

    }
}
