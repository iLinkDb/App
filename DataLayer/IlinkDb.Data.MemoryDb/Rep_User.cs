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

        public User UserGet(long id)
        {
            throw new NotImplementedException();
        }

        public User UserSave(User user)
        {
            throw new NotImplementedException();
        }

        public bool UserDelete(User user)
        {
            throw new NotImplementedException();
        }

        public IQueryable<User> UserList()
        {
            throw new NotImplementedException();
        }
    }
}
