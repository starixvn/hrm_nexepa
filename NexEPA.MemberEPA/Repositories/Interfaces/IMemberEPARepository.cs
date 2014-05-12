// --------------------------------------------------------------------------
// <copyright file="IMemberEPARepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.4.21">Created</change>
//   <change who="Josh.Phan" date="2014.4.22">Added - GetEPAByEmployee</change>
// </history>

namespace NexEPA.MemberEPA.Repositories.Interfaces
{
    using System.Collections.Generic;
    using Fanex.NAP.Framework.Data;
    using NexEPA.MemberEPA.Models;
    using NexEPA.MemberEPA.Models.MyEPA;

    /// <summary>
    /// Interface Member EPA Repository.
    /// </summary>
    public interface IMemberEPARepository
    {
        EPAInfoModel GetEPAInfo(ICriteria criteria);

        IEnumerable<MemberEPAModel> GetEPAByEmployee(ICriteria criteria);

        IEnumerable<MemberEPAModel> GetEPAOfEmployeeByManager(ICriteria criteria);
    }
}
