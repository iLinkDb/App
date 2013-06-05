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
    public class TenantController : ApiController
    {

        [AcceptVerbs("GET")]
        public HttpResponseMessage Get(long id)
        {
            HttpResponseMessage retVal = new HttpResponseMessage();

            string logMsg = "TenantController/Get";

            try
            {
                TenantManager mgr = new TenantManager();
                Tenant tenant = mgr.Get(id);

                Logging.LogDebug(logMsg + string.Format(" Tenant: {0}" , tenant));

                retVal = Request.CreateResponse(HttpStatusCode.OK, tenant);
            }
            catch (Exception ex)
            {
                Logging.LogError(logMsg + ", EXCEPTION: " + ex.Message, ex);
                retVal.StatusCode = HttpStatusCode.InternalServerError;
                retVal.Content = new StringContent(ex.Message);
            }
            return retVal;
        }

        [AcceptVerbs("GET")]
        public HttpResponseMessage List()
        {
            HttpResponseMessage retVal = new HttpResponseMessage();

            string logMsg = "TenantController/List";

            try
            {
                TenantManager mgr = new TenantManager();
                IEnumerable<Tenant> tenants = mgr.List();

                Logging.LogDebug(logMsg + " Tenants Count: " + tenants.Count());

                retVal = Request.CreateResponse(HttpStatusCode.OK, tenants);
            }
            catch (Exception ex)
            {
                Logging.LogError(logMsg + ", EXCEPTION: " + ex.Message, ex);
                retVal.StatusCode = HttpStatusCode.InternalServerError;
                retVal.Content = new StringContent(ex.Message);
            }
            return retVal;
        }

        [AcceptVerbs("PUT")]
        public HttpResponseMessage Put(Tenant tenant)
        {
            HttpResponseMessage retVal = Request.CreateResponse(HttpStatusCode.OK, tenant);

            string logMsg = "TenantController/Put";

            try
            {
                TenantManager mgr = new TenantManager();

                Tenant updatedTenant = mgr.Save(tenant);

                Logging.LogDebug(logMsg + string.Format(" Tenant: {0}", updatedTenant));

                retVal = Request.CreateResponse(HttpStatusCode.OK, updatedTenant);

                //string uri = Url.Route(null, new { id = timesheet.Id }); // Where is the modified timesheet?
                //response.Headers.Location = new Uri(Request.RequestUri, uri);

            }
            catch (Exception ex)
            {
                Logging.LogError(logMsg + ", EXCEPTION: " + ex.Message, ex);
                retVal.StatusCode = HttpStatusCode.InternalServerError;
                retVal.Content = new StringContent(ex.Message);
            }
            return retVal;

        }

        [AcceptVerbs("DELETE")]
        public HttpResponseMessage Delete(Tenant tenant)
        {
            HttpResponseMessage retVal = Request.CreateResponse(HttpStatusCode.OK, tenant);

            string logMsg = "TenantController/Delete";

            try
            {
                TenantManager mgr = new TenantManager();

                bool success = mgr.Delete(tenant);

                string delMsg = "Success deleting";
                if (!success)
                { delMsg = "Failure deleting"; }

                Logging.LogDebug(logMsg + string.Format(" {0} Tenant: {1}", delMsg, tenant));

                retVal = Request.CreateResponse(HttpStatusCode.OK, success);

                //string uri = Url.Route(null, new { id = timesheet.Id }); // Where is the modified timesheet?
                //response.Headers.Location = new Uri(Request.RequestUri, uri);

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
