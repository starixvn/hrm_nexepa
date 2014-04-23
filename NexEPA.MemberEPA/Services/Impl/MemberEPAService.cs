// --------------------------------------------------------------------------
// <copyright file="MemberEPAService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.4.21">Created</change>
//   <change who="Josh.Phan" date="2014.4.22">Added - GetEPAByEmployee</change>
// </history>

namespace NexEPA.MemberEPA.Services.Impl
{
    using System.Collections.Generic;
    using Fanex.Common.Helper;
    using Fanex.NAP.Framework.Data;
    using NexEPA.Core.Services;
    using NexEPA.MemberEPA.Criteria;
    using NexEPA.MemberEPA.Models;
    using NexEPA.MemberEPA.Models.MyEPA;
    using NexEPA.MemberEPA.Repositories.Impl;
    using NexEPA.MemberEPA.Repositories.Interfaces;
    using NexEPA.MemberEPA.Services.Interfaces;

    /// <summary>
    /// Member EPA Service.
    /// </summary>
    public class MemberEPAService : BaseService, IMemberEPAService
    {
        private readonly IMemberEPARepository _memberEPARepository;

        public MemberEPAService(IMemberEPARepository memberEPARepository = null)
        {
            _memberEPARepository = memberEPARepository ?? new MemberEPARepository();
        }

        public EPAInfoModel GetEPAInfo(GetEPAInfoCriteria criteria)
        {
            EPAInfoModel result = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            { 
                result = _memberEPARepository.GetEPAInfo(criteria);
            }

            return result;
        }

        /// <summary>
        /// Get EPA by employee.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The List MemberEPAModel.</returns>
        public List<MemberEPAModel> GetEPAByEmployee(GetEPAByEmployeeCriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");
            var employeeEPAs = new List<MemberEPAModel>();

            if (criteria.IsValid())
            {
                employeeEPAs = (List<MemberEPAModel>)_memberEPARepository.GetEPAByEmployee(criteria);
            }

            return employeeEPAs;
        }

        /// <summary>
        /// Add or Update Employee EPA.
        /// </summary>
        /// <param name="criteria">AddOrUpdateEPAScore Criteria.</param>
        /// <returns>Command Result.</returns>
        public CommandResult AddOrUpdateEmployeeEPA(AddOrUpdateEPAScore criteria)
        {
            CommandResult result = null;
            Check.Argument.IsNotNull(criteria, "criteria");
            if (criteria.IsValid())
            {
                result = this._Repository.Insert(criteria);
            }

            return result;
        }
    }
}
