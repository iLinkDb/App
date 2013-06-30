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
        public string Parameters { get; set; }

        public GetRequest()
        {
        }

        public GetRequest(string action)
        {
            Action = action;
        }
        public GetRequest(string action, string parameters)
        {
            Action = action;
            Parameters = parameters;
        }
    }

}
