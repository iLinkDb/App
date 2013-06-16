using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using AppCommon;

namespace IlinkDb.Entity
{
    internal class EntityHelper
    {
        internal static string GetStringElementValue(XElement node, string nodeName)
        {
            string retVal = "";

            string logMsg = "EntityHelper/GetStringElementValue";

            try
            {
                XElement element = node.Element(nodeName);

                if (element != null)
                { retVal = element.Value; }
            }
            catch (Exception ex)
            { Logging.LogError(logMsg + ", EXCEPTION: " + ex.Message, ex); }

            return retVal;
        }

        internal static int GetIntElementValue(XElement node, string nodeName)
        {
            int retVal = 0;

            string logMsg = "EntityHelper/GetIntElementValue";

            try
            {
                XElement element = node.Element(nodeName);

                if (element != null)
                {
                    int.TryParse(element.Value, out retVal);
                }
            }
            catch (Exception ex)
            { Logging.LogError(logMsg + ", EXCEPTION: " + ex.Message, ex); }

            return retVal;
        }

        internal static bool GetBoolElementValue(XElement node, string nodeName)
        {
            bool retVal = false;

            string logMsg = "EntityHelper/GetBoolElementValue";

            try
            {
                XElement element = node.Element(nodeName);

                if (element != null)
                {
                    bool.TryParse(element.Value, out retVal);
                }
            }
            catch (Exception ex)
            { Logging.LogError(logMsg + ", EXCEPTION: " + ex.Message, ex); }

            return retVal;
        }

        internal static DateTime? GetDateTimeElementValue(XElement node, string nodeName)
        {
            string logMsg = "EntityHelper/GetDateTimeElementValue";
            DateTime? retVal = null;

            try
            {
                XElement element = node.Element(nodeName);

                if (element != null)
                {
                    DateTime work;
                    if (DateTime.TryParse(element.Value, out work))
                    { retVal = work; }
                    else
                    {
                        // PivotalApi likes to put a UTC on the end which TryParse
                        // doesn't know how to deal with.
                        // The doc also states "By default, all dates are returned
                        // in the user's timezone", so the UTC doesn't really help us.
                        string dateText = element.Value.Replace("UTC", "");
                        if (DateTime.TryParse(dateText, out work))
                        { retVal = work; }
                    }
                }
            }
            catch (Exception ex)
            { Logging.LogError(logMsg + ", EXCEPTION: " + ex.Message, ex); }

            return retVal;
        }
    }
}
