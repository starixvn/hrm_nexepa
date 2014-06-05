/* exported registrationModule, projectsSettingModule, resourceModule, projectService, kpiSettingModule, targetTemplateModule, targetModule, epaModule*/
/* exported employeeModule, homeModule, reportModule, generalSettingModule, testModule, departmentModule, authorityModule*/
/*global angular*/
'use strict';

var projectsSettingModule = angular.module("projectsSettingModule", ['ngRoute', 'ngResource'])
.config(['$routeProvider', function ($routeProvider) {
	$routeProvider.when('/Projects/Index', { templateUrl: '../templates/Projects/Index.html', controller: 'ProjectController' });
	$routeProvider.when('/Projects/Create', { templateUrl: '../templates/Projects/Create.html', controller: 'CreateProjectController' });
	$routeProvider.when('/Projects/Edit/:projectId', { templateUrl: '/templates/Projects/Edit.html', controller: 'ViewDetailAndEditProjectController' });
	$routeProvider.when('/Projects/Detail/:projectId', { templateUrl: '/templates/Projects/Edit.html', controller: 'ViewDetailAndEditProjectController' });
	$routeProvider.when('/ProjectMembers/View/:projectId', { templateUrl: '/templates/ProjectMembers/Index.html', controller: 'ProjectMembersController' });
	$routeProvider.when('/ProjectMembers/Create/:projectId',
    { templateUrl: '/templates/ProjectMembers/Create.html', controller: 'AddProjectMembersController' });
	$routeProvider.when('/ProjectMembers/Edit/ProjectId=:projectId/EmployeeId=:employeeId',
     { templateUrl: '/templates/ProjectMembers/Edit.html', controller: 'EditProjectMembersController' });
} ])
.factory("ProjectSettingCache", ['$cacheFactory', function ($cacheFactory) {
	return $cacheFactory();
} ]);

var kpiSettingModule = angular.module("kpiSettingModule", ['ngRoute', 'ngResource'])
.config(['$routeProvider', function ($routeProvider) {
	$routeProvider.when('/KPI/GetAllKPIGroup', { templateUrl: '/templates/KPI/KPIGroup/ViewAll.html', controller: 'GetAllKPIGroupController' });
	$routeProvider.when('/KPI/CreateNewKPIGroup', { templateUrl: '/templates/KPI/KPIGroup/Create.html', controller: 'CreateNewKPIGroupController' });
	$routeProvider.when('/KPI/EditKPIGroup/:id', { templateUrl: '/templates/KPI/KPIGroup/Edit.html', controller: 'EditKPIGroupController' });
	$routeProvider.when('/KPI/CreateKPI/', { templateUrl: '/templates/KPI/KPI/Create.html', controller: 'CreateNewKPIController' });
	$routeProvider.when('/KPI/ViewKPI/', { templateUrl: '/templates/KPI/KPI/View.html', controller: 'ViewKPIController' });
	$routeProvider.when('/KPI/EditKPI/:id', { templateUrl: '/templates/KPI/KPI/Edit.html', controller: 'EditKPIController' });
} ]);

var targetModule = angular.module("targetModule", ['ngRoute', 'ngResource'])
.config(['$routeProvider', function ($routeProvider) {
	$routeProvider.when('/Target/ViewTarget', { templateUrl: '/templates/Target/View.html', controller: 'ViewTargetController' });
	$routeProvider.when('/Target/CreateTarget', { templateUrl: '/templates/Target/Create.html', controller: 'CreateTargetController' });
	$routeProvider.when('/Target/EditTarget/:id', { templateUrl: '/templates/Target/Edit.html', controller: 'TargetDetailAndEditController' });
	$routeProvider.when('/Target/TargetEmployee/:id', { templateUrl: '/templates/Target/Detail.html', controller: 'TargetDetailAndEditController' });
	$routeProvider.when('/Target/TargetKPI/:id', { templateUrl: '/templates/Target/Detail.html', controller: 'TargetDetailAndEditController' });
	$routeProvider.when('/Target/TargetDetail/:id', { templateUrl: '/templates/Target/Detail.html', controller: 'TargetDetailAndEditController' });
} ]);

var targetTemplateModule = angular.module("targetTemplateModule", ['ngRoute', 'ngResource'])
.config(['$routeProvider', function ($routeProvider) {
	$routeProvider.when('/TargetTemplate/Index', { templateUrl: '../templates/TargetTemplate/Index.html', controller: 'TargetTemplateController' });

	$routeProvider.when('/TargetTemplate/Create', { templateUrl: '../templates/TargetTemplate/Create.html', controller: 'CreateTargetTemplateController' });

	$routeProvider.when('/TargetTemplate/Edit/:targetTemplateId',
	{ templateUrl: '/templates/TargetTemplate/Edit.html', controller: 'EditTargetTemplateController' });

	$routeProvider.when('/TargetTemplate/Details/:targetTemplateId',
	{ templateUrl: '/templates/TargetTemplate/Details.html', controller: 'DetailTargetTemplateController' });
} ]);

var epaModule = angular.module("epaModule", ['ngRoute', 'ngResource'])
.config(['$routeProvider', function ($routeProvider) {
	$routeProvider.when('/MyEPA/Index', { templateUrl: '../templates/EPA/MyEPA/Index.html', controller: 'MyEPAController' });

	$routeProvider.when('/EmployeeEPA/Index', { templateUrl: '../templates/EPA/MyEPA/Index.html', controller: 'EmployeeEPAController' });

	$routeProvider.when('/MemberEPA/ViewCurrentMember',
		{ templateUrl: '/templates/EPA/MemberEPA/ViewCurrentMember.html', controller: 'ViewCurrentMemberEPAController' });

	$routeProvider.when('/MemberEPA/ViewMember',
		{ templateUrl: '/templates/EPA/MemberEPA/ViewAllMemberOfManager.html', controller: 'ViewMemberByManagerEPAController' });
} ]);

var generalSettingModule = angular.module("generalSettingModule", ['ngRoute', 'ngResource'])
.config(['$routeProvider', function ($routeProvider) {
	$routeProvider.when('/Setting/Index', { templateUrl: '../templates/Setting/Index.html', controller: 'GeneralSettingController' });
} ]);

var employeeModule = angular.module("employeeModule", ['ngRoute', 'ngResource'])
.config(['$routeProvider', function ($routeProvider) {
	$routeProvider.when('/Employee/GetAllEmployee', { templateUrl: '/templates/Employee/View.html', controller: 'ViewAllEmployeeController' });
} ]);

var homeModule = angular.module("homeModule", ['ngRoute', 'ngResource'])
.config(['$routeProvider', function ($routeProvider) {
	$routeProvider.when('/', { templateUrl: '/templates/Home/View.html', controller: 'ViewHomePageController' });
} ]);

var reportModule = angular.module("reportModule", ['ngRoute', 'ngResource'])
.config(['$routeProvider', function ($routeProvider) {
	$routeProvider.when('/Report/EmployeePerformanceTendency', 
		{ templateUrl: '/templates/Report/EmployeePerformanceTendency/View.html', controller: 'ViewEmployeePerformanceTendencyController' });
	$routeProvider.when('/Report/EmployeePerformanceByTeam',
		{ templateUrl: '/templates/Report/EmployeePerformanceByTeam/View.html', controller: 'ViewEmployeePerformanceByTeamController' });
} ]);

var departmentModule = angular.module("departmentModule", ['ngRoute', 'ngResource'])
.config(['$routeProvider', function ($routeProvider) {
	$routeProvider.when('/Department/Index', { templateUrl: '/templates/Organization/Department/Index.html', controller: 'ManageDepartmentController' });
} ]);

var authorityModule = angular.module("authorityModule", ['ngRoute', 'ngResource'])
.config(['$routeProvider', function ($routeProvider) {
	$routeProvider.when('/Authority/Index',
		{ templateUrl: '/templates/Authority/Index.html', controller: 'AssignPermissionController' });
} ]);

var testModule = angular.module("testModule", ['ngRoute', 'ngResource'])
.config(['$routeProvider', function ($routeProvider) {
	$routeProvider.when('/Test/LoginAsAnotherUser',
		{ templateUrl: '/templates/Test/LoginAsAnotherUser.html', controller: 'LoginAsAnotherUserController' });
} ]);

var registrationModule = angular.module("registrationModule",
['projectsSettingModule', 'kpiSettingModule', 'targetTemplateModule', 'targetModule', 'employeeModule', 'generalSettingModule',
'epaModule', 'homeModule', 'reportModule', 'testModule', 'departmentModule', 'authorityModule', 'ui.bootstrap', 'ngRoute', 
'ngResource', 'pascalprecht.translate'])
.config(['$routeProvider', '$locationProvider', '$compileProvider', '$translateProvider', 'datepickerConfig', 'datepickerPopupConfig',
function ($routeProvider, $locationProvider, $compileProvider, $translateProvider, datepickerConfig, datepickerPopupConfig) {
	$translateProvider.useLoader('resourceRepository', {});
	$translateProvider.preferredLanguage('en');
	$routeProvider.when('/Home/AccessDenied', { templateUrl: '/templates/Common/AccessDenied.html' });
	$compileProvider.aHrefSanitizationWhitelist(/^\s*(https?|ftp|mailto|skype):/);
	datepickerConfig.showWeeks = false;
	datepickerPopupConfig.showButtonBar = false;
	$locationProvider.html5Mode(true);
} ]);