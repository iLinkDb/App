using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IlinkDb.Entity;

namespace IlinkDb.Data
{
    public interface IRepository
    {
        void Initialize();
        T Add<T>(T t) where T : EntityBase;
        IQueryable<T> List<T>() where T : EntityBase;
    }
}
