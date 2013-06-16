using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;

namespace IlinkDb.Data.PivotalApi
{
    public class Common
    {
        internal const string ApiVersion = "v3";
        internal const string ApiToken = "4a445bda5668e2e79a6a5852810f36b0";

        internal const string ApiUrl = "https://www.pivotaltracker.com/services";

        internal static PivotApiResponse GetXmlFromPivotApi(PivotApiGetRequest getRequest)
        {
            PivotApiResponse retVal = new PivotApiResponse();

            string url = string.Format("{0}/{1}/{2}", ApiUrl, ApiVersion, getRequest.Action);

            url += "?token=" + ApiToken;

            // url += "&format=xml";

            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(url);
            //CredentialCache myCache = new CredentialCache();
            //myCache.Add(new Uri(url), "Basic", new NetworkCredential(ApiToken, ""));
            //myReq.Credentials = myCache;

            myReq.ContentType = "application/xml";
            myReq.ContentLength = 0;
            myReq.Method = "GET";

            try
            {
                HttpWebResponse wr = (HttpWebResponse)myReq.GetResponse();
                retVal.StatusCode = (int)wr.StatusCode;

                Stream receiveStream = wr.GetResponseStream();
                StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
                retVal.Xml = reader.ReadToEnd();
                retVal.Success = true;
            }
            catch (WebException ex)
            {
                HttpWebResponse exResponse = (HttpWebResponse)ex.Response;
                retVal.StatusCode = (int)exResponse.StatusCode;
                retVal.ErrorMessage = ex.Message;
            }
            return retVal;
        }

        internal static PivotApiResponse PutXmlToPivotApi(PivotApiPutRequest putRequest)
        {
            PivotApiResponse retVal = new PivotApiResponse();

            string url = string.Format("{0}/{1}/{2}", ApiUrl, ApiVersion, putRequest.Action);

            url += "?auth_token=" + ApiToken;

            url += "&format=xml";

            string postRequest = url + Uri.EscapeUriString(putRequest.PostRequest);

            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(postRequest);
            myReq.Method = "POST";

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)myReq.GetResponse())
                {
                    // in the case of a PivotApi exception, the reason is in the response body, so need to extract this.
                    using (Stream receiveStream = response.GetResponseStream())
                    {
                        Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                        using (StreamReader readStream = new StreamReader(receiveStream, encode))
                        {
                            retVal.Xml = readStream.ReadToEnd();
                            retVal.Success = true;
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                HttpWebResponse exResponse = (HttpWebResponse)ex.Response;
                retVal.StatusCode = (int)exResponse.StatusCode;
                retVal.ErrorMessage = ex.Message;
            }

            return retVal;
        }

    }
}
