// --------------------------------------------------------------------------
// <copyright file="IEmployeePerformanceTendencyRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.2.5">Created</change>
// </history>

namespace NexEPA.Report.Repositories.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NexEPA.Report.Models.EmployeePerformanceTendency;
    using Fanex.Data.Repository;

    /// <summary>
    /// IEmployeePerformanceTendencyRepository interface.
    /// </summary>
    public interface IEmployeePerformanceTendencyRepository
    {
        EmployeePerformanceModel GetEmployeePerformanceTendancyReport(ICriteria criteria);
    }
}
