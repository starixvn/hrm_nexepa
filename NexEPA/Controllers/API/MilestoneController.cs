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
    using Fanex.NAP.Framework.Data;
    using NexEPA.Project.Criteria.Milestone;
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

        /// <summary>
        /// Get Milestone.
        /// </summary>
        /// <param name="projectId">Project id.</param>
        /// <returns>List Milestone Model.</returns>
        public IEnumerable<MilestoneModel> GetMilestones(int projectId)
        {
            IEnumerable<MilestoneModel> result = null;
            if (projectId > 0)
            {
                GetMilestoneByProjectIdCriteria getMilestoneCriteria = new GetMilestoneByProjectIdCriteria()
                {
                    ProjectId = projectId
                };

                result = _projectService.GetMilestoneByProjectId(getMilestoneCriteria);
            }
            
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
                AddMilestoneCriteria criteria = new AddMilestoneCriteria()
                {
                    Description = milestone.Description,
                    Name = milestone.Name,
                    ProjectId = milestone.ProjectId,
                    StartDate = TimeZone.CurrentTimeZone.ToLocalTime(milestone.StartDate.Value),
                    EndDate = TimeZone.CurrentTimeZone.ToLocalTime(milestone.EndDate.Value),
                    Status = milestone.Status,
                    CreatedBy = 1,
                    CreatedDate = DateTime.Now.Date
                };

                CommandResult result = _projectService.InsertMilestone(criteria);

                if (result.Code != 0)
                {
                    var errorMessage = new List<string>() { result.Message };
                    return request.CreateResponse(HttpStatusCode.InternalServerError, errorMessage);
                }

                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            return request.CreateResponse(HttpStatusCode.BadRequest, RetrieveErrorMessages());
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
                DeleteMilestoneCriteria criteria = new DeleteMilestoneCriteria()
                {
                    MilestoneId = milestoneId,
                    ModifiedBy = 1,
                    ModifiedDate = DateTime.Now.Date
                };

                CommandResult result = _projectService.DeleteMilestone(criteria);

                if (result.Code != 0)
                {
                    List<string> errorMessage = new List<string>() { result.Message };
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
                UpdateMilestoneCriteria criteria = new UpdateMilestoneCriteria()
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

                CommandResult result = _projectService.UpdateMilestone(criteria);

                if (result.Code != 0)
                {
                    List<string> errorMessage = new List<string>() { result.Message };
                    return request.CreateResponse(HttpStatusCode.InternalServerError, errorMessage);
                }

                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            return request.CreateResponse(HttpStatusCode.BadRequest, RetrieveErrorMessages());
        }

        private IEnumerable<string> RetrieveErrorMessages()
        {
            return ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage));
        }
    }
}
