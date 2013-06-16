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
    public class Note : EntityBase
    {
        [Required]
        public string Text { get; set; }

        [Required]
        public string User { get; set; }

        [Required]
        public DateTime? DateAdded { get; set; }

        public Note()
        {
            DateAdded = DateTime.Now;
        }

        public Note(XElement node)
        {
            string logMsg = "Note/Constructor";

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

        public string PivotApiPostRequest
        {
            get
            {
                string logMsg = "Note/PivotApiPostRequest";
                StringBuilder sb = new StringBuilder();

                try
                {
                    if (Id > 0)
                    { sb.AppendFormat("&id={0}", Id); }

                    if (!string.IsNullOrEmpty(Text))
                    { sb.AppendFormat("&text={0}", Text); }

                    if (!string.IsNullOrEmpty(User))
                    { sb.AppendFormat("&author={0}", User); }

                    if (DateAdded.HasValue)
                    { sb.AppendFormat("&noted_at={0}", DateAdded); }

                }
                catch (Exception ex)
                { Logging.LogError(logMsg + ", EXCEPTION: " + ex.Message, ex); }

                return sb.ToString();
            }
        }

    }
}
