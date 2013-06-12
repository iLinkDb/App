using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace IlinkDb.Entity
{
    public class Note : EntityBase
    {
        [Required]
        public string NotePath { get; set; }

        public long ParentId { get; set; }

        [Required]
        public NoteTypeEnum NoteType { get; set; }

        [Required]
        public NoteStatusEnum Status { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public DateTime DateAddedUtc { get; set; }

        public Note()
        {
            DateAddedUtc = DateTime.UtcNow;
        }
    }
}
