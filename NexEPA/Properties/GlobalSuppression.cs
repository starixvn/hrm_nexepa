// -----------------------------------------------------------------------
// <copyright file="GlobalSuppression.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Josh.Phan" date="2014.8.8">Created.</change>
// </history>

using System.Diagnostics.CodeAnalysis;

[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member",
    Target = "NexEPA.Controllers.API.AuthorityController.#GetAllSystemRole()", Justification = "Reviewed. Suppression is OK here.")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member",
    Target = "NexEPA.Controllers.API.AuthorityController.#GetManageRolePermission()", Justification = "Reviewed. Suppression is OK here.")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member",
    Target = "NexEPA.Controllers.API.AuthorityController.#GetManageUserPermission()", Justification = "Reviewed. Suppression is OK here.")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member",
    Target = "NexEPA.Controllers.API.CoreController.#GetAllStatus()", Justification = "Reviewed. Suppression is OK here.")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member",
    Target = "NexEPA.Controllers.API.CoreController.#GetCurrentUser()", Justification = "Reviewed. Suppression is OK here.")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member",
    Target = "NexEPA.Controllers.API.CoreController.#GetTargetTypeExceptAll()", Justification = "Reviewed. Suppression is OK here.")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member",
    Target = "NexEPA.Controllers.API.DepartmentController.#GetAllDeparment()", Justification = "Reviewed. Suppression is OK here.")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member",
    Target = "NexEPA.Controllers.API.EmployeeController.#GetAllGroup()", Justification = "Reviewed. Suppression is OK here.")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member",
    Target = "NexEPA.Controllers.API.EmployeeEPAController.#GetAllProcessPeriod()", Justification = "Reviewed. Suppression is OK here.")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member",
    Target = "NexEPA.Controllers.API.EmployeeEPAController.#GetAppraisalFormPermission()", Justification = "Reviewed. Suppression is OK here.")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member",
    Target = "NexEPA.Controllers.API.EmployeeEPAController.#GetPermission()", Justification = "Reviewed. Suppression is OK here.")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member",
    Target = "NexEPA.Controllers.API.HomeController.#GetCurrentEmployeeId()", Justification = "Reviewed. Suppression is OK here.")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member",
    Target = "NexEPA.Controllers.API.HomeController.#GetGeneralInformationOfCurrentEmployee()", Justification = "Reviewed. Suppression is OK here.")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member",
    Target = "NexEPA.Controllers.API.KPIController.#GetPermission()", Justification = "Reviewed. Suppression is OK here.")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member",
    Target = "NexEPA.Controllers.API.KPIGroupController.#GetPermission()", Justification = "Reviewed. Suppression is OK here.")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member",
    Target = "NexEPA.Controllers.API.ProjectController.#GetPermission()", Justification = "Reviewed. Suppression is OK here.")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member",
    Target = "NexEPA.Controllers.API.TargetTemplateController.#GetAllTargetType()", Justification = "Reviewed. Suppression is OK here.")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member",
    Target = "NexEPA.Controllers.API.TargetTemplateController.#GetEPAType()", Justification = "Reviewed. Suppression is OK here.")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member",
    Target = "NexEPA.Controllers.API.TargetTemplateController.#GetPermissionTargetEmployeeSetting()", Justification = "Reviewed. Suppression is OK here.")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member",
    Target = "NexEPA.Controllers.API.TargetTemplateController.#GetPermissionTargetTemplate()", Justification = "Reviewed. Suppression is OK here.")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member",
    Target = "NexEPA.Controllers.API.TargetTemplateController.#GetTargetSetting()", Justification = "Reviewed. Suppression is OK here.")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member",
    Target = "NexEPA.Controllers.API.TestController.#GetIsSimulatedLoginUser()", Justification = "Reviewed. Suppression is OK here.")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member",
    Target = "NexEPA.Controllers.API.TargetController.#GetPermission()", Justification = "Reviewed. Suppression is OK here.")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member",
    Target = "NexEPA.Controllers.API.TargetController.#GetTargetToFilter()", Justification = "Reviewed. Suppression is OK here.")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member",
    Target = "NexEPA.Controllers.API.EmployeeEPAController.#GetIsShowFullEPAComment()", Justification = "Reviewed. Suppression is OK here.")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member",
    Target = "NexEPA.Controllers.API.GroupProjectController.#GetAllGroupProject()", Justification = "Reviewed. Suppression is OK here.")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member",
    Target = "NexEPA.Controllers.API.GroupProjectController.#GetPermission()", Justification = "Reviewed. Suppression is OK here.")]
[module: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member",
    Target = "NexEPA.Controllers.API.GroupProjectController.#GetAllProject()", Justification = "Reviewed. Suppression is OK here.")]