using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IlinkDb.Entity;

namespace IlinkDb.Data.MemoryDb
{
    public class RepositoryMemory : IRepository
    {
        private IList<Link> _linkList;
        private IList<Tenant> _tenantList;

        private int _linkListCount;
        private int _tenantListCount;

        public RepositoryMemory()
        {
            Initialize();
        }

        public void Initialize()
        {
            var random = new RandomData();

            LinkInitialize(random);
            TenantInitialize(random);
        }

        #region Tenant

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
            Tenant retVal = null;
            foreach (Tenant item in _tenantList)
            {
                if (item.Id == id)
                { retVal = item; break; }
            }
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

        #endregion

        #region Link

        private void LinkInitialize(RandomData random)
        {
            _linkList = new List<Link>();
            for (int iLoop = 0; iLoop < random.Int(3, 10); iLoop++)
            {
                _linkList.Add(new Link
                {
                    Id = iLoop + 1,
                    Url = "www." + random.Word() + random.Word() + ".com"
                });
            }
            _linkListCount = _linkList.Count + 1;
        }

        public Link LinkGet(long id)
        {
            Link retVal = null;
            foreach (Link item in _linkList)
            {
                if (item.Id == id)
                { retVal = item; break; }
            }
            return retVal;
        }

        public Link LinkSave(Link newOne)
        {
            Link retVal = null;

            if (newOne.Id > 0)
            { retVal = _linkList.First(o => o.Id == newOne.Id); }

            if (retVal == null)
            {
                newOne.Id = _linkListCount++;
                _linkList.Add(newOne);
                retVal = newOne;
            }
            else
            {
                retVal.Url = newOne.Url;
            }

            return retVal;
        }

        public bool LinkDelete(Link link)
        {
            bool retVal = false;

            Link existing = _linkList.First(o => o.Id == link.Id);
            if (existing != null)
            {
                _linkList.Remove(existing);
                retVal = true;
            }

            return retVal;
        }

        public IQueryable<Link> LinkList()
        {
            return ((IEnumerable<Link>)_linkList).Select(x => x).AsQueryable();
        }

        #endregion



        public IQueryable<Tenant> List()
        {
            throw new NotImplementedException();
        }
    }
}
