using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

using IlinkDb.Data;
using IlinkDb.Entity;

namespace IlinkDb.Business
{
    public class LinkManager
    {
        private IRepository _db;

        public LinkManager()
            : this(false)
        {
        }

        public LinkManager(bool initializeData)
        {
            _db = Factory.Current.Repository;
            if (initializeData)
            { _db.Initialize(); }
        }

        public Link Get(long id)
        {
            return _db.LinkGet(id);
        }

        public Link Save(Link link)
        {
            return _db.LinkSave(link);
        }

        public List<Link> List()
        {
            return _db.LinkList().ToList();
        }
    }
}
