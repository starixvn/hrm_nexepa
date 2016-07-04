// --------------------------------------------------------------------------
// <copyright file="RedmineProjectResourceModel.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.3.23">Created</change>
// </history>

namespace NexEPA.Report.Models.ResourcePlanningReport
{
    using System.Collections.Generic;
    using NexEPA.Employee.Models;
    using NexEPA.Project.Models;

    /// <summary>
    /// RedmineProjectResourceModel class.
    /// </summary>
    public class RedmineProjectResourceModel
    {
        public RedmineEmployeeModel Employee { get; set; }

        public RedmineProjectModel Project { get; set; }        

        public IEnumerable<RedmineProjectModel> Projects { get; set; }

        public int TotalIssueCount { get; set; }
    }
}
