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
		},
		deleteDepartment: function (departmentId) {
			var deferred = $q.defer();
			var response = {};
			$http['delete']('/api/Department/', { params: { departmentId: departmentId} })
			.success(function (data, status) {
				deferred.resolve(response = { data: data, status: status });
			}).error(function (data, status) {
				deferred.reject(response = { data: data, status: status });
			});
			return deferred.promise;
		}
	};
} ]);