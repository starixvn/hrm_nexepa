// --------------------------------------------------------------------------
// <copyright file="IMemberEPAService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.4.21">Created</change>
//   <change who="Josh.Phan" date="2014.4.22">Added - GetEPAByEmployee</change>
// </history>

namespace NexEPA.MemberEPA.Services.Interfaces
{
    using System.Collections.Generic;
    using NexEPA.MemberEPA.Criteria;
    using NexEPA.MemberEPA.Models;
    using NexEPA.MemberEPA.Models.MyEPA;

    /// <summary>
    /// Interface Member EPA Service.
    /// </summary>
    public interface IMemberEPAService
    {
        EPAInfoModel GetEPAInfo(GetEPAInfoCriteria criteria);

        List<MemberEPAModel> GetEPAByEmployee(GetEPAByEmployeeCriteria criteria);
    }
}
