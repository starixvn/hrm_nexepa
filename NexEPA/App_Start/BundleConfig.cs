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
    using System.Diagnostics.CodeAnalysis;
    using System.Web.Optimization;

    /// <summary>
    /// Bundle Config.
    /// </summary>
    public static class BundleConfig
    {
        /// <summary>
        /// Register Bundles.
        /// </summary>
        /// <param name="bundles">Bundle Collection.</param>
        [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Reviewed. Suppression is OK here.")]
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/Common/css").Include(
                "~/Content/Common/toastr.css",
                "~/Content/Common/bootstrap.css",
                "~/Content/Common/bootstrap-theme.css",
                "~/Content/Common/angular-multi-select.css",
                "~/Content/Common/ladda-themeless.min.css",
                "~/Content/Common/jquery-ui-1.10.4.custom.min.css",
                "~/Content/Common/Fanex.Grid.css",
                "~/Content/Common/Fanex.Pagination.css",
                "~/Content/Common/chosen.min.css",
                "~/Content/Common/scrollable-table.css"));

            bundles.AddAndInclude(
                "~/EPA/Less",
                "~/Content/Site/layout.less",
                "~/Content/Site/Employee/employee.less",
                "~/Content/Site/Projects/project.less",
                "~/Content/Site/KPI/KPI.less",
                "~/Content/Site/Target/target.less",
                "~/Content/Site/EPA/EPA.less",
                "~/Content/Site/Home/home.less",
                "~/Content/Site/Report/report.less",
                "~/Content/Site/Admin/department.less",
                "~/Content/Site/Admin/flowmanagement.less",
                "~/Content/Site/Authority/authority.less",
                "~/Content/Site/AppraisalForm/appraisalForm.less",
                "~/Content/Site/GroupProject/groupProject.less");

            bundles.Add(new ScriptBundle("~/bundles/vendor").Include(
                "~/Scripts/Libs/jquery-2.1.0.js",
                "~/Scripts/Libs/jquery-ui-1.10.4.custom.min.js",
                "~/Scripts/Libs/highcharts.js",
                "~/Scripts/Libs/highcharts-exporting.js",
                "~/Scripts/Libs/bootstrap.js",
                "~/Scripts/Libs/angular.js",
                "~/Scripts/Libs/ui-bootstrap-tpls-0.10.0.js",
                "~/Scripts/Libs/angular-multi-select.js",
                "~/Scripts/Libs/angular-resource.js",
                "~/Scripts/Libs/angular-route.js",
                "~/Scripts/Libs/toastr.js",
                "~/Scripts/Libs/angular-translate.js",
                "~/Scripts/Libs/spin.min.js",
                "~/Scripts/Libs/ladda.min.js",
                "~/Scripts/Libs/Fanex.Grid.js",
                "~/Scripts/Libs/moment.min.js",
                "~/Scripts/Libs/numeral.min.js",
                "~/Scripts/Libs/Fanex.Pagination.js",
                "~/Scripts/Libs/chosen.jquery.min.js",
                "~/Scripts/Libs/jquery-dateFormat.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/Scripts/registration-module.js",
                "~/Scripts/Views/Resource/resource-repository.js",
                "~/Scripts/Views/Resource/resource-controller.js",
                "~/Scripts/Views/Employee/employee-controller.js",
                "~/Scripts/Views/Employee/employee-repository.js",
                "~/Scripts/Views/Projects/projects-controller.js",                
                "~/Scripts/Views/Projects/syncRedmineProject-controller.js",
                "~/Scripts/Views/Projects/viewProjectMember-controller.js",
                "~/Scripts/Views/Projects/syncRedmineProject-directive.js",
                "~/Scripts/Views/Projects/projects-repository.js",
                "~/Scripts/Views/ProjectMember/projectmember-controller.js",
                "~/Scripts/Views/ProjectMember/projectmember-repository.js",
                "~/Scripts/Extensions/Common/modalconfirm-controller.js",
                "~/Scripts/Views/KPI/KPIGroup/KPIGroup-controller.js",
                "~/Scripts/Views/KPI/KPIGroup/KPIGroup-repository.js",
                "~/Scripts/Views/KPI/KPI/KPI-controller.js",
                "~/Scripts/Views/KPI/KPI/KPI-repository.js",
                "~/Scripts/Views/TargetTemplate/targettemplate-controller.js",
                "~/Scripts/Views/TargetTemplate/targettemplate-repository.js",
                "~/Scripts/Views/Target/syncRedmineTarget-controller.js",
                "~/Scripts/Views/Target/target-controller.js",
                "~/Scripts/Views/Target/target-repository.js",
                "~/Scripts/Views/Target/target-directive.js",
                "~/Scripts/Views/EPA/myepa-controller.js",
                "~/Scripts/Views/EPA/myepa-repository.js",
                "~/Scripts/Views/EPA/myepa-directive.js",
                "~/Scripts/Views/Home/home-controller.js",
                "~/Scripts/Views/Home/home-repository.js",
                "~/Scripts/Views/Report/report-repository.js",
                "~/Scripts/Views/Report/teamEPAReport-controller.js",
                "~/Scripts/Views/Report/teamEffortReport-controller.js",
                "~/Scripts/Views/Report/employeePersonReport-controller.js",
                "~/Scripts/Views/Report/resourcePlanningReport-controller.js",
                "~/Scripts/Views/Report/resourcePlanningReport-directive.js",
                "~/Scripts/Views/Setting/generalsetting-controller.js",
                "~/Scripts/Views/Setting/generalsetting-repository.js",
                "~/Scripts/Views/Test/test-controller.js",
                "~/Scripts/Views/Test/test-repository.js",
                "~/Scripts/Views/Department/department-controller.js",
                "~/Scripts/Views/Department/department-repository.js",
                "~/Scripts/Views/Authority/authority-controller.js",
                "~/Scripts/Views/Authority/authority-repository.js",
                "~/Scripts/Views/TargetTemplate/targetEmployeeSetting-controller.js",
                "~/Scripts/Views/TargetTemplate/targetEmployeeSetting-repository.js",
                "~/Scripts/Views/FlowManagement/flowmanagement-controller.js",
                "~/Scripts/Views/FlowManagement/flowmanagement-repository.js",
                "~/Scripts/Views/AppraisalForm/appraisalForm-controller.js",
                "~/Scripts/Views/AppraisalForm/appraisalForm-repository.js",
                "~/Scripts/Views/GroupProject/groupProject-repository.js",
                "~/Scripts/Views/GroupProject/groupProject-controller.js",
                "~/Scripts/Views/GroupProject/groupProject-directive.js"));

            bundles.Add(new ScriptBundle("~/bundles/epa-extension").Include(
                "~/Scripts/Extensions/Filters/epa-filter.js",
                "~/Scripts/Extensions/Directives/epa-directive.js",
                 "~/Scripts/Extensions/Services/projectsetting-service.js",
                 "~/Scripts/Extensions/Services/error-service.js",
                 "~/Scripts/Extensions/Common/notification-factory.js",
                 "~/Scripts/Extensions/Common/common-factory.js",
                 "~/Scripts/Views/KPI/KPICommon.js"));
        }
    }
}