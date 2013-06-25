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

        public Task Get(long id)
        {
            throw new NotImplementedException();
        }

        public Task Save(Task task)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Task task)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Task> List(Story story)
        {
            throw new NotImplementedException();
        }

        public Task Add(Story story, Task task)
        {
            throw new NotImplementedException();
        }
    }
}
