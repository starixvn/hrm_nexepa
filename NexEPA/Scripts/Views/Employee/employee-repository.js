/* global registrationModule */
'use strict';

registrationModule.factory('employeeRepository', function ($http, $q) {
    return {
        get: function () {
            var deferred = $q.defer();
            $http.get('/api/Employee/GetEmployees').success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        },
        save: function (employee) {
            $http.post('/Employee/Create', employee);
        }
    };
});