// -----------------------------------------------------------------------
// <copyright file="IKPIGroupService.cs" company="Nexcel Solution Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.03.17">Created</change>
// </history>

namespace NexEPA.KPI.Services.Interface
{
    using System.Collections.Generic;
    using Fanex.NAP.Framework.Data;
    using NexEPA.KPI.Criteria.KPIGroup;
    using NexEPA.KPI.ViewModels;

    /// <summary>
    /// The interface of KPI service.
    /// </summary>
    public interface IKPIGroupService
    {
        List<KPIGroupViewModel> GetAllKPIGroup(GetAllKPIGroupCriteria criteria);

        KPIGroupViewModel GetKPIGroupById(GetKPIGroupByIdCriteria criteria);

        CommandResult InsertKPIGroup(InsertKPIGroupCriteria criteria);

        CommandResult UpdateKPIGroup(UpdateKPIGroupCriteria criteria);

        CommandResult DeleteKPIGroup(DeleteKPIGroupCriteria criteria);

        List<KPIGroupViewModel> GetGroupListByTargetTypeId(GetAllKPIGroupCriteria criteria);

        CommandResult UpdateKPIGroupStatus(UpdateKPIGroupStatusCriteria criteria);
    }
}
