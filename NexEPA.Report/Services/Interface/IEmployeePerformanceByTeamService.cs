// --------------------------------------------------------------------------
// <copyright file="IEmployeePerformanceByTeamService.cs" company="Nexcel Solutions Vietnam">
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
    using NexEPA.Report.Criteria;
    using NexEPA.Report.Models.EmployeePerformanceByTeam;

    /// <summary>
    /// IEmployeePerformanceByTeamService interface.
    /// </summary>
    public interface IEmployeePerformanceByTeamService
    {
        TeamPerformanceModel GetEmployeePerformanceByTeamReport(GetEmployeePerformanceByTeamCriteria criteria);
    }
}
