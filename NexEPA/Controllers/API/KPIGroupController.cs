 // -----------------------------------------------------------------------
// <copyright file="KPIGroupController.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.03.20">Created</change>
// </history>

namespace NexEPA.Controllers.API
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Core.Services;
    using NexEPA.Core.Services.Interface;
    using NexEPA.KPI.Criteria.KPIGroup;
    using NexEPA.KPI.Services.Implement;
    using NexEPA.KPI.Services.Interface;
    using NexEPA.KPI.ViewModels;
    using NexEPA.Web.Attributes;
    using NexEPA.Web.Controllers;

    /// <summary>
    /// KPI Group Controller.
    /// </summary>
    public class KPIGroupController : BaseController
    {
        private IKPIGroupService _kpiGroupServices;

        private IEPAService _epaService;

        public KPIGroupController()
            : this(null, null)
        {
        }

        public KPIGroupController(IKPIGroupService kpiGroupService, IEPAService epaService)
        {
            _kpiGroupServices = kpiGroupService ?? new KPIGroupService();
            _epaService = epaService ?? new EPAService();
        }

        [HttpGet]
        [ApiAuthorize(Roles = "SA")]
        public List<KPIGroupViewModel> GetAll(int? targetTypeId = null, bool? isDisabled = null)
        {
            var criteria = new GetAllKPIGroupCriteria
            {
                TargetTypeId = targetTypeId,
                IsDisabled = isDisabled
            };

            return _kpiGroupServices.GetAllKPIGroup(criteria);
        }

        [HttpGet]
        [ApiAuthorize(Roles = "SA")]
        public KPIGroupViewModel GetGroupById(int id)
        {
            var criteria = new GetKPIGroupByIdCriteria
            {
                KPIGroupId = id
            };

            return _kpiGroupServices.GetKPIGroupById(criteria);
        }

        [ApiAuthorize(Roles = "SA")]
        public CommandResult Post(InsertKPIGroupCriteria criteria)
        {
            if (criteria != null)
            {
                criteria.CreatedBy = this.CurrentUser.UserId;
                criteria.CreatedDate = DateTime.Now;
            }

            return _kpiGroupServices.InsertKPIGroup(criteria);
        }

        [ApiAuthorize(Roles = "SA")]
        [HttpPut]
        public CommandResult Put(UpdateKPIGroupCriteria criteria)
        {
            if (criteria != null)
            {
                criteria.ModifiedBy = this.CurrentUser.UserId;
                criteria.ModifiedDate = DateTime.Now;
            }

            return _kpiGroupServices.UpdateKPIGroup(criteria);
        }

        /// <summary>
        /// Delete KPI Group.
        /// </summary>
        /// <param name="id">The KPI Group ID.</param>
        /// <returns>Command Result.</returns>
        [ApiAuthorize(Roles = "SA")]
        public CommandResult Delete(int id)
        {
            var criteria = new DeleteKPIGroupCriteria
            {
                KPIGroupId = id,
                ModifiedDate = DateTime.Now,
                ModifiedBy = this.CurrentUser.UserId
            };

            return _kpiGroupServices.DeleteKPIGroup(criteria);
        }

        public List<KPIGroupViewModel> GetGroupListByTargetTypeId(int? targetTypeId = null)
        {
            var criteria = new GetAllKPIGroupCriteria
            {
                TargetTypeId = targetTypeId
            };

            return _kpiGroupServices.GetGroupListByTargetTypeId(criteria);
        }

        [HttpPut]
        public CommandResult UpdateKPIGroupStatus(UpdateKPIGroupStatusCriteria criteria)
        {
            if (criteria != null)
            {
                criteria.ModifiedBy = this.CurrentUser.UserId;
                criteria.ModifiedDate = DateTime.Now;
            }

            return _kpiGroupServices.UpdateKPIGroupStatus(criteria);
        }
    }
}
