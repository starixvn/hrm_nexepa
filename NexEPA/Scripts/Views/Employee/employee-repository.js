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
		},
		getEmployeeByCurrentUserId: function (keyword) {
			return $http.get('/api/Employee/GetEmployeeByCurrentUserId', { params: { keyword: keyword} });
		},
		getEmployeeById: function (employeeId) {
			return $http.get('/api/Employee/GetEmployeeById', { params: { employeeId: employeeId} });
		},
		getEmployeeInDepartment: function (departmentId) {
			return $http.get('/api/Employee/GetEmployeeInDepartment', { params: { departmentId: departmentId} });
		},
		getEmployeeByTeamProject: function (groupId, projectId) {
			return $http.get('/api/Employee/GetEmployeeByTeamProject', {
				params: {
					groupId: groupId ? groupId : '',
					projectId: projectId ? projectId : ''
				}
			});
		}
	};
} ]);

employeeModule.factory('employeeCacheFactory', ['$cacheFactory', function ($cacheFactory) {
	return $cacheFactory("EmployeeCache");
} ]);