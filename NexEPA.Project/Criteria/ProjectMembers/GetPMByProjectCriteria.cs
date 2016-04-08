namespace NexEPA.Project.Criteria.ProjectMembers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.Data.Repository;

    public class GetPMByProjectCriteria : CriteriaBase
    {
        public int ProjectId { get; set; }

        public override bool IsValid()
        {
            return ProjectId > 0;
        }

        public override string GetSettingKey()
        {
            return "ProjectRedmine_GetPMByProject";
        }
    }
}
