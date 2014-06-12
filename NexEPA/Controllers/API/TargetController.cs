// -----------------------------------------------------------------------
// <copyright file="TargetController.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.04">Created</change>
// </history>

namespace NexEPA.Controllers.Target
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Core.Constants;
    using NexEPA.Core.Enums;
    using NexEPA.Employee.Models;
    using NexEPA.Project.Criteria.Milestone;
    using NexEPA.Project.Services.Implement;
    using NexEPA.Project.Services.Interface;
    using NexEPA.Target.Criteria.Target.Target;
    using NexEPA.Target.Criteria.Target.TargetEmployee;
    using NexEPA.Target.Criteria.Target.TargetKPI;
    using NexEPA.Target.Models;
    using NexEPA.Target.Services.Implement;
    using NexEPA.Target.Services.Interface;
    using NexEPA.Target.ViewModels;
    using NexEPA.Web.Attributes;
    using NexEPA.Web.Controllers;
    using NexEPA.Web.Services.Impl;
    using NexEPA.Web.Services.Interfaces;

    /// <summary>
    /// Target Controller.
    /// </summary>
    public class TargetController : BaseController
    {
        private ITargetService _targetService;

        private IProjectService _projectService;

        private ICommonService _commonService;

        public TargetController()
            : this(null, null, null)
        {
        }

        public TargetController(
            ITargetService targetService, 
            IProjectService projectService,
            ICommonService commonService)
        {
            _targetService = targetService ?? new TargetService();
            _projectService = projectService ?? new ProjectService();
            _commonService = commonService ?? new CommonService();
        }

        /// <summary>
        /// Method Get Target.
        /// </summary>
        /// <param name="targetTypeId">Target Type Id.</param>
        /// <param name="startDate">Query Start date.</param>
        /// <param name="endDate">Query End Date.</param>
        /// <returns>Target View Model.</returns>
        [HttpGet]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageTarget, Policy = Policy.Read)]
        public List<TargetViewModel> GetTarget(
            int? targetTypeId = null,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            var criteria = new GetTargetCriteria
            {
                ActorUserId = this.CurrentUser.UserId,
                TargetTypeId = targetTypeId,
                StartDate = startDate,
                EndDate = endDate
            };

            return _targetService.GetAllTarget(criteria);
        }

        [HttpGet]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageTarget, Policy = Policy.Read)]
        public TargetViewModel GetTargetById(int targetId)
        {
            var criteria = new GetTargetByIdCriteria
            {
                TargetId = targetId,
                ActorUserId = this.CurrentUser.UserId
            };

            return _targetService.GetTargetById(criteria);
        }

        /// <summary>
        /// Insert new Target.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Command Result.</returns>
        [HttpPost]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageTarget, Policy = Policy.Host)]
        public CommandResult InsertTarget(InsertTargetCriteria criteria)
        {
            if (criteria != null)
            {
                criteria.CreatedBy = this.CurrentUser.UserId;
                criteria.CreatedDate = DateTime.Now;
                //// default status is Open when create the new target
                criteria.Status = (int)Status.Open;
                //// default target type is Project
                criteria.TargetTypeId = 2;
            }

            return _targetService.Insert(criteria);
        }

        [HttpPut]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageTarget, Policy = Policy.Host)]
        public CommandResult Put(UpdateTargetCriteria criteria)
        {
            if (criteria != null)
            {
                criteria.ModifiedBy = this.CurrentUser.UserId;
                criteria.ModifiedDate = DateTime.Now;
            }

            return _targetService.Update(criteria);
        }

        [HttpPut]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageTarget, Policy = Policy.Host)]
        public CommandResult UpdateStatus(UpdateTargetStatusCriteria criteria)
        {
            if (criteria != null)
            {
                criteria.ModifiedBy = this.CurrentUser.UserId;
                criteria.ModifiedDate = DateTime.Now;
            }

            return _targetService.UpdateStatus(criteria);
        }

        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageTarget, Policy = Policy.Host)]
        public CommandResult Delete(int targetId)
        {
            var criteria = new DeleteTargetCriteria
            {
                TargetId = targetId,
                ModifiedBy = this.CurrentUser.UserId,
                ModifiedDate = DateTime.Now
            };

            return _targetService.Delete(criteria);
        }

        [HttpGet]
        public IEnumerable<dynamic> GetMilestoneByProjectId(int projectId)
        {
            var getMilestoneCriteria = new GetMilestoneByProjectIdCriteria()
            {
                ProjectId = projectId
            };
            var milestoneList = _projectService.GetMilestoneByProjectId(getMilestoneCriteria);

            IEnumerable<dynamic> milestoneResult = milestoneList.Select(mi => new { mi.Name, mi.MilestoneId });
            return milestoneResult;
        }

        [HttpGet]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageTarget, Policy = Policy.Read)]
        public IEnumerable<EmployeeModel> GetEmployeeByTargetId(int targetId)
        {
            var criteria = new GetAllEmployeeByTargetIdCriteria
            {
                 TargetId = targetId
            };

            return _targetService.GetTargetEmployeeByTargetId(criteria);
        }

        [HttpPost]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageTarget, Policy = Policy.Host)]
        public CommandResult InsertTargetEmployee(AddTargetEmployeeCriteria criteria)
        {
            if (criteria != null)
            {
                criteria.AddedBy = this.CurrentUser.UserId;
                criteria.AddedDate = DateTime.Now;
            }

            return _targetService.InsertTargetEmployee(criteria);
        }

        /// <summary>
        /// Delete Target Employee.
        /// </summary>
        /// <param name="targetId">The target ID.</param>
        /// <param name="employeeId">The employee ID.</param>
        /// <returns>The Command Result.</returns>
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageTarget, Policy = Policy.Host)]
        public CommandResult DeleteTargetEmployee(int targetId, int employeeId)
        {
            var criteria = new DeleteTargetEmployeeCriteria
            {
                TargetId = targetId,
                EmployeeId = employeeId,
                ModifiedBy = this.CurrentUser.UserId,
                ModifiedDate = DateTime.Now
            };

            return _targetService.DeleteTargetEmployee(criteria);
        }

        [HttpGet]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageTarget, Policy = Policy.Read)]
        public List<TargetKPIGroupModel> GetKPIByTargetId(int targetId, int? groupId = null)
        {
            var criteria = new GetKPIByTargetIdCriteria
            {
                TargetId = targetId,
                KPIGroupId = groupId
            };

            return _targetService.GetKPIByTargetId(criteria);
        }

        [HttpGet]
        public TargetDateModel GetProjectOrMilestoneDateById(int projectId, int? milestoneId = null)
        {
            var criteria = new GetProjectOrMilestoneDateCriteria
            {
                ProjectId = projectId,
                MilestoneId = milestoneId
            };

            return _targetService.GetProjectOrMilestoneDate(criteria);
        }

        [HttpGet]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageTarget, Policy = Policy.Read)]
        public IEnumerable<dynamic> GetKPIGroupByTargetId(int targetId)
        {
            var criteria = new GetKPIGroupByTargetIdCriteria
            {
                TargetId = targetId
            };

            return _targetService.GetKPIGroupByTargetId(criteria);
        }

        [HttpGet]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageTarget, Policy = Policy.Read)]
        public IEnumerable<TargetFilterModel> GetTargetToFilter()
        {
            var criteria = new GetTargetToFilterCriteria
            {
                ActorUserId = this.CurrentUser.UserId,
            };

            return _targetService.GetTargetToFilter(criteria);
        }

        /// <summary>
        /// Get permission of user on current function.
        /// </summary>
        /// <returns>The policy.</returns>
        [HttpGet]
        public int GetPermission()
        {
            int policy = _commonService.GetPermission(EPAFunctionKeys.ManageTarget);
            return policy;
        }
    }
}