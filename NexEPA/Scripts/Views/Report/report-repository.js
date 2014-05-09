'use strict';
/*global reportModule*/

reportModule.factory('reportFactory', ['$http', function ($http) {
	return {
		getEmployeePerformanceTendencyReport: function (employeeId) {
			return $http.get('/api/Report/GetEmployeePerformanceTendencyReport', { params: { employeeId: employeeId} });
		},
		getEmployeePerformanceByTeamReport: function (filter, fromDate, toDate, employeeId) {
			return $http.get('/api/Report/GetEmployeePerformanceByTeamReport',
				{ params: { fromDate: fromDate,
					toDate: toDate,
					employeeId: employeeId,
					groupId: filter.groupId,
					projectId: filter.projectId
				}
				});
		}
	};
} ]);