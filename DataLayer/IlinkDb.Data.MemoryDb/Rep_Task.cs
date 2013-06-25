using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using IlinkDb.Entity;

namespace IlinkDb.Data.MemoryDb
{
    public partial class RepositoryMemory : IRepository
    {
        private IList<Task> _taskList;
        private int _taskListCount;

        private IList<Task> TaskInitialize(RandomData random, Story story)
        {
            _taskList = new List<Task>();

            // Add a random number of tasks.
            for (int iLoop = 0; iLoop < random.Int(5, 10); iLoop++)
            {
                _taskList.Add(new Task
                {
                    Id = iLoop + 1,
                    Description = random.Ipsum(10, 30) + " task",
                    Position = iLoop + 1
                });
            }
            return _taskList;
        }

        public Task Add(Story story, Task task)
        {
            throw new NotImplementedException();
        }

        public Task Get(long id)
        {
            Task retVal = _taskList.First<Task>(t => t.Id == id);
            return retVal;
        }

        public Task Save(Task newOne)
        {
            Task retVal = null;

            if (newOne.Id > 0)
            { retVal = _taskList.First(o => o.Id == newOne.Id); }

            if (retVal == null)
            {
                newOne.Id = _taskListCount++;
                _taskList.Add(newOne);
                retVal = newOne;
            }
            else
            {
                retVal.Description = newOne.Description;
            }

            return retVal;
        }

        public bool Delete(Task task)
        {
            bool retVal = false;

            Task existing = _taskList.First(o => o.Id == task.Id);
            if (existing != null)
            {
                _taskList.Remove(existing);
                retVal = true;
            }

            return retVal;
        }

        public IQueryable<Task> List(Story story)
        {
            return ((IEnumerable<Task>)_taskList).Select(x => x).AsQueryable();
        }
    }
}
