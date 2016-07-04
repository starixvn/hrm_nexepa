// --------------------------------------------------------------------------
// <copyright file="ICommonRepository.cs" company="">
// Copyright (c) . All rights reserved.
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
