/* exported registrationModule, projectsSettingModule, resourceModule, projectService, kpiSettingModule*/
/* exported employeeModule*/
/*global angular*/
'use strict';

var projectsSettingModule = angular.module("projectsSettingModule", ['ngRoute', 'ngResource'])
.config(function ($routeProvider) {
    $routeProvider.when('/Projects/Index', { templateUrl: '../templates/Projects/Index.html', controller: 'ProjectController' });
    $routeProvider.when('/Projects/Create', { templateUrl: '../templates/Projects/Create.html', controller: 'CreateProjectController' });
    $routeProvider.when('/Projects/Edit/:projectId', { templateUrl: '/templates/Projects/Edit.html', controller: 'EditProjectController' });
    $routeProvider.when('/ProjectMembers/View/:projectId', { templateUrl: '/templates/ProjectMembers/Index.html', controller: 'ProjectMembersController' });
    $routeProvider.when('/ProjectMembers/Create', { templateUrl: '../templates/ProjectMembers/Create.html', controller: 'AddProjectMembersController' });
    $routeProvider.when('/Member/Edit/:employeeId', { templateUrl: '/templates/ProjectMembers/Edit.html', controller: 'EditProjectMembersController' });
})
.factory("ProjectSettingCache", function ($cacheFactory) {
    return $cacheFactory();
});

var resourceModule = angular.module('resourceModule', ['pascalprecht.translate'])
.config(function ($translateProvider) {
    $translateProvider.useLoader('resourceRepository', {});
    $translateProvider.preferredLanguage('en');
});

var kpiSettingModule = angular.module("kpiSettingModule", ['ngRoute', 'ngResource'])
.config(function ($routeProvider) {
	$routeProvider.when('/KPI/GetAllKPIGroup', { templateUrl: '/templates/KPI/KPIGroup/ViewAll.html', controller: 'GetAllKPIGroupController' });
	$routeProvider.when('/KPI/CreateNewKPIGroup', { templateUrl: '/templates/KPI/KPIGroup/Create.html', controller: 'CreateNewKPIGroupController' });
	$routeProvider.when('/KPI/EditKPIGroup/:id', { templateUrl: '/templates/KPI/KPIGroup/Edit.html', controller: 'EditKPIGroupController' });
	$routeProvider.when('/KPI/CreateKPI/', { templateUrl: '/templates/KPI/KPI/Create.html', controller: 'CreateNewKPIController' });
	$routeProvider.when('/KPI/ViewKPI/', { templateUrl: '/templates/KPI/KPI/View.html', controller: 'ViewKPIController' });
	$routeProvider.when('/KPI/EditKPI/:id', { templateUrl: '/templates/KPI/KPI/Edit.html', controller: 'EditKPIController' });
});

var employeeModule = angular.module("employeeModule", ['ngRoute', 'ngResource'])
.config(function ($routeProvider) {
	$routeProvider.when('/Employee/GetAllEmployee', { templateUrl: '/templates/Employee/View.html', controller: 'ViewAllEmployeeController' });
});

var registrationModule = angular.module("registrationModule", ['resourceModule', 'projectsSettingModule', 'ui.bootstrap', 'kpiSettingModule', 'employeeModule'])
.config(function ($routeProvider, $locationProvider) {
    $routeProvider.when('/Employee/Index', { templateUrl: '../templates/employee/view.html', controller: 'EmployeeController' });
    $routeProvider.when('/Employee/Create', { templateUrl: '../templates/employee/create.html', controller: 'EmployeeController' });
    $locationProvider.html5Mode(true);
});