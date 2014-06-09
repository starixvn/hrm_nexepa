/* global departmentModule */
'use strict';

departmentModule.factory('departmentDataFactory', ['$http', '$q', function ($http, $q) {
	return {
		getAllDepartment: function () {
			var deferred = $q.defer();
			$http.get('/api/Department/GetAllDeparment').success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		},
		addDepartment: function (model) {
			var deferred = $q.defer();
			$http.post('/api/Department/AddDepartment', model).success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		},
		updateDepartment: function (model) {
			var deferred = $q.defer();
			$http.post('/api/Department/UpdateDepartment', model).success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		}
	};
} ]);