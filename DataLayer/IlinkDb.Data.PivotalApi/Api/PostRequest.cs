using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace IlinkDb.Data.PivotalApi
{
    public class PostRequest
    {
        public string Action { get; set; }
        public XmlDocument XmlDoc { get; set; }

        public PostRequest()
        {
        }

        public PostRequest(string action)
        {
            Action = action;
        }
    }
}
