using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using IlinkDb.Entity;

namespace IlinkDb.Data.EntityFramework
{
    public partial class RepositoryEntityFramework : IRepository
    {
        public Task TaskGet(Story story, long id)
        {
            throw new NotImplementedException();
        }

        public Task TaskSave(Story story, Task task)
        {
            throw new NotImplementedException();
        }

        public bool TaskDelete(Task task)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Task> TaskList(Story story)
        {
            throw new NotImplementedException();
        }
    }
}
