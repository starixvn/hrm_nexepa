// --------------------------------------------------------------------------
// <copyright file="ProjectMembersModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.3.26">Created</change>
// </history>

namespace NexEPA.Project.Models
{
    /// <summary>
    /// Project Members Model.
    /// </summary>
    public class ProjectMembersModel
    {
        public int EmployeeId { get; set; }

        public string NickName { get; set; }

        public string FullName { get; set; }

        public int ProjectRoleId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }
    }
}
