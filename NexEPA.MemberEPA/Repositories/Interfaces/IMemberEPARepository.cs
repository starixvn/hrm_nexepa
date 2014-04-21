// --------------------------------------------------------------------------
// <copyright file="IMemberEPARepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.4.21">Created</change>
// </history>

namespace NexEPA.MemberEPA.Repositories.Interfaces
{
    using Fanex.NAP.Framework.Data;
    using NexEPA.MemberEPA.Models;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IMemberEPARepository
    {
        EPAInfoModel GetEPAInfo(ICriteria criteria);
    }
}
