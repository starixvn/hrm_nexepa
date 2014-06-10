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
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using NexEPA.Core.Constants;
    using NexEPA.Core.Enums;
    using NexEPA.Web.Models;

    /// <summary>
    /// APIAuthorize Attribute.
    /// </summary>
    public class ApiAuthorizeAttribute : AuthorizeAttribute
    {
        private string _functionKey;

        private Policy _policy;

        public string FunctionKey
        {
            get 
            {
                return _functionKey != null ? _functionKey : string.Empty;
            }

            set
            {
                _functionKey = value;
            }
        }

        public Policy Policy
        {
            get
            {
                return _policy;
            }

            set
            {
                _policy = value;
            }
        }

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

            UserSession currentUser = null;
            var session = HttpContext.Current.Session;

            if (HttpContext.Current.Request.Headers.AllKeys.Contains("Authorization"))
            {
                if (HttpContext.Current.Session.IsNewSession)
                {
                    HandleUnauthorizedRequest(actionContext);
                }
            }
            else 
            {
                if (HttpContext.Current.Session[Constants.CurrentSessionKey] == null)
                {
                    currentUser = SessionManager.CurrentUser;
                    CheckUserPermission(actionContext, currentUser);

                    var responseMessage = new HttpResponseMessage(HttpStatusCode.OK);
                    actionContext.Response = responseMessage;
                }
                else
                {
                    currentUser = (UserSession)HttpContext.Current.Session[Constants.CurrentSessionKey];
                }
            }

            if (currentUser != null)
            {
                CheckUserPermission(actionContext, currentUser);
                return;
            }
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            if (HttpContext.Current.Session.IsNewSession)
            {
                var responseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                actionContext.Response = responseMessage;
            }
        }

        private void HandlePermissionRequest(HttpActionContext actionContext)
        {
            var responseMessage = new HttpResponseMessage(HttpStatusCode.MethodNotAllowed);
            actionContext.Response = responseMessage;
        }

        /// <summary>
        /// Check user permission.
        /// </summary>
        /// <param name="actionContext">The action context.</param>
        /// <param name="currentUser">The current user.</param>
        private void CheckUserPermission(HttpActionContext actionContext, UserSession currentUser)
        {
            if (currentUser != null)
            {
                Collection<ApiAuthorizeAttribute> attributes = actionContext.ActionDescriptor.GetCustomAttributes<ApiAuthorizeAttribute>();

                foreach (ApiAuthorizeAttribute item in attributes)
                {
                    if (!currentUser.HasPermissionOnFunction(item.FunctionKey, item.Policy))
                    {
                        HandlePermissionRequest(actionContext);
                    }
                }
            }
        }
    }
}
