using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppCommon;
using IlinkDb.Entity;

namespace IlinkDb.Data.MemoryDb
{
    public partial class RepositoryMemory : IRepository
    {
        private IList<Note> _noteList = new List<Note>();
        private int _noteListCount;

        private IList<Note> NoteInitialize(RandomData random, Story story)
        {
            IList<Note> retVal = new List<Note>();

            // Add a random number of notes.
            for (int iLoop = 0; iLoop < random.Int(3, 6); iLoop++)
            {
                Note newNote = new Note
                {
                    Id = iLoop + 1,
                    StoryId = story.Id,
                    User = random.NameAndTitle(),
                    Text = random.Ipsum(6, 10) + " newItem",
                };

                retVal.Add(newNote);
                _noteList.Add(newNote);
            }
            _noteListCount = _noteList.Count;

            return retVal;
        }

        public Note NoteGet(Story story, long noteId)
        {
            Note retVal = _noteList.First<Note>(n => n.Id == noteId);
            return retVal;
        }

        public Note NoteSave(Story story, Note newItem)
        {
            Note retVal = null;

            string logMsg = "Rep_Note/Save";

            try
            {
                if (newItem.StoryId < 1)
                {
                    Logging.LogError(logMsg + ", ERROR - Invalid StoryId");
                }
                else
                {
                    if (newItem.Id > 0)
                    {
                        foreach (Story item in _storyList)
                        {
                            if (item.Id == newItem.StoryId)
                            {
                                if (newItem.Id <= _noteList.Count)
                                {
                                    retVal = _noteList.First(o => o.Id == newItem.Id);
                                    break;
                                }
                            }
                        }
                    }

                    if (retVal == null)
                    {
                        newItem.Id = ++_noteListCount;
                        _noteList.Add(newItem);
                        retVal = newItem;
                    }
                    else
                    {
                        retVal.Text = newItem.Text;
                        retVal.User = newItem.User;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.LogError(logMsg + ", EXCEPTION: " + ex.Message, ex);
            }

            return retVal;
        }

        public bool NoteDelete(Note note)
        {
            bool retVal = false;

            Note existing = _noteList.First(o => o.Id == note.Id);
            if (existing != null)
            {
                _noteList.Remove(existing);
                retVal = true;
            }

            return retVal;
        }

        public IQueryable<Note> NoteList(Story story)
        {
            //return ((IEnumerable<Note>)_noteList).Select(x => x).AsQueryable();
            return ((IEnumerable<Note>)_noteList).Where(x => x.StoryId == story.Id).AsQueryable();

        }
    }
}
