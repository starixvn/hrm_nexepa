// --------------------------------------------------------------------------
// <copyright file="MemberEPAService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.4.21">Created</change>
// </history>

namespace NexEPA.MemberEPA.Services.Impl
{
    using NexEPA.MemberEPA.Criteria;
    using NexEPA.MemberEPA.Models;
    using NexEPA.MemberEPA.Repositories.Impl;
    using NexEPA.MemberEPA.Repositories.Interfaces;
    using NexEPA.MemberEPA.Services.Interfaces;

    /// <summary>
    /// Member EPA Service.
    /// </summary>
    public class MemberEPAService : IMemberEPAService
    {
        private readonly IMemberEPARepository _memberEPARepository;

        public MemberEPAService(IMemberEPARepository memberEPARepository = null)
        {
            _memberEPARepository = memberEPARepository ?? new MemberEPARepository();
        }

        public EPAInfoModel GetEPAInfo(GetEPAInfoCriteria criteria)
        {
            return _memberEPARepository.GetEPAInfo(criteria);
        }
    }
}
