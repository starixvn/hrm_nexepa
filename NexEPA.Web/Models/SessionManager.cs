// --------------------------------------------------------------------------
// <copyright file="SessionManager.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Quinn.Vo" date="2014.3.28">Created</change>
// </history>

namespace NexEPA.Web.Models
{
    using System.Web;
    using NexEPA.Core.Constants;
    using NexEPA.Web.Criteria;
    using NexEPA.Web.Services.Impl;

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
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
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
                        _currentUser = new CommonService().GetUserByDomainUsername(criteria);

                        if (_currentUser != null && !_currentUser.IsDisabled)
                        {
                            HttpContext.Current.Session[Constants.CurrentSessionKey] = _currentUser;
                        }
                    }
                }
                
                return _currentUser;
            }
        }
    }
}
