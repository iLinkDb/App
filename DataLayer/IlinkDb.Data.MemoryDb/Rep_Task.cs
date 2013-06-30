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
            for (int iLoop = 0; iLoop < random.Int(3, 6); iLoop++)
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
            _taskListCount = _taskList.Count;
            return retVal;
        }

        public Task TaskGet(Story story, long id)
        {
            Task retVal = _taskList.First<Task>(t => t.Id == id);
            return retVal;
        }

        public Task TaskSave(Story story, Task task)
        {
            Task retVal = null;

            string logMsg = "Rep_Task/Save";

            try
            {
                if (task.StoryId < 1)
                {
                    Logging.LogError(logMsg + ", ERROR - Invalid StoryId");
                }
                else
                {
                    if (task.Id > 0)
                    {
                        foreach (Story item in _storyList)
                        {
                            if (item.Id == task.StoryId)
                            {
                                if (task.Id <= _taskList.Count)
                                {
                                    retVal = _taskList.First(o => o.Id == task.Id);
                                    break;
                                }
                            }
                        }
                    }

                    if (retVal == null)
                    {
                        task.Id = ++_taskListCount;
                        _taskList.Add(task);
                        retVal = task;
                    }
                    else
                    {
                        retVal.Description = task.Description;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.LogError(logMsg + ", EXCEPTION: " + ex.Message, ex);
            }

            return retVal;
        }

        public bool TaskDelete(Task task)
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

        public IQueryable<Task> TaskList(Story story)
        {
            return ((IEnumerable<Task>)_taskList).Where(x => x.StoryId == story.Id).AsQueryable();
        }

    }
}
