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
    using System.Web.Http;
    using NexEPA.Core.Criteria;
    using NexEPA.Core.Models;
    using NexEPA.Core.Services;
    using NexEPA.Core.Services.Interface;
    using NexEPA.Core.Utilities;
    using NexEPA.Project.Services.Impl;
    using NexEPA.Project.Services.Interfaces;

    /// <summary>
    /// Core controller.
    /// </summary>
    public class CoreController : ApiController
    {
        private IEPAService _epaService;
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
            var criteria = new GetAllProjectToFilterCriteria();
            return _epaService.GetAllProjectToFilter(criteria);
        }

        [HttpGet]
        public IEnumerable<SelectListModel> GetAllStatus()
        {
            return EnumStatus.GetAllStatus();
        }
    }
}
