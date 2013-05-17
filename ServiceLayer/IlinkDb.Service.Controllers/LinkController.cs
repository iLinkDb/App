using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Web.Http;
using System.Net.Http;

using IlinkDb.Entity;
using IlinkDb.Business;

using AppCommon;

namespace IlinkDb.Service.Controllers
{
    public class LinkController : ApiController
    {

        [AcceptVerbs("GET")]
        public HttpResponseMessage List()
        {
            HttpResponseMessage retVal = new HttpResponseMessage();
            string logMsg = "LinkController/List";

            try
            {
                LinkManager mgr = new LinkManager();
                IEnumerable<Link> links = mgr.List();

                Logging.LogDebug(logMsg + " Links Count: " + links.Count());

                retVal = Request.CreateResponse(HttpStatusCode.OK, links);
            }
            catch (Exception ex)
            {
                Logging.LogError(logMsg + ", EXCEPTION: " + ex.Message, ex);
                retVal.StatusCode = HttpStatusCode.InternalServerError;
                retVal.Content = new StringContent(ex.Message);
            }
            return retVal;
        }
    }
}
