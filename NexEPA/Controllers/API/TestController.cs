// -----------------------------------------------------------------------
// <copyright file="TestController.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.05.12">Created</change>
// </history>

namespace NexEPA.Controllers.API
{
    using System.Configuration;
    using System.Web;
    using System.Web.Http;
    using NexEPA.Core.Constants;
    using NexEPA.Web.Controllers;
    using NexEPA.Web.Criteria;
    using NexEPA.Web.Models;
    using NexEPA.Web.Services.Impl;

    /// <summary>
    /// Test controller.
    /// </summary>
    public class TestController : BaseController
    {
        [HttpGet]
        public bool GetIsSimulatedLoginUser()
        {
            string isSimulatedLoginUser = ConfigurationManager.AppSettings["IsSimulatedLoginUser"].ToString();
            return isSimulatedLoginUser == "true" ? true : false;
        }

        /// <summary>
        /// Login as another user.
        /// </summary>
        /// <param name="domainUsername">The domain username.</param>
        /// <returns>The integer value to identify success or not.</returns>
        [HttpGet]
        public int LoginAsAnotherUser(string domainUsername)
        {
            var isSimulatedLoginUser = GetIsSimulatedLoginUser();
            if (isSimulatedLoginUser)
            {
                var criteria = new GetEmployeeByDomainUsernameCriteria
                {
                    DomainUsername = domainUsername
                };

                UserSession _currentUser = new CommonService().GetUserByDomainUsername(criteria);

                if (_currentUser != null && !_currentUser.IsDisabled)
                {
                    HttpContext.Current.Session[Constants.CurrentSessionKey] = _currentUser;
                    return 0;
                }
            }

            return -1;
        }
    }
}
