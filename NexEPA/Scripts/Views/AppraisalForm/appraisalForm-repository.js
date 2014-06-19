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
		},
		getAllProcessPeriod: function () {
			return $http.get('/api/EmployeeEPA/GetAllProcessPeriod');
		}
	};
} ]);

epaModule.factory('commonAppraisalFormFactory', function () {
	var common = {};

	common.validateAppraisalForm = function (appraisal) {
		if (appraisal) {
			if (!appraisal.epaTypeId || appraisal.epaTypeId < 0) {
				return false;
			}
			else if (!appraisal.year || !appraisal.month) {
				return false;
			}
			else if (!appraisal.employeeId || appraisal.employeeId <= 0) {
				return false;
			}
		}
		else {
			return false;
		}

		return true;
	};

	common.showRequireMessage = function (scope, notificationFactory) {
		scope.$watchCollection('[submitted,form.$invalid,form.epaType.$error.required, \
							form.year.$error.required,form.month.$error.required, \
							form.employee.$error.required,form.calendar.$error.required]',
			function (newVals) {
				if (newVals[0] && newVals[1]) {
					if (newVals[6]) {
						notificationFactory.error("DateRequire");
					}
					if (newVals[5]) {
						notificationFactory.error("EmployeeRequire");
					}
					if (newVals[4]) {
						notificationFactory.error("MonthRequire");
					}
					if (newVals[3]) {
						notificationFactory.error("YearRequire");
					}
					if (newVals[2]) {
						notificationFactory.error("EPATypeRequire");
					}
				}
			}
		);
	};

	return common;
});