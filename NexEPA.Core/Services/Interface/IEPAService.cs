// -----------------------------------------------------------------------
// <copyright file="IEPAService.cs" company="Nexcel Solution Viet Nam">
// Copyright (c) . All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.05.09">Created</change>
// </history>

namespace NexEPA.Core.Services.Interface
{
    using System.Collections.Generic;
    using NexEPA.Core.Criteria;
    using NexEPA.Core.Models;

    /// <summary>
    /// The interface of EPA service.
    /// </summary>
    public interface IEPAService
    {
        IEnumerable<TargetType> GetAllTargetType(GetAllTargetTypeCriteria criteria);

        List<SelectListModel> GetAllProjectToFilter(GetAllProjectToFilterCriteria criteria);
    }
}
