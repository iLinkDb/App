using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IlinkDb.Entity;

namespace IlinkDb.Data.MemoryDb
{
    public partial class RepositoryMemory : IRepository
    {

        public Project ProjectGet(long id)
        {
            throw new NotImplementedException();
        }

        public Project ProjectSave(Project project)
        {
            throw new NotImplementedException();
        }

        public bool ProjectDelete(Project project)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Project> ProjectList()
        {
            throw new NotImplementedException();
        }
    }
}
