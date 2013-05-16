using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IlinkDb.Entity;
using AppCommon;

namespace IlinkDb.Data.EntityFramework
{
    public class RepositoryEntityFramework : IRepository
    {
        internal EntityFrameworkContext _context;

        public RepositoryEntityFramework()
        {
            _context = new EntityFrameworkContext();
        }

        //public IQueryable<Person> ListPeople()
        //{
        //   return _context.Persons.AsQueryable();
        //}

        //public Person AddPerson(Person person)
        //{
        //   Person retVal = null;

        //   //using (var db = _context)
        //   //{
        //   //   retVal = db.Set<Person>().Add(person);
        //   //   db.SaveChanges();
        //   //}
        //   retVal = _context.Set<Person>().Add(person);
        //   _context.SaveChanges();

        //   return retVal;
        //}

        public IQueryable<T> List<T>() where T : EntityBase
        {
            return _context.Set<T>();
        }


        public T Add<T>(T t) where T : EntityBase
        {
            T retVal = null;

            retVal = _context.Set<T>().Add(t);
            _context.SaveChanges();

            return retVal;
        }


        public void Initialize()
        {
            // Nothing to do here.  This method only works on memory databases.
        }
    }

}
