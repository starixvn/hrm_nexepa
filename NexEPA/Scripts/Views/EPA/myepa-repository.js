/* global epaModule */
'use strict';

epaModule.factory('employeeEPAFactory', ['$http', '$q', function ($http, $q) {
	return {
		getMyEPA: function (targetId, employeeId) {
			var deferred = $q.defer();
			$http.get('/api/EmployeeEPA/GetMyEPA', {
				params: { targetId: targetId, employeeId: employeeId }
			}).success(deferred.resolve).error(function (data, status) {
				deferred.reject(status);
			});
			return deferred.promise;
		},
		getEmployeeEPA: function (targetId, employeeId) {
			var deferred = $q.defer();
			$http.get('/api/EmployeeEPA/GetEmployeeEPA', {
				params: { targetId: targetId, employeeId: employeeId }
			}).success(deferred.resolve).error(function (data, status) {
				deferred.reject(status);
			});
			return deferred.promise;
		},
		addEmployeeEPA: function (employeeEPAResult) {
			var deferred = $q.defer();
			$http.post('/api/EmployeeEPA/AddEmployeeEPA', employeeEPAResult).success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		},
		getEPAOfCurrentMember: function (projectId, status) {
			var deferred = $q.defer();
			$http.get('/api/EmployeeEPA/GetEPAOfCurrentMember',
					{ params: { projectId: projectId,
						status: status
					}
					}).success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		},
		getAllProject: function () {
			var deferred = $q.defer();
			$http.get('/api/Core/GetAllProject').success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		},
		getAllStatus: function () {
			var deferred = $q.defer();
			$http.get('/api/Core/GetAllStatus').success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		},
		getEPAOfEmployeeByManager: function (targetId, projectId, status, employeeId) {
			var deferred = $q.defer();
			$http.get('/api/EmployeeEPA/GetEPAOfEmployeeByManager',
					{ params: { targetId: targetId,
						projectId: projectId,
						status: status,
						employeeId: employeeId
					}
					}).success(deferred.resolve).error(function (response, status) {
						deferred.reject(status);
					});
			return deferred.promise;
		},
		updateEPAStatus: function (employeeEPA) {
			var deferred = $q.defer();
			$http.post('/api/EmployeeEPA/UpdateEPAStatus', employeeEPA)
			.success(deferred.resolve).error(function (data, status) {
				deferred.reject(status);
			});
			return deferred.promise;
		},
		getTargetToFilter: function () {
			var deferred = $q.defer();
			$http.get('/api/Target/GetTargetToFilter').success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		},
		getCurrentUser: function () {
			var deferred = $q.defer();
			$http.get('/api/core/GetCurrentUser').success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		}

	};
} ]);