// --------------------------------------------------------------------------
// <copyright file="MyEPAController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Bradley.Mac" date="2014.4.21">Created</change>
// </history>

namespace NexEPA.Controllers.API
{
    using NexEPA.MemberEPA.Criteria;
    using NexEPA.MemberEPA.Models;
    using NexEPA.MemberEPA.Services.Impl;
    using NexEPA.MemberEPA.Services.Interfaces;
    using NexEPA.Web.Controllers;

    /// <summary>
    /// MyEPA Controller.
    /// </summary>
    public class MyEPAController : BaseController
    {
        private readonly IMemberEPAService _memberEPAService;

        public MyEPAController()
            : this(null)
        {
        }

        public MyEPAController(IMemberEPAService memberEPAService)
        {
            this._memberEPAService = memberEPAService ?? new MemberEPAService();
        }

        /// <summary>
        /// Get My EPa.
        /// </summary>
        /// <param name="targetId">Target id.</param>
        /// <returns>EPAInfo Model.</returns>
        public EPAInfoModel GetMyEPA(int targetId)
        {
            EPAInfoModel data = null;
            GetEPAInfoCriteria criteria = new GetEPAInfoCriteria()
            {
                EmployeeId = this.CurrentUser.UserId,
                TargetId = targetId
            };
            
            data = _memberEPAService.GetEPAInfo(criteria);

            return data;
        }
    }
}
