// --------------------------------------------------------------------------
// <copyright file="ProjectMemberController.cs" company="Nexcel Solutions Vietnam">
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
    using NexEPA.Core.Constants;
    using NexEPA.Core.Enums;
    using NexEPA.Core.Models;
    using NexEPA.Project.Criteria.ProjectMembers;
    using NexEPA.Project.Models;
    using NexEPA.Project.Services.Implement;
    using NexEPA.Project.Services.Interface;
    using NexEPA.Web.Attributes;
    using NexEPA.Web.Controllers;

    /// <summary>
    /// Project Members Controller.
    /// </summary>
    public class ProjectMemberController : BaseController
    {
        private readonly IProjectMemberService projectMemberService;

        public ProjectMemberController()
            : this(null)
        {
        }

        public ProjectMemberController(IProjectMemberService projectMemberService)
        {
            this.projectMemberService = projectMemberService ?? new ProjectMemberService();
        }

        /// <summary>
        /// Get All Member of Project.
        /// </summary>
        /// <param name="projectId">Project Id.</param>
        /// <param name="isShowDeletedMember">Get disabled use.</param>
        /// <param name="milestoneId">Belong to milestone.</param>
        /// <returns>List of Project Member Model.</returns>
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageProject, Policy = Policy.Read)]
        public IEnumerable<ProjectMembersModel> Get(int projectId, bool? isShowDeletedMember = null, int? milestoneId = null)
        {
            IEnumerable<ProjectMembersModel> members = null;
            var getAllProjectMembersCriteria = new GetAllProjectMembersCriteria()
            {
                ProjectId = projectId,
                IsShowDeletedMember = isShowDeletedMember,
                MilestoneId = milestoneId
            };
            members = projectMemberService.GetAllProjectMember(getAllProjectMembersCriteria);

            return members;
        }

        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageProject, Policy = Policy.Read)]
        public IEnumerable<ProjectMembersModel> GetMembersByProject(int projectId)
        {
            IEnumerable<ProjectMembersModel> members = null;
            var getMembersByProjectsCriteria = new GetMembersByProjectCriteria()
            {
                ProjectId = projectId
            };
            members = projectMemberService.GetMembersByProject(getMembersByProjectsCriteria);

            return members;
        }

        /// <summary>
        /// Method get Member role in project.
        /// </summary>
        /// <param name="projectId">Project id.</param>
        /// <param name="employeeId">Employee id.</param>
        /// <returns>Project Members Model.</returns>
        public IEnumerable<ProjectMembersModel> GetMemberRoleInProject(int projectId, int employeeId)
        {
            IEnumerable<ProjectMembersModel> memberRoles = null;
            var getMemberRoleInProjectCriteria = new GetMemberRoleInProjectCriteria
            {
                ProjectId = projectId,
                EmployeeId = employeeId
            };
            memberRoles = projectMemberService.GetMemberRoleInProject(getMemberRoleInProjectCriteria);

            return memberRoles;
        }

        /// <summary>
        /// Add member.
        /// </summary>
        /// <param name="member">Member Model.</param>
        /// <returns>Http response message.</returns>
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageProject, Policy = Policy.Host)]
        public HttpResponseMessage Post(MemberModel member)
        {
            if (ModelState.IsValid)
            {
                var adddMemberCriteria = new AddMemberCriteria()
                {
                    ProjectId = member.ProjectId,
                    EmployeeId = member.EmployeeId,
                    ProjectRoleIds = member.ProjectRoleIds,
                    AddedBy = this.CurrentUser.UserId,
                    AddedDate = DateTime.Now.Date,
                    JoinDate = member.JoinDate.Value,
                };

                CommandResult result = projectMemberService.AddMember(adddMemberCriteria);
                if (result.Code != 0)
                {
                    var errorMessage = new List<string>() { result.Message };
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
        /// Method Edit Member.
        /// </summary>
        /// <param name="member">Member Model.</param>
        /// <returns>Http Response Message.</returns>
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageProject, Policy = Policy.Host)]
        public HttpResponseMessage Put(MemberModel member)
        {
            if (ModelState.IsValid)
            {
                var editMemberCriteria = new EditMemberCriteria()
                {
                    ProjectId = member.ProjectId,
                    EmployeeId = member.EmployeeId,
                    ProjectRoleIds = member.ProjectRoleIds,
                    ModifiedBy = this.CurrentUser.UserId,
                    ModifiedDate = DateTime.Now.Date,
                    JoinDate = member.JoinDate.Value,
                };

                CommandResult result = projectMemberService.EditMember(editMemberCriteria);
                if (result.Code != 0)
                {
                    var errorMessage = new List<string>() { result.Message };
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
        /// <returns>Http response message.</returns>
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageProject, Policy = Policy.Host)]
        public HttpResponseMessage Delete(int projectId, int employeeId)
        {
            var deleteMemberCriteria = new DeleteMemberCriteria()
            {
                ProjectId = projectId,
                EmployeeId = employeeId,
                ModifiedBy = this.CurrentUser.UserId,
                ModifiedDate = DateTime.Now.Date
            };

            CommandResult result = projectMemberService.DeleteMember(deleteMemberCriteria);
            if (result.Code != 0)
            {
                var errorMessage = new List<string>() { result.Message };
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
            var getAllProjectRoleCriteria = new GetAllProjectRoleCriteria();
            IEnumerable<ProjectRoleModel> data = projectMemberService.GetAllProjectRole(getAllProjectRoleCriteria);

            if (projectId != null && employeeId != null)
            {
                IEnumerable<ProjectMembersModel> roles = GetMemberRoleInProject(projectId.Value, employeeId.Value);
                if (roles != null)
                {
                    foreach (ProjectRoleModel item in data.Where(x => roles.Select(y => y.ProjectRoleId).Contains(x.ProjectRoleId)))
                    {
                        item.Selected = true;
                    }
                }
            }

            return data;
        }

        [HttpGet]
        public IList<ProjectMemberRoleModel> GetGroupedMembersByProject(int projectId)
        {
            var criteria = new GetMembersByProjectCriteria
            {
                ProjectId = projectId
            };

            IList<ProjectMemberRoleModel> groupedMembers = projectMemberService.GetGroupedMembersByProject(criteria);

            return groupedMembers;
        }

        private IEnumerable<string> RetrieveErrorMessages()
        {
            IEnumerable<string> errors = ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage));
            return errors;
        }

        [HttpGet]
        public IEnumerable<CommonEmployeeModel> GetPMByProject(int projectId)
        {
            var criteria = new GetPMByProjectCriteria()
            {
                ProjectId = projectId
            };
            return projectMemberService.GetPMByProject(criteria);
        }
    }
}
