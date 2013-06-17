using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Xml;
using System.Xml.Linq;

using AppCommon;

namespace IlinkDb.Entity
{
    public class Task : EntityBase
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public int Position { get; set; }

        [Required]
        public bool? Complete { get; set; }

        [Required]
        public DateTime? CreatedAt { get; set; }

        public Task()
        {
            CreatedAt = DateTime.Now;
        }

        public Task(XElement node)
        {
            string logMsg = "Task/Constructor";

            try
            {
                Id = EntityHelper.GetIntElementValue(node, "id");
                Description = EntityHelper.GetStringElementValue(node, "description");
                Position = EntityHelper.GetIntElementValue(node, "position");
                Complete = EntityHelper.GetBoolElementValue(node, "complete");

                CreatedAt = EntityHelper.GetDateTimeElementValue(node, "created_at");
            }
            catch (Exception ex)
            { Logging.LogError(logMsg + ", EXCEPTION: " + ex.Message, ex); }
        }

        public XmlDocument XmlDoc
        {
            get
            {
                string logMsg = "Task/XmlDoc";

                XmlDocument retVal = new XmlDocument();

                try
                {
                    XmlElement xmlNode = retVal.CreateElement("task");

                    if (!string.IsNullOrEmpty(Description))
                    {
                        XmlElement element = retVal.CreateElement("description");
                        element.InnerText = Description;
                        xmlNode.AppendChild(element);
                    }

                    if (Position > 0)
                    {
                        XmlElement element = retVal.CreateElement("position");
                        element.InnerText = Position.ToString();
                        xmlNode.AppendChild(element);
                    }

                    if (Complete.HasValue)
                    {
                        XmlElement element = retVal.CreateElement("complete");
                        if (Complete.GetValueOrDefault())
                        { element.InnerText = "true"; }
                        else
                        { element.InnerText = "false"; }
                        xmlNode.AppendChild(element);
                    }

                    if (CreatedAt.HasValue)
                    {
                        XmlElement element = retVal.CreateElement("created_at");
                        element.InnerText = CreatedAt.GetValueOrDefault().ToString("yyyyMMdd"); //TaskDueDate
                        xmlNode.AppendChild(element);
                    }

                    retVal.AppendChild(xmlNode);
                }
                catch (Exception ex)
                { Logging.LogError(logMsg + ", EXCEPTION: " + ex.Message, ex); }

                return retVal;
            }
        }
    }
}
