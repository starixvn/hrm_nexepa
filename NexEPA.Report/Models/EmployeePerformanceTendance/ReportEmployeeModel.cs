// -----------------------------------------------------------------------
// <copyright file="ReportEmployeeModel.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.05.08">Created</change>
// </history>

namespace NexEPA.Report.Models.EmployeePerformanceTendance
{
    /// <summary>
    /// The model of employee in report.
    /// </summary>
    public class ReportEmployeeModel
    {
        public int EmployeeId { get; set; }

        public string DomainUserName { get; set; }

        public string FullName { get; set; }

        public string NickName { get; set; }

        public string Code { get; set; }

        public string JobTitle { get; set; }

        public string TeamName { get; set; }

        public string Roles { get; set; }
    }
}
