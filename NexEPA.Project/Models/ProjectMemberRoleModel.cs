namespace NexEPA.Project.Models
{
    using System.Collections.Generic;
    using NexEPA.Core.Models;

    public class ProjectMemberRoleModel
    {
        public CommonEmployeeModel Member { get; set; }

        public IList<ProjectRoleModel> Roles { get; set; }
    }
}
