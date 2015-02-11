// --------------------------------------------------------------------------
// <copyright file="IAuthorityRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Larry.Tran" date="2015.2.11">Created</change>
// </history>

namespace NexEPA.Authority.Repositories
{
    using Fanex.Data.Repository;

    /// <summary>
    /// IAuthorityRepository interface.
    /// </summary>
    public interface IAuthorityRepository
    {
        bool IsOnlyMember(ICriteria criteria);
    }
}
