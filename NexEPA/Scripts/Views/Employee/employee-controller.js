/* global registrationModule */
'use strict';

registrationModule.controller("EmployeeController", function ($scope, employeeRepository) {
    $scope.get = function () {
        employeeRepository.get().then(function (employees) {
            $scope.employees = employees;
        });
    };

    $scope.save = function (employee) {
        employeeRepository.save(employee);
    };
});
