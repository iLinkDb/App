using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IlinkDb.Data;
using IlinkDb.Entity;

namespace IlinkDb.Business
{
    public class StoryManager
    {
        private IRepository _db;

        public StoryManager()
            : this(false)
        {
        }

        public StoryManager(bool initializeData)
        {
            _db = Factory.Current.Repository;
            if (initializeData)
            { _db.Initialize(); }
        }

        public Story Get(long id)
        {
            return _db.StoryGet(id);
        }

        public Story Save(Story story)
        {
            return _db.StorySave(story);
        }

        public bool Delete(Story story)
        {
            return _db.StoryDelete(story);
        }

        public List<Story> List(long projectId)
        {
            return _db.StoryList(projectId).ToList();
        }

        public Note AddNote(long projectId, Note note)
        {
            return _db.AddNote(projectId, note);
        }
    }
}
