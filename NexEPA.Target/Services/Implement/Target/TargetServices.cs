// -----------------------------------------------------------------------
// <copyright file="TargetServices.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.04.04">Created</change>
// </history>

namespace NexEPA.Target.Services.Implement.Target
{
    using System;
    using System.Collections.Generic;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Core.Constants;
    using NexEPA.Core.Enums;
    using NexEPA.Core.Models;
    using NexEPA.Core.Services;
    using NexEPA.Core.Utilities;
    using NexEPA.Target.Criteria.Target;
    using NexEPA.Target.Models.Target;
    using NexEPA.Target.Repositories.Implement.Target;
    using NexEPA.Target.Repositories.Interface.Target;
    using NexEPA.Target.Services.Interface.Target;
    using NexEPA.Target.Target.Criteria;
    using NexEPA.Target.ViewModels.Target;

    /// <summary>
    /// The implement of Target Services.
    /// </summary>
    public class TargetServices : BaseService, ITargetServices
    {
        private ITargetRepositories _targetRepositories;

        public TargetServices()
        {
            _targetRepositories = new TargetRepositories();
        }

        /// <summary>
        /// Get target.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>List Target Item.</returns>
        public List<TargetItem> GetAllTarget(GetTargetCriteria criteria)
        {
            var targetList = new List<TargetItem>();

            if (criteria.IsValid())
            {
                List<TargetInfo> rawTargetList = (List<TargetInfo>)_targetRepositories.GetTarget(criteria);

                foreach (var rawTarget in rawTargetList)
                {
                    var startDate = rawTarget.StartDate != null ? ((DateTime)rawTarget.StartDate).ToString(Constants.ShortDateStringFormat) : string.Empty;
                    var endDate = rawTarget.EndDate != null ? ((DateTime)rawTarget.EndDate).ToString(Constants.ShortDateStringFormat) : string.Empty;

                    var targetItem = new TargetItem
                    {
                        TargetId = rawTarget.TargetId,
                        Name = rawTarget.Name,
                        Description = rawTarget.Description,
                        StartDate = startDate,
                        EndDate = endDate,
                        StatusName = EnumStatus.GetDescripton((Status)rawTarget.Status),
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
            TargetInfo rawTarget = _targetRepositories.GetTargetById(criteria);

            var startDate = rawTarget.StartDate != null ? ((DateTime)rawTarget.StartDate).ToString(Constants.ShortDateStringFormat) : string.Empty;
            var endDate = rawTarget.EndDate != null ? ((DateTime)rawTarget.EndDate).ToString(Constants.ShortDateStringFormat) : string.Empty;

            var targetItem = new TargetItem
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

            return targetItem;
        }

        /// <summary>
        /// Insert new target.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Command Result.</returns>
        public CommandResult Insert(InsertTargetCriteria criteria)
        {
            criteria.CreatedBy = SessionManager.CurrentUser.UserId;
            criteria.CreatedDate = DateTime.Now;
            //// default new status when create the new target
            criteria.Status = (int)Status.New;
            return _Repository.Insert(criteria);
        }

        /// <summary>
        /// Update Target.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Command Result.</returns>
        public CommandResult Update(UpdateTargetCriteria criteria)
        {
            criteria.ModifiedBy = SessionManager.CurrentUser.UserId;
            criteria.ModifiedDate = DateTime.Now;
            return _Repository.Update(criteria);
        }

        /// <summary>
        /// Delete Target.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Command Result.</returns>
        public CommandResult Delete(DeleteTargetCriteria criteria)
        {
            criteria.ModifiedBy = SessionManager.CurrentUser.UserId;
            criteria.ModifiedDate = DateTime.Now;
            return _Repository.Delete(criteria);
        }
    }
}
