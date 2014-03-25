// --------------------------------------------------------------------------
// <copyright file="MilestoneController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.3.25">Created</change>
// </history>

namespace NexEPA.Controllers.API
{
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using NexEPA.Project.Criteria;
    using NexEPA.Project.Models;
    using NexEPA.Project.Services.Impl;
    using NexEPA.Project.Services.Interfaces;

    /// <summary>
    /// Milestone Controller.
    /// </summary>
    public class MilestoneController : ApiController
    {
        private readonly IProjectService _projectService;

        public MilestoneController()
            : this(null)
        {
        }

        public MilestoneController(IProjectService projectService)
        {
            this._projectService = projectService ?? new ProjectService();
        }

        public IEnumerable<MilestoneModel> GetMilestones(int projectId)
        {
            var getMilestoneCriteria = new GetMilestoneByProjectIdCriteria()
            {
                ProjectId = projectId
            };
            var result = _projectService.GetMilestoneByProjectId(getMilestoneCriteria);

            return result;
        }

        public HttpResponseMessage POST(HttpRequestMessage request, MilestoneModel milestone)
        {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        public HttpResponseMessage DELETE(HttpRequestMessage request)
        {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        public HttpResponseMessage PUT(HttpRequestMessage request, MilestoneModel milestone)
        {
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
