// -----------------------------------------------------------------------
// <copyright file="KPIGroupService.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.03.18">Created</change>
// </history>

namespace NexEPA.KPI.Services.Implement
{
    using System.Collections.Generic;
    using Fanex.Common.Helper;
    using NexEPA.Core.Models;
    using NexEPA.Core.Services;
    using NexEPA.KPI.Criteria.KPIGroup;
    using NexEPA.KPI.Model;
    using NexEPA.KPI.Repositories.Implement;
    using NexEPA.KPI.Repositories.Interface;
    using NexEPA.KPI.Services.Interface;
    using NexEPA.KPI.ViewModels;

    /// <summary>
    /// The implement of KPI service.
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
        /// <returns>The list KPI Group.</returns>
        public List<KPIGroupViewModel> GetAllKPIGroup(GetAllKPIGroupCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var groupList = new List<KPIGroupViewModel>();

            if (criteria.IsValid())
            {
                IEnumerable<KPIGroupModel> groupInfoList = _kpiGroupRepository.GetAllKPIGroup(criteria);

                if (groupInfoList != null)
                {
                    foreach (KPIGroupModel group in groupInfoList)
                    {
                        var groupItem = new KPIGroupViewModel
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
            }

            return groupList;
        }

        /// <summary>
        /// Get KPI Group by id.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The KPI Group Item.</returns>
        public KPIGroupViewModel GetKPIGroupById(GetKPIGroupByIdCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var kpiGroupItem = new KPIGroupViewModel();

            if (criteria.IsValid())
            {
                KPIGroupModel groupInfo = _kpiGroupRepository.GetKPIGroupById(criteria);

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
                commandResult = Repository.Insert(criteria);
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
                commandResult = Repository.Update(criteria);
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
                commandResult = Repository.Delete(criteria);
            }

            return commandResult;
        }

        /// <summary>
        ///  Get list Group by Target Type ID.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>List KPI Group.</returns>
        public List<KPIGroupViewModel> GetGroupListByTargetTypeId(GetAllKPIGroupCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var allGroup = new List<KPIGroupViewModel>();

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
                commandResult = Repository.Update(criteria);
            }

            return commandResult;
        }
    }
}