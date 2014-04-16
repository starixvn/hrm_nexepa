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
    using System.Collections.Generic;
    using Fanex.Common.Helper;
    using Fanex.NAP.Framework.Data;
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
        /// <param name="criteria">The criteria.</param>
        /// <returns>The list KPI.</returns>
        public List<KPIItem> GetKPI(GetKPICriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var listKPI = new List<KPIItem>();

            if (criteria.IsValid())
            {
                IEnumerable<KPIInfo> listKPIInfo = _kpiRepository.GetKPI(criteria);

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
            }

            return listKPI; 
        }

        /// <summary>
        /// Get KPI by ID.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The KPI Item.</returns>
        public KPIItem GetKPIById(GetKPIByIdCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var kpiItem = new KPIItem();

            if (criteria.IsValid())
            {
                KPIInfo infoKPI = _kpiRepository.GetKPIById(criteria);

                kpiItem = new KPIItem
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
            CommandResult commandResult = new CommandResult();

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
            CommandResult commandResult = new CommandResult();

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
            CommandResult commandResult = new CommandResult();

            if (criteria.IsValid())
            {
                commandResult = _Repository.Delete(criteria);
            }

            return commandResult;
        }
    }
}
