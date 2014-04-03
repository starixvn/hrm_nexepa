// --------------------------------------------------------------------------
// <copyright file="BundleConfig.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Quinn.Vo" date="2014.3.6">Created</change>
//   <change who="Bradley.Mac" date="2014.4.2">Add Bundles</change>
// </history>

namespace NexEPA
{
    using System.Web.Optimization;

    /// <summary>
    /// Bundle Config.
    /// </summary>
    public class BundleConfig
    {
        /// <summary>
        /// Register Bundles.
        /// </summary>
        /// <param name="bundles">Bundle Collection.</param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/Common/toastr.css",
                "~/Content/Common/bootstrap.css",
                "~/Content/Common/bootstrap-theme.css",
                "~/Content/Site/layout.less",
                "~/Content/Site/Employee/employee.less",
                "~/Content/Site/Projects/project.less",
                "~/Content/Site/KPI/KPI.less"));

            bundles.Add(new ScriptBundle("~/bundles/vendor").Include(
                "~/Scripts/Libs/jquery-2.1.0.js",
                "~/Scripts/Libs/bootstrap.js",
                "~/Scripts/Libs/angular.js",
                "~/Scripts/Libs/ui-bootstrap-tpls-0.10.0.js",
                "~/Scripts/Libs/angular-resource.js",
                "~/Scripts/Libs/angular-route.js",
                "~/Scripts/Libs/toastr.js",
                "~/Scripts/Libs/angular-translate.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/Scripts/registration-module.js",
                "~/Scripts/Views/Resource/resource-repository.js",
                "~/Scripts/Views/Resource/resource-controller.js",
                "~/Scripts/Views/Employee/employee-controller.js",
                "~/Scripts/Views/Employee/employee-repository.js",
                "~/Scripts/Views/Projects/projects-controller.js",
                "~/Scripts/Views/Projects/projects-repository.js",
                "~/Scripts/Views/ProjectMember/projectmembers-controller.js",
                "~/Scripts/Views/ProjectMember/projectmembers-repository.js",
                "~/Scripts/Extensions/Filters/projectstatus-filter.js",
                "~/Scripts/Extensions/Services/projectsetting-service.js",
                "~/Scripts/Extensions/CommonFactories/notification-factory.js",
                "~/Scripts/Views/KPI/KPIGroup/KPIGroup-controller.js",
                "~/Scripts/Views/KPI/KPIGroup/KPIGroup-repository.js",
                "~/Scripts/Views/KPI/KPI/KPI-controller.js",
                "~/Scripts/Views/KPI/KPI/KPI-repository.js",
                "~/Scripts/Views/KPI/KPICommon.js"));
        }
    }
}