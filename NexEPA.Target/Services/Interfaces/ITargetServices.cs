// -----------------------------------------------------------------------
// <copyright file="ITargetServices.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.04">Created</change>
// </history>

namespace NexEPA.Target.Services.Interfaces
{
    using System.Collections.Generic;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Target.Criteria.Target.Target;
    using NexEPA.Target.Criteria.Target.TargetEmployee;
    using NexEPA.Target.Criteria.Target.TargetKPI;
    using NexEPA.Target.ViewModels.Target;

    /// <summary>
    /// The interface of Target Services.
    /// </summary>
    public interface ITargetServices
    {
        List<TargetItem> GetAllTarget(GetTargetCriteria criteria);

        TargetItem GetTargetById(GetTargetByIdCriteria criteria);

        CommandResult Insert(InsertTargetCriteria criteria);

        CommandResult Update(UpdateTargetCriteria criteria);

        CommandResult Delete(DeleteTargetCriteria criteria);

        List<TargetEmployeeItem> GetTargetEmployeeByTargetId(GetAllEmployeeByTargetIdCriteria criteria);

        CommandResult InsertTargetEmployee(AddTargetEmployeeCriteria criteria);

        CommandResult DeleteTargetEmployee(DeleteTargetEmployeeCriteria criteria);

        List<TargetKPIGroupItem> GetKPIByTargetId(GetKPIByTargetIdCriteria criteria);

        CommandResult UpdateStatus(UpdateTargetStatusCriteria criteria);
    }
}
