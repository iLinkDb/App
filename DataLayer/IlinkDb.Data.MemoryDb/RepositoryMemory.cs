﻿using System;
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

        public T Get<T>(long id) where T : EntityBase
        {
            T retVal = null;

            if (typeof(T).Name == "Link")
            {
                foreach (Link item in _linkList)
                {
                    if (item.Id == id)
                    { retVal = item as T; break; }
                }
            }
            else if (typeof(T).Name == "Tenant")
            {
                foreach (Tenant item in _tenantList)
                {
                    if (item.Id == id)
                    { retVal = item as T; break; }
                }
            }
            else
            {
                throw new NotImplementedException(typeof(T).Name + " for Add method not implemented in RepositoryMemory");
            }
            return retVal;
        }

        public T Save<T>(T t) where T : EntityBase
        {
            T retVal = t;

            if (typeof(T).Name == "Link")
            {
                t.Id = _linkListCount++;
                Link link = t as Link;
                _linkList.Add(link);
                retVal = t;
            }
            else if (typeof(T).Name == "Tenant")
            {
                Tenant existing = null;
                if (t.Id > 0)
                { existing = _tenantList.First(o => o.Id == t.Id); }

                if (existing == null)
                {
                    t.Id = _tenantListCount++;
                    Tenant tenant = t as Tenant;
                    _tenantList.Add(tenant);
                }
                else
                {
                    Tenant tenant = t as Tenant;
                    existing.Domain = tenant.Domain;
                }
                retVal = t;
            }
            else
            {
                throw new NotImplementedException(typeof(T).Name + " for Save method not implemented in RepositoryMemory");
            }
            return retVal;
        }

        public IQueryable<T> List<T>() where T : EntityBase
        {
            if (typeof(T).Name == "Link")
            {
                return ((IEnumerable<T>)_linkList).Select(x => x).AsQueryable();
            }
            else if (typeof(T).Name == "Tenant")
            {
                return ((IEnumerable<T>)_tenantList).Select(x => x).AsQueryable();
            }
            throw new NotImplementedException(typeof(T).Name + " for List method not implemented in RepositoryMemory");
        }

        public bool Delete<T>(T t) where T : EntityBase
        {
            bool retVal = false;

            if (typeof(T).Name == "Link")
            {
                Link existing = _linkList.First(o => o.Id == t.Id);
                if (existing != null)
                { _linkList.Remove(existing); retVal = true; }
            }
            else if (typeof(T).Name == "Tenant")
            {
                Tenant existing = _tenantList.First(o => o.Id == t.Id);
                if (existing != null)
                { _tenantList.Remove(existing); retVal = true; }
            }
            else
            {
                throw new NotImplementedException(typeof(T).Name + " for Add method not implemented in RepositoryMemory");
            }
            return retVal;
        }
    }
}
