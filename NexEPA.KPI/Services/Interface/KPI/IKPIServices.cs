// -----------------------------------------------------------------------
// <copyright file="IKPIServices.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.03.18">Created</change>
// </history>

namespace NexEPA.KPI.Services.Interface.KPI
{
    using System.Collections.Generic;
    using Fanex.NAP.Framework.Data;
    using NexEPA.KPI.Criteria.KPI;
    using NexEPA.KPI.ViewModel.KPI;

    /// <summary>
    /// The interface of KPI services.
    /// </summary>
    public interface IKPIServices
    {
        List<KPIItem> GetKPI(int? targetTypeId, int? groupId);

        KPIItem GetKPIById(int kpiId);

        CommandResult InsertKPI(InsertKPICriteria criteria);

        CommandResult UpdateKPI(UpdateKPICriteria criteria);

        CommandResult DeleteKPI(DeleteKPICriteria criteria);
    }
}
