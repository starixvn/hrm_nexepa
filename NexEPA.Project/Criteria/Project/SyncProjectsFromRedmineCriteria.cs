namespace NexEPA.Project.Criteria.Project
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.Data.Repository;
    using NexEPA.Core.Extensions;
    using NexEPA.Core.Models;

    public class SyncProjectsFromRedmineCriteria : CriteriaBase
    {
        private const string ProjectPMTemplate = "<ProjectPM RedmineProjectId=\"{0}\" EmployeeId=\"{1}\"></ProjectPM>";

        public string ProjectPMIds
        {
            get
            {
                string projectPMIds = GenerateXmlData();

                return projectPMIds;
            }
        }

        public int AddedBy { get; set; }

        [Output]
        public int ErrCode { get; set; }

        [Output]
        public string ErrMsg { get; set; }

        [SpParam(Ignored = true)]
        public IList<CommonEmployeeModel> ProjectPMs { get; set; }

        public override bool IsValid()
        {
            return AddedBy > 0 && !string.IsNullOrWhiteSpace(ProjectPMIds);
        }

        public override string GetSettingKey()
        {
            return "Project_SyncFromRedmine";
        }

        private string GenerateXmlData()
        {
            string xmlData = string.Empty;

            if (ProjectPMs.Count > 0)
            {
                var xmlDataBuilder = new StringBuilder();
                xmlDataBuilder.Append("<ROOT>");
                ProjectPMs = ProjectPMs.Where(x => x.EmployeeId > 0).ToList();

                foreach (CommonEmployeeModel projectPM in ProjectPMs)
                {
                    xmlDataBuilder.AppendFormat(ProjectPMTemplate, projectPM.ProjectId, projectPM.EmployeeId);
                }

                xmlDataBuilder.Append("</ROOT>");

                xmlData = xmlDataBuilder.ToString();
            }

            return xmlData;
        }
    }
}
