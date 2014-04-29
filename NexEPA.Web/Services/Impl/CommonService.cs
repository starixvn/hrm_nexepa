// --------------------------------------------------------------------------
// <copyright file="CommonService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Quinn.Vo" date="2014.4.4">Created</change>
// </history>

namespace NexEPA.Web.Services.Impl
{
    using Fanex.Common.Helper;
    using NexEPA.Web.Criteria;
    using NexEPA.Web.Models;
    using NexEPA.Web.Repositories.Impl;
    using NexEPA.Web.Repositories.Interfaces;
    using NexEPA.Web.Services.Interfaces;

    /// <summary>
    /// Common Service.
    /// </summary>
    public class CommonService : ICommonService
    {
        private readonly ICommonRepository _commonRepository;

        public CommonService(ICommonRepository commonRepository = null)
        {
            this._commonRepository = commonRepository ?? new CommonRepository();
        }

        public UserSession GetUserByDomainUsername(GetEmployeeByDomainUsernameCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                UserSession user = this._commonRepository.GetUserByDomainUsername(criteria);
                return user;
            }

            return null;
        }
    }
}
