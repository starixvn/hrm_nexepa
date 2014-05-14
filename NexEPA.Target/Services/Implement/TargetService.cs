// -----------------------------------------------------------------------
// <copyright file="TargetService.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.04">Created</change>
// </history>

namespace NexEPA.Target.Services.Implement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Common.Helper;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Core.Constants;
    using NexEPA.Core.Enums;
    using NexEPA.Core.Services;
    using NexEPA.Core.Utilities;
    using NexEPA.Employee.Models;
    using NexEPA.KPI.Model;
    using NexEPA.Target.Criteria.Target.Target;
    using NexEPA.Target.Criteria.Target.TargetEmployee;
    using NexEPA.Target.Criteria.Target.TargetKPI;
    using NexEPA.Target.Models;
    using NexEPA.Target.Repositories.Implement;
    using NexEPA.Target.Repositories.Interfaces;
    using NexEPA.Target.Services.Interfaces;
    using NexEPA.Target.ViewModels;

    /// <summary>
    /// The implement of Target Services.
    /// </summary>
    public class TargetService : BaseService, ITargetServices
    {
        private ITargetRepository _targetRepository;

        public TargetService(ITargetRepository targetRepository = null)
        {
            _targetRepository = targetRepository ?? new TargetRepository();
        }

        /// <summary>
        /// Get target.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>List Target Item.</returns>
        public List<TargetViewModel> GetAllTarget(GetTargetCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var targetList = new List<TargetViewModel>();

            if (criteria.IsValid())
            {
                IEnumerable<TargetModel> rawTargets = _targetRepository.GetTarget(criteria);

                foreach (TargetModel rawTarget in rawTargets)
                {
                    string startDate = rawTarget.StartDate != null 
                        ? ((DateTime)rawTarget.StartDate).ToString(Constants.ShortDateStringFormat) 
                        : string.Empty;
                    string endDate = rawTarget.EndDate != null 
                        ? ((DateTime)rawTarget.EndDate).ToString(Constants.ShortDateStringFormat) 
                        : string.Empty;

                    var targetItem = new TargetViewModel
                    {
                        TargetId = rawTarget.TargetId,
                        Name = rawTarget.Name,
                        Description = rawTarget.Description,
                        StartDate = startDate,
                        EndDate = endDate,
                        StatusName = EnumStatus.GetDescripton((Status)rawTarget.Status),
                        NumberKPI = rawTarget.KPIs,
                        NumberMember = rawTarget.Members,
                        TargetType = rawTarget.TargetType,
                        Project = rawTarget.Project,
                        Milestone = rawTarget.Milestone
                    };

                    targetList.Add(targetItem);
                }
            }

            return targetList;
        }

        /// <summary>
        /// Get Target by ID.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Target Item.</returns>
        public TargetViewModel GetTargetById(GetTargetByIdCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var targetItem = new TargetViewModel();

            if (criteria.IsValid())
            {
                TargetModel rawTarget = _targetRepository.GetTargetById(criteria);

                string startDate = rawTarget.StartDate != null 
                    ? ((DateTime)rawTarget.StartDate).ToString(Constants.ShortDateStringFormat) 
                    : string.Empty;
                string endDate = rawTarget.EndDate != null 
                    ? ((DateTime)rawTarget.EndDate).ToString(Constants.ShortDateStringFormat) 
                    : string.Empty;

                targetItem = new TargetViewModel
                {
                    TargetId = rawTarget.TargetId,
                    Name = rawTarget.Name,
                    Description = rawTarget.Description,
                    StartDate = startDate,
                    EndDate = endDate,
                    StatusName = EnumStatus.GetDescripton((Status)rawTarget.Status),
                    Status = rawTarget.Status,
                    TargetType = rawTarget.TargetType,
                    Project = rawTarget.Project,
                    Milestone = rawTarget.Milestone
                };
            }

            return targetItem;
        }

        /// <summary>
        /// Insert new target.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Command Result.</returns>
        public CommandResult Insert(InsertTargetCriteria criteria)
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
        /// Update Target.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Command Result.</returns>
        public CommandResult Update(UpdateTargetCriteria criteria)
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
        /// Update Target Status.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Command Result.</returns>
        public CommandResult UpdateStatus(UpdateTargetStatusCriteria criteria)
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
        /// Delete Target.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Command Result.</returns>
        public CommandResult Delete(DeleteTargetCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            CommandResult commandResult = new CommandResult();

            if (criteria.IsValid())
            {
                commandResult = _Repository.Delete(criteria);
            }

            return commandResult;
        }

        /// <summary>
        /// Get Target's Employee by Target Id.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The list TargetEmployeeItem.</returns>
        public IEnumerable<EmployeeModel> GetTargetEmployeeByTargetId(GetAllEmployeeByTargetIdCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            IEnumerable<EmployeeModel> employeeList = null;

            if (criteria.IsValid())
            {
                employeeList = _targetRepository.GetTargetEmployeeByTargetId(criteria);
            }

            return employeeList;
        }

        /// <summary>
        /// Insert Target's Employee.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The CommandResult.</returns>
        public CommandResult InsertTargetEmployee(AddTargetEmployeeCriteria criteria)
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
        /// Delete Target's Employee.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The CommandResult.</returns>
        public CommandResult DeleteTargetEmployee(DeleteTargetEmployeeCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            CommandResult commandResult = new CommandResult();

            if (criteria.IsValid())
            {
                commandResult = _Repository.Delete(criteria);
            }

            return commandResult;
        }

        /// <summary>
        /// Get KPIs by Target ID.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>List Target KPI Item.</returns>
        public List<TargetKPIGroupModel> GetKPIByTargetId(GetKPIByTargetIdCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var kpiGroupItemList = new List<TargetKPIGroupModel>();

            if (criteria.IsValid())
            {
                var rawKPIsInfo = (List<KPIModel>)_targetRepository.GetKPIByTargetId(criteria);

                for (int i = 0; i < rawKPIsInfo.Count; i++) 
                {
                    KPIModel rawKPIInfo = rawKPIsInfo[i];

                    var kpiGroupItem = new TargetKPIGroupModel
                    {
                        Group = rawKPIInfo.Group
                    };

                    kpiGroupItem.KPIs.Add(rawKPIInfo);

                    List<KPIModel> kpiSameGroup = rawKPIsInfo.Where(item => item.Group.KPIGroupId == rawKPIInfo.Group.KPIGroupId
                                            && item.KPIId != rawKPIInfo.KPIId).ToList();

                    foreach (KPIModel kpiInfo in kpiSameGroup)
                    {
                        kpiGroupItem.KPIs.Add(kpiInfo);
                        rawKPIsInfo.Remove(kpiInfo);
                    }

                    kpiGroupItemList.Add(kpiGroupItem);
                }
            }

            return kpiGroupItemList;
        }
    }
}
