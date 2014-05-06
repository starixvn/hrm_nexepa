// -----------------------------------------------------------------------
// <copyright file="TargetService.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.04">Created</change>
// </history>

namespace NexEPA.Target.Services.Impl.Target
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
    using NexEPA.Target.Criteria.Target.Target;
    using NexEPA.Target.Criteria.Target.TargetEmployee;
    using NexEPA.Target.Criteria.Target.TargetKPI;
    using NexEPA.Target.Models.Target;
    using NexEPA.Target.Repositories.Implement;
    using NexEPA.Target.Repositories.Interfaces;
    using NexEPA.Target.Services.Interfaces;
    using NexEPA.Target.ViewModels.Target;
    using NexEPA.Web.Models;

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
        public List<TargetItem> GetAllTarget(GetTargetCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var targetList = new List<TargetItem>();

            if (criteria.IsValid())
            {
                IEnumerable<TargetInfo> rawTargets = _targetRepository.GetTarget(criteria);

                foreach (TargetInfo rawTarget in rawTargets)
                {
                    string startDate = rawTarget.StartDate != null 
                        ? ((DateTime)rawTarget.StartDate).ToString(Constants.ShortDateStringFormat) 
                        : string.Empty;
                    string endDate = rawTarget.EndDate != null 
                        ? ((DateTime)rawTarget.EndDate).ToString(Constants.ShortDateStringFormat) 
                        : string.Empty;

                    var targetItem = new TargetItem
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
        public TargetItem GetTargetById(GetTargetByIdCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var targetItem = new TargetItem();

            if (criteria.IsValid())
            {
                TargetInfo rawTarget = _targetRepository.GetTargetById(criteria);

                string startDate = rawTarget.StartDate != null 
                    ? ((DateTime)rawTarget.StartDate).ToString(Constants.ShortDateStringFormat) 
                    : string.Empty;
                string endDate = rawTarget.EndDate != null 
                    ? ((DateTime)rawTarget.EndDate).ToString(Constants.ShortDateStringFormat) 
                    : string.Empty;

                targetItem = new TargetItem
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
                criteria.CreatedBy = SessionManager.CurrentUser.UserId;
                criteria.CreatedDate = DateTime.Now;
                //// default status is New when create the new target
                criteria.Status = (int)Status.Open;

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
                criteria.ModifiedBy = SessionManager.CurrentUser.UserId;
                criteria.ModifiedDate = DateTime.Now;
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
        public List<TargetEmployeeItem> GetTargetEmployeeByTargetId(GetAllEmployeeByTargetIdCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var employeeList = new List<TargetEmployeeItem>();

            if (criteria.IsValid())
            {
                IEnumerable<TargetEmployeeInfo> rawEmployees = GetData<TargetEmployeeInfo>(criteria);

                foreach (TargetEmployeeInfo rawEmployee in rawEmployees)
                {
                    var employeeItem = new TargetEmployeeItem
                    {
                        EmployeeId = rawEmployee.EmployeeId,
                        FullName = rawEmployee.FullName,
                        NickName = rawEmployee.NickName,
                        GroupId = rawEmployee.GroupId,
                        GroupName = rawEmployee.Name
                    };

                    employeeList.Add(employeeItem);
                }
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
                criteria.AddedBy = SessionManager.CurrentUser.UserId;
                criteria.AddedDate = DateTime.Now;
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
        public List<TargetKPIGroupItem> GetKPIByTargetId(GetKPIByTargetIdCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var kpiGroupItemList = new List<TargetKPIGroupItem>();

            if (criteria.IsValid())
            {
                var rawKPIsInfo = (List<TargetKPIInfo>)_targetRepository.GetKPIByTargetId(criteria);

                for (int i = 0; i < rawKPIsInfo.Count; i++) 
                {
                    TargetKPIInfo rawKPIInfo = rawKPIsInfo[i];
                    var kpiItem = new TargetKPIItem
                    {
                        KPIId = rawKPIInfo.KPIId,
                        Name = rawKPIInfo.Name,
                        Weight = rawKPIInfo.Weight
                    };

                    var kpiGroupItem = new TargetKPIGroupItem
                    {
                        Group = rawKPIInfo.Group
                    };

                    kpiGroupItem.KPIs.Add(kpiItem);

                    List<TargetKPIInfo> kpiSameGroup = rawKPIsInfo.Where(item => item.Group.KPIGroupId == rawKPIInfo.Group.KPIGroupId
                                            && item.KPIId != rawKPIInfo.KPIId).ToList();

                    foreach (TargetKPIInfo kpiInfo in kpiSameGroup)
                    {
                        var kpiItemSameGroup = new TargetKPIItem
                        {
                            KPIId = kpiInfo.KPIId,
                            Name = kpiInfo.Name,
                            Weight = kpiInfo.Weight
                        };

                        kpiGroupItem.KPIs.Add(kpiItemSameGroup);
                        rawKPIsInfo.Remove(kpiInfo);
                    }

                    kpiGroupItemList.Add(kpiGroupItem);
                }
            }

            return kpiGroupItemList;
        }
    }
}
