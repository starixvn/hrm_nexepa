'use strict';
/* global targetTemplateModule */

targetTemplateModule.factory('targetEmployeeSettingFactory', ['$http', function ($http) {
	return {
		getEPAType: function () {
			return $http.get('/api/TargetTemplate/GetEPAType');
		},
		getTargetEmployeeSetting: function (epaTypeId, departmentId) {
			return $http.get('/api/TargetTemplate/GetTargetEmployeeSetting',
				{ params: { epaTypeId: epaTypeId, departmentId: departmentId} });
		},
		updateTargetEmployeeSetting: function (targetTemplate) {
			return $http.put('/api/TargetTemplate/UpdateTargetEmployeeSetting', targetTemplate);
		}
	};
} ]);
