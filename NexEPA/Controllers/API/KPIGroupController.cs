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
    using NexEPA.KPI.ViewModel.KPIGroup;
    using NexEPA.Web.Controllers;

    /// <summary>
    /// KPI Group Controller.
    /// </summary>
    public class KPIGroupController : ApiController
    {
        private IKPIGroupServices _kpiGroupServices;

        public KPIGroupController()
            : this(null)
        {
        }

        public KPIGroupController(KPIGroupServices kpiGroupServices)
        {
            _kpiGroupServices = kpiGroupServices ?? new KPIGroupServices();
        }

        [HttpGet]
        public List<KPIGroupItem> GetAll(int? targetTypeId = null)
        {
            var criteria = new GetAllKPIGroupCriteria
            {
                TargetTypeId = targetTypeId
            };
            return _kpiGroupServices.GetAllKPIGroup(criteria);
        }

        [HttpGet]
        public KPIGroupItem GetUserById(int id)
        {
            var criteria = new GetKPIGroupByIdCriteria
            {
                KPIGroupId = id
            };

            return _kpiGroupServices.GetKPIGroupById(criteria);
        }

        public CommandResult Post(InsertKPIGroupCriteria criteria)
        {
            return _kpiGroupServices.InsertKPIGroup(criteria);
        }

        public CommandResult Put(UpdateKPIGroupCriteria criteria)
        {
            return _kpiGroupServices.UpdateKPIGroup(criteria);
        }

        /// <summary>
        /// Delete KPI Group.
        /// </summary>
        /// <param name="id">The KPI Group ID.</param>
        /// <returns>Command Result.</returns>
        public CommandResult Delete(int id)
        {
            var criteria = new DeleteKPIGroupCriteria
            {
                KPIGroupId = id,
                ModifiedDate = DateTime.Now
                //// TODO : criteria.ModifiedBy = current login user
            };

            return _kpiGroupServices.DeleteKPIGroup(criteria);
        }

        public List<KPIGroupItem> GetGroupListByTargetTypeId(int targetTypeId)
        {
            return _kpiGroupServices.GetGroupListByTargetTypeId(targetTypeId);
        }
    }
}
