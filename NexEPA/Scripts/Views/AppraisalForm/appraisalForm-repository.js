'use strict';
/*global epaModule*/

epaModule.factory('appraisalFormFactory', ['$http', function ($http) {
	return {
		getAppraisalForm: function (epaTypeId, keywork, year, month) {
			return $http.get('/api/EmployeeEPA/GetAppraisalForm',
				{ params: { epaTypeId: epaTypeId, year: year, month: month, keyword: keywork} });
		},
		insertAppraisalForm: function (appraisal) {
			return $http.post('/api/EmployeeEPA/InsertAppraisalForm', appraisal);
		}
	};
} ]);