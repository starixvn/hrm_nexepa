/* global employeeModule */
'use strict';

employeeModule.factory('employeeRepository', ['$http', function ($http) {
	return {
		getAllEmployee: function (keyword, groupId) {
			return $http.get('/api/Employee/GetAllEmployee/', { params: { keyword: keyword, groupId: groupId} });
		},
		getAllGroup: function () {
			return $http.get('/api/Employee/GetAllGroup');
		},
		getEmployeeByProjectId: function (projectId, keyword) {
			return $http.get('/api/Employee/GetEmployeeByProjectId', { params: { projectId: projectId, keyword: keyword} });
		}
	};
} ]);

employeeModule.factory('employeeCacheFactory', ['$cacheFactory', function ($cacheFactory) {
	return $cacheFactory("EmployeeCache");
} ]);