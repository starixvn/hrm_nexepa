namespace NexEPA.Project.Criteria.ProjectRedmine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.Data.Repository;

    public class GetProjectRedmineByIdCriteria : CriteriaBase
    {
        public int ProjectId { get; set; }

        public override bool IsValid()
        {
            return ProjectId > 0;
        }

        public override string GetSettingKey()
        {
            return "ProjectRedmine_GetProjectById";
        }
    }
}
