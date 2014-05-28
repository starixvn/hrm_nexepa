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
    using System;
    using System.Configuration;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Web;
    using System.Web.Http;
    using NexEPA.Core.Constants;
    using NexEPA.Web.Controllers;
    using NexEPA.Web.Criteria;
    using NexEPA.Web.Models;
    using NexEPA.Web.Services.Impl;
    using System.Linq;

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

        /// <summary>
        /// User logout.
        /// </summary>
        /// <returns>The response.</returns>
        [HttpGet]
        public HttpResponseMessage Logout()
        {
            string username = "Test",
            password = "Test";
            AuthenticationHeaderValue headerValue = Request.Headers.Authorization;
            string httpAuth = headerValue != null ? headerValue.ToString() : string.Empty;

            if (!string.IsNullOrEmpty(httpAuth) && httpAuth.StartsWith("basic", StringComparison.OrdinalIgnoreCase))
            {
                byte[] securityToken = Encoding.UTF8.GetBytes(username + ":" + password);
                string expected = "basic " + Convert.ToBase64String(securityToken);

                if (string.Equals(httpAuth, expected, StringComparison.OrdinalIgnoreCase))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "You are logged out");
                }
            }

           
            if (HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session.Abandon();
                HttpContext.Current.Session.RemoveAll();
            }

            if (HttpContext.Current.Request.Headers.AllKeys.Contains("Authorization")) 
            {
                return Request.CreateResponse(HttpStatusCode.OK, "You are logged in");
            }
            
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Unauthorized);

            return response;

            //return Request.CreateResponse(HttpStatusCode.OK, "You are logged out");
        }
    }
}
