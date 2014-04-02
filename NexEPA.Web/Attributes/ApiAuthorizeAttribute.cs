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
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Security.Principal;
    using System.Text;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Controllers;
    using NexEPA.Core.Criteria;
    using NexEPA.Core.Models;
    using NexEPA.Core.Services;

    /// <summary>
    /// APIAuthorize Attribute (TODO).
    /// </summary>
    public class ApiAuthorizeAttribute : AuthorizeAttribute
    {
        private bool _requireSsl = false; //// Convert.ToBoolean(ConfigurationManager.AppSettings["RequireSsl"]);
        private bool _requireAuthentication = true;

        public bool RequireSsl
        {
            get { return _requireSsl; }
            set { _requireSsl = value; }
        }

        public bool RequireAuthentication
        {
            get { return _requireAuthentication; }
            set { _requireAuthentication = value; }
        }

        /// <summary>
        /// Authorization event handler.
        /// </summary>
        /// <param name="actionContext">HttpAction Context.</param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var attributes = actionContext.ActionDescriptor.GetCustomAttributes<AuthorizeAttribute>();
            
            if (Authenticate(actionContext) || !RequireAuthentication)
            {
                var currentUser = SessionManager.CurrentUser;
                if (currentUser != null)
                {
                    foreach (AuthorizeAttribute item in attributes)
                    {
                        if (item.Roles != string.Empty && currentUser != null && !currentUser.IsInRoles(item.Roles.Split(new[] { ',' }).ToList()))
                        {
                            var message = new HttpResponseMessage(HttpStatusCode.MethodNotAllowed);
                            throw new HttpResponseException(message);
                        }
                    }

                    return;
                }
                else
                {
                    var message = new HttpResponseMessage(HttpStatusCode.MethodNotAllowed);
                    throw new HttpResponseException(message);
                }
            }
            else
            {
                HandleUnauthorizedRequest(actionContext);
            }
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            var challengeMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            challengeMessage.Headers.Add("WWW-Authenticate", "Basic");
            ////throw new HttpResponseException(challengeMessage);
            actionContext.Response = challengeMessage;
            return;
        }

        /// <summary>
        /// Authenticate function.
        /// </summary>
        /// <param name="actionContext">HttpAction Context.</param>
        /// <returns>Is Authenticate.</returns>
        private bool Authenticate(HttpActionContext actionContext)
        {
            if (RequireSsl && !HttpContext.Current.Request.IsSecureConnection && !HttpContext.Current.Request.IsLocal)
            {
                return false;
            }

            if (!HttpContext.Current.Request.Headers.AllKeys.Contains("Authorization"))
            {
                return false;
            }

            string authHeader = HttpContext.Current.Request.Headers["Authorization"];

            IPrincipal principal;
            if (TryGetPrincipal(authHeader, out principal))
            {
                HttpContext.Current.User = principal;
                return true;
            }

            return false;
        }

        /// <summary>
        /// GetPrincipal function.
        /// </summary>
        /// <param name="authHeader">Authentication Header.</param>
        /// <param name="principal">Principal object.</param>
        /// <returns>Result of get.</returns>
        private bool TryGetPrincipal(string authHeader, out IPrincipal principal)
        {
            var creds = ParseAuthHeader(authHeader);
            if (creds != null)
            {
                if (TryGetPrincipal(creds[0], creds[1], out principal))
                {
                    return true;
                }
            }

            principal = null;
            return false;
        }

        /// <summary>
        /// Parse Authentication Header function.
        /// </summary>
        /// <param name="authHeader">Authentication Header.</param>
        /// <returns>Array of header.</returns>
        private string[] ParseAuthHeader(string authHeader)
        {
            // Check this is a Basic Auth header 
            if (authHeader == null || authHeader.Length == 0 || !authHeader.StartsWith("Basic"))
            {
                return null;
            }

            // Pull out the Credentials with are seperated by ':' and Base64 encoded 
            string base64Credentials = authHeader.Substring(6);
            string[] credentials = Encoding.ASCII.GetString(Convert.FromBase64String(base64Credentials)).Split(new char[] { ':' });

            if (credentials.Length != 2 || string.IsNullOrEmpty(credentials[0]) || string.IsNullOrEmpty(credentials[0]))
            {
                return null;
            }

            // Okay this is the credentials 
            return credentials;
        }

        /// <summary>
        /// GetPrincipal function.
        /// </summary>
        /// <param name="username">The Username.</param>
        /// <param name="password">The Password.</param>
        /// <param name="principal">Principal object.</param>
        /// <returns>Result of get.</returns>
        private bool TryGetPrincipal(string username, string password, out IPrincipal principal)
        {
            // this is the method that does the authentication 

            //// users often add a copy/paste space at the end of the username
            username = username.Trim();
            password = password.Trim();

            ////TODO
            ////Replace this with your own Authentication Code
            var criteria = new GetEmployeeByDomainUsernameCriteria()
            {
                DomainUsername = HttpContext.Current.User.Identity.Name
            };
            var currentUser = new BaseService().GetDataEntity<UserSession>(criteria);

            if (currentUser != null)
            {
                // once the user is verified, assign it to an IPrincipal with the identity name and applicable roles
                principal = new GenericPrincipal(new GenericIdentity(username), System.Web.Security.Roles.GetRolesForUser(HttpContext.Current.User.Identity.Name));

                return true;
            }
            else
            {
                ////if (!String.IsNullOrWhiteSpace(username))
                ////{

                ////}
                principal = null;
                return false;
            }
        }
    }
}
