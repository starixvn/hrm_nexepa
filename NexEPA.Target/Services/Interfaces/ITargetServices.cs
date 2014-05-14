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
    using NexEPA.Employee.Models;
    using NexEPA.Target.Criteria.Target.Target;
    using NexEPA.Target.Criteria.Target.TargetEmployee;
    using NexEPA.Target.Criteria.Target.TargetKPI;
    using NexEPA.Target.Models;
    using NexEPA.Target.ViewModels;

    /// <summary>
    /// The interface of Target Services.
    /// </summary>
    public interface ITargetServices
    {
        List<TargetViewModel> GetAllTarget(GetTargetCriteria criteria);

        TargetViewModel GetTargetById(GetTargetByIdCriteria criteria);

        CommandResult Insert(InsertTargetCriteria criteria);

        CommandResult Update(UpdateTargetCriteria criteria);

        CommandResult Delete(DeleteTargetCriteria criteria);

        IEnumerable<EmployeeModel> GetTargetEmployeeByTargetId(GetAllEmployeeByTargetIdCriteria criteria);

        CommandResult InsertTargetEmployee(AddTargetEmployeeCriteria criteria);

        CommandResult DeleteTargetEmployee(DeleteTargetEmployeeCriteria criteria);

        List<TargetKPIGroupModel> GetKPIByTargetId(GetKPIByTargetIdCriteria criteria);

        CommandResult UpdateStatus(UpdateTargetStatusCriteria criteria);
    }
}
