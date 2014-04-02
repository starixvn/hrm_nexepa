'use strict';
/*global registrationModule*/

registrationModule.factory('KPIGroupRepository', ['$http', function ($http) {
	var url = '/api/KPIGroup/';

	return {
		getAll: function (targetTypeId) {
			return $http.get('/api/KPIGroup/GetAll', { params: { targetTypeId: targetTypeId } });
		},
		getGroupById: function (groupId) {
			return $http.get('/api/KPIGroup/GetUserById', { params: { id: groupId} });
		},
		addGroup: function (group) {
			return $http.post(url, group);
		},
		updateGroup: function (group) {
			return $http.put(url, group);
		},
		deleteGroup: function (groupId) {
			return $http['delete'](url, { params: { id: groupId} });
		}
	};
} ]);