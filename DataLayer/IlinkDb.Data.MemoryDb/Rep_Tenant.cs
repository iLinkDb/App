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
        private IList<Tenant> _tenantList;
        private int _tenantListCount;

        private void TenantInitialize(RandomData random)
        {
            _tenantList = new List<Tenant>();
            for (int iLoop = 0; iLoop < random.Int(5, 10); iLoop++)
            {
                _tenantList.Add(new Tenant
                {
                    Id = iLoop + 1,
                    Domain = random.Word()
                });
            }
            // Add one known domain to the list.
            _tenantList.Add(new Tenant
            {
                Id = _tenantList.Count + 1,
                Domain = "test"
            });
            _tenantListCount = _tenantList.Count + 1;

        }

        public Tenant TenantGet(long id)
        {
            Tenant retVal = _tenantList.First<Tenant>(t => t.Id == id);
            //foreach (Tenant item in _tenantList)
            //{
            //    if (item.Id == id)
            //    { retVal = item; break; }
            //}
            return retVal;
        }

        public Tenant TenantSave(Tenant newOne)
        {
            Tenant retVal = null;

            if (newOne.Id > 0)
            { retVal = _tenantList.First(o => o.Id == newOne.Id); }

            if (retVal == null)
            {
                newOne.Id = _tenantListCount++;
                _tenantList.Add(newOne);
                retVal = newOne;
            }
            else
            {
                retVal.Domain = newOne.Domain;
            }

            return retVal;
        }

        public bool TenantDelete(Tenant tenant)
        {
            bool retVal = false;

            Tenant existing = _tenantList.First(o => o.Id == tenant.Id);
            if (existing != null)
            {
                _tenantList.Remove(existing);
                retVal = true;
            }

            return retVal;
        }

        public IQueryable<Tenant> TenantList()
        {
            return ((IEnumerable<Tenant>)_tenantList).Select(x => x).AsQueryable();
        }
    }
}
