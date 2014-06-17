'use strict';
/*global epaModule*/

epaModule.factory('appraisalFormFactory', ['$http', function ($http) {
	return {
		getAppraisalForm: function (epaTypeId, employeeId, year, month) {
			$http.get('/api/EmployeeEPA/GetAppraisalForm',
				{ params: { epaTypeId: epaTypeId, employeeId: employeeId, year: year, month: month} });
		}
	};
} ]);