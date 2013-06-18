using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace IlinkDb.Entity
{
    public enum StoryTypeEnum
    {
        // The PivotalTracker API appears to be case sensative about
        // this stuff, do we use the attribute to get the value to pass.
        [Description("feature")]
        Feature,
        [Description("bug")]
        Bug,
        [Description("chore")]
        Chore,
        [Description("release")]
        Release
    }
}
