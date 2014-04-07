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
    using NexEPA.Core.Models;
    using NexEPA.Core.Services;
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
        private ITargetRepositories _targetRepository;

        private EPAService _epaService;

        public TargetServices()
        {
            _targetRepository = new TargetRepositories();
            _epaService = new EPAService();
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
                List<TargetInfo> rawTargetList = (List<TargetInfo>)_targetRepository.GetTarget(criteria);

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
                        Status = _epaService.GetStatusById(rawTarget.Status),
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
        /// <returns>The TargetItem.</returns>
        public TargetItem GetTargetById(GetTargetByIdCriteria criteria)
        {
            TargetInfo rawTarget = _targetRepository.GetTargetById(criteria);

            var startDate = rawTarget.StartDate != null ? ((DateTime)rawTarget.StartDate).ToString(Constants.ShortDateStringFormat) : string.Empty;
            var endDate = rawTarget.EndDate != null ? ((DateTime)rawTarget.EndDate).ToString(Constants.ShortDateStringFormat) : string.Empty;

            var targetItem = new TargetItem
            {
                TargetId = rawTarget.TargetId,
                Name = rawTarget.Name,
                Description = rawTarget.Description,
                StartDate = startDate,
                EndDate = endDate,
                Status = _epaService.GetStatusById(rawTarget.Status),
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
            //// default active status when create the new target
            criteria.Status = (int)Constants.Status.Active;
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
    }
}
