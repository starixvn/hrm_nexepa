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
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using NexEPA.Employee.Services;
    using NexEPA.Project.Models;

    /// <summary>
    /// Group API Controller.
    /// </summary>
    [ExceptionHandling]
    public class ProjectController : ApiController
    {
        private readonly GroupService _groupService;

        private readonly EmployeeService _employeeService;

        public ProjectController()
            : this(null, null)
        {
        }

        public ProjectController(GroupService groupService, EmployeeService employeeService)
        {
            this._groupService = groupService ?? new GroupService();
            this._employeeService = employeeService ?? new EmployeeService();
        }


        public HttpResponseMessage Post(HttpRequestMessage request, ProjectModel project)
        {
            if (ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                return request.CreateResponse(HttpStatusCode.BadRequest, RetrieveErrorMessages());
            }
        }


        private IEnumerable<string> RetrieveErrorMessages()
        {
            return ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage));
        }
    }
}
