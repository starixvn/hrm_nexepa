// --------------------------------------------------------------------------
// <copyright file="ApiAuthorizeAttribute.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Quinn.Vo" date="2014.3.31">Created</change>
// </history>

namespace NexEPA.Web.Attributes
{
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using NexEPA.Core.Models;

    /// <summary>
    /// APIAuthorize Attribute.
    /// </summary>
    public class ApiAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// Authorization event handler.
        /// </summary>
        /// <param name="actionContext">HttpAction Context.</param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                HandleUnauthorizedRequest(actionContext);
            }

            var session = HttpContext.Current.Session;
            if (!HttpContext.Current.Request.Headers.AllKeys.Contains("Authorization")
                && !HttpContext.Current.Request.Headers.AllKeys.Contains("Cookie"))
            {
                HandleUnauthorizedRequest(actionContext);
            }

            var currentUser = SessionManager.CurrentUser;
            if (currentUser != null)
            {
                var attributes = actionContext.ActionDescriptor.GetCustomAttributes<ApiAuthorizeAttribute>();
                foreach (AuthorizeAttribute item in attributes)
                {
                    if (item.Roles != string.Empty 
                        && currentUser != null 
                        && !currentUser.IsInRoles(item.Roles.Split(new[] { ',' }).ToList()))
                    {
                        HandlePermissionRequest(actionContext);
                    }
                }

                return;
            }
            else
            {
                HandleUnauthorizedRequest(actionContext);
            }
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            if (!HttpContext.Current.Request.IsLocal)
            {
                var challengeMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                actionContext.Response = challengeMessage;
                ////throw new HttpResponseException(challengeMessage);
            }
        }

        private void HandlePermissionRequest(HttpActionContext actionContext)
        {
            var message = new HttpResponseMessage(HttpStatusCode.MethodNotAllowed);
            actionContext.Response = message;
        }
    }
}
