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
    using System;
    using System.Collections.Generic;
    using System.Linq;
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

        /// <summary>
        /// Update Milestone.
        /// </summary>
        /// <param name="request">Http Request Message.</param>
        /// <param name="milestone">Milestone Model.</param>
        /// <returns>Http Message.</returns>
        public HttpResponseMessage POST(HttpRequestMessage request, MilestoneModel milestone)
        {
            if (ModelState.IsValid)
            {
                var criteria = new AddMilestoneCriteria()
                {
                    Description = milestone.Description,
                    Name = milestone.Name,
                    ProjectId = milestone.ProjectId,
                    StartDate = milestone.StartDate,
                    EndDate = milestone.EndDate,
                    Status = milestone.Status,
                    CreatedBy = 1,
                    CreatedDate = DateTime.Now.Date
                };
                var result = _projectService.InsertMilestone(criteria);
                if (result.Code != 0)
                {
                    var errorMessage = new List<string>() { result.Message };
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, errorMessage);
                }

                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, RetrieveErrorMessages());
        }

        /// <summary>
        /// Delete Milestone.
        /// </summary>
        /// <param name="milestoneId">Milestone Id.</param>
        /// <returns>Http message.</returns>
        public HttpResponseMessage DELETE(int milestoneId)
        {
            if (milestoneId > 0)
            {
                var criteria = new DeleteMilestoneCriteria()
                {
                    MilestoneId = milestoneId,
                    ModifiedBy = 1,
                    ModifiedDate = DateTime.Now.Date
                };
                var result = _projectService.DeleteMilestone(criteria);
                if (result.Code != 0)
                {
                    var errorMessage = new List<string>() { result.Message };
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, errorMessage);
                }

                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }

        /// <summary>
        /// Update Milestone.
        /// </summary>
        /// <param name="request">Http request message.</param>
        /// <param name="milestone">Milestone model.</param>
        /// <returns>Http message.</returns>
        public HttpResponseMessage PUT(HttpRequestMessage request, MilestoneModel milestone)
        {
            if (ModelState.IsValid)
            {
                var criteria = new UpdateMilestoneCriteria()
                {
                    Description = milestone.Description,
                    MilestoneId = milestone.MilestoneId,
                    Name = milestone.Name,
                    ProjectId = milestone.ProjectId,
                    StartDate = milestone.StartDate,
                    EndDate = milestone.EndDate,
                    Status = milestone.Status,
                    ModifiedBy = 1,
                    ModifiedDate = DateTime.Now.Date
                };
                var result = _projectService.UpdateMilestone(criteria);
                if (result.Code != 0)
                {
                    var errorMessage = new List<string>() { result.Message };
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, errorMessage);
                }

                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, RetrieveErrorMessages());
        }

        private IEnumerable<string> RetrieveErrorMessages()
        {
            return ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage));
        }
    }
}
