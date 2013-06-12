using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IlinkDb.Entity
{
    public enum UserRoleEnum
    {
        User = 0,       // Readonly
        PowerUser = 1,  // Allow Add, but no Edit or Delete (after xx minutes)
        Admin = 2       // Do it all
    }
}
