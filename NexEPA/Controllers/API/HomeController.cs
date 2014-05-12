// -----------------------------------------------------------------------
// <copyright file="HomeController.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.05.06">Created</change>
// </history>

namespace NexEPA.Controllers.API
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Web;
    using System.Web.Http;
    using NexEPA.Core.Constants;
    using NexEPA.Report.Criteria;
    using NexEPA.Report.Services.Implement;
    using NexEPA.Report.Services.Interface;
    using NexEPA.Web.Controllers;
    using NexEPA.Web.Criteria;
    using NexEPA.Web.Models;
    using NexEPA.Web.Services.Impl;

    /// <summary>
    /// Home Controller.
    /// </summary>
    public class HomeController : BaseController
    {
        private IReportService _reportService;

        public HomeController()
            : this(null)
        {
        }

        public HomeController(IReportService reportService)
        {
            _reportService = reportService ?? new ReportService();
        }

        [HttpGet]
        public List<object> GetGeneralInformationOfCurrentEmployee()
        {
            var criteria = new GetGeneralInformationOfCurrentEmployeeCriteria
            {
                EmployeeId = SessionManager.CurrentUser.UserId
            };

            return _reportService.GetGeneralInformationOfCurrentEmployee(criteria);
        }

        [HttpGet]
        public int GetCurrentEmployeeId()
        {
            return this.CurrentUser.UserId;
        }

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

            return -1;
        }
    }
}
