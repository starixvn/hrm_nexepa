'use strict';
/*global authorityModule*/

authorityModule.factory('authorityRepository', ['$http', function ($http) {
	return {
		getAllFunction: function () {
			return $http.get('/api/Authority/GetAllFunction');
		},
		getAllSystemRole: function () {
			return $http.get('/api/Authority/GetAllSystemRole');
		}
	};
} ]);