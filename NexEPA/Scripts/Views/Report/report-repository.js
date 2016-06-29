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
        getRedminePersonalEffort: function (employeeId, fromDate, toDate, isFilterGroup, projectIds, groupProjectId) {
            return $http.get('/api/Report/GetRedminePersonalEffort', {
                params: {
                    employeeId: employeeId,
                    fromDate: fromDate.toDateString(),
                    toDate: toDate.toDateString(),
                    isFilterGroup: isFilterGroup,
                    projectIds: projectIds,
                    groupProjectId: groupProjectId
                }
            });
        },
        getRedminePersonalEffortDetail: function (employeeId, fromDate, toDate, isFilterGroup, projectIds, groupProjectId) {
            return $http.get('/api/Report/GetRedminePersonalEffortDetail', {
                params: {
                    employeeId: employeeId,
                    fromDate: fromDate.toDateString(),
                    toDate: toDate.toDateString(),
                    isFilterGroup: isFilterGroup,
                    projectIds: projectIds,
                    groupProjectId: groupProjectId
                }
            });
        },
        getRedmineTeamEffort: function (filter) {
            var criteria = {
                fromDate: filter.fromDate.toDateString(),
                toDate: filter.toDate.toDateString(),
                nickName: filter.nickNames,
                isFilterGroup: filter.isFilterGroup,
                projectIds: filter.projectIds ? filter.projectIds : '',
                groupProjectId: filter.groupProjectId ? filter.groupProjectId : ''
            };

            return $http.post('/api/Report/GetRedmineTeamEffort', criteria);
        },
        checkTheRightToViewTeamEPAReport: function () {
            return $http.get('/api/Report/CheckTheRightToViewTeamEPAReport');
        },
        checkTheRightToViewTeamEffortReport: function () {
            return $http.get('/api/Report/CheckTheRightToViewTeamEffortReport');
        },
        getRedmineResourcePlanning: function (filter) {
            return $http.get('/api/Report/GetRedmineResourcePlanning', {
                params: {
                    fromDate: filter.fromDate.toDateString(),
                    toDate: filter.toDate.toDateString(),
                    nickNames: filter.nickNames,
                    isFilterGroup: filter.isFilterGroup,
                    projectIds: filter.projectIds ? filter.projectIds : '',
                    groupProjectId: filter.groupProjectId ? filter.groupProjectId : ''
                }
            });
        }
    };
}]);