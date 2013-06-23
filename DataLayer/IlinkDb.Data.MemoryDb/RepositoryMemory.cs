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
        public RepositoryMemory()
        {
            Initialize();
        }

        public void Initialize()
        {
            var random = new RandomData();

            LinkInitialize(random);
            TenantInitialize(random);
            NoteInitialize(random);
            StoryInitialize(random);
        }
    }
}
