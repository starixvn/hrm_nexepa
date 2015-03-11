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
		getEmployeePerformanceByTeamReport: function (filter) {
			return $http.get('/api/Report/GetEmployeePerformanceByTeamReport', {
				params: {
					fromDate: filter.fromDate.toDateString(),
					toDate: filter.toDate.toDateString(),
					employeeIds: filter.employeeIds,
					groupId: filter.groupId ? filter.groupId : '',
					projectId: filter.projectId ? filter.projectId : ''
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
		},
		getRedmineTeamEffort: function (filter) {
			return $http.get('/api/Report/GetRedmineTeamEffort', {
				params: {
					fromDate: filter.fromDate.toDateString(),
					toDate: filter.toDate.toDateString(),
					nickNames: filter.nickNames,
					projectId: filter.projectId ? filter.projectId : ''
				}
			});
		},
		checkTheRightToViewTeamEPAReport: function () {
			return $http.get('/api/Report/CheckTheRightToViewTeamEPAReport');
		},
		checkTheRightToViewTeamEffortReport: function () {
			return $http.get('/api/Report/CheckTheRightToViewTeamEffortReport');
		}
	};
} ]);