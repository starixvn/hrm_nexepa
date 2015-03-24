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
			var response = {};
			$http.post('/api/EmployeeEPA/AddEmployeeEPA', employeeEPAResult)
			.success(function (data) {
				deferred.resolve(response = { data: data });
			}).error(function (data, status) {
				deferred.reject(response = { status: status });
			});
			return deferred.promise;
		},
		getEPAOfCurrentMember: function (projectId, status) {
			var deferred = $q.defer();
			$http.get('/api/EmployeeEPA/GetEPAOfCurrentMember',
					{ params: {
						projectId: projectId,
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
		getEPAOfEmployeeByManager: function (fromDate, toDate, teamId, targetId, projectId, status, employeeId, isShowAll) {
			var deferred = $q.defer();
			$http.get('/api/EmployeeEPA/GetEPAOfEmployeeByManager',
					{ params: { fromDate: fromDate.toDateString(),
						toDate: toDate ? toDate.toDateString() : '',
						teamId: teamId ? teamId : '',
						targetId: targetId ? targetId : '',
						projectId: projectId ? projectId : '',
						status: status ? status : '',
						employeeId: employeeId,
						isShowAll: isShowAll ? isShowAll : ''
					}
					}).success(deferred.resolve).error(function (response, status) {
						deferred.reject(status);
					});
			return deferred.promise;
		},
		updateEPAStatus: function (employeeEPA) {
			var deferred = $q.defer();
			var response = {};
			$http.post('/api/EmployeeEPA/UpdateEPAStatus', employeeEPA)
			.success(function (data) {
				deferred.resolve(response = { data: data });
			}).error(function (data, status) {
				deferred.reject(response = { status: status });
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
		},
		getPermission: function () {
			var deferred = $q.defer();
			$http.get('/api/EmployeeEPA/GetPermission').success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		},
		getIsShowFullEPAComment: function () {
			var deferred = $q.defer();
			$http.get('/api/EmployeeEPA/GetIsShowFullEPAComment').success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		}
	};
} ]);