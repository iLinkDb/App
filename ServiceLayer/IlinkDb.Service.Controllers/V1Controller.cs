using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.Http;
using System.Net.Http;

using AppCommon;

namespace IlinkDb.Service.Controllers
{
    public class V1Controller : ApiController
    {
        //public HttpResponseMessage Get()
        //{
        //    return new HttpResponseMessage
        //    {
        //        Content = new StringContent("Hello HTTP")
        //    };
        //}

        [AcceptVerbs("GET")]
        public HttpResponseMessage Time()
        {
            string logMsg = "V1Controller/Time";
            string now = DateTime.Now.ToString();
            Logging.LogDebug(logMsg + ": " + now);
            return new HttpResponseMessage
            {
                Content = new StringContent(now)
            };
        }

        [AcceptVerbs("GET")]
        public HttpResponseMessage Version()
        {
            string logMsg = "V1Controller/Version";
            string version = Computer.GetVersion();
            Logging.LogDebug(logMsg + ": " + version);
            return new HttpResponseMessage
            {
                Content = new StringContent(version)
            };
        }

    }

}
