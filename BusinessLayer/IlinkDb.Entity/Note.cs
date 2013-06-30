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
    public class Note : EntityBase
    {
        [JsonProperty(PropertyName = "storyId")]
        [Display(Name = "StoryId")]
        public long StoryId { get; set; }

        [Required]
        [JsonProperty(PropertyName = "text")]
        [Display(Name = "Text")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "user")]
        [Display(Name = "User")]
        public string User { get; set; }

        [JsonProperty(PropertyName = "dateAdded")]
        [Display(Name = "DateAdded")]
        public DateTime? DateAdded { get; set; }

        public Note()
        {
            DateAdded = DateTime.Now;
        }

        public Note(XElement node)
        {
            string logMsg = "Note/Constructor";

            if (node == null)
            { throw new ArgumentNullException(logMsg + " node parameter must not be null"); }

            try
            {
                Id = EntityHelper.GetIntElementValue(node, "id");
                Text = EntityHelper.GetStringElementValue(node, "text");
                User = EntityHelper.GetStringElementValue(node, "author");

                DateAdded = EntityHelper.GetDateTimeElementValue(node, "noted_at");
            }
            catch (Exception ex)
            { Logging.LogError(logMsg + ", EXCEPTION: " + ex.Message, ex); }
        }

        public XmlDocument XmlDoc
        {
            get
            {
                string logMsg = "Note/XmlDoc";

                XmlDocument retVal = new XmlDocument();

                try
                {
                    XmlElement xmlNode = retVal.CreateElement("note");

                    if (!string.IsNullOrEmpty(Text))
                    {
                        XmlElement element = retVal.CreateElement("text");
                        element.InnerText = Text;
                        xmlNode.AppendChild(element);
                    }

                    if (!string.IsNullOrEmpty(User))
                    {
                        XmlElement element = retVal.CreateElement("author");
                        element.InnerText = User;
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
