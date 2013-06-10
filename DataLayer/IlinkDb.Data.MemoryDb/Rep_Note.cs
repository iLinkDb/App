﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IlinkDb.Entity;

namespace IlinkDb.Data.MemoryDb
{
    public partial class RepositoryMemory : IRepository
    {
        private IList<Note> _noteList;
        private int _noteListCount;

        private void NoteInitialize(RandomData random)
        {
            _noteList = new List<Note>();

            // Add one known domain to the list.
            _noteList.Add(new Note
            {
                Id = _noteList.Count + 1,
                NotePath = "/",
                Status = NoteStatusEnum.Open,
                Text = "The main note for the root of the site."
            });
            _noteListCount = _noteList.Count + 1;

            // Add a random number of notes.
            for (int iLoop = 0; iLoop < random.Int(5, 10); iLoop++)
            {
                _noteList.Add(new Note
                {
                    Id = iLoop + 1,
                    NotePath = "/tenant",
                    Status = NoteStatusEnum.Open,
                    Text = random.Ipsum(10, 30)
                });
            }
        }

        public Note NoteGet(long id)
        {
            Note retVal = _noteList.First<Note>(n => n.Id == id);
            //foreach (Note item in _noteList)
            //{
            //    if (item.Id == id)
            //    { retVal = item; break; }
            //}
            return retVal;
        }

        public Note NoteSave(Note newOne)
        {
            Note retVal = null;

            if (newOne.Id > 0)
            { retVal = _noteList.First(o => o.Id == newOne.Id); }

            if (retVal == null)
            {
                newOne.Id = _noteListCount++;
                _noteList.Add(newOne);
                retVal = newOne;
            }
            else
            {
                retVal.NotePath = newOne.NotePath;
                retVal.Status = newOne.Status;
                retVal.Text = newOne.Text;
                retVal.DateAddedUtc = newOne.DateAddedUtc;
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

        public IQueryable<Note> NoteList()
        {
            return ((IEnumerable<Note>)_noteList).Select(x => x).AsQueryable();
        }

        public IQueryable<Note> NoteListForPath(string path)
        {
            return ((IEnumerable<Note>)_noteList).Select(x => x).Where(w => w.NotePath == path).AsQueryable();
        }
    }
}
