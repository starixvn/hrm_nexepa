// -----------------------------------------------------------------------
// <copyright file="EPAService.cs" company="Nexcel Solution Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.03.18">Created</change>
// </history>

namespace NexEPA.Core.Services
{
    using System.Collections.Generic;
    using Fanex.Common.Helper;
    using NexEPA.Core.Criteria;
    using NexEPA.Core.Models;
    using NexEPA.Core.Services.Interface;

    /// <summary>
    /// EPA services.
    /// </summary>
    public class EPAService : BaseService, IEPAService
    {
        /// <summary>
        /// Get all Target Type.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>List Target Type.</returns>
        public IEnumerable<TargetType> GetAllTargetType(GetAllTargetTypeCriteria criteria)
        {
            return GetData<TargetType>(criteria);
        }

        /// <summary>
        /// Get all project to filter.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The list SelectListModel.</returns>
        public List<SelectListModel> GetAllProjectToFilter(GetAllProjectToFilterCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var allProjects = new List<SelectListModel>();

            if (criteria.IsValid())
            {
                IEnumerable<ProjectFilterModel> rawProjects = GetData<ProjectFilterModel>(criteria);

                foreach (ProjectFilterModel rawProject in rawProjects)
                {
                    var projectSelectItem = new SelectListModel
                    {
                        Id = rawProject.ProjectId,
                        Name = rawProject.Name
                    };

                    allProjects.Add(projectSelectItem);
                }
            }

            return allProjects;
        }
    }
}
