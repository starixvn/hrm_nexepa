// -----------------------------------------------------------------------
// <copyright file="HistoryProjectModel.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.05.06">Created</change>
// </history>

namespace NexEPA.Report.Models.Dashboard
{
    using System;
    using NexEPA.Employee.Models;
    using NexEPA.Project.Models;

    /// <summary>
    /// The Project History Model.
    /// </summary>
    public class HistoryProjectModel
    {
        public int TargetId { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Roles { get; set; }

        public string Score { get; set; }

        public string Comment { get; set; }

        public ProjectModel Project { get; set; }

        public EmployeeModel Evaluator { get; set; }

        public ReportMilestoneModel Milestone { get; set; }
    }
}
