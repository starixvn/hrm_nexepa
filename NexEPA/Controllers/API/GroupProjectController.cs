// --------------------------------------------------------------------------
// <copyright file="GroupProjectController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Tim.Trinh" date="2015.4.15">Created</change>
// </history>

namespace NexEPA.Controllers.API
{
    using System.Collections.Generic;
    using System.Web.Http;
    using NexEPA.Core.Constants;
    using NexEPA.Core.Enums;
    using NexEPA.Core.Models;
    using NexEPA.Project.Criteria.GroupProject;
    using NexEPA.Project.Models.GroupProject;
    using NexEPA.Project.Services.Implement;
    using NexEPA.Project.Services.Interface;
    using NexEPA.Web.Attributes;
    using NexEPA.Web.Controllers;
    using NexEPA.Web.Services.Impl;
    using NexEPA.Web.Services.Interfaces;

    /// <summary>
    /// Group project controller.
    /// </summary>
    public class GroupProjectController : BaseController
    {
        private readonly IGroupProjectService _groupProjectService;
        private readonly ICommonService _commonService;

        public GroupProjectController()
            : this(null, null)
        {
        }

        public GroupProjectController(IGroupProjectService groupProjectService, ICommonService commonService)
        {
            _groupProjectService = groupProjectService ?? new GroupProjectService();
            _commonService = commonService ?? new CommonService();
        }

        /// <summary>
        /// Get all group project.
        /// </summary>
        /// <returns>List group project.</returns>
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageGroupProject, Policy = Policy.Read)]
        public List<GroupProjectModel> GetAllGroupProject()
        {
            var criteria = new GetAllGroupProjectCriteria();

            List<GroupProjectModel> groupProjects = _groupProjectService.GetAllGroupProject(criteria);

            return groupProjects;
        }

        /// <summary>
        /// Insert group project.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Command result.</returns>
        [HttpPost]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageGroupProject, Policy = Policy.Host)]
        public object InsertGroupProject(InsertGroupProjectCriteria criteria)
        {
            if (criteria != null)
            {
                criteria.CreatedBy = this.CurrentUser.UserId;
            }

            CommandResult result = _groupProjectService.InsertGroupProject(criteria);

            return new { result = result, groupProjectId = criteria.GroupProjectId };
        }

        /// <summary>
        /// Update group project.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>Command result.</returns>
        [HttpPut]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageGroupProject, Policy = Policy.Host)]
        public CommandResult UpdateGroupProject(UpdateGroupProjectCriteria criteria)
        {
            if (criteria != null)
            {
                criteria.ModifiedBy = this.CurrentUser.UserId;
            }

            CommandResult result = _groupProjectService.UpdateGroupProject(criteria);

            return result;
        }

        /// <summary>
        /// Delete group project.
        /// </summary>
        /// <param name="groupProjectId">Group project id.</param>
        /// <returns>Command result.</returns>
        [HttpDelete]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageGroupProject, Policy = Policy.Host)]
        public CommandResult DeleteGroupProject(int groupProjectId)
        {
            var criteria = new DeleteGroupProjectCriteria()
            {
                GroupProjectId = groupProjectId,
                ModifiedBy = this.CurrentUser.UserId
            };

            CommandResult result = _groupProjectService.DeleteGroupProject(criteria);

            return result;
        }

        /// <summary>
        /// Get permission of user on current function.
        /// </summary>
        /// <returns>The policy.</returns>
        [HttpGet]
        public int GetPermission()
        {
            int policy = _commonService.GetPermission(EPAFunctionKeys.ManageGroupProject);
            return policy;
        }
    }
}