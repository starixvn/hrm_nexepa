// --------------------------------------------------------------------------
// <copyright file="RedmineDetailResourcePlanningModel.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.3.23">Created</change>
// </history>

namespace NexEPA.Report.Models.ResourcePlanningReport
{
    using NexEPA.Core.Models;
    using NexEPA.Employee.Models;
    using NexEPA.Project.Models;

    /// <summary>
    /// RedmineDetailResourcePlanningModel class.
    /// </summary>
    public class RedmineDetailResourcePlanningModel
    {
        public RedmineEmployeeModel Employee { get; set; }

        public RedmineProjectModel Project { get; set; }

        public RedmineIssueModel Issue { get; set; }
    }
}
