// -----------------------------------------------------------------------
// <copyright file="IKPIRepository.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.03.31">Created</change>
// </history>

namespace NexEPA.KPI.Repositories.Interface.KPI
{
    using System.Collections.Generic;
    using Fanex.NAP.Framework.Data;
    using NexEPA.KPI.Model;

    /// <summary>
    /// The interface of KPI repository.
    /// </summary>
    public interface IKPIRepository
    {
        IEnumerable<KPIInfo> GetKPI(ICriteria criteria);

        IEnumerable<KPIInfo> GetKPIById(ICriteria criteria);
    }
}
