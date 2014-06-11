'use strict';
/*global authorityModule*/

authorityModule.factory('authorityRepository', ['$http', function ($http) {
	return {
		getFunctionByRole: function (roleId) {
			return $http.get('/api/Authority/GetFunctionByRole', { params: { roleId: roleId} });
		},
		getAllSystemRole: function () {
			return $http.get('/api/Authority/GetAllSystemRole');
		},
		assignPermissionToRole: function (roleFunction) {
			return $http.post('/api/Authority/AssignPermissionToRole', roleFunction);
		},
		getPermission: function () {
			return $http.get('/api/Authority/GetPermission');
		}
	};
} ]);