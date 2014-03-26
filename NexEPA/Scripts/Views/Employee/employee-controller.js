/* global registrationModule */
'use strict';

registrationModule.controller("EmployeeController", function ($scope, $translate, employeeRepository) {
    $scope.get = function () {
        employeeRepository.get().then(function (employees) {
            $scope.employees = employees;
        });
    };

    $scope.save = function (employee) {
        employeeRepository.save(employee);
    };

    $scope.changeLanguage = function (langKey) {
        $translate.use(langKey);
    };
});
