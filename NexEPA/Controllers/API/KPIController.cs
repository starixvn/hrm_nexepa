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
    using NexEPA.KPI.ViewModel.KPI;
    using NexEPA.Web.Attributes;
    using NexEPA.Web.Controllers;

    /// <summary>
    /// KPI controller.
    /// </summary>
    public class KPIController : BaseController
    {
        private IKPIServices _kpiServices;

        public KPIController()
            : this(null)
        {
        }

        public KPIController(KPIServices kpiServices)
        {
            _kpiServices = kpiServices ?? new KPIServices();
        }

        [HttpGet]
        [ApiAuthorize(Roles = "SA")]
        public List<KPIItem> ViewKPI(int? targetTypeId = null, int? groupId = null)
        {
            var getKPIByGroupIdCriteria = new GetKPICriteria
            {
                TargetTypeId = targetTypeId,
                KPIGroupId = groupId
            };

            return _kpiServices.GetKPI(getKPIByGroupIdCriteria);
        }

        [HttpGet]
        [ApiAuthorize(Roles = "SA")]
        public KPIItem GetKPIById(int kpiId)
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
            return _kpiServices.InsertKPI(criteria);
        }

        [ApiAuthorize(Roles = "SA")]
        public CommandResult Put(UpdateKPICriteria criteria)
        {
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
    }
}
