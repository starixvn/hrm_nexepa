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
    using System.Collections.Generic;
    using NexEPA.Core.Constants;
    using NexEPA.Core.Services;
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

        public TargetServices()
        {
            _targetRepository = new TargetRepositories();
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
                    var targetItem = new TargetItem
                    {
                        TargetId = rawTarget.TargetId,
                        Name = rawTarget.Name,
                        Description = rawTarget.Description,
                        StartDate = rawTarget.StartDate.ToString(Constants.ShortDateStringFormat),
                        EndDate = rawTarget.EndDate.ToString(Constants.ShortDateStringFormat),
                        //// TODO : get status by status id
                        TargetType = rawTarget.TargetType,
                        Project = rawTarget.Project,
                        Milestone = rawTarget.Milestone
                    };

                    targetList.Add(targetItem);
                }
            }

            return targetList;
        }
    }
}
