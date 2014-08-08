// -----------------------------------------------------------------------
// <copyright file="ITargetService.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.04">Created</change>
// </history>

namespace NexEPA.Target.Services.Interface
{
    using System.Collections.Generic;
    using NexEPA.Core.Models;
    using NexEPA.Employee.Models;
    using NexEPA.Target.Criteria.Target.Target;
    using NexEPA.Target.Criteria.Target.TargetEmployee;
    using NexEPA.Target.Criteria.Target.TargetKPI;
    using NexEPA.Target.Models;

    /// <summary>
    /// The interface of Target Service.
    /// </summary>
    public interface ITargetService
    {
        IEnumerable<TargetModel> GetAllTarget(GetTargetCriteria criteria);

        TargetModel GetTargetById(GetTargetByIdCriteria criteria);

        CommandResult Insert(InsertTargetCriteria criteria);

        CommandResult Update(UpdateTargetCriteria criteria);

        CommandResult Delete(DeleteTargetCriteria criteria);

        IEnumerable<EmployeeModel> GetTargetEmployeeByTargetId(GetAllEmployeeByTargetIdCriteria criteria);

        CommandResult InsertTargetEmployee(AddTargetEmployeeCriteria criteria);

        CommandResult DeleteTargetEmployee(DeleteTargetEmployeeCriteria criteria);

        List<TargetKPIGroupModel> GetKPIByTargetId(GetKPIByTargetIdCriteria criteria);

        CommandResult UpdateStatus(UpdateTargetStatusCriteria criteria);

        TargetDateModel GetProjectOrMilestoneDate(GetProjectOrMilestoneDateCriteria criteria);

        IEnumerable<dynamic> GetKPIGroupByTargetId(GetKPIGroupByTargetIdCriteria criteria);

        IEnumerable<TargetFilterModel> GetTargetToFilter(GetTargetToFilterCriteria criteria);

        IEnumerable<EmployeeModel> GetEmployeeByKPI(GetEmployeeByKPICriteria criteria);

        string ConvertTargetKPIInfoToXML(IEnumerable<EmployeeModel> employees, int kpiGroupId, int kpiId);
    }
}
