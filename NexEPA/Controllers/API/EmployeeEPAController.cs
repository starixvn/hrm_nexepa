// --------------------------------------------------------------------------
// <copyright file="EmployeeEPAController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.4.21">Created</change>
//   <change who="Josh.Phan" date="2014.4.22">Added - GetEPAByEmployee</change>
// </history>

namespace NexEPA.Controllers.API
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Web.Http;
    using Fanex.NAP.Framework.Data;
    using NexEPA.MemberEPA.Criteria;
    using NexEPA.MemberEPA.Models;
    using NexEPA.MemberEPA.Models.MyEPA;
    using NexEPA.MemberEPA.Services.Implement;
    using NexEPA.MemberEPA.Services.Interface;
    using NexEPA.Web.Attributes;
    using NexEPA.Web.Controllers;

    /// <summary>
    /// MyEPA Controller.
    /// </summary>
    public class EmployeeEPAController : BaseController
    {
        private readonly IMemberEPAService _memberEPAService;

        public EmployeeEPAController()
            : this(null)
        {
        }

        public EmployeeEPAController(IMemberEPAService memberEPAService)
        {
            this._memberEPAService = memberEPAService ?? new MemberEPAService();
        }

        /// <summary>
        /// Add employee score.
        /// </summary>
        /// <param name="model">EPA Info Model.</param>
        /// <returns>Http Response.</returns>
        [ApiAuthorize(Roles = "PM,SA")]
        [HttpPost]
        public HttpResponseMessage AddEmployeeEPA(EPAInfoModel model)
        {
            var criteria = new AddOrUpdateEPAScoreCriteria()
            {
                TargetId = model.TargetId,
                EmployeeId = model.EmployeeId,
                EvaluatorId = model.ProjectManagerId,
                Comment = model.Comment,
                Score = model.FinalScore,
                KPIs = model.ConvertKPIInfoToXML,
                MarkedBy = this.CurrentUser.UserId,
                MarkedDate = DateTime.Now
            };

            CommandResult result = _memberEPAService.AddOrUpdateEmployeeEPA(criteria);
            if (result.Code != 0)
            {
                var errorMessage = new List<string>() { result.Message };
                return Request.CreateResponse(HttpStatusCode.InternalServerError, errorMessage);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        /// <summary>
        /// Get Current User's EPA.
        /// </summary>
        /// <param name="targetId">Target id.</param>
        /// <param name="employeeId">Employee id.</param>
        /// <returns>Http Response Message.</returns>
        public HttpResponseMessage GetMyEPA(int targetId, int employeeId)
        {
            int currentUserId = this.CurrentUser.UserId;
            EPAInfoModel epaInfo = null;
            if (currentUserId == employeeId)
            {
                epaInfo = GetEPAInformation(targetId, employeeId);
                if (epaInfo != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, epaInfo);
                }

                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.MethodNotAllowed, epaInfo);
            }
        }

        /// <summary>
        /// Method Get Employee EPA.
        /// </summary>
        /// <param name="targetId">Target Id.</param>
        /// <param name="employeeId">Employee Id.</param>
        /// <returns>Http Response Message.</returns>
        [ApiAuthorize(Roles = "PM,SA,Leader")]
        public HttpResponseMessage GetEmployeeEPA(int targetId, int employeeId)
        {
            EPAInfoModel epaInfo = null;
            var checkPermissionCriteria = new GetEmployeePermissionCriteria()
            {
                ActorUserId = this.CurrentUser.UserId,
                EmployeeId = employeeId,
                TargetId = targetId
            };
            int permission = _memberEPAService.CheckEmployeePermission(checkPermissionCriteria);

            switch (permission)
            {
                case 0:
                    return Request.CreateResponse(HttpStatusCode.MethodNotAllowed);
                default:
                    epaInfo = GetEPAInformation(targetId, employeeId);
                    if (epaInfo != null)
                    {
                        epaInfo.Permission = permission;
                    }
                    
                    return Request.CreateResponse(HttpStatusCode.OK, epaInfo);
            }
        }

        [HttpGet]
        public List<MemberEPAModel> GetEPAOfCurrentMember(int? projectId = null, int? status = null)
        {
            var criteria = new GetEPAByEmployeeCriteria
            {
                EmployeeId = this.CurrentUser.UserId,
                ProjectId = projectId,
                Status = status
            };

            return _memberEPAService.GetEPAByEmployee(criteria);
        }

        /// <summary>
        /// Get EPA of employees by manager.
        /// </summary>
        /// <param name="targetId">The target id.</param>
        /// <param name="projectId">The project id.</param>
        /// <param name="status">The status.</param>
        /// <param name="employeeId">The employee id.</param>
        /// <returns>The list Member EPA Model.</returns>
        [ApiAuthorize(Roles = "PM,SA,Leader")]
        [HttpGet]
        public List<MemberEPAModel> GetEPAOfEmployeeByManager(
            int? targetId = null,
            int? projectId = null,
            int? status = null,
            int? employeeId = null)
        {
            var criteria = new GetEPAOfAllEmployeeByManagerCriteria
            {
                ActorUserId = this.CurrentUser.UserId,
                EmployeeId = employeeId,
                TargetId = targetId,
                ProjectId = projectId,
                Status = status
            };

            return _memberEPAService.GetEPAOfEmployeeByManager(criteria);
        }

        /// <summary>
        /// Update EPA Status.
        /// </summary>
        /// <param name="model">EPA Information Model.</param>
        /// <returns>Http Response Message.</returns>
        public HttpResponseMessage UpdateEPAStatus(EPAInfoModel model)
        {
            var updateEPAStatusCriteria = new UpdateEPAStatusCriteria()
            {
                EmployeeId = model.EmployeeId,
                Status = model.Status,
                TargetId = model.TargetId,
                ModifiedBy = this.CurrentUser.UserId,
                ModifiedDate = DateTime.Now
            };

            CommandResult result = _memberEPAService.UpdateEPAStatus(updateEPAStatusCriteria);
            if (result.Code != 0)
            {
                var warningMessage = new List<string>() { result.Message };
                return Request.CreateResponse(HttpStatusCode.OK, warningMessage);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        /// <summary>
        /// Method Get EPAInfo.
        /// </summary>
        /// <param name="targetId">Target Id.</param>
        /// <param name="employeeId">Employee Id.</param>
        /// <returns>EPAInfo Model.</returns>
        private EPAInfoModel GetEPAInformation(int targetId, int employeeId)
        {
            EPAInfoModel data = null;
            var getEPAInfoCriteria = new GetEPAInfoCriteria()
            {
                EmployeeId = employeeId,
                TargetId = targetId
            };

            data = _memberEPAService.GetEPAInfo(getEPAInfoCriteria);

            return data;
        }
    }
}
