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
    using NexEPA.Web.Controllers;

    /// <summary>
    /// Project Members Controller.
    /// </summary>
    public class ProjectMembersController : BaseController
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
        /// <param name="isShowDeletedMember">Get disabled use.</param>
        /// <param name="milestoneId">Belong to milestone.</param>
        /// <returns>List of Project Member Model.</returns>
        public IEnumerable<ProjectMembersModel> Get(int projectId, bool? isShowDeletedMember = null, int? milestoneId = null)
        {
            GetAllProjectMembersCriteria criteria = new GetAllProjectMembersCriteria()
            {
                ProjectId = projectId,
                IsShowDeletedMember = isShowDeletedMember,
                MilestoneId = milestoneId
            };
            IEnumerable<ProjectMembersModel> projects = _projectMemberService.GetAllProjectMember(criteria);

            return projects;
        }

        public IEnumerable<ProjectMembersModel> GetMemberRoleInProject(int projectId, int employeeId)
        {
            GetMemberRoleInProjectCriteria criteria = new GetMemberRoleInProjectCriteria
            {
                ProjectId = projectId,
                EmployeeId = employeeId
            };
            IEnumerable<ProjectMembersModel> memberRoles = _projectMemberService.GetMemberRoleInProject(criteria);

            return memberRoles;
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
                    AddedBy = this.CurrentUser.UserId,
                    AddedDate = DateTime.Now.Date,
                    JoinDate = TimeZone.CurrentTimeZone.ToLocalTime(member.JoinDate.Value),
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
        /// Delete Project Member.
        /// </summary>
        /// <param name="projectId">Project id.</param>
        /// <param name="employeeId">Employee Id.</param>
        /// <param name="projectRoleId">Project role id.</param>
        /// <returns>Http response message.</returns>
        public HttpResponseMessage Delete(int projectId, int employeeId, int projectRoleId)
        {
            DeleteMemberCriteria criteria = new DeleteMemberCriteria()
            {
                ProjectId = projectId,
                EmployeeId = employeeId,
                ProjectRoleId = projectRoleId,
                ModifiedBy = this.CurrentUser.UserId,
                ModifiedDate = DateTime.Now.Date
            };

            CommandResult result = _projectMemberService.DeleteMember(criteria);
            if (result.Code != 0)
            {
                List<string> errorMessage = new List<string>() { result.Message };
                return Request.CreateResponse(HttpStatusCode.InternalServerError, errorMessage);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        /// <summary>
        /// Get Project Role.
        /// </summary>
        /// <param name="projectId">Project Id.</param>
        /// <param name="employeeId">Employee Id.</param>
        /// <returns>List ProjectRoleModel.</returns>
        [HttpGet]
        public IEnumerable<ProjectRoleModel> ProjectRole(int? projectId = null, int? employeeId = null)
        {
            GetAllProjectRoleCriteria criteria = new GetAllProjectRoleCriteria();
            IEnumerable<ProjectRoleModel> data = _projectMemberService.GetAllProjectRole(criteria);

            if (projectId != null && employeeId != null)
            {
                IEnumerable<ProjectMembersModel> roles = GetMemberRoleInProject(projectId.Value, employeeId.Value);
                if (roles != null)
                {
                    foreach (var item in data.Where(x => roles.Select(y => y.ProjectRoleId).Contains(x.ProjectRoleId)))
                    {
                        item.Selected = true;
                    }
                }
            }

            return data;
        }

        private IEnumerable<string> RetrieveErrorMessages()
        {
            return ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage));
        }
    }
}
