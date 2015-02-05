// --------------------------------------------------------------------------
// <copyright file="IEmployeePerformanceTendencyService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.2.5">Created</change>
// </history>

namespace NexEPA.Report.Services.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NexEPA.Report.Models.EmployeePerformanceTendency;
    using NexEPA.Report.Criteria;

    /// <summary>
    /// IEmployeePerformanceTendencyService interface.
    /// </summary>
    public interface IEmployeePerformanceTendencyService
    {
        EmployeePerformanceModel GetEmployeePerformanceTendancyReport(GetEmployeePerformanceTendencyReportCriteria criteria);
    }
}
