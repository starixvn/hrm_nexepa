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
    using Fanex.Data.Repository;
    using NexEPA.Web.Models;

    public interface ICommonRepository
    {
        UserSession GetUserByDomainUsername(ICriteria criteria);
    }
}
