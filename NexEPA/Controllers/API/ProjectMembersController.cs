// --------------------------------------------------------------------------
// <copyright file="ProjectMembersController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.3.26">Created</change>
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
    using NexEPA.Project.Criteria.ProjectMembers;
    using NexEPA.Project.Models;
    using NexEPA.Project.Services.Impl;
    using NexEPA.Project.Services.Interfaces;

    /// <summary>
    /// Project Members Controller.
    /// </summary>
    public class ProjectMembersController : ApiController
    {
        private readonly IProjectMemberService _projectMemberService;

        public ProjectMembersController()
            : this(null)
        {
        }

        public ProjectMembersController(IProjectMemberService projectMemberService)
        {
            this._projectMemberService = projectMemberService ?? new ProjectMemberService();
        }

        /// <summary>
        /// Get All Member of Project.
        /// </summary>
        /// <param name="projectId">Project Id.</param>
        /// <param name="isDisabled">Get disabled use.</param>
        /// <param name="milestoneId">Belong to milestone.</param>
        /// <returns>List of Project Member Model.</returns>
        public IEnumerable<ProjectMembersModel> Get(int projectId, bool? isDisabled = null, int? milestoneId = null)
        {
            GetAllProjectMembersCriteria criteria = new GetAllProjectMembersCriteria()
            {
                ProjectId = projectId,
                IsDisabled = isDisabled,
                MilestoneId = milestoneId
            };
            IEnumerable<ProjectMembersModel> projects = _projectMemberService.GetAllProjectMember(criteria);

            return projects;
        }

        /// <summary>
        /// Add member.
        /// </summary>
        /// <param name="member">Member Model.</param>
        /// <returns>Http response message.</returns>
        public HttpResponseMessage Post(MemberModel member)
        {
            if (ModelState.IsValid)
            {
                AddMemberCriteria criteria = new AddMemberCriteria()
                {
                    ProjectId = member.ProjectId,
                    EmployeeId = member.EmployeeId,
                    ProjectRoleIds = member.ProjectRoleIds,
                    AddedBy = 1,
                    AddedDate = DateTime.Now.Date
                };

                CommandResult result = _projectMemberService.AddMember(criteria);
                if (result.Code != 0)
                {
                    List<string> errorMessage = new List<string>() { result.Message };
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, errorMessage);
                }

                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, RetrieveErrorMessages());
            }
        }

        /// <summary>
        /// Delete member.
        /// </summary>
        /// <param name="member">Member model.</param>
        /// <returns>Http Response Message.</returns>
        public HttpResponseMessage Delete(MemberModel member)
        {
            if (member != null)
            {
                DeleteMemberCriteria criteria = new DeleteMemberCriteria()
                {
                    ProjectId = member.ProjectId,
                    EmployeeId = member.EmployeeId,
                    ProjectRoleId = member.ProjectRoleId,
                    ModifiedBy = 1,
                    ModifiedDate = DateTime.Now.Date
                };

                CommandResult result = _projectMemberService.DeleteMemebr(criteria);
                if (result.Code != 0)
                {
                    List<string> errorMessage = new List<string>() { result.Message };
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, errorMessage);
                }

                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        private IEnumerable<string> RetrieveErrorMessages()
        {
            return ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage));
        }
    }
}
