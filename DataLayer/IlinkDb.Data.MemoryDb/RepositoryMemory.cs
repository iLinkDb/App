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
        private IList<Link> _links;

        public RepositoryMemory()
        {
            Initialize();
        }

        public void Initialize()
        {
            var random = new RandomData();

            _links = new List<Link>();
            for (int iLoop = 0; iLoop < random.Int(3, 10); iLoop++)
            {
                _links.Add(new Link
                {
                    Id = iLoop + 1,
                    Url = "www." + random.Word() + random.Word() + ".com"
                });
            }
        }

        public IQueryable<T> List<T>() where T : EntityBase
        {
            if (typeof(T).Name == "Link")
            {
                return ((IEnumerable<T>)_links).Select(x => x).AsQueryable();
            }
            throw new NotImplementedException(typeof(T).Name + " for List method not implemented in RepositoryMemory");
        }

        public T Add<T>(T t) where T : EntityBase
        {
            T retVal = t;

            if (typeof(T).Name == "Link")
            {
                t.Id = _links.Count + 1;
                Link link = t as Link;
                _links.Add(link);
                retVal = t;
            }
            //else if (typeof(T).Name == "InfoInquiry")
            //{
            //    t.Id = _infoInquiries.Count + 1;
            //    InfoInquiry p = t as InfoInquiry;
            //    _infoInquiries.Add(p);
            //    retVal = t;
            //}
            else
            {
                throw new NotImplementedException(typeof(T).Name + " for Add method not implemented in RepositoryMemory");
            }
            return retVal;
        }

    }
}
