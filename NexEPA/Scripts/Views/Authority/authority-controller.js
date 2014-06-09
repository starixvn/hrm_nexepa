'use strict';
/*global authorityModule*/

authorityModule.controller("AssignPermissionController", ['$scope', '$route', 'authorityRepository',
function ($scope, $route, authorityRepository) {

	authorityRepository.getAllSystemRole().success(function (roles) {
		if (roles) {
			var roleId = roles[0].roleId;
			$scope.filter = {
				roleId: roleId
			};

			getFunctionByRole(roleId);
			$scope.roleOptions = roles;
		}
	}).error();

	var getFunctionByRole = function (roleId) {
		authorityRepository.getFunctionByRole(roleId).success(function (functionGroup) {
			$scope.functionGroup = functionGroup;
		}).error();
	};

	$scope.query = function (roleId) {
		getFunctionByRole(roleId);
	};

	$scope.reload = function () {
		$route.reload();
	};
} ]);