using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IlinkDb.Data.PivotalApi
{
    internal class PivotApiGetRequest
    {
        public string Action { get; set; }

        public PivotApiGetRequest()
        {
        }

        public PivotApiGetRequest(string action)
        {
            Action = action;
        }
    }

}
