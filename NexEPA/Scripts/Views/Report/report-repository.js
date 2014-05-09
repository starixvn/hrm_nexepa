'use strict';
/*global reportModule*/

reportModule.factory('reportFactory', ['$http', function ($http) {
	return {
		getEmployeePerformanceTendencyReport: function (employeeId) {
			return $http.get('/api/Report/GetEmployeePerformanceTendencyReport', { params: { employeeId: employeeId} });
		},
		getEmployeePerformanceByTeamReport: function (filter, fromDate, toDate) {
			return $http.get('/api/Report/GetEmployeePerformanceByTeamReport',
				{ params: { fromDate: fromDate,
					toDate: toDate,
					employeeId: filter.employeeId,
					groupId: filter.groupId,
					projectId: filter.projectId
				}
				});
		}
	};
} ]);