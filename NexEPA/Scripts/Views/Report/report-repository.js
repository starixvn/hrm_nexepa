'use strict';
/*global reportModule*/

reportModule.factory('reportFactory', ['$http', function ($http) {
	return {
		getEmployeePerformanceTendencyReport: function (employeeId) {
			return $http.get('/api/Report/GetEmployeePerformanceTendencyReport', { params: { employeeId: employeeId } });
		}
	};
} ]);