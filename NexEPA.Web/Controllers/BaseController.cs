// --------------------------------------------------------------------------
// <copyright file="BaseController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Quinn.Vo" date="2014.3.28">Created</change>
// </history>

namespace NexEPA.Web.Controllers
{
    using System.Web;
    using System.Web.Http;
    using System.Web.Security;
    using NexEPA.Web.Attributes;
    using NexEPA.Web.Models;

    /// <summary>
    /// Base Controller.
    /// </summary>
    [ApiAuthorize]
    public class BaseController : ApiController
    {
        /// <summary>
        /// Gets Current Session.
        /// </summary>
        /// <value>Current Session.</value>
        protected UserSession CurrentUser
        {
            get
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    FormsAuthentication.RedirectToLoginPage();
                }
                
                var currentUser = SessionManager.CurrentUser;

                if (currentUser == null)
                {
                    FormsAuthentication.RedirectToLoginPage();
                }

                return currentUser;
            }
        }
    }
}
