// --------------------------------------------------------------------------
// <copyright file="IFlowManagementService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.6.18">Created</change>
// </history>

namespace NexEPA.Organization.Services.Interface
{
    using Fanex.NAP.Framework.Data;
    using NexEPA.Organization.Criteria.FlowManagement;

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
    }
}
