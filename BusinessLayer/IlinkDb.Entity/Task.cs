using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json;

using AppCommon;

namespace IlinkDb.Entity
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class Task : EntityBase
    {
        [Required]
        [JsonProperty(PropertyName = "description")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "position")]
        [Display(Name = "Position")]
        public int Position { get; set; }

        [JsonProperty(PropertyName = "complete")]
        [Display(Name = "Complete")]
        public bool? Complete { get; set; }

        [JsonProperty(PropertyName = "createdAt")]
        [Display(Name = "CreatedAt")]
        public DateTime? CreatedAt { get; set; }

        public long StoryId { get; set; }

        public Task()
        {
            CreatedAt = DateTime.Now;
        }

        public Task(XElement node)
        {
            string logMsg = "Task/Constructor";

            if (node == null)
            { throw new ArgumentNullException(logMsg + " node parameter must not be null"); }

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
