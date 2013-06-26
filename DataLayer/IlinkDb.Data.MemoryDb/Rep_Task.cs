using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using IlinkDb.Entity;
using AppCommon;

namespace IlinkDb.Data.MemoryDb
{
    public partial class RepositoryMemory : IRepository
    {
        private IList<Task> _taskList = new List<Task>();
        private int _taskListCount;

        private IList<Task> TaskInitialize(RandomData random, Story story)
        {
            IList<Task> retVal = new List<Task>();

            // Add a random number of tasks.
            for (int iLoop = 0; iLoop < random.Int(5, 10); iLoop++)
            {
                Task newTask = new Task
                {
                    Id = iLoop + 1,
                    StoryId = story.Id,
                    Description = random.Ipsum(6, 10) + " task",
                    Position = iLoop + 1
                };

                retVal.Add(newTask);
                _taskList.Add(newTask);
            }
            return retVal;
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

            string logMsg = "Rep_Task/Save";

            try
            {
                if (newOne.StoryId < 1)
                {
                    Logging.LogError(logMsg + ", ERROR - Invalid StoryId");
                }
                else
                {
                    if (newOne.Id > 0)
                    {
                        foreach (Story story in _storyList)
                        {
                            if (story.Id == newOne.StoryId)
                            {
                                if (newOne.Id <= story.Tasks.Count)
                                {
                                    retVal = story.Tasks.First(o => o.Id == newOne.Id);
                                    break;
                                }
                            }
                        }
                    }

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
                }

            }
            catch (Exception ex)
            {
                Logging.LogError(logMsg + ", EXCEPTION: " + ex.Message, ex);
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
