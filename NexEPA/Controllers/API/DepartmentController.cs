// --------------------------------------------------------------------------
// <copyright file="DepartmentController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.6.4">Created</change>
// </history>

namespace NexEPA.Controllers.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Mvc;
    using NexEPA.Core.Constants;
    using NexEPA.Core.Enums;
    using NexEPA.Core.Models;
    using NexEPA.Organization.Criteria;
    using NexEPA.Organization.Models;
    using NexEPA.Organization.Services.Implement;
    using NexEPA.Organization.Services.Interface;
    using NexEPA.Web.Attributes;
    using NexEPA.Web.Controllers;

    /// <summary>
    /// Department Controller.
    /// </summary>
    public class DepartmentController : BaseController
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController()
            : this(null)
        {
        }

        public DepartmentController(IDepartmentService departmentService)
        {
            this._departmentService = departmentService ?? new DepartmentService();
        }

        /// <summary>
        /// Method Get All Department.
        /// </summary>
        /// <returns>Tuple List Department.</returns>
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageDepartment, Policy = Policy.Read)] 
        public AllDepartmentInfoModel GetAllDeparment()
        {
            DepartmentModel treeList = null;
            var getAllDepartmentCriteria = new GetAllDepartmentCriteria();
            List<DepartmentModel> departments = _departmentService.GetAllDepartment(getAllDepartmentCriteria);
            if (departments != null)
            {
                treeList = new DepartmentModel();
                treeList.Children.Add(departments.Where(x => x.ParentId == null).SingleOrDefault());
            }

            var allDepartmentInfo = new AllDepartmentInfoModel
            {
                TreeDepartment = treeList,
                AllDepartments = departments
            };

            return allDepartmentInfo;
        }

        /// <summary>
        /// Method Add Department.
        /// </summary>
        /// <param name="model">Department Model.</param>
        /// <returns>Http Response Message.</returns>
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageDepartment, Policy = Policy.Host)] 
        public HttpResponseMessage AddDepartment(DepartmentModel model)
        {
            if (ModelState.IsValid)
            {
                var addDepartmentCriteria = new AddDepartmentCriteria()
                {
                    Code = model.Code,
                    ManagerId = model.Manager.EmployeeId,
                    Name = model.Name,
                    ParentId = model.ParentId,
                    CreatedDate = DateTime.Now,
                    CreatedBy = this.CurrentUser.UserId
                };
                CommandResult result = _departmentService.AddDepartment(addDepartmentCriteria);
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
        /// Method Update Department.
        /// </summary>
        /// <param name="model">Department Model.</param>
        /// <returns>Http Response Message.</returns>
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageDepartment, Policy = Policy.Host)] 
        public HttpResponseMessage UpdateDepartment(DepartmentModel model)
        {
            var updateDepartment = new UpdateDepartmentCriteria()
            {
                Code = model.Code,
                DepartmentId = model.DepartmentId,
                IsDisabled = model.IsDisabled,
                ManagerId = model.Manager.EmployeeId,
                ModifiedBy = this.CurrentUser.UserId,
                ModifiedDate = DateTime.Now,
                Name = model.Name,
                ParentId = model.ParentId
            };

            CommandResult result = _departmentService.UpdateDepartment(updateDepartment);
            if (result.Code != 0)
            {
                var errorMessage = new List<string>() { result.Message };
                return Request.CreateResponse(HttpStatusCode.InternalServerError, errorMessage);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        /// <summary>
        /// Delete Node.
        /// </summary>
        /// <param name="departmentId">Department Id.</param>
        /// <returns>Http Response Message.</returns>
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageDepartment, Policy = Policy.Host)] 
        public HttpResponseMessage Delete(int departmentId)
        {
            var deleteDepartmentCriteria = new DeleteDepartmentCriteria()
            {
                DepartmentId = departmentId,
                ModifiedBy = this.CurrentUser.UserId,
                ModifiedDate = DateTime.Now
            };

            CommandResult result = _departmentService.DeleteDepartment(deleteDepartmentCriteria);
            if (result.Code != 0)
            {
                var errorMessage = new List<string>() { result.Message };
                return Request.CreateResponse(HttpStatusCode.OK, errorMessage);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        private IEnumerable<string> RetrieveErrorMessages()
        {
            IEnumerable<string> errors = ModelState.Values.SelectMany(x => x.Errors.Select(e => e.ErrorMessage));
            return errors;
        }
    }
}
