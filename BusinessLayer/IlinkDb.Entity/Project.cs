using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using AppCommon;

namespace IlinkDb.Entity
{
    public class Project : EntityBase
    {
        public int Iteration { get; set; }
        public int PointScale { get; set; }
        public int CurrentVelocity { get; set; }
        public int InitialVelocity { get; set; }
        public int DoneIterationsToShow { get; set; }

        public string Name { get; set; }
        public string WeekStartDay { get; set; }
        public string Labels { get; set; }
        public string VelocityScheme { get; set; }

        public bool? AllowAttachments { get; set; }
        public bool? Public { get; set; }
        public bool? UseHttps { get; set; }
        public bool? BugsAndChoresAreEstimatable { get; set; }
        public bool? CommitMode { get; set; }

        public DateTime? LastActivity { get; set; }

        public Project(XElement node)
        {
            string logMsg = "Project/Constructor";

            try
            {
                Id = EntityHelper.GetIntElementValue(node, "id");
                Iteration = EntityHelper.GetIntElementValue(node, "iteration_length");
                PointScale = EntityHelper.GetIntElementValue(node, "point_scale");
                CurrentVelocity = EntityHelper.GetIntElementValue(node, "current_velocity");
                InitialVelocity = EntityHelper.GetIntElementValue(node, "initial_velocity");
                DoneIterationsToShow = EntityHelper.GetIntElementValue(node, "number_of_done_iterations_to_show");

                Name = EntityHelper.GetStringElementValue(node, "name");
                WeekStartDay = EntityHelper.GetStringElementValue(node, "week_start_day");
                VelocityScheme = EntityHelper.GetStringElementValue(node, "velocity_scheme");
                Labels = EntityHelper.GetStringElementValue(node, "labels");

                AllowAttachments = EntityHelper.GetBoolElementValue(node, "allow_attachments");
                Public = EntityHelper.GetBoolElementValue(node, "public");
                UseHttps = EntityHelper.GetBoolElementValue(node, "use_https");
                BugsAndChoresAreEstimatable = EntityHelper.GetBoolElementValue(node, "bugs_and_chores_are_estimatable");
                CommitMode = EntityHelper.GetBoolElementValue(node, "commit_mode");

                LastActivity = EntityHelper.GetDateTimeElementValue(node, "last_activity_at");
            }
            catch (Exception ex)
            { Logging.LogError(logMsg + ", EXCEPTION: " + ex.Message, ex); }

        }

        public string PivotApiPostRequest
        {
            get
            {
                string logMsg = "Project/PivotApiPostRequest";
                StringBuilder sb = new StringBuilder();

                try
                {
                    if (Id > 0)
                    { sb.AppendFormat("&id={0}", Id); }

                    if (Iteration >= 0)
                    { sb.AppendFormat("&iteration_length={0}", Iteration); }

                    if (PointScale >= 0)
                    { sb.AppendFormat("&point_scale={0}", PointScale); }

                    if (CurrentVelocity >= 0)
                    { sb.AppendFormat("&current_velocity={0}", CurrentVelocity); }

                    if (InitialVelocity >= 0)
                    { sb.AppendFormat("&initial_velocity={0}", InitialVelocity); }

                    if (DoneIterationsToShow >= 0)
                    { sb.AppendFormat("&number_of_done_iterations_to_show={0}", DoneIterationsToShow); }


                    if (!string.IsNullOrEmpty(Name))
                    { sb.AppendFormat("&name={0}", Name); }

                    if (!string.IsNullOrEmpty(WeekStartDay))
                    { sb.AppendFormat("&week_start_day={0}", WeekStartDay); }

                    if (!string.IsNullOrEmpty(VelocityScheme))
                    { sb.AppendFormat("&velocity_scheme={0}", VelocityScheme); }

                    if (!string.IsNullOrEmpty(Labels))
                    { sb.AppendFormat("&labels={0}", Labels); }


                    if (AllowAttachments.HasValue)
                    { sb.AppendFormat("&allow_attachments={0}", AllowAttachments); }

                    if (Public.HasValue)
                    { sb.AppendFormat("&public={0}", Public); }

                    if (UseHttps.HasValue)
                    { sb.AppendFormat("&use_https={0}", UseHttps); }

                    if (BugsAndChoresAreEstimatable.HasValue)
                    { sb.AppendFormat("&bugs_and_chores_are_estimatable={0}", BugsAndChoresAreEstimatable); }

                    if (CommitMode.HasValue)
                    { sb.AppendFormat("&commit_mode={0}", CommitMode); }

                    if (LastActivity.HasValue)
                    { sb.AppendFormat("&last_activity_at={0}", LastActivity); }

                }
                catch (Exception ex)
                { Logging.LogError(logMsg + ", EXCEPTION: " + ex.Message, ex); }

                return sb.ToString();
            }
        }
    }
}
