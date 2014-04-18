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
    using NexEPA.Core.Model.Project;
    using NexEPA.Core.Utilities;
    using NexEPA.Project.Criteria.Milestone;
    using NexEPA.Project.Criteria.Project;
    using NexEPA.Project.Models;
    using NexEPA.Project.Services.Impl;
    using NexEPA.Project.Services.Interfaces;
    using NexEPA.Target.Criteria.Target.Target;
    using NexEPA.Target.Criteria.Target.TargetEmployee;
    using NexEPA.Target.Criteria.Target.TargetKPI;
    using NexEPA.Target.Services.Impl.Target;
    using NexEPA.Target.Services.Interfaces;
    using NexEPA.Target.ViewModels.Target;
    using NexEPA.Web.Attributes;
    using NexEPA.Web.Controllers;

    /// <summary>
    /// Target Controller.
    /// </summary>
    public class TargetController : BaseController
    {
        private ITargetServices _targetServices;
        private IProjectService _projectService;

        public TargetController()
            : this(null, null)
        {
        }

        public TargetController(
            TargetServices targetServices, 
            ProjectService projectService)
        {
            _targetServices = targetServices ?? new TargetServices();
            _projectService = projectService ?? new ProjectService();
        }

        [HttpGet]
        [ApiAuthorize(Roles = "PM,SA")]
        public List<TargetItem> GetTarget(
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

            return _targetServices.GetAllTarget(criteria);
        }

        [HttpGet]
        [ApiAuthorize(Roles = "PM,SA")]
        public TargetItem GetTargetById(int targetId)
        {
            var criteria = new GetTargetByIdCriteria
            {
                TargetId = targetId
            };

            return _targetServices.GetTargetById(criteria);
        }

        [HttpPost]
        [ApiAuthorize(Roles = "PM,SA")]
        public CommandResult InsertTarget(InsertTargetCriteria criteria)
        {
            return _targetServices.Insert(criteria);
        }

        [ApiAuthorize(Roles = "PM,SA")]
        public CommandResult Put(UpdateTargetCriteria criteria)
        {
            return _targetServices.Update(criteria);
        }

        [ApiAuthorize(Roles = "PM,SA")]
        public CommandResult Delete(int targetId)
        {
            var criteria = new DeleteTargetCriteria
            {
                TargetId = targetId
            };

            return _targetServices.Delete(criteria);
        }

        [HttpGet]
        public IEnumerable<dynamic> GetAllProject()
        {
            var criteria = new GetAllProjectCriteria();
            IEnumerable<ProjectModel> allProjectList = _projectService.GetAllProject(criteria);

            IEnumerable<dynamic> projectOfTarget = allProjectList.Select(pro => new { pro.Name, pro.ProjectId });
            return projectOfTarget;
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
        public IEnumerable<SelectListModel> GetAllStatus()
        {
            return EnumStatus.GetAllStatus();
        }

        [HttpGet]
        [ApiAuthorize(Roles = "PM,SA")]
        public List<TargetEmployeeItem> GetEmployeeByTargetId(int targetId)
        {
            var criteria = new GetAllEmployeeByTargetIdCriteria
            {
                 TargetId = targetId
            };

            return _targetServices.GetTargetEmployeeByTargetId(criteria);
        }

        [HttpPost]
        [ApiAuthorize(Roles = "PM,SA")]
        public CommandResult InsertTargetEmployee(AddTargetEmployeeCriteria criteria)
        {
            return _targetServices.InsertTargetEmployee(criteria);
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

            return _targetServices.DeleteTargetEmployee(criteria);
        }

        [HttpGet]
        [ApiAuthorize(Roles = "PM,SA")]
        public List<TargetKPIItem> GetKPIByTargetId(int targetId, int? groupId = null)
        {
            var criteria = new GetKPIByTargetIdCriteria
            {
                TargetId = targetId,
                KPIGroupId = groupId
            };

            return _targetServices.GetKPIByTargetId(criteria);
        }
    }
}