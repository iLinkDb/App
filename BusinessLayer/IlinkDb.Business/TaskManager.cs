using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using IlinkDb.Data;
using IlinkDb.Entity;

namespace IlinkDb.Business
{
    public class TaskManager
    {
        private IRepository _db;

        public TaskManager()
            : this(false)
        {
        }

        public TaskManager(bool initializeData)
        {
            _db = Factory.Current.Repository;
            if (initializeData)
            { _db.Initialize(); }
        }

        public Task Get(Story story, long id)
        {
            story.ProjectId = TenantManager.ProjectId;
            return _db.TaskGet(story, id);
        }

        public Task Save(Story story, Task task)
        {
            story.ProjectId = TenantManager.ProjectId;
            return _db.TaskSave(story, task);
        }

        public bool Delete(Task task)
        {
            return _db.TaskDelete(task);
        }

        public List<Task> List(Story story)
        {
            story.ProjectId = TenantManager.ProjectId;
            return _db.TaskList(story).ToList();
        }

    }
}
