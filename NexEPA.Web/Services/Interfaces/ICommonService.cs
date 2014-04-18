// --------------------------------------------------------------------------
// <copyright file="ICommonService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Quinn.Vo" date="2014.4.4">Created</change>
// </history>

namespace NexEPA.Web.Services.Interfaces
{
    using NexEPA.Web.Criteria;
    using NexEPA.Web.Models;

    public interface ICommonService
    {
        UserSession GetUserByDomainUsername(GetEmployeeByDomainUsernameCriteria criteria);
    }
}
