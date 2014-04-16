// --------------------------------------------------------------------------
// <copyright file="RouteConfig.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Quinn.Vo" date="2014.3.7">Created</change>
// </history>

namespace NexEPA
{
    using System.Diagnostics.CodeAnalysis;
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// Route Config.
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// Function to register routes.
        /// </summary>
        /// <param name="routes">Route Collection.</param>
        [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Reviewed. Suppression is OK here.")]
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Employee",
                url: "Employee/Index",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
                name: "AccessDenied",
                url: "Home/AccessDenied",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
               name: "ProjectsEdit",
               url: "Projects/Edit/{Id}",
               defaults: new { controller = "Home", action = "Index" });

            routes.MapRoute(
               name: "Get all KPI Group",
               url: "KPI/GetAllKPIGroup/{id}",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
                name: "ProjectsDafault",
                url: "Projects/{*catchall}",
                defaults: new { controller = "Home", action = "Index" });

            routes.MapRoute(
               name: "ProjectsMemberView",
               url: "ProjectMembers/View/{Id}",
               defaults: new { controller = "Home", action = "Index" });

            routes.MapRoute(
               name: "ProjectsMemberCreate",
               url: "ProjectMembers/Create/{Id}",
               defaults: new { controller = "Home", action = "Index" });

            routes.MapRoute(
               name: "ProjectsMemberEdit",
               url: "ProjectMembers/Edit/ProjectId={proId}/EmployeeId={empId}",
               defaults: new { controller = "Home", action = "Index" });

            routes.MapRoute(
                name: "ProjectsMemberDefault",
                url: "ProjectMembers/{*catchall}",
                defaults: new { controller = "Home", action = "Index" });

            routes.MapRoute(
               name: "TargetTemplateView",
               url: "TargetTemplate/Edit/{Id}",
               defaults: new { controller = "Home", action = "Index" });

            routes.MapRoute(
                name: "TargetTemplateDefault",
                url: "TargetTemplate/{*catchall}",
                defaults: new { controller = "Home", action = "Index" });

            routes.MapRoute(
                name: "Create new KPI Group",
                url: "KPI/CreateNewKPIGroup",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
                name: "Edit KPI Group",
                url: "KPI/EditKPIGroup/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
               name: "Create new KPI",
               url: "KPI/CreateKPI/{id}",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
               name: "ViewKPI",
               url: "KPI/ViewKPI/{id}",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
               name: "Edit KPI",
               url: "KPI/EditKPI/{id}",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
               name: "View Target",
               url: "Target/ViewTarget/",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
               name: "Create Target",
               url: "Target/CreateTarget/",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
               name: "Edit Target",
               url: "Target/EditTarget/{id}",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
              name: "View Employee",
              url: "Employee/GetAllEmployee",
              defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
              name: "View Target Employee",
              url: "Target/TargetEmployee/{id}",
              defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }
    }
}