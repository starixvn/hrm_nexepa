// -----------------------------------------------------------------------
// <copyright file="CoreController.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.03.27">Created</change>
// </history>

namespace NexEPA.Controllers.API
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;
    using NexEPA.Core.Criteria;
    using NexEPA.Core.Model.Target;
    using NexEPA.Core.Services;
    using NexEPA.Project.Criteria.Project;
    using NexEPA.Project.Models;
    using NexEPA.Project.Services.Impl;
    using NexEPA.Project.Services.Interfaces;

    /// <summary>
    /// Core controller.
    /// </summary>
    public class CoreController : ApiController
    {
        private EPAService _epaService;
        private IProjectService _projectService;

        public CoreController()
            : this(null, null)
        { 
        }

        public CoreController(EPAService epaService, ProjectService projectService)
        {
            _epaService = epaService ?? new EPAService();
            _projectService = projectService ?? new ProjectService();
        }

        [HttpGet]
        public IEnumerable<TargetType> AllTargetType()
        {
            var criteria = new GetAllTargetTypeCriteria();
            return _epaService.GetAllTargetType(criteria);
        }

        [HttpGet]
        public IEnumerable<dynamic> GetAllProject()
        {
            var criteria = new GetAllProjectCriteria();
            IEnumerable<ProjectModel> allProjectList = _projectService.GetAllProject(criteria);

            IEnumerable<dynamic> projectOfTarget = allProjectList.Select(pro => new { pro.Name, pro.ProjectId });
            return projectOfTarget;
        }
    }
}
