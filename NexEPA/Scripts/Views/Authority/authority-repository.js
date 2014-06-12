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
		getManageRolePermission: function () {
			return $http.get('/api/Authority/GetManageRolePermission');
		},
		getAllEmployee: function (keyword, groupId) {
			return $http.get('/api/Authority/GetAllEmployee/', { params: { keyword: keyword, groupId: groupId} });
		},
		getRolesByEmployeeId: function (employeeId) {
			return $http.get('/api/Authority/GetRolesByEmployeeId', { params: { employeeId: employeeId} });
		},
		updateRole: function (userRole) {
			return $http.put('/api/Authority/UpdateRole', userRole);
		},
		getManageUserPermission: function () {
			return $http.get('/api/Authority/GetManageUserPermission');
		}
	};
} ]);