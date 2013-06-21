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
        private IList<Link> _linkList;
        private int _linkListCount;

        private void LinkInitialize(RandomData random)
        {
            _linkList = new List<Link>();
            for (int iLoop = 0; iLoop < random.Int(7, 10); iLoop++)
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

    }
}
