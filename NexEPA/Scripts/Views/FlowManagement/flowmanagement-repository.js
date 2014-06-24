/* global flowManagementModule */
'use strict';

flowManagementModule.factory('flowDataFactory', ['$http', '$q', function ($http, $q) {
	return {
		getFlow: function (departmentId, flowType) {
			var deferred = $q.defer();
			$http.get('/api/FlowManagement/GetFlow', { params: {
				departmentId: departmentId,
				flowType: flowType
			}
			}).success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		},
		saveFlow: function (model) {
			var deferred = $q.defer();
			$http.post('/api/FlowManagement/InsertFlow', model).success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		},
		deleteFlow: function (departmentId, flowType) {
			var deferred = $q.defer();
			$http['delete']('/api/FlowManagement/DeleteFlow', { params: {
				departmentId: departmentId,
				flowType: flowType
			}
			}).success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		},
		getEmployeePersonalFlow: function(departmentId){
			var deferred = $q.defer();
			$http.get('/api/FlowManagement/EmployeePersonalFlow', { params: {
				departmentId: departmentId
			}
			}).success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		}
	};
} ]);