using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IlinkDb.Entity
{
    public class User : EntityBase
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public UserRoleEnum UserRole { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
