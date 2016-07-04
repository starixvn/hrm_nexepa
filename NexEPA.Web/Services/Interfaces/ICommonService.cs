// --------------------------------------------------------------------------
// <copyright file="ICommonService.cs" company="">
// Copyright (c) . All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Quinn.Vo" date="2014.4.4">Created</change>
// </history>

namespace NexEPA.Web.Services.Interfaces
{
    using NexEPA.Core.Enums;
    using NexEPA.Web.Criteria;
    using NexEPA.Web.Models;

    public interface ICommonService
    {
        UserSession GetUserByDomainUsername(GetEmployeeByDomainUsernameCriteria criteria);

        bool HasPermissionOnFunction(string functionKey, Policy policy);

        int GetPermission(string functionKey);
    }
}
