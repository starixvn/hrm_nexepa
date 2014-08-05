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
    using System.Web.Http;
    using NexEPA.Core.Constants;
    using NexEPA.Core.Enums;
    using NexEPA.Core.Services;
    using NexEPA.Core.Services.Interface;
    using NexEPA.Report.Criteria;
    using NexEPA.Report.Models.Dashboard;
    using NexEPA.Report.Services.Implement;
    using NexEPA.Report.Services.Interface;
    using NexEPA.Web.Attributes;
    using NexEPA.Web.Controllers;
    using NexEPA.Web.Models;

    /// <summary>
    /// Home Controller.
    /// </summary>
    public class HomeController : BaseController
    {
        private IReportService _reportService;
        private IEncodeService _encodeSerivce;

        public HomeController()
            : this(null, null)
        {
        }

        public HomeController(IReportService reportService, IEncodeService encodeService)
        {
            _reportService = reportService ?? new ReportService();
            _encodeSerivce = encodeService ?? new EncodeService();
        }

        [HttpGet]
        [ApiAuthorize(FunctionKey = EPAFunctionKeys.Dashboard, Policy = Policy.Read)]
        public DashboardModel GetGeneralInformationOfCurrentEmployee()
        {
            var criteria = new GetGeneralInformationOfCurrentEmployeeCriteria
            {
                EmployeeId = SessionManager.CurrentUser.UserId
            };
            string cipherText = _encodeSerivce.Encode("Josh.Phan");
            string plainText = _encodeSerivce.Decode(cipherText);
            return _reportService.GetGeneralInformationOfCurrentEmployee(criteria);
        }

        [HttpGet]
        public int GetCurrentEmployeeId()
        {
            return this.CurrentUser.UserId;
        }
    }
}
