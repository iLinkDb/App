using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using IlinkDb.Entity;
using AppCommon;

namespace IlinkDb.Data.DynamoDb
{
    public partial class RepositoryDynamoDb : IRepository
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
