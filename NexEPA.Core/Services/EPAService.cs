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
    using NexEPA.Core.Criteria;
    using NexEPA.Core.Model.Target;

    /// <summary>
    /// EPA services.
    /// </summary>
    public class EPAService : BaseService
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
    }
}
