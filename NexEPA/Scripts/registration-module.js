/* exported registrationModule, projectsSettingModule, resourceModule, projectService*/
/*global $, angular, toastr*/

'use strict';

var projectsSettingModule = angular.module("projectsSettingModule", ['ngRoute', 'ngResource'])
.config(function ($routeProvider) {
    $routeProvider.when('/Projects/Index', { templateUrl: '../templates/Projects/Index.html', controller: 'ProjectController' });
    $routeProvider.when('/Projects/Create', { templateUrl: '../templates/Projects/Create.html', controller: 'ProjectController' });
    $routeProvider.when('/Projects/Edit/:projectId', { templateUrl: '/templates/Projects/Edit.html', controller: 'ProjectController' });
    $routeProvider.when('/ProjectMembers/View/:projectId', { templateUrl: '/templates/ProjectMembers/Index.html', controller: 'ProjectMembersController' });
    $routeProvider.when('/ProjectMembers/Create', { templateUrl: '../templates/ProjectMembers/Create.html', controller: 'ProjectMembersController' });
});

var resourceModule = angular.module('resourceModule', ['pascalprecht.translate'])
  .config(function ($translateProvider) {
      $translateProvider.useLoader('resourceRepository', {});
      $translateProvider.preferredLanguage('en');
  });

var registrationModule = angular.module("registrationModule", ['resourceModule', 'projectsSettingModule'])
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
                    //showOn: "button",
                    //buttonImage: "../../Content/Common/Images/calendar.png",
                    //buttonImageOnly: true,
                    onSelect: function (date) {
                        scope.$apply(function () {
                            ngModelCtrl.$setViewValue(date);
                        });
                    }
                });
            });
        }
    };
});

registrationModule.directive('datepicker1', function ($parse) {
    return {
        restrict: "A",
        replace: true,
        transclude: false,
        compile: function (element, attrs) {
            var modelAccessor = $parse(attrs.ngModel);
            return function (scope, element) {
                var processChange = function () {
                    var date = element.val();
                    scope.$apply(function (scope) {
                        modelAccessor.assign(scope, date);
                        element.blur();
                    });
                };
                element.datepicker({
                    dateFormat: 'dd-M-yy',
                    onClose: processChange,
                    onSelect: processChange
                });
                scope.$watch(modelAccessor, function (val) {
                    var date = new Date(val);
                    element.datepicker("setDate", date);
                });
            };

        }
    };
});

registrationModule.filter('enumStatusText', function () {
    var statusEnum = [{ text: "Active", value: 1 }, { text: "Closed", value: 2}];
    return function (input) {
        var statusText;
        $.each(statusEnum, function (i, val) {
            if (statusEnum[i].value === input) {
                statusText = val.text;
            }
        });
        return statusText;
    };
});

projectsSettingModule.service('projectService', function () {
    var property;
    return {
        getProperty: function () {
            return property;
        },
        setProperty: function (value) {
            property = value;
        }
    };
});

projectsSettingModule.factory('notificationFactory', function () {

    return {
        success: function () {
            toastr.success("Success");
        },
        error: function (text) {
            toastr.error(text, "Error!");
        }
    };
});
