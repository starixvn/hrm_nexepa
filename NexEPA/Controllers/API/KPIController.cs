// -----------------------------------------------------------------------
// <copyright file="KPIController.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.03.28">Created</change>
// </history>

namespace NexEPA.Controllers.API
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using NexEPA.Core.Constants;
    using NexEPA.Core.Enums;
    using NexEPA.Core.Models;
    using NexEPA.KPI.Criteria.KPI;
    using NexEPA.KPI.Services.Implement;
    using NexEPA.KPI.Services.Interface;
    using NexEPA.KPI.ViewModels;
    using NexEPA.Web.Attributes;
    using NexEPA.Web.Controllers;
    using NexEPA.Web.Services.Impl;
    using NexEPA.Web.Services.Interfaces;

    /// <summary>
    /// KPI controller.
    /// </summary>
    public class KPIController : BaseController
    {
        private readonly IKPIService _kpiServices;

        private ICommonService _commonService;

        public KPIController()
            : this(null, null)
        {
        }

        public KPIController(IKPIService kpiServices, ICommonService commonServices)
        {
            _kpiServices = kpiServices ?? new KPIService();
            _commonService = commonServices ?? new CommonService();
        }

        [HttpGet]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageKPI, Policy = Policy.Read)]
        public List<KPIViewModel> ViewKPI(
            int? targetTypeId = null,
            int? groupId = null,
            bool? isDisabled = null)
        {
            var getKPIByGroupIdCriteria = new GetKPICriteria
            {
                TargetTypeId = targetTypeId,
                KPIGroupId = groupId,
                IsDisabled = isDisabled
            };

            return _kpiServices.GetKPI(getKPIByGroupIdCriteria);
        }

        [HttpGet]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageKPI, Policy = Policy.Read)]
        public KPIViewModel GetKPIById(int kpiId)
        {
            var criteria = new GetKPIByIdCriteria
            {
                KPIId = kpiId
            };

            return _kpiServices.GetKPIById(criteria);
        }

        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageKPI, Policy = Policy.Host)]
        public CommandResult Post(InsertKPICriteria criteria)
        {
            if (criteria != null)
            {
                criteria.CreatedBy = this.CurrentUser.UserId;
                criteria.CreatedDate = DateTime.Now;
            }

            return _kpiServices.InsertKPI(criteria);
        }

        [HttpPut]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageKPI, Policy = Policy.Host)]
        public CommandResult Put(UpdateKPICriteria criteria)
        {
            if (criteria != null)
            {
                criteria.ModifiedBy = this.CurrentUser.UserId;
                criteria.ModifiedDate = DateTime.Now;
            }

            return _kpiServices.UpdateKPI(criteria);
        }

        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageKPI, Policy = Policy.Host)]
        public CommandResult Delete(int kpiId)
        {
            var criteria = new DeleteKPICriteria
            {
                KPIId = kpiId,
                ModifiedDate = DateTime.Now,
                ModifiedBy = this.CurrentUser.UserId
            };

            return _kpiServices.DeleteKPI(criteria);
        }

        [HttpPut]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.ManageKPI, Policy = Policy.Host)]
        public CommandResult UpdateKPIStatus(UpdateKPIStatusCriteria criteria)
        {
            if (criteria != null)
            {
                criteria.ModifiedBy = this.CurrentUser.UserId;
                criteria.ModifiedDate = DateTime.Now;
            }

            return _kpiServices.UpdateKPIStatus(criteria);
        }

        /// <summary>
        /// Get permission of user on current function.
        /// </summary>
        /// <returns>The policy.</returns>
        [HttpGet]
        public int GetPermission()
        {
            int policy = _commonService.GetPermission(EPAFunctionKeys.ManageKPI);
            return policy;
        }
    }
}
