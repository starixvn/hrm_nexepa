// -----------------------------------------------------------------------
// <copyright file="KPIService.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.03.18">Created</change>
// </history>

namespace NexEPA.KPI.Services.Implement.KPI
{
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Common.Helper;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Core.Services;
    using NexEPA.KPI.Criteria.KPI;
    using NexEPA.KPI.Model;
    using NexEPA.KPI.Repositories.Implement;
    using NexEPA.KPI.Repositories.Interface;
    using NexEPA.KPI.Services.Interface;
    using NexEPA.KPI.ViewModels;

    /// <summary>
    /// The implement of KPI service.
    /// </summary>
    public class KPIService : BaseService, IKPIService 
    {
        private IKPIRepository _kpiRepository;

        public KPIService(IKPIRepository kpiRepository = null)
        {
            _kpiRepository = kpiRepository ?? new KPIRepository();
        }

        /// <summary>
        /// Get KPI list by group ID.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The list KPI.</returns>
        public List<KPIViewModel> GetKPI(GetKPICriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var listKPI = new List<KPIViewModel>();

            if (criteria.IsValid())
            {
                var listKPIInfo = (List<KPIModel>)_kpiRepository.GetKPI(criteria);

                listKPI = GroupKPIGroupsSameKPI(listKPI, listKPIInfo);
            }

            return listKPI; 
        }

        /// <summary>
        /// Get KPI by ID.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The KPI Item.</returns>
        public KPIViewModel GetKPIById(GetKPIByIdCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var kpiItem = new KPIViewModel();

            if (criteria.IsValid())
            {
                var infoKPIs = (List<KPIModel>)_kpiRepository.GetKPIById(criteria);

                if (infoKPIs != null && infoKPIs.Count > 0)
                {
                    var kpiGroupIds = new List<int>();
                    var kpiGroupNames = new List<string>();

                    foreach (KPIModel infoKPI in infoKPIs)
                    {
                        kpiGroupIds.Add(infoKPI.Group.KPIGroupId);
                        kpiGroupNames.Add(infoKPI.Group.Name);
                    }

                    kpiItem = new KPIViewModel
                    {
                        KPIId = infoKPIs[0].KPIId,
                        Name = infoKPIs[0].Name,
                        Description = infoKPIs[0].Description,
                        IsRequireComment = infoKPIs[0].IsRequireComment,
                        IsDisabled = infoKPIs[0].IsDisabled,
                        KPIGroupIds = kpiGroupIds,
                        KPIGroupNames = kpiGroupNames,
                        TargetTypeId = infoKPIs[0].TargetType.TargetTypeId,
                        TargetTypeName = infoKPIs[0].TargetType.Name
                    };
                }
            }

            return kpiItem;
        }

        /// <summary>
        /// Insert new KPI.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Command Result.</returns>
        public CommandResult InsertKPI(InsertKPICriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var commandResult = new CommandResult();

            if (criteria.IsValid())
            {
                commandResult = _Repository.Insert(criteria);
            }

            return commandResult;
        }

        /// <summary>
        /// Edit the KPI.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Command Result.</returns>
        public CommandResult UpdateKPI(UpdateKPICriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var commandResult = new CommandResult();

            if (criteria.IsValid())
            {
                commandResult = _Repository.Update(criteria);
            }

            return commandResult;
        }

        /// <summary>
        /// Delete KPI.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Command Result.</returns>
        public CommandResult DeleteKPI(DeleteKPICriteria criteria)
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
        /// Get All KPI By KPIGroup.
        /// </summary>
        /// <param name="criteria">GetAllKPIByKPIGroup Criteria.</param>
        /// <returns>KPIItem items.</returns>
        public IEnumerable<KPIViewModel> GetAllKPIByKPIGroup(GetAllKPIByKPIGroupCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            IEnumerable<KPIViewModel> kpis = null;
            if (criteria.IsValid())
            {
                kpis = GetData<KPIViewModel>(criteria);
            }

            return kpis;
        }

        /// <summary>
        /// Update status of KPI.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Command Result.</returns>
        public CommandResult UpdateKPIStatus(UpdateKPIStatusCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var commandResult = new CommandResult();

            if (criteria.IsValid())
            {
                commandResult = _Repository.Update(criteria);
            }

            return commandResult;
        }

        /// <summary>
        /// Group KPIGroup that they have same KPI.
        /// </summary>
        /// <param name="listKPI">The list KPIs after group.</param>
        /// <param name="listKPIInfo">The list KPIs before group.</param>
        /// <returns>The list KPI.</returns>
        private List<KPIViewModel> GroupKPIGroupsSameKPI(List<KPIViewModel> listKPI, List<KPIModel> listKPIInfo)
        {
            for (int i = 0; i < listKPIInfo.Count; i++)
            {
                KPIModel infoKPI = listKPIInfo[i];
                List<KPIModel> kpisSameKPIDifferentGroup = listKPIInfo.Where(item => item.KPIId == infoKPI.KPIId
                                                && item.Group.KPIGroupId != infoKPI.Group.KPIGroupId).ToList<KPIModel>();

                int numberKPISameKPIDifferentGroup = kpisSameKPIDifferentGroup.Count();

                var kpiGroupIds = new List<int>();
                var kpiGroupNames = new List<string>();

                kpiGroupIds.Add(infoKPI.Group.KPIGroupId);
                kpiGroupNames.Add(infoKPI.Group.Name);

                if (numberKPISameKPIDifferentGroup > 0)
                {
                    kpisSameKPIDifferentGroup.ForEach(item =>
                    {
                        kpiGroupIds.Add(item.Group.KPIGroupId);
                        kpiGroupNames.Add(item.Group.Name);
                        listKPIInfo.Remove(item);
                    });
                }

                var itemKPI = new KPIViewModel
                {
                    KPIId = infoKPI.KPIId,
                    Name = infoKPI.Name,
                    Description = infoKPI.Description,
                    IsRequireComment = infoKPI.IsRequireComment,
                    IsDisabled = infoKPI.IsDisabled,
                    KPIGroupIds = kpiGroupIds,
                    KPIGroupNames = kpiGroupNames,
                    TargetTypeId = infoKPI.TargetType.TargetTypeId,
                    TargetTypeName = infoKPI.TargetType.Name
                };

                listKPI.Add(itemKPI);
            }

            return listKPI;
        }
    }
}
