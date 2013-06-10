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

        public Link LinkGet(long id)
        {
            throw new NotImplementedException();
        }

        public Link LinkSave(Link link)
        {
            throw new NotImplementedException();
        }

        public bool LinkDelete(Link link)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Link> LinkList()
        {
            throw new NotImplementedException();
        }
    }
}
