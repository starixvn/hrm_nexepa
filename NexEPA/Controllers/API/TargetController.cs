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

    /// <summary>
    /// Target Controller.
    /// </summary>
    public class TargetController : BaseController
    {
        private ITargetService _targetService;
        private IProjectService _projectService;

        public TargetController()
            : this(null, null)
        {
        }

        public TargetController(
            TargetService targetService, 
            ProjectService projectService)
        {
            _targetService = targetService ?? new TargetService();
            _projectService = projectService ?? new ProjectService();
        }

        [HttpGet]
        [ApiAuthorize(Roles = "PM,SA")]
        public List<TargetViewModel> GetTarget(
            int? targetTypeId = null,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            var criteria = new GetTargetCriteria
            {
               TargetTypeId = targetTypeId,
               StartDate = startDate,
               EndDate = endDate
            };

            return _targetService.GetAllTarget(criteria);
        }

        [HttpGet]
        [ApiAuthorize(Roles = "PM,SA")]
        public TargetViewModel GetTargetById(int targetId)
        {
            var criteria = new GetTargetByIdCriteria
            {
                TargetId = targetId
            };

            return _targetService.GetTargetById(criteria);
        }

        /// <summary>
        /// Insert new Target.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Command Result.</returns>
        [HttpPost]
        [ApiAuthorize(Roles = "PM,SA")]
        public CommandResult InsertTarget(InsertTargetCriteria criteria)
        {
            if (criteria != null)
            {
                criteria.CreatedBy = this.CurrentUser.UserId;
                criteria.CreatedDate = DateTime.Now;
                //// default status is Open when create the new target
                criteria.Status = (int)Status.Open;
            }

            return _targetService.Insert(criteria);
        }

        [ApiAuthorize(Roles = "PM,SA")]
        [HttpPut]
        public CommandResult Put(UpdateTargetCriteria criteria)
        {
            if (criteria != null)
            {
                criteria.ModifiedBy = this.CurrentUser.UserId;
                criteria.ModifiedDate = DateTime.Now;
            }

            return _targetService.Update(criteria);
        }

        [ApiAuthorize(Roles = "PM,SA")]
        [HttpPut]
        public CommandResult UpdateStatus(UpdateTargetStatusCriteria criteria)
        {
            if (criteria != null)
            {
                criteria.ModifiedBy = this.CurrentUser.UserId;
                criteria.ModifiedDate = DateTime.Now;
            }

            return _targetService.UpdateStatus(criteria);
        }

        [ApiAuthorize(Roles = "PM,SA")]
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
        [ApiAuthorize(Roles = "PM,SA")]
        public IEnumerable<EmployeeModel> GetEmployeeByTargetId(int targetId)
        {
            var criteria = new GetAllEmployeeByTargetIdCriteria
            {
                 TargetId = targetId
            };

            return _targetService.GetTargetEmployeeByTargetId(criteria);
        }

        [HttpPost]
        [ApiAuthorize(Roles = "PM,SA")]
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
        [ApiAuthorize(Roles = "PM,SA")]
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
        [ApiAuthorize(Roles = "PM,SA")]
        public List<TargetKPIGroupModel> GetKPIByTargetId(int targetId, int? groupId = null)
        {
            var criteria = new GetKPIByTargetIdCriteria
            {
                TargetId = targetId,
                KPIGroupId = groupId
            };

            return _targetService.GetKPIByTargetId(criteria);
        }
    }
}