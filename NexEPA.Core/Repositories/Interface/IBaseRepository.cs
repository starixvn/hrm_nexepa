// --------------------------------------------------------------------------
// <copyright file="IBaseRepository.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Quinn.Vo" date="2014.3.18">Created</change>
// </history>

namespace NexEPA.Core.Repositories.Interface
{
    using Fanex.Data.Repository;
    using NexEPA.Core.Models;

    public interface IBaseRepository
    {
        CommandResult Insert(ICriteria criteria, bool hasGetOutputParams = false);

        CommandResult Update(ICriteria criteria, bool hasGetOutputParams = false);

        CommandResult Delete(ICriteria criteria, bool hasGetOutputParams = false);
    }
}
