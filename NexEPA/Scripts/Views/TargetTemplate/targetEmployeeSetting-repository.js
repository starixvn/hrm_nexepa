'use strict';
/* global targetTemplateModule */

targetTemplateModule.factory('targetEmployeeSettingFactory', ['$http', function ($http) {
	return {
		getEPAType: function () {
			return $http.get('/api/TargetTemplate/GetEPAType');
		},
		getTargetEmployeeSetting: function (epaTypeId, departmentId, groupId) {
			return $http.get('/api/TargetTemplate/GetTargetEmployeeSetting',
				{ params: { epaTypeId: epaTypeId, departmentId: departmentId, groupId: groupId} });
		},
		updateTargetEmployeeSetting: function (targetTemplate) {
			return $http.put('/api/TargetTemplate/UpdateTargetEmployeeSetting', targetTemplate);
		},
		getPermissionTargetEmployeeSetting: function () {
			return $http.get('/api/TargetTemplate/GetPermissionTargetEmployeeSetting');
		}
	};
} ]);
