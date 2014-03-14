// --------------------------------------------------------------------------
// <copyright file="GroupController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Quinn.Vo" date="2014.3.11">Created</change>
// </history>

namespace NexEPA.Controllers.API
{
    using System.Collections.Generic;
    using System.Web.Http;
    using NexEPA.Employee.Criteria;
    using NexEPA.Employee.Models;
    using NexEPA.Employee.Services;

    /// <summary>
    /// Group API Controller.
    /// </summary>
    [ExceptionHandling]
    public class GroupController : ApiController
    {
        private readonly GroupService _groupService;

        public GroupController()
            : this(null)
        {
        }

        public GroupController(GroupService groupService)
        {
            this._groupService = groupService ?? new GroupService();
        }

        public IEnumerable<Group> GetGroups()
        {
            var criteria = new GetGroupByIdCriteria
            {
                GroupId = 1
            };

            var groups = _groupService.GetData<Group>(criteria);

            return groups;
        }
    }
}
