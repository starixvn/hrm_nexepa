// --------------------------------------------------------------------------
// <copyright file="Global.asax.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Quinn.Vo" date="2014.3.7">Created</change>
// </history>

namespace NexEPA
{
    using System;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Validation.Providers;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using Fanex.Data.Configuration;
    using Fanex.Logging;

    /// <summary>
    /// MVC Application.
    /// </summary>
    public class MvcApplication : HttpApplication
    {
        /// <summary>
        /// Application Start.
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            DbSettingProviderManager.Current.Start();
            GlobalConfiguration.Configuration.Services.RemoveAll(
                typeof(System.Web.Http.Validation.ModelValidatorProvider),
                v => v is InvalidModelValidatorProvider);
        }

        /// <summary>
        /// Application Error.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">The EventArgs.</param>
        protected void Application_Error(object sender, EventArgs e)
        {
            if (!Request.IsLocal)
            {
                Exception ex = Server.GetLastError().GetBaseException();
                string message = ex.Message +
                        "\r\nSource: " + ex.Source +
                        "\r\nDomain: " + HttpContext.Current.Request.Headers["Host"] +
                        "\r\nTargetSite: " + ex.TargetSite +
                        "\r\nStackTrace: " + ex.StackTrace;

                LogService.Error(message);
            }
        }
    }
}