// --------------------------------------------------------------------------
// <copyright file="ProjectController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.3.19">Created</change>
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
    /// Project Controller.
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
                return request.CreateResponse(HttpStatusCode.OK, RetrieveErrorMessages());
            }
        }

        private IEnumerable<string> RetrieveErrorMessages()
        {
            return ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage));
        }
    }
}
