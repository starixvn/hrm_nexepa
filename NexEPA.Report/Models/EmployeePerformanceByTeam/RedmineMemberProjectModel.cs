// --------------------------------------------------------------------------
// <copyright file="RedmineMemberProjectModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.2.26">Created</change>
// </history>

namespace NexEPA.Report.Models.EmployeePerformanceByTeam
{
    using System.Collections.Generic;
    using NexEPA.Employee.Models;
    using NexEPA.Project.Models;

    /// <summary>
    /// RedmineMemberProjectModel class.
    /// </summary>
    public class RedmineMemberProjectModel
    {
        public RedmineEmployeeModel Employee { get; set; }

        public RedmineProjectModel Project { get; set; }

        public List<RedmineProjectModel> Projects { get; set; }
    }
}
