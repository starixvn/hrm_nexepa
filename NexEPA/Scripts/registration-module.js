/* exported registrationModule */
'use strict';

var projectsSettingModule = angular.module("projectsSettingModule", ['ngRoute', 'ngResource'])
.config(function ($routeProvider, $locationProvider) {
    $routeProvider.when('/Projects/Index', { templateUrl: '../templates/Projects/Index.html', controller: 'ProjectController' });
    $routeProvider.when('/Projects/Create', { templateUrl: '../templates/Projects/Create.html', controller: 'CreateProjectController' });
});

var registrationModule = angular.module("registrationModule", ['projectsSettingModule'])
    .config(function ($routeProvider, $locationProvider) {
        $routeProvider.when('/Employee/Index', { templateUrl: '../templates/employee/view.html', controller: 'EmployeeController' });
        $routeProvider.when('/Employee/Create', { templateUrl: '../templates/employee/create.html', controller: 'EmployeeController' });
        $locationProvider.html5Mode(true);
    });

registrationModule.directive('datepicker', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {
            $(function () {
                element.datepicker({
                    dateFormat: 'dd-M-yy',
//                    showOn: "button",
//                    buttonImage: "../../Content/Common/Images/calendar.png",
//                    buttonImageOnly: true,
                    onSelect: function (date) {
                        scope.$apply(function () {
                            ngModelCtrl.$setViewValue(date);
                        });
                    }
                });
            });
        }
    }
});

