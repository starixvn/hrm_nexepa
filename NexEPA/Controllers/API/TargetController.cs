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
    using NexEPA.Target.Criteria.Target;
    using NexEPA.Target.Services.Impl.Target;
    using NexEPA.Target.Services.Interfaces;
    using NexEPA.Target.Target.Criteria;
    using NexEPA.Target.ViewModels.Target;
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
        public TargetItem GetTargetById(int targetId)
        {
            var criteria = new GetTargetByIdCriteria
            {
                TargetId = targetId
            };

            return _targetServices.GetTargetById(criteria);
        }

        public CommandResult Post(InsertTargetCriteria criteria)
        {
            return _targetServices.Insert(criteria);
        }

        public CommandResult Put(UpdateTargetCriteria criteria)
        {
            return _targetServices.Update(criteria);
        }

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

            var projectOfTarget = allProjectList.Select(pro => new { pro.Name, pro.ProjectId });
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

            var milestoneResult = milestoneList.Select(mi => new { mi.Name, mi.MilestoneId });
            return milestoneResult;
        }

        [HttpGet]
        public IEnumerable<SelectListModel> GetAllStatus()
        {
            return EnumStatus.GetAllStatus();
        }
    }
}