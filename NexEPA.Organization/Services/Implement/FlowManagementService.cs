// --------------------------------------------------------------------------
// <copyright file="FlowManagementService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.6.18">Created</change>
// </history>

namespace NexEPA.Organization.Services.Implement
{
    using Fanex.Common.Helper;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Core.Services;
    using NexEPA.Organization.Criteria.FlowManagement;
    using NexEPA.Organization.Services.Interface;

    /// <summary>
    /// Flow Management Service.
    /// </summary>
    public class FlowManagementService : BaseService, IFlowManagementService
    {
        public CommandResult AddDepartmentFlow(AddDepartmentFlowCriteria criteria)
        {
            CommandResult result = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                result = this._Repository.Insert(criteria);
            }

            return result;
        }

        public CommandResult AddGroupFlow(AddGroupFlowCriteria criteria)
        {
            CommandResult result = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                result = this._Repository.Insert(criteria);
            }

            return result;
        }

        public CommandResult AddEmployeeFlow(AddEmployeeFlowCriteria criteria)
        {
            CommandResult result = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                result = this._Repository.Insert(criteria);
            }

            return result;
        }

        public CommandResult DeleteDepartmentFlow(DeleteDepartmentFlowCriteria criteria)
        {
            CommandResult result = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                result = this._Repository.Delete(criteria);
            }

            return result;
        }

        public CommandResult DeleteGroupFlow(DeleteGroupFlowCriteria criteria)
        {
            CommandResult result = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                result = this._Repository.Delete(criteria);
            }

            return result;
        }

        public CommandResult DeleteEmployeeFlow(DeleteEmployeeFlowCriteria criteria)
        {
            CommandResult result = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                result = this._Repository.Delete(criteria);
            }

            return result;
        }
    }
}
