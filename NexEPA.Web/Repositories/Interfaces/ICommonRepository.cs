// --------------------------------------------------------------------------
// <copyright file="ICommonRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Quinn.Vo" date="2014.4.4">Created</change>
// </history>

namespace NexEPA.Web.Repositories.Interfaces
{
    using Fanex.NAP.Framework.Data;
    using NexEPA.Core.Models;

    public interface ICommonRepository
    {
        UserSession GetUserByDomainUsername(ICriteria criteria);
    }
}
