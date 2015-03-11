// --------------------------------------------------------------------------
// <copyright file="RedmineMemberProjectModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.2.26">Created</change>
// </history>

namespace NexEPA.Report.Models.TeamEffortReport
{
    using System.Collections.Generic;
    using NexEPA.Employee.Models;
    using NexEPA.Project.Models;

    /// <summary>
    /// RedmineMemberProjectModel class.
    /// </summary>
    public class RedmineMemberProjectModel
    {
        public decimal SubTotal { get; set; }

        public int Index { get; set; }

        public RedmineEmployeeModel Employee { get; set; }

        public RedmineProjectModel Project { get; set; }

        public List<RedmineProjectModel> Projects { get; set; }
    }
}
