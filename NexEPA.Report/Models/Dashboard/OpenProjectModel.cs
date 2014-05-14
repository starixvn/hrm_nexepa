// -----------------------------------------------------------------------
// <copyright file="OpenProjectModel.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.05.06">Created</change>
// </history>

namespace NexEPA.Report.Models.Dashboard
{
    using NexEPA.Employee.ViewModels;

    /// <summary>
    /// Open Project Model.
    /// </summary>
    public class OpenProjectModel
    {
        public ReportProjectModel Project { get; set; }

        public EmployeeModel Evaluator { get; set; }

        public string Roles { get; set; }
    }
}
