using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using IlinkDb.Entity;

namespace IlinkDb.Data.EntityFramework
{
    public class EntityFrameworkContext : DbContext
    {
        public EntityFrameworkContext()
        {
        }

        public DbSet<Link> Links { get; set; }
    }
}
