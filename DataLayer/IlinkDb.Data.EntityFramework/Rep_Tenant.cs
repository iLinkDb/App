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

        public Tenant TenantGet(long id)
        {
            throw new NotImplementedException();
        }

        public Tenant TenantSave(Tenant tenant)
        {
            throw new NotImplementedException();
        }

        public bool TenantDelete(Tenant tenant)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Tenant> TenantList()
        {
            throw new NotImplementedException();
        }
    }
}
