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
            return _db.Get<Tenant>(id);
        }

        public Tenant Save(Tenant tenant)
        {
            return _db.Save<Tenant>(tenant);
        }

        public bool Delete(Tenant tenant)
        {
            return _db.Delete<Tenant>(tenant);
        }


        public List<Tenant> List()
        {
            return _db.List<Tenant>().ToList();
        }
    }
}
