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

        public Task Get(long id)
        {
            return _db.Get(id);
        }

        public Task Add(Story story, Task task)
        {
            return _db.Add(story, task);
        }

        //public Task Save(Task task)
        //{
        //    return _db.TaskSave(task);
        //}

        public bool Delete(Task task)
        {
            return _db.Delete(task);
        }

        public List<Task> List(Story story)
        {
            return _db.List(story).ToList();
        }

    }
}
