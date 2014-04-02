// -----------------------------------------------------------------------
// <copyright file="KPIGroupServices.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.03.17">Created</change>
// </history>

namespace NexEPA.KPI.Services.Implement.KPIGroup
{
    using System;
    using System.Collections.Generic;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Core.Services;
    using NexEPA.KPI.Criteria.KPIGroup;
    using NexEPA.KPI.Model.KPIGroup;
    using NexEPA.KPI.Repository.Implement.KPIGroup;
    using NexEPA.KPI.Repository.Interface.KPIGroup;
    using NexEPA.KPI.Services.Implement.KPI;
    using NexEPA.KPI.Services.Interface.KPI;
    using NexEPA.KPI.Services.Interface.KPIGroup;
    using NexEPA.KPI.ViewModel.KPIGroup;

    /// <summary>
    /// The implement of KPI services.
    /// </summary>
    public class KPIGroupServices : BaseService, IKPIGroupServices
    {
        private IKPIGroupRepository _kpiGroupRepository;

        public KPIGroupServices()
        {
            _kpiGroupRepository = new KPIGroupRepository();
        }

        /// <summary>
        /// Get all KPI group.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The list KPIGroupItem.</returns>
        public List<KPIGroupItem> GetAllKPIGroup(GetAllKPIGroupCriteria criteria)
        { 
            var groupList = new List<KPIGroupItem>();

            if (criteria.IsValid())
            {
                IList<KPIGroupInfo> groupInfoList = _kpiGroupRepository.GetAllKPIGroup(criteria);

                foreach (var group in groupInfoList)
                {
                    var groupItem = new KPIGroupItem
                    {
                        KPIGroupID = group.KPIGroupId,
                        Name = group.Name,
                        Description = group.Description,
                        Weight = group.Weight,
                        TargetTypeId = group.TargetType.TargetTypeId,
                        Form = group.TargetType.Name,
                        IsDisabled = group.IsDisabled,
                        NumberKPI = group.NumberKPI
                    };

                    groupList.Add(groupItem);
                }

                ////TODO : save all group in session
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
            //// TODO : criteria.CreatedBy = current user login
            criteria.CreatedDate = DateTime.Now;
            return _Repository.Insert(criteria);
        }

        /// <summary>
        /// Update KPI group.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The error code and error message.</returns>
        public CommandResult UpdateKPIGroup(UpdateKPIGroupCriteria criteria)
        {
            //// TODO : criteria.ModifiedBy = current user login
            criteria.ModifiedDate = DateTime.Now;
            return _Repository.Update(criteria);
        }

        public CommandResult DeleteKPIGroup(DeleteKPIGroupCriteria criteria) 
        {
            return _Repository.Delete(criteria);
        }

        public List<KPIGroupItem> GetGroupListByTargetTypeId(int targetTypeId)
        {
            //// TODO : get all group from session
            var criteria = new GetAllKPIGroupCriteria();
            List<KPIGroupItem> allGroup = this.GetAllKPIGroup(criteria);
            return allGroup.FindAll(s => s.TargetTypeId == targetTypeId);
        }
    }
}
