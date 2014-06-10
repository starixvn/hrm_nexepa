'use strict';
/*global authorityModule*/

authorityModule.controller("AssignPermissionController",
['$scope', '$route', 'authorityRepository', 'notificationFactory', 'ErrorService',
function ($scope, $route, authorityRepository, notificationFactory, ErrorService) {

	$scope.roleFunction = {
		functions: '',
		roleId: ''
	};

	authorityRepository.getAllSystemRole().success(function (roles) {
		if (roles) {
			var roleId = roles[0].roleId;
			$scope.filter = {
				roleId: roleId
			};

			getFunctionByRole(roleId);
			$scope.roleOptions = roles;
		}
	}).error(function () {
		notificationFactory.error("ErrorMessage");
	});

	var getFunctionByRole = function (roleId) {
		authorityRepository.getFunctionByRole(roleId).success(function (functionGroup) {
			$scope.functionGroup = functionGroup;
		}).error(function (response, status) {
			if (status === 405) {
				ErrorService.errorHandle(status);
			}
			else {
				notificationFactory.error("ErrorMessage");
			}
		});
	};

	$scope.query = function (roleId) {
		getFunctionByRole(roleId);
	};

	$scope.save = function (roleFunction) {
		$scope.disabledSubmitButton = true;

		authorityRepository.assignPermissionToRole(roleFunction).success(function () {
			notificationFactory.success("DataChangeSuccessfully");
			$scope.disabledSubmitButton = false;
		}).error(function (response, status) {
			if (status === 405) {
				ErrorService.errorHandle(status);
			}
			else {
				notificationFactory.error("ErrorMessage");
			}

			$scope.disabledSubmitButton = false;
		});
	};

	$scope.reload = function () {
		$route.reload();
	};
} ]);