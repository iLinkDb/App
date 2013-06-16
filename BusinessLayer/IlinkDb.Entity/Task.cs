using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
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

        public string PivotApiPostRequest
        {
            get
            {
                string logMsg = "Task/PivotApiPostRequest";
                StringBuilder sb = new StringBuilder();

                try
                {
                    if (Id > 0)
                    { sb.AppendFormat("&id={0}", Id); }

                    if (!string.IsNullOrEmpty(Description))
                    { sb.AppendFormat("&description={0}", Description); }

                    if (Position > 0)
                    { sb.AppendFormat("&position={0}", Position); }

                    if (Complete.HasValue)
                    { sb.AppendFormat("&complete={0}", Complete); }

                    if (CreatedAt.HasValue)
                    { sb.AppendFormat("&created_at={0}", CreatedAt); }

                }
                catch (Exception ex)
                { Logging.LogError(logMsg + ", EXCEPTION: " + ex.Message, ex); }

                return sb.ToString();
            }
        }

    }
}
