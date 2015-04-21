'use strict';
/*global groupProjectModule*/

groupProjectModule.factory('groupProjectFactory', ['$http', function ($http) {
	return {
		getGroupProject: function () {
			return $http.get('/api/GroupProject/GetAllGroupProject');
		},
		insertGroupProject: function (groupProject) {
			return $http.post('/api/GroupProject/InsertGroupProject', groupProject);
		},
		updateGroupProject: function (groupProject) {
			return $http.put('/api/GroupProject/UpdateGroupProject', groupProject);
		},
		deleteGroupProject: function (groupProjectId) {
			return $http['delete']('/api/GroupProject/DeleteGroupProject', { params: { groupProjectId: groupProjectId} });
		},
		getPermission: function () {
			return $http.get('/api/GroupProject/GetPermission');
		}
	};
} ]);