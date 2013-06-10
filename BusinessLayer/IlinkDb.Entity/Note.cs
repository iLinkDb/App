using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IlinkDb.Entity
{
    public class Note : EntityBase
    {
        public string NotePath { get; set; }
        public NoteStatusEnum Status { get; set; }
        public string Text { get; set; }
        public DateTime DateAddedUtc { get; set; }

        public Note()
        {
            DateAddedUtc = DateTime.UtcNow;
        }
    }
}
