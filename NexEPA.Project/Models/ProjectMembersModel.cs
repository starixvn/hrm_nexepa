namespace NexEPA.Project.Models
{
    using System;

    /// <summary>
    /// Project Members Model.
    /// </summary>
    public class ProjectMembersModel
    {
        public int EmployeeId { get; set; }

        public string NickName { get; set; }

        public string FullName { get; set; }

        public int ProjectRoleId { get; set; }

        public string RoleCode { get; set; }

        public string RoleName { get; set; }

        public DateTime? JoinDate { get; set; }
    }
}
