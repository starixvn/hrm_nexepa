/* global epaModule */
'use strict';

epaModule.factory('employeeEPAFactory', ['$http', '$q', function ($http, $q) {
	return {
		getEmployeeEPA: function (targetId) {
			var deferred = $q.defer();
			$http.get('/api/EmployeeEPA/GetMyEPA', {
				params: { targetId: targetId }
			}).success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		},
		addEmployeeEPA: function (employeeEPAResult) {
			var deferred = $q.defer();
			$http.post('/api/EmployeeEPA/', employeeEPAResult).success(deferred.resolve).error(deferred.reject);
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
		getAllStatus: function() {
			var deferred = $q.defer();
			$http.get('/api/Core/GetAllStatus').success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		},
		getEPAOfEmployeeByManager: function (projectId, status) {
			var deferred = $q.defer();
			$http.get('/api/EmployeeEPA/GetEPAOfEmployeeByManager',
					{ params: { projectId: projectId,
						status: status
					}
					}).success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		}
	};
} ]);