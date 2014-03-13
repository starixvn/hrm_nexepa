/* exported registrationModule */
'use strict';

var registrationModule = angular.module("registrationModule", [])
    .config(function ($routeProvider, $locationProvider) {
        $routeProvider.when('/Employee/Index', { templateUrl: '../templates/employee/employees.html', controller: 'EmployeeController' });
        $routeProvider.when('/Group/Index', { templateUrl: '../templates/group/groups.html', controller: 'GroupController' });
        $routeProvider.when('/Employee/Create', { templateUrl: '../templates/employee/create.html', controller: 'EmployeeController' });
        $locationProvider.html5Mode(true);
    });