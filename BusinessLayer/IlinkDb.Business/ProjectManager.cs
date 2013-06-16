using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IlinkDb.Data;
using IlinkDb.Entity;

namespace IlinkDb.Business
{
    public class ProjectManager
    {
        private IRepository _db;

        public ProjectManager()
            : this(false)
        {
        }

        public ProjectManager(bool initializeData)
        {
            _db = Factory.Current.Repository;
            if (initializeData)
            { _db.Initialize(); }
        }

        public Project Get(long id)
        {
            return _db.ProjectGet(id);
        }

        public Project Save(Project project)
        {
            return _db.ProjectSave(project);
        }

        public bool Delete(Project project)
        {
            return _db.ProjectDelete(project);
        }

        public List<Project> List()
        {
            return _db.ProjectList().ToList();
        }
    }
}
