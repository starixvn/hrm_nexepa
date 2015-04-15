'use strict';
/*global groupProjectModule*/

groupProjectModule.factory('groupProjectFactory', ['$http', function ($http) {
	return {
		getGroupProject: function () {
			return $http.get('/api/GroupProject/GetAllGroupProject');
		},
		getPermission: function () {
			return $http.get('/api/GroupProject/GetPermission');
		}
	};
} ]);