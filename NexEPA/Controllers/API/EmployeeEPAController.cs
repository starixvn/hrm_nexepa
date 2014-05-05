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
    using NexEPA.MemberEPA.Services.Impl;
    using NexEPA.MemberEPA.Services.Interfaces;
    using NexEPA.Web.Attributes;
    using NexEPA.Web.Controllers;

    /// <summary>
    /// MyEPA Controller.
    /// </summary>
    public class EmployeeEPAController : BaseController
    {
        private const string _EmployeeEPAScore = "<KPI KPIId='{0}' KPIGroupId='{1}' ScoreId='{2}' Comment='{3}'></KPI>";

        private const string _BeginRootTag = "<ROOT>";

        private const string _EndRootTag = "</ROOT>";

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
        public HttpResponseMessage Post(EPAInfoModel model)
        {
            AddOrUpdateEPAScore criteria = new AddOrUpdateEPAScore()
            {
                TargetId = model.TargetId,
                EmployeeId = model.EmployeeId,
                EvaluatorId = model.ProjectManagerId,
                Comment = model.Comment,
                Score = model.FinalScore,
                KPIs = ConvertToXML(model.KPIInfo),
                MarkedBy = this.CurrentUser.UserId,
                MarkedDate = DateTime.Now
            };

            CommandResult result = _memberEPAService.AddOrUpdateEmployeeEPA(criteria);
            if (result.Code != 0)
            {
                List<string> errorMessage = new List<string>() { result.Message };
                return Request.CreateResponse(HttpStatusCode.InternalServerError, errorMessage);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        public EPAInfoModel GetMyEPA(int targetId, int employeeId)
        {
            EPAInfoModel epaInfo = GetEPAInformation(targetId, employeeId);
            return epaInfo;
        }

        [ApiAuthorize(Roles = "PM,SA")]
        public EPAInfoModel GetEmployeeEPA(int targetId, int employeeId)
        {
            EPAInfoModel epaInfo = GetEPAInformation(targetId, employeeId);
            return epaInfo;
        }

        [HttpGet]
        public List<MemberEPAModel> GetEPAOfCurrentMember(
            int? projectId = null,
            int? status = null)
        {
            var criteria = new GetEPAByEmployeeCriteria
            {
                EmployeeId = this.CurrentUser.UserId,
                ProjectId = projectId,
                Status = status
            };

            return _memberEPAService.GetEPAByEmployee(criteria);
        }

        [HttpGet]
        public List<MemberEPAModel> GetEPAOfEmployeeByManager(
            int? projectId = null,
            int? status = null)
        {
            var criteria = new GetEPAOfAllEmployeeByManagerCriteria
            {
                EmployeeId = this.CurrentUser.UserId,
                ProjectId = projectId,
                Status = status
            };

            return _memberEPAService.GetEPAOfEmployeeByManager(criteria);
        }

        /// <summary>
        /// Method convert data to XML.
        /// </summary>
        /// <param name="kpis">List GroupingKPI model.</param>
        /// <returns>XML format.</returns>
        private string ConvertToXML(List<GroupingKPIModel> kpis)
        {
            var value = new StringBuilder(_BeginRootTag);
            foreach (var kpi in kpis)
            {
                foreach (var item in kpi.KPIGroupItems)
                {
                    value.AppendFormat(_EmployeeEPAScore, item.KPIId, item.Group.KPIGroupId, item.Score, item.Comment);
                }
            }

            return value.Append(_EndRootTag).ToString();
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
            GetEPAInfoCriteria criteria = new GetEPAInfoCriteria()
            {
                EmployeeId = employeeId,
                TargetId = targetId
            };

            data = _memberEPAService.GetEPAInfo(criteria);

            return data;
        }
    }
}
