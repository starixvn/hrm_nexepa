// -----------------------------------------------------------------------
// <copyright file="KPIGroupService.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.03.18">Created</change>
// </history>

namespace NexEPA.KPI.Services.Implement.KPIGroup
{
    using System;
    using System.Collections.Generic;
    using Fanex.Common.Helper;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Core.Services;
    using NexEPA.KPI.Criteria.KPIGroup;
    using NexEPA.KPI.Model;
    using NexEPA.KPI.Repositories.Implement;
    using NexEPA.KPI.Repositories.Interface.KPIGroup;
    using NexEPA.KPI.Services.Interface.KPIGroup;
    using NexEPA.KPI.ViewModels;
    using NexEPA.Web.Models;

    /// <summary>
    /// The implement of KPI services.
    /// </summary>
    public class KPIGroupService : BaseService, IKPIGroupService
    {
        private IKPIGroupRepository _kpiGroupRepository;

        public KPIGroupService(IKPIGroupRepository kpiGroupRepository = null)
        {
            _kpiGroupRepository = kpiGroupRepository ?? new KPIGroupRepository();
        }

        /// <summary>
        /// Get all KPI group.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The list KPIGroupItem.</returns>
        public List<KPIGroupItem> GetAllKPIGroup(GetAllKPIGroupCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var groupList = new List<KPIGroupItem>();

            if (criteria.IsValid())
            {
                IEnumerable<KPIGroupInfo> groupInfoList = _kpiGroupRepository.GetAllKPIGroup(criteria);

                foreach (KPIGroupInfo group in groupInfoList)
                {
                    var groupItem = new KPIGroupItem
                    {
                        KPIGroupID = group.KPIGroupId,
                        Name = group.Name,
                        Description = group.Description,
                        TargetTypeId = group.TargetType.TargetTypeId,
                        Form = group.TargetType.Name,
                        IsDisabled = group.IsDisabled,
                        NumberKPI = group.NumberKPI
                    };

                    groupList.Add(groupItem);
                }
            }

            return groupList;
        }

        /// <summary>
        /// Get KPI Group by id.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The KPI Group Item.</returns>
        public KPIGroupItem GetKPIGroupById(GetKPIGroupByIdCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var kpiGroupItem = new KPIGroupItem();

            if (criteria.IsValid())
            {
                KPIGroupInfo groupInfo = _kpiGroupRepository.GetKPIGroupById(criteria);

                if (groupInfo != null)
                {
                    kpiGroupItem.KPIGroupID = groupInfo.KPIGroupId;
                    kpiGroupItem.Name = groupInfo.Name;
                    kpiGroupItem.Description = groupInfo.Description;
                    kpiGroupItem.Weight = groupInfo.Weight;
                    kpiGroupItem.TargetTypeId = groupInfo.TargetType.TargetTypeId;
                    kpiGroupItem.Form = groupInfo.TargetType.Name;
                    kpiGroupItem.IsDisabled = groupInfo.IsDisabled;
                    kpiGroupItem.NumberKPI = groupInfo.NumberKPI;
                }
            }

            return kpiGroupItem;
        }

        /// <summary>
        /// Insert KPI group.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The error code and error message.</returns>
        public CommandResult InsertKPIGroup(InsertKPIGroupCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var commandResult = new CommandResult();

            if (criteria.IsValid())
            {
                criteria.CreatedBy = SessionManager.CurrentUser.UserId;
                criteria.CreatedDate = DateTime.Now;
                commandResult = _Repository.Insert(criteria);
            }

            return commandResult;
        }

        /// <summary>
        /// Update KPI group.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The error code and error message.</returns>
        public CommandResult UpdateKPIGroup(UpdateKPIGroupCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var commandResult = new CommandResult();

            if (criteria.IsValid())
            {
                criteria.ModifiedBy = SessionManager.CurrentUser.UserId;
                criteria.ModifiedDate = DateTime.Now;
                commandResult = _Repository.Update(criteria);
            }

            return commandResult;
        }

        /// <summary>
        /// Delete KPI Group.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Command Result.</returns>
        public CommandResult DeleteKPIGroup(DeleteKPIGroupCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var commandResult = new CommandResult();

            if (criteria.IsValid())
            {
                commandResult = _Repository.Delete(criteria);
            }

            return commandResult;
        }

        /// <summary>
        ///  Get list Group by Target Type ID.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>List KPIGroupItem.</returns>
        public List<KPIGroupItem> GetGroupListByTargetTypeId(GetAllKPIGroupCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var allGroup = new List<KPIGroupItem>();

            if (criteria.IsValid())
            {
                allGroup = this.GetAllKPIGroup(criteria);
            }

            return allGroup;
        }

        /// <summary>
        /// Update KPI Group Status.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Command Result.</returns>
        public CommandResult UpdateKPIGroupStatus(UpdateKPIGroupStatusCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var commandResult = new CommandResult();

            if (criteria.IsValid())
            {
                criteria.ModifiedBy = SessionManager.CurrentUser.UserId;
                criteria.ModifiedDate = DateTime.Now;
                commandResult = _Repository.Update(criteria);
            }

            return commandResult;
        }
    }
}