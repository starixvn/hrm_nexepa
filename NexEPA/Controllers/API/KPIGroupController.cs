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
    using NexEPA.KPI.Criteria.KPIGroup;
    using NexEPA.KPI.Services.Implement.KPIGroup;
    using NexEPA.KPI.Services.Interface.KPIGroup;
    using NexEPA.KPI.ViewModels;
    using NexEPA.Web.Attributes;
    using NexEPA.Web.Controllers;

    /// <summary>
    /// KPI Group Controller.
    /// </summary>
    public class KPIGroupController : BaseController
    {
        private IKPIGroupService _kpiGroupServices;

        public KPIGroupController()
            : this(null)
        {
        }

        public KPIGroupController(KPIGroupService kpiGroupServices)
        {
            _kpiGroupServices = kpiGroupServices ?? new KPIGroupService();
        }

        [HttpGet]
        [ApiAuthorize(Roles = "SA")]
        public List<KPIGroupItem> GetAll(int? targetTypeId = null)
        {
            var criteria = new GetAllKPIGroupCriteria
            {
                TargetTypeId = targetTypeId
            };

            return _kpiGroupServices.GetAllKPIGroup(criteria);
        }

        [HttpGet]
        [ApiAuthorize(Roles = "SA")]
        public KPIGroupItem GetUserById(int id)
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
            return _kpiGroupServices.InsertKPIGroup(criteria);
        }

        [ApiAuthorize(Roles = "SA")]
        [HttpPut]
        public CommandResult Put(UpdateKPIGroupCriteria criteria)
        {
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

        public List<KPIGroupItem> GetGroupListByTargetTypeId(int targetTypeId)
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
            return _kpiGroupServices.UpdateKPIGroupStatus(criteria);
        }
    }
}
