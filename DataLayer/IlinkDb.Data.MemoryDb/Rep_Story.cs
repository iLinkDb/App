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
        private IList<Story> _storyList;
        private int _storyListCount;

        private void StoryInitialize(RandomData random)
        {
            _storyList = new List<Story>();
            for (int iLoop = 0; iLoop < random.Int(5, 10); iLoop++)
            {
                Story newStory = new Story
                {
                    Id = iLoop + 1,
                    ProjectId = 830205,
                    Name = random.Ipsum(3, 8),
                    Description = random.Ipsum(10, 20),
                    Estimate = random.Int(0, 3)
                };

                newStory.Tasks = TaskInitialize(random, newStory).ToList();

                _storyList.Add(newStory);
            }

        }

        public Story StoryGet(long id)
        {
            Story retVal = _storyList.First<Story>(t => t.Id == id);
            return retVal;
        }

        public Story StorySave(Story newOne)
        {
            Story retVal = null;

            if (newOne.Id > 0)
            { retVal = _storyList.First(o => o.Id == newOne.Id); }

            if (retVal == null)
            {
                newOne.Id = _storyListCount++;
                _storyList.Add(newOne);
                retVal = newOne;
            }
            else
            {
                retVal.Name = newOne.Name;
                retVal.ProjectId = newOne.ProjectId;
                retVal.Description = newOne.Description;
                retVal.Estimate = newOne.Estimate;
            }

            return retVal;
        }

        public bool StoryDelete(Story story)
        {
            bool retVal = false;

            Story existing = _storyList.First(o => o.Id == story.Id);
            if (existing != null)
            {
                _storyList.Remove(existing);
                retVal = true;
            }

            return retVal;
        }

        public IQueryable<Story> StoryList(long projectId)
        {
            return ((IEnumerable<Story>)_storyList).Select(x => x).Where(x => x.ProjectId == projectId).AsQueryable();
        }

        public Story Add(long projectId, Story story)
        {
            story.ProjectId = projectId;
            return StorySave(story);
        }
    }
}
