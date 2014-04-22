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
    using System.Collections.Generic;
    using System.Web.Http;
    using NexEPA.MemberEPA.Criteria;
    using NexEPA.MemberEPA.Models;
    using NexEPA.MemberEPA.Models.MyEPA;
    using NexEPA.MemberEPA.Services.Impl;
    using NexEPA.MemberEPA.Services.Interfaces;
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
        /// Get My EPa.
        /// </summary>
        /// <param name="targetId">Target id.</param>
        /// <returns>EPAInfo Model.</returns>
        public EPAInfoModel GetMyEPA(int targetId)
        {
            EPAInfoModel data = null;
            GetEPAInfoCriteria criteria = new GetEPAInfoCriteria()
            {
                EmployeeId = this.CurrentUser.UserId,
                TargetId = targetId
            };
            
            data = _memberEPAService.GetEPAInfo(criteria);

            return data;
        }

        [HttpGet]
        public List<MemberEPAModel> GetEPAByEmployee(
            int employeeId,
            int? projectId = null,
            int? status = null)
        {
            var criteria = new GetEPAByEmployeeCriteria
            {
                EmployeeId = employeeId,
                ProjectId = projectId,
                Status = status
            };

            return _memberEPAService.GetEPAByEmployee(criteria);
        }
    }
}
