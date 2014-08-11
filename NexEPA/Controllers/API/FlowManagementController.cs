// --------------------------------------------------------------------------
// <copyright file="FlowManagementController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.6.18">Created</change>
// </history>

namespace NexEPA.Controllers.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using NexEPA.Core.Constants;
    using NexEPA.Core.Enums;
    using NexEPA.Core.Models;
    using NexEPA.Organization.Criteria.FlowManagement;
    using NexEPA.Organization.Models;
    using NexEPA.Organization.Services.Implement;
    using NexEPA.Organization.Services.Interface;
    using NexEPA.Web.Attributes;
    using NexEPA.Web.Controllers;

    /// <summary>
    /// Flow Management Controller.
    /// </summary>
    public class FlowManagementController : BaseController
    {
        private readonly IFlowManagementService _flowManagementService;

        public FlowManagementController()
            : this(null)
        {
        }

        public FlowManagementController(IFlowManagementService flowManagementService)
        {
            this._flowManagementService = flowManagementService ?? new FlowManagementService();
        }

        /// <summary>
        /// Method Insert EPA Flow.
        /// </summary>
        /// <param name="model">EPA Flow Model.</param>
        /// <returns>Http Response Message.</returns>
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.FlowManagement, Policy = Policy.Host)]
        public HttpResponseMessage InsertFlow(InsertEPAFlowModel model)
        {
            CommandResult result = null;
            List<string> errorMessage = null;
            if (model.FlowType > 0)
            {
                AddDepartmentFlowCriteria departmentFlowCriteria = new AddDepartmentFlowCriteria()
                {
                    DepartmentId = model.DepartmentId,
                    ModifiedBy = this.CurrentUser.UserId,
                    ModifiedDate = DateTime.Now,
                    FlowDetails = model.FlowDetails
                };
                result = _flowManagementService.AddDepartmentFlow(departmentFlowCriteria);
            }

            if (model.FlowType < 0)
            {
                AddGroupFlowCriteria groupFlowCriteria = new AddGroupFlowCriteria()
                {
                    GroupId = Math.Abs(model.DepartmentId),
                    ModifiedBy = this.CurrentUser.UserId,
                    ModifiedDate = DateTime.Now,
                    FlowDetails = model.FlowDetails
                };
                result = _flowManagementService.AddGroupFlow(groupFlowCriteria);
            }

            if (result.Code != 0)
            {
                errorMessage = new List<string>() { result.Message };
            }

            return Request.CreateResponse(HttpStatusCode.OK, errorMessage);
        }

        /// <summary>
        /// Method add Personal Flow.
        /// </summary>
        /// <param name="model">EPAFlow Model.</param>
        /// <returns>Http Response Message.</returns>
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.FlowManagement, Policy = Policy.Host)]
        public HttpResponseMessage InsertPersonalFlow(InsertEPAFlowModel model)
        {
            List<string> errorMessage = null;
            var addEmployeeFlowCriteria = new AddEmployeeFlowCriteria()
            {
                EmployeeId = model.Personal[0].EmployeeId,
                FlowDetails = model.FlowDetails,
                ModifiedBy = this.CurrentUser.UserId,
                ModifiedDate = DateTime.Now
            };

            CommandResult result = _flowManagementService.AddEmployeeFlow(addEmployeeFlowCriteria);

            if (result.Code != 0)
            {
                errorMessage = new List<string>() { result.Message };
            }

            return Request.CreateResponse(HttpStatusCode.OK, errorMessage);
        }

        /// <summary>
        /// Method Get Flow Information.
        /// </summary>
        /// <param name="departmentId">Department / Group id.</param>
        /// <param name="flowType">Department or Group.</param>
        /// <returns>IEnumerable EPAFlowModel.</returns>
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.FlowManagement, Policy = Policy.Read)]
        public IEnumerable<EPAFlowModel> GetFlow(int departmentId, int flowType)
        {
            IEnumerable<EPAFlowModel> data = null;
            if (flowType > 0)
            {
                var departmentFlowCriteria = new GetDepartmentFlowCriteria() { DepartmentId = departmentId };
                data = _flowManagementService.GetFlowDepartment(departmentFlowCriteria);
            }

            if (flowType < 0)
            {
                var groupFlowCriteria = new GetGroupFlowCriteria() { GroupId = Math.Abs(departmentId) };
                data = _flowManagementService.GetFlowGroup(groupFlowCriteria);
            }

            return data;
        }

        /// <summary>
        /// Get Personal Flow.
        /// </summary>
        /// <param name="employeeId">Employee Id.</param>
        /// <returns>EPA Flow Model.</returns>
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.FlowManagement, Policy = Policy.Read)]
        public IEnumerable<EPAFlowModel> GetPersonalFlow(int employeeId)
        {
            IEnumerable<EPAFlowModel> data = null;
            if (employeeId > 0)
            {
                var getEmployeeFlowCriteria = new GetEmployeeFlowCriteria()
                {
                    EmployeeId = employeeId
                };
                data = _flowManagementService.GetFlowEmployee(getEmployeeFlowCriteria);
            }

            return data;
        }

        /// <summary>
        /// Method Delete Flow.
        /// </summary>
        /// <param name="departmentId">Department id.</param>
        /// <param name="flowType">Flow type.</param>
        /// <returns>Http response message.</returns>
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.FlowManagement, Policy = Policy.Host)]
        public HttpResponseMessage DeleteFlow(int departmentId, int flowType)
        {
            CommandResult result = null;
            List<string> errorMessage = null;
            if (flowType > 0)
            {
                var deleteDepartmentFlowCriteria = new DeleteDepartmentFlowCriteria()
                {
                    DepartmentId = departmentId,
                    ModifiedBy = this.CurrentUser.UserId,
                    ModifiedDate = DateTime.Now
                };
                result = _flowManagementService.DeleteDepartmentFlow(deleteDepartmentFlowCriteria);
            }

            if (flowType < 0)
            {
                var deleteGroupFlowCriteria = new DeleteGroupFlowCriteria()
                {
                    GroupId = departmentId,
                    ModifiedBy = this.CurrentUser.UserId,
                    ModifiedDate = DateTime.Now
                };
                result = _flowManagementService.DeleteGroupFlow(deleteGroupFlowCriteria);
            }

            if (flowType == 0)
            {
                var deleteEmployeeFlowCriteria = new DeleteEmployeeFlowCriteria()
                {
                    EmployeeId = departmentId,
                    ModifiedBy = this.CurrentUser.UserId,
                    ModifiedDate = DateTime.Now
                };

                result = _flowManagementService.DeleteEmployeeFlow(deleteEmployeeFlowCriteria);
            }

            if (result.Code != 0)
            {
                errorMessage = new List<string>() { result.Message };
            }

            return Request.CreateResponse(HttpStatusCode.OK, errorMessage);
        }
    }
}
