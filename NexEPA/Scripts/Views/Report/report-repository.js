'use strict';
/*global reportModule*/

reportModule.factory('reportFactory', ['$http', function ($http) {
	return {
		getEmployeePerformanceTendencyReport: function (employeeId, fromDate, toDate) {
			return $http.get('/api/Report/GetEmployeePerformanceTendencyReport', {
				params: {
					employeeId: employeeId,
					fromDate: fromDate.toDateString(),
					toDate: toDate.toDateString()
				}
			});
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
		},
		getRedminePersonalEffort: function (employeeId, fromDate, toDate) {
			return $http.get('/api/Report/GetRedminePersonalEffort', {
				params: {
					employeeId: employeeId,
					fromDate: fromDate.toDateString(),
					toDate: toDate.toDateString()
				}
			});
		},
		getRedminePersonalEffortDetail: function (employeeId, fromDate, toDate) {
			return $http.get('/api/Report/GetRedminePersonalEffortDetail', {
				params: {
					employeeId: employeeId,
					fromDate: fromDate.toDateString(),
					toDate: toDate.toDateString()
				}
			});
		}
	};
} ]);