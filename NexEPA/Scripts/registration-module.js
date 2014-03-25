/* exported registrationModule, projectsSettingModule, projectService*/
/*global $, angular*/

'use strict';

var projectsSettingModule = angular.module("projectsSettingModule", ['ngRoute', 'ngResource'])
.config(function ($routeProvider) {
    $routeProvider.when('/Projects/Index', { templateUrl: '../templates/Projects/Index.html', controller: 'ProjectController' });
    $routeProvider.when('/Projects/Create', { templateUrl: '../templates/Projects/Create.html', controller: 'CreateProjectController' });
    $routeProvider.when('/Projects/Edit', { templateUrl: '../templates/Projects/Edit.html', controller: 'UpdateProjectController' });
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

