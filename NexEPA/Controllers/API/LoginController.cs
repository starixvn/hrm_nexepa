// -----------------------------------------------------------------------
// <copyright file="LoginController.cs" company="Nexcel Solutions Viet Nam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.05.29">Created</change>
// </history>

namespace NexEPA.Controllers.API
{
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Web;
    using System.Web.Http;
    using NexEPA.Web.Controllers;

    /// <summary>
    /// Login controller.
    /// </summary>
    public class LoginController : BaseController
    {
        /// <summary>
        /// User logout.
        /// </summary>
        /// <returns>The response.</returns>
        [HttpGet]
        public HttpResponseMessage Logout()
        {
            AuthenticationHeaderValue headerValue = Request.Headers.Authorization;

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
        }
    }
}
