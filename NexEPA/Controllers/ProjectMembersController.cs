// --------------------------------------------------------------------------
// <copyright file="ProjectMembersController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.3.26">Created</change>
// </history>

namespace NexEPA.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using NexEPA.Project.Criteria.ProjectMembers;
    using NexEPA.Project.Models;
    using NexEPA.Project.Services.Impl;
    using NexEPA.Project.Services.Interfaces;

    /// <summary>
    /// Project Members Controller.
    /// </summary>
    public class ProjectMembersController : JsonController
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

        public ActionResult GetProjectRole()
        {
            GetAllProjectRoleCriteria criteria = new GetAllProjectRoleCriteria();
            IEnumerable<ProjectRoleModel> data = _projectMemberService.GetAllProjectRole(criteria);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
