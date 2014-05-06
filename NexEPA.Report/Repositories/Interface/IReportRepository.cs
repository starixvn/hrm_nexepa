// -----------------------------------------------------------------------
// <copyright file="IReportRepository.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.05.06">Created</change>
// </history>

namespace NexEPA.Report.Repositories.Interface
{
    using System.Collections.Generic;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Report.Models.Dashboard;

    /// <summary>
    /// The interface of Report Repository.
    /// </summary>
    public interface IReportRepository
    {
        List<object> GetGeneralInformationOfCurrentEmployee(ICriteria criteria);
    }
}
