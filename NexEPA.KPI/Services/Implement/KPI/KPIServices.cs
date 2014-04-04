// -----------------------------------------------------------------------
// <copyright file="KPIServices.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.03.18">Created</change>
// </history>

namespace NexEPA.KPI.Services.Implement.KPI
{
    using System;
    using System.Collections.Generic;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Core.Models;
    using NexEPA.Core.Services;
    using NexEPA.KPI.Criteria.KPI;
    using NexEPA.KPI.Model.KPI;
    using NexEPA.KPI.Repository.Implement.KPI;
    using NexEPA.KPI.Repository.Interface.KPI;
    using NexEPA.KPI.Services.Interface.KPI;
    using NexEPA.KPI.ViewModel.KPI;

    /// <summary>
    /// The implement of KPI service.
    /// </summary>
    public class KPIServices : BaseService, IKPIServices 
    {
        private IKPIRepository _kpiRepository;

        public KPIServices()
        {
            _kpiRepository = new KPIRepository();
        }

        /// <summary>
        /// Get KPI list by group ID.
        /// </summary>
        /// <param name="targetTypeId">The Target Type ID.</param>
        /// <param name="groupId">The group ID.</param>
        /// <returns>The list KPI.</returns>
        public List<KPIItem> GetKPI(int? targetTypeId, int? groupId)
        { 
            var getKPIByGroupIdCriteria = new GetKPICriteria
            {
                TargetTypeId = targetTypeId,
                KPIGroupId = groupId
            };

            var listKPIInfo = (List<KPIInfo>)_kpiRepository.GetKPI(getKPIByGroupIdCriteria);
            var listKPI = new List<KPIItem>();

            foreach (var infoKPI in listKPIInfo)
            {
                var itemKPI = new KPIItem
                {
                    KPIId = infoKPI.KPIId,
                    Name = infoKPI.Name,
                    Description = infoKPI.Description,
                    IsRequireComment = infoKPI.IsRequireComment,
                    KPIGroupId = infoKPI.Group.KPIGroupId,
                    KPIGroupName = infoKPI.Group.Name,
                    TargetTypeId = infoKPI.TargetType.TargetTypeId,
                    TargetTypeName = infoKPI.TargetType.Name
                };

                listKPI.Add(itemKPI);
            }

            return listKPI; 
        }

        /// <summary>
        /// Get KPI by ID.
        /// </summary>
        /// <param name="kpiId">The KPI identity.</param>
        /// <returns>The KPI Item.</returns>
        public KPIItem GetKPIById(int kpiId)
        {
            var criteria = new GetKPIByIdCriteria
            {
                KPIId = kpiId
            };

            KPIInfo infoKPI = _kpiRepository.GetKPIById(criteria);

            var kpiItem = new KPIItem
            {
                KPIId = infoKPI.KPIId,
                Name = infoKPI.Name,
                Description = infoKPI.Description,
                IsRequireComment = infoKPI.IsRequireComment,
                KPIGroupId = infoKPI.Group.KPIGroupId,
                KPIGroupName = infoKPI.Group.Name,
                TargetTypeId = infoKPI.TargetType.TargetTypeId,
                TargetTypeName = infoKPI.TargetType.Name
            };

            return kpiItem;
        }

        /// <summary>
        /// Insert new KPI.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Command Result.</returns>
        public CommandResult InsertKPI(InsertKPICriteria criteria)
        {
            criteria.CreatedBy = SessionManager.CurrentUser.UserId;
            criteria.CreatedDate = DateTime.Now;
            return _Repository.Insert(criteria);
        }

        /// <summary>
        /// Edit the KPI.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Command Result.</returns>
        public CommandResult UpdateKPI(UpdateKPICriteria criteria)
        {
            criteria.ModifiedBy = SessionManager.CurrentUser.UserId;
            criteria.ModifiedDate = DateTime.Now;
            return _Repository.Update(criteria);
        }

        public CommandResult DeleteKPI(DeleteKPICriteria criteria)
        {
            return _Repository.Delete(criteria);
        }
    }
}
