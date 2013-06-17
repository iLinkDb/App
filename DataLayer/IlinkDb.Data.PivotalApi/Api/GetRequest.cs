using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IlinkDb.Data.PivotalApi
{
    internal class GetRequest
    {
        public string Action { get; set; }

        public GetRequest()
        {
        }

        public GetRequest(string action)
        {
            Action = action;
        }
    }

}
