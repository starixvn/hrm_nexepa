// --------------------------------------------------------------------------
// <copyright file="IFlowManagementService.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.6.18">Created</change>
// </history>

namespace NexEPA.Organization.Services.Interface
{
    using System.Collections.Generic;
    using NexEPA.Core.Models;
    using NexEPA.Organization.Criteria.FlowManagement;
    using NexEPA.Organization.Models;

    /// <summary>
    /// Interface Flow Management Service.
    /// </summary>
    public interface IFlowManagementService
    {
        CommandResult AddDepartmentFlow(AddDepartmentFlowCriteria criteria);

        CommandResult AddGroupFlow(AddGroupFlowCriteria criteria);

        CommandResult AddEmployeeFlow(AddEmployeeFlowCriteria criteria);

        CommandResult DeleteDepartmentFlow(DeleteDepartmentFlowCriteria criteria);

        CommandResult DeleteGroupFlow(DeleteGroupFlowCriteria criteria);

        CommandResult DeleteEmployeeFlow(DeleteEmployeeFlowCriteria criteria);

        IEnumerable<EPAFlowModel> GetFlowDepartment(GetDepartmentFlowCriteria criteria);

        IEnumerable<EPAFlowModel> GetFlowGroup(GetGroupFlowCriteria criteria);

        IEnumerable<EPAFlowModel> GetFlowEmployee(GetEmployeeFlowCriteria criteria);
    }
}
