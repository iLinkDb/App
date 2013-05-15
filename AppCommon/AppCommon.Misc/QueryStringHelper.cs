using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ServiceModel.Web;

namespace AppCommon
{
   public class QueryStringHelper
   {
      static public int GetInt(string parmName)
      {
         return GetInt(parmName, 0, false);
      }

      static public int GetInt(string parmName, int defaultValue)
      {
         return GetInt(parmName, defaultValue, false);
      }

      static public int GetRequiredInt(string parmName)
      {
         return GetInt(parmName, 0, true);
      }

      static public int GetInt(string parmName, int defaultValue, bool isRequired)
      {
         string value = GetString(parmName, defaultValue.ToString(), isRequired);
         int returnValue = defaultValue;

         if (!string.IsNullOrEmpty(value))
         {
            if (!Int32.TryParse(value, out returnValue))
               SetBadRequest(string.Format("Invalid query parameter \"{0}\", value \"{1}\"", parmName, value));
         }

         return returnValue;
      }

      static public string GetString(string parmName)
      {
         return GetString(parmName, string.Empty, false);
      }

      static public string GetString(string parmName, string defaultValue)
      {
         return GetString(parmName, defaultValue, false);
      }

      static public string GetRequiredString(string parmName)
      {
         return GetString(parmName, string.Empty, true);
      }

      static public string GetString(string parmName, string defaultValue, bool isRequired)
      {
         if (WebOperationContext.Current == null)
            throw new InvalidOperationException("WebOperationContext is null");

         string returnValue = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters[parmName];

         // if the parameter is required then this is a bad request - this will throw ArgumentException
         if (isRequired && string.IsNullOrEmpty(returnValue))
         {
            SetBadRequest(string.Format("Missing required query parameter \"{0}\"", parmName));
         }
         else if (returnValue == null)
         {
            // If null (not found) use default value
            returnValue = defaultValue;
         }

         return returnValue;
      }

      static private void SetBadRequest(string description)
      {
         if (WebOperationContext.Current != null)
         {
            WebOperationContext.Current.OutgoingResponse.StatusDescription = description;
            WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
            WebOperationContext.Current.OutgoingResponse.SuppressEntityBody = true;
         }
         throw new ArgumentException(description);
      }
   }

}
