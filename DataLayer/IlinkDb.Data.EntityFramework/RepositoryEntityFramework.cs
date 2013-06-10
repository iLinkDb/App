using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IlinkDb.Entity;
using AppCommon;

namespace IlinkDb.Data.EntityFramework
{
    public class RepositoryEntityFramework : IRepository
    {
        internal EntityFrameworkContext _context;

        public RepositoryEntityFramework()
        {
            _context = new EntityFrameworkContext();
        }

        public void Initialize()
        {
            // Nothing to do here.  This method only works on memory databases.
        }

        public T Get<T>(long id) where T : EntityBase
        {
            T retVal = _context.Set<T>()
                    .Where(o => o.Id == id)
                    .FirstOrDefault();
            return retVal;
        }

        public T Save<T>(T t) where T : EntityBase
        {
            T retVal = null;

            retVal = _context.Set<T>().Add(t);
            _context.SaveChanges();

            return retVal;
        }

        public IQueryable<T> List<T>() where T : EntityBase
        {
            return _context.Set<T>();
        }


        public bool Delete<T>(T t) where T : EntityBase
        {
            throw new NotImplementedException();
        }


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
