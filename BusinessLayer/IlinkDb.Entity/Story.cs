using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public string StoryType { get; set; }
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
        }

        public Story(XElement node)
        {
            string logMsg = "Story/Constructor";

            try
            {
                Id = EntityHelper.GetIntElementValue(node, "id");
                ProjectId = EntityHelper.GetIntElementValue(node, "project_id");
                Estimate = EntityHelper.GetIntElementValue(node, "estimate");

                Name = EntityHelper.GetStringElementValue(node, "name");
                Description = EntityHelper.GetStringElementValue(node, "description");
                StoryType = EntityHelper.GetStringElementValue(node, "story_type");
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

        public string PivotApiPostRequest
        {
            get
            {
                string logMsg = "Story/PivotApiPostRequest";
                StringBuilder sb = new StringBuilder();

                try
                {
                    if (Id > 0)
                    { sb.AppendFormat("&id={0}", Id); }

                    if (ProjectId >= 0)
                    { sb.AppendFormat("&project_id={0}", ProjectId); }

                    if (Estimate >= 0)
                    { sb.AppendFormat("&estimate={0}", Estimate); }

                    if (!string.IsNullOrEmpty(Name))
                    { sb.AppendFormat("&name={0}", Name); }

                    if (!string.IsNullOrEmpty(Description))
                    { sb.AppendFormat("&description={0}", Description); }

                    if (!string.IsNullOrEmpty(StoryType))
                    { sb.AppendFormat("&story_type={0}", StoryType); }

                    if (!string.IsNullOrEmpty(Url))
                    { sb.AppendFormat("&url={0}", Url); }


                    if (!string.IsNullOrEmpty(CurrentState))
                    { sb.AppendFormat("&current_state={0}", CurrentState); }

                    if (!string.IsNullOrEmpty(RequestedBy))
                    { sb.AppendFormat("&requested_by={0}", RequestedBy); }

                    if (!string.IsNullOrEmpty(OwnedBy))
                    { sb.AppendFormat("&owned_by={0}", OwnedBy); }

                    if (!string.IsNullOrEmpty(Labels))
                    { sb.AppendFormat("&labels={0}", Labels); }

                    if (CreatedAt.HasValue)
                    { sb.AppendFormat("&created_at={0}", CreatedAt); }

                    if (AcceptedAt.HasValue)
                    { sb.AppendFormat("&accepted_at={0}", AcceptedAt); }

                }
                catch (Exception ex)
                { Logging.LogError(logMsg + ", EXCEPTION: " + ex.Message, ex); }

                return sb.ToString();
            }
        }

    }
}
