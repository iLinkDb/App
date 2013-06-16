using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IlinkDb.Entity;

namespace IlinkDb.Data.EntityFramework
{
    public partial class RepositoryEntityFramework : IRepository
    {

        public Story StoryGet(long id)
        {
            throw new NotImplementedException();
        }

        public Story StorySave(Story story)
        {
            throw new NotImplementedException();
        }

        public bool StoryDelete(Story story)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Story> StoryList(long projectId)
        {
            throw new NotImplementedException();
        }
    }
}
