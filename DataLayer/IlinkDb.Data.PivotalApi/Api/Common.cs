using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;

using AppCommon;

namespace IlinkDb.Data.PivotalApi
{
    public class PivotApi
    {
        // Pivotal Community site: http://community.pivotaltracker.com/pivotal/problems/common
        internal const string ApiVersion = "v3";
        internal const string ApiToken = "4a445bda5668e2e79a6a5852810f36b0";

        internal const string ApiUrl = "https://www.pivotaltracker.com/services";

        internal static ApiResponse Get(GetRequest getRequest)
        {
            ApiResponse retVal = new ApiResponse();

            string logMsg = "PivotApi/Get";

            try
            {

                string url = string.Format("{0}/{1}/{2}?token={3}", 
                    ApiUrl, ApiVersion, getRequest.Action, ApiToken);

                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(url);
                //CredentialCache myCache = new CredentialCache();
                //myCache.Add(new Uri(url), "Basic", new NetworkCredential(ApiToken, ""));
                //myReq.Credentials = myCache;

                myReq.ContentType = "application/xml";
                myReq.ContentLength = 0;
                myReq.Method = "GET";

                HttpWebResponse wr = (HttpWebResponse)myReq.GetResponse();
                retVal.StatusCode = (int)wr.StatusCode;

                Stream receiveStream = wr.GetResponseStream();
                StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
                retVal.Xml = reader.ReadToEnd();
                retVal.Success = true;
            }
            catch (WebException ex)
            {
                Logging.LogError(logMsg + ", WEBEXCEPTION: " + ex.Message, ex);
                HttpWebResponse exResponse = (HttpWebResponse)ex.Response;
                retVal.StatusCode = (int)exResponse.StatusCode;
                retVal.ErrorMessage = ex.Message;
            }
            catch (Exception ex)
            { Logging.LogError(logMsg + ", EXCEPTION: " + ex.Message, ex); }

            return retVal;
        }

        internal static ApiResponse Post(PostRequest putRequest)
        {
            ApiResponse retVal = new ApiResponse();

            string logMsg = "PivotApi/Post";
            try
            {
                string url = string.Format("{0}/{1}/{2}?token={3}", 
                    ApiUrl, ApiVersion, putRequest.Action, ApiToken);

                HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(url);

                byte[] byteArray = Encoding.UTF8.GetBytes(putRequest.XmlDoc.InnerXml);

                myReq.ContentType = "application/xml";
                myReq.ContentLength = byteArray.Length;
                myReq.Method = "POST";

                Stream dataStream = myReq.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                HttpWebResponse wr = (HttpWebResponse)myReq.GetResponse();
                retVal.StatusCode = (int)wr.StatusCode;

                string location = wr.Headers["Location"];

                if (!string.IsNullOrEmpty(location))
                {
                    int pos = location.LastIndexOf("/");
                    if (pos > 0)
                    {
                        int newId = 0;
                        if (int.TryParse(location.Substring(pos + 1), out newId))
                        { retVal.NewId = newId; }
                    }
                }

                Stream receiveStream = wr.GetResponseStream();
                StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
                retVal.Xml = reader.ReadToEnd();
                retVal.Success = true;
            }
            catch (WebException ex)
            {
                Logging.LogError(logMsg + ", WEBEXCEPTION: " + ex.Message, ex);
                HttpWebResponse exResponse = (HttpWebResponse)ex.Response;
                retVal.StatusCode = (int)exResponse.StatusCode;
                retVal.ErrorMessage = ex.Message;
            }
            catch (Exception ex)
            { Logging.LogError(logMsg + ", EXCEPTION: " + ex.Message, ex); }

            return retVal;
        }

    }
}
