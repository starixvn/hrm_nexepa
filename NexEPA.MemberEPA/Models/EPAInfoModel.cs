// --------------------------------------------------------------------------
// <copyright file="EPAInfoModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.4.21">Created</change>
// </history>

namespace NexEPA.MemberEPA.Models
{
    using System.Collections.Generic;
    using NexEPA.KPI.Model.KPI;

    /// <summary>
    /// EPA Info Model.
    /// </summary>
    public class EPAInfoModel
    {
        public int EmployeeId { get; set; }

        public string Code { get; set; }

        public string NickName { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public int GroupId { get; set; }

        public string GroupName { get; set; }

        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public int MilestoneId { get; set; }

        public string MilestoneName { get; set; }

        public List<EPAProjectRoleModel> ProjectRoles { get; set; }

        public List<KPIInfo> KPIInfo { get; set; }
    }
}
