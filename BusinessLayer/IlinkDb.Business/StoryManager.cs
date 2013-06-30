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

        public Story Get(long storyId)
        {
            return _db.StoryGet(TenantManager.ProjectId, storyId);
        }

        public Story Save(Story story)
        {
            story.ProjectId = TenantManager.ProjectId;
            return _db.StorySave(story);
        }

        public bool Delete(Story story)
        {
            story.ProjectId = TenantManager.ProjectId;
            return _db.StoryDelete(story);
        }

        public List<Story> List()
        {
            return _db.StoryList(TenantManager.ProjectId).ToList();
        }

        public List<Story> ListForLabel(string label)
        {
            return _db.StoryListForLabel(TenantManager.ProjectId, label).ToList();
        }

    }
}
