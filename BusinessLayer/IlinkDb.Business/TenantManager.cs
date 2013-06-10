using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IlinkDb.Data;
using IlinkDb.Entity;

namespace IlinkDb.Business
{
    public class TenantManager
    {
        private IRepository _db;

        public TenantManager()
            : this(false)
        {
        }

        public TenantManager(bool initializeData)
        {
            _db = Factory.Current.Repository;
            if (initializeData)
            { _db.Initialize(); }
        }

        public Tenant Get(long id)
        {
            return _db.TenantGet(id);
        }

        public Tenant Save(Tenant tenant)
        {
            return _db.TenantSave(tenant);
        }

        public bool Delete(Tenant tenant)
        {
            return _db.TenantDelete(tenant);
        }

        public List<Tenant> List()
        {
            return _db.TenantList().ToList();
        }
    }
}
