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
    using Fanex.NAP.Framework.Data;
    using NexEPA.KPI.Criteria.KPI;
    using NexEPA.KPI.Services.Implement.KPI;
    using NexEPA.KPI.Services.Interface.KPI;
    using NexEPA.KPI.ViewModels;
    using NexEPA.Web.Attributes;
    using NexEPA.Web.Controllers;

    /// <summary>
    /// KPI controller.
    /// </summary>
    public class KPIController : BaseController
    {
        private readonly IKPIService _kpiServices;

        public KPIController()
            : this(null)
        {
        }

        public KPIController(IKPIService kpiServices)
        {
            _kpiServices = kpiServices ?? new KPIService();
        }

        [HttpGet]
        [ApiAuthorize(Roles = "SA")]
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
        [ApiAuthorize(Roles = "SA")]
        public KPIViewModel GetKPIById(int kpiId)
        {
            var criteria = new GetKPIByIdCriteria
            {
                KPIId = kpiId
            };

            return _kpiServices.GetKPIById(criteria);
        }

        [ApiAuthorize(Roles = "SA")]
        public CommandResult Post(InsertKPICriteria criteria)
        {
            if (criteria != null)
            {
                criteria.CreatedBy = this.CurrentUser.UserId;
                criteria.CreatedDate = DateTime.Now;
            }

            return _kpiServices.InsertKPI(criteria);
        }

        [ApiAuthorize(Roles = "SA")]
        [HttpPut]
        public CommandResult Put(UpdateKPICriteria criteria)
        {
            if (criteria != null)
            {
                criteria.ModifiedBy = this.CurrentUser.UserId;
                criteria.ModifiedDate = DateTime.Now;
            }

            return _kpiServices.UpdateKPI(criteria);
        }

        [ApiAuthorize(Roles = "SA")]
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

        [ApiAuthorize(Roles = "SA")]
        [HttpPut]
        public CommandResult UpdateKPIStatus(UpdateKPIStatusCriteria criteria)
        {
            if (criteria != null)
            {
                criteria.ModifiedBy = this.CurrentUser.UserId;
                criteria.ModifiedDate = DateTime.Now;
            }

            return _kpiServices.UpdateKPIStatus(criteria);
        }
    }
}
