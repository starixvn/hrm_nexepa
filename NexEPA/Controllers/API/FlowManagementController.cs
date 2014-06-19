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
    using Fanex.NAP.Framework.Data;
    using NexEPA.Core.Constants;
    using NexEPA.Core.Enums;
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
                    GroupId = model.DepartmentId,
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
                GetDepartmentFlowCriteria departmentFlowCriteria = new GetDepartmentFlowCriteria() { DepartmentId = departmentId };
                data = _flowManagementService.GetFlowDepartment(departmentFlowCriteria);
            }

            if (flowType < 0)
            {
                GetGroupFlowCriteria groupFlowCriteria = new GetGroupFlowCriteria() { GroupId = departmentId };
                data = _flowManagementService.GetFlowGroup(groupFlowCriteria);
            }

            return data;
        }

        /// <summary>
        /// Method Delete Flow.
        /// </summary>
        /// <param name="departmentId">Department id.</param>
        /// <param name="flowType">Flow type.</param>
        /// <returns>Http response message.</returns>
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

            if (result.Code != 0)
            {
                errorMessage = new List<string>() { result.Message };
            }

            return Request.CreateResponse(HttpStatusCode.OK, errorMessage);
        }

        private IEnumerable<string> RetrieveErrorMessages()
        {
            IEnumerable<string> errors = ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage));
            return errors;
        }
    }
}
