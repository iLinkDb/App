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
    public class Story : EntityBase
    {
        public long ProjectId { get; set; }
        public int Estimate { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public StoryTypeEnum StoryType { get; set; }
        public string Url { get; set; }

        public string CurrentState { get; set; }
        public string RequestedBy { get; set; }
        public string OwnedBy { get; set; }
        public string Labels { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? AcceptedAt { get; set; }

        public List<Note> Notes { get { return _noteList; } }
        public List<Task> Tasks { get { return _taskList; } }

        private List<Note> _noteList { get; set; }
        private List<Task> _taskList { get; set; }

        public Story()
        {
            _noteList = new List<Note>();
            Estimate = -1;
        }

        public Story(XElement node)
        {
            string logMsg = "Story/Constructor";

            try
            {
                Id = EntityHelper.GetIntElementValue(node, "id");
                ProjectId = EntityHelper.GetIntElementValue(node, "project_id");
                Estimate = EntityHelper.GetIntElementValue(node, "estimate", -1);

                Name = EntityHelper.GetStringElementValue(node, "name");
                Description = EntityHelper.GetStringElementValue(node, "description");

                XElement elementStoryType = node.Element("story_type");
                if (elementStoryType != null)
                { StoryType = Conversion.GetEnumFromString<StoryTypeEnum>(elementStoryType.Value, StoryTypeEnum.Feature); }

                Url = EntityHelper.GetStringElementValue(node, "url");

                CurrentState = EntityHelper.GetStringElementValue(node, "current_state");
                RequestedBy = EntityHelper.GetStringElementValue(node, "requested_by");
                OwnedBy = EntityHelper.GetStringElementValue(node, "owned_by");
                Labels = EntityHelper.GetStringElementValue(node, "labels");

                CreatedAt = EntityHelper.GetDateTimeElementValue(node, "created_at");
                AcceptedAt = EntityHelper.GetDateTimeElementValue(node, "accepted_at");

                // Tasks
                XElement tasks = node.Element("tasks");
                if (tasks == null)
                { _taskList = new List<Task>(); }
                else
                {
                    var list = from nodeTask in tasks.Descendants("task")
                               select new Task(nodeTask);

                    _taskList = list.ToList<Task>();
                }

                // Notes
                XElement notes = node.Element("notes");
                if (notes == null)
                { _noteList = new List<Note>(); }
                else
                {
                    var list = from nodeNote in notes.Descendants("note")
                               select new Note(nodeNote);

                    _noteList = list.ToList<Note>();
                }
            }
            catch (Exception ex)
            { Logging.LogError(logMsg + ", EXCEPTION: " + ex.Message, ex); }

        }

        public XmlDocument XmlDoc
        {
            get
            {
                string logMsg = "Story/XmlDoc";

                XmlDocument retVal = new XmlDocument();

                try
                {
                    XmlElement xmlNode = retVal.CreateElement("story");

                    XmlElement storyTypeElement = retVal.CreateElement("story_type");
                    storyTypeElement.InnerText = Conversion.GetEnumDescription(StoryType);
                    xmlNode.AppendChild(storyTypeElement);

                    if (Id > 0)
                    {
                        XmlElement element = retVal.CreateElement("id");
                        element.InnerText = Id.ToString();
                        xmlNode.AppendChild(element);
                    }

                    if (ProjectId >= 0)
                    {
                        XmlElement element = retVal.CreateElement("project_id");
                        element.InnerText = ProjectId.ToString();
                        xmlNode.AppendChild(element);
                    }

                    if (Estimate >= 0)
                    {
                        XmlElement element = retVal.CreateElement("estimate");
                        element.InnerText = Estimate.ToString();
                        xmlNode.AppendChild(element);
                    }

                    if (!string.IsNullOrEmpty(Name))
                    {
                        XmlElement element = retVal.CreateElement("name");
                        element.InnerText = Name;
                        xmlNode.AppendChild(element);
                    }

                    if (!string.IsNullOrEmpty(Description))
                    {
                        XmlElement element = retVal.CreateElement("description");
                        element.InnerText = Description;
                        xmlNode.AppendChild(element);
                    }

                    if (!string.IsNullOrEmpty(Url))
                    {
                        XmlElement element = retVal.CreateElement("url");
                        element.InnerText = Url;
                        xmlNode.AppendChild(element);
                    }

                    if (!string.IsNullOrEmpty(CurrentState))
                    {
                        XmlElement element = retVal.CreateElement("current_state");
                        element.InnerText = CurrentState;
                        xmlNode.AppendChild(element);
                    }
                    if (!string.IsNullOrEmpty(RequestedBy))
                    {
                        XmlElement element = retVal.CreateElement("requested_by");
                        element.InnerText = RequestedBy;
                        xmlNode.AppendChild(element);
                    }
                    if (!string.IsNullOrEmpty(OwnedBy))
                    {
                        XmlElement element = retVal.CreateElement("owned_by");
                        element.InnerText = OwnedBy;
                        xmlNode.AppendChild(element);
                    }
                    if (!string.IsNullOrEmpty(Labels))
                    {
                        XmlElement element = retVal.CreateElement("labels");
                        element.InnerText = Labels;
                        xmlNode.AppendChild(element);
                    }

                    if (CreatedAt.HasValue)
                    {
                        XmlElement element = retVal.CreateElement("created_at");
                        element.InnerText = CreatedAt.GetValueOrDefault().ToString("yyyyMMdd"); //TaskDueDate
                        xmlNode.AppendChild(element);
                    }

                    if (AcceptedAt.HasValue)
                    {
                        XmlElement element = retVal.CreateElement("accepted_at");
                        element.InnerText = AcceptedAt.GetValueOrDefault().ToString("yyyyMMdd"); //TaskDueDate
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
