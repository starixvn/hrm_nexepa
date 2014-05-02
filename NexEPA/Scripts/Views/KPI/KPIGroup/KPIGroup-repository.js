'use strict';
/*global kpiSettingModule*/

kpiSettingModule.factory('KPIGroupRepository', ['$http', function ($http) {
	var url = '/api/KPIGroup/';

	return {
		getAll: function (targetTypeId, isDisabled) {
			return $http.get('/api/KPIGroup/GetAll', 
				{ params: { targetTypeId: targetTypeId, isDisabled: isDisabled} });
		},
		getGroupById: function (groupId) {
			return $http.get('/api/KPIGroup/GetUserById', { params: { id: groupId} });
		},
		addGroup: function (group) {
			return $http.post(url, group);
		},
		updateGroup: function (group) {
			return $http.put('/api/KPIGroup/Put', group);
		},
		deleteGroup: function (groupId) {
			return $http['delete'](url, { params: { id: groupId} });
		},
		updateGroupStatus: function (group) {
			return $http.put('/api/KPIGroup/UpdateKPIGroupStatus', group);
		}
	};
} ]);