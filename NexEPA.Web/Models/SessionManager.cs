// --------------------------------------------------------------------------
// <copyright file="SessionManager.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Quinn.Vo" date="2014.3.28">Created</change>
// </history>

namespace NexEPA.Core.Models
{
    using System.Linq;
    using System.Web;
    using System.Web.Security;
    using NexEPA.Core.Constants;
    using NexEPA.Core.Criteria;
    using NexEPA.Core.Services;

    /// <summary>
    /// Session Manager.
    /// </summary>
    public static class SessionManager
    {
        private static UserSession _currentUser;

        public static UserSession CurrentUser
        {
            get
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    FormsAuthentication.RedirectToLoginPage();
                }

                var session = HttpContext.Current.Session;
                if (session != null && session[Constants.CurrentSessionKey] != null)
                {
                    _currentUser = (UserSession)session[Constants.CurrentSessionKey];
                }
                else
                {
                    var criteria = new GetEmployeeByDomainUsernameCriteria()
                    {
                        DomainUsername = HttpContext.Current.User.Identity.Name
                    };
                    _currentUser = new BaseService().GetDataEntity<UserSession>(criteria);

                    if (_currentUser != null && !_currentUser.IsDisabled)
                    {
                        HttpContext.Current.Session[Constants.CurrentSessionKey] = _currentUser;
                    }
                }

                return _currentUser;
            }
        }

        public static void ClearAll()
        {
            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session.Clear();
                HttpContext.Current.Session.Abandon();
                HttpContext.Current.Response.Cookies.Clear();
            }
        }
    }
}
