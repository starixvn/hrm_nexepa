'use strict';
/*global groupProjectModule*/

groupProjectModule.factory('groupProjectFactory', ['$http', function ($http) {
	return {
		getGroupProject: function () {
			return $http.get('/api/GroupProject/GetAllGroupProject');
		},
		getAllProject: function () {
			return $http.get('/api/GroupProject/GetAllProject');
		},
		insertGroupProject: function (groupProject) {
			return $http.post('/api/GroupProject/InsertGroupProject', groupProject);
		},
		getPermission: function () {
			return $http.get('/api/GroupProject/GetPermission');
		}
	};
} ]);