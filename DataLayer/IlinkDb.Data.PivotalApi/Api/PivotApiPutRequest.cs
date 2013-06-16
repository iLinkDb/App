using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IlinkDb.Data.PivotalApi
{
    public class PivotApiPutRequest
    {
      public string PostRequest { get; set; }
      public string Action { get; set; }

      public PivotApiPutRequest()
      {
      }

      public PivotApiPutRequest(string action, string postRequest)
      {
         Action = action;
         PostRequest = postRequest;
      }
    }
}
