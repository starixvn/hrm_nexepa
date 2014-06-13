'use strict';
/*global authorityModule*/

authorityModule.controller("AssignPermissionController",
['$scope', '$route', 'authorityRepository', 'notificationFactory', 'ErrorService',
function ($scope, $route, authorityRepository, notificationFactory, ErrorService) {

	$scope.roleFunction = {
		functions: '',
		roleId: ''
	};

	authorityRepository.getManageRolePermission().success(function (policy) {
		$scope.policy = {
			policy : policy
		};
	}).error(function () {
		notificationFactory.error("ErrorMessage");
	});

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

		authorityRepository.assignPermissionToRole(roleFunction).success(function (response) {
			if (response.isSuccessful === true) {
				notificationFactory.success("DataChangeSuccessfully");
			}
			else {
				notificationFactory.error("", response.message);
			}

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

authorityModule.controller('ManageUserPermissionController',
['$scope', '$modal', 'employeeRepository', 'commonFactory', 'authorityRepository', 'ErrorService', 'notificationFactory',
function ($scope, $modal, employeeRepository, commonFactory, authorityRepository, ErrorService, notificationFactory) {

	authorityRepository.getManageUserPermission().success(function (policy) {
		$scope.permission = {
			policy: policy
		};
	}).error(function () {
		notificationFactory.error("ErrorMessage");
	});

	employeeRepository.getAllGroup().success(function (groupList) {
		$scope.groupOptions = groupList;
	}).error(function () {
		notificationFactory.error("ErrorMessage");
	});

	var getEmployee = function (keyword, groupId) {
		authorityRepository.getAllEmployee(keyword, groupId).success(function (employees) {
			$scope.employees = employees;
			commonFactory.tablePagination($scope, employees, 25);
		}).error();
	};

	getEmployee(null, null);

	$scope.query = function (keyword, groupId) {
		getEmployee(keyword, groupId);
	};

	var getSelectedRole = function (roles) {
		var selectedRoles = {
			selectedRolesId : '',
			selectedRolesName : ''
		};

		for (var i = 0; i < roles.length; i++) {
			var role = roles[i];

			if (roles[i].permission) {
				if (selectedRoles.selectedRolesId === '') {
					selectedRoles.selectedRolesId += role.roleId;
					selectedRoles.selectedRolesName = role.name;
				}
				else {
					selectedRoles.selectedRolesId += ',' + role.roleId;
					selectedRoles.selectedRolesName += ', ' + role.name;
				}
			}
		}

		return selectedRoles;
	};

	$scope.assignRole = function (employee) {

		var modalAssignInstance = $modal.open({
			templateUrl: '/templates/Authority/UserPermission/AssignRole.html',
			controller: "AssignRoleController",
			resolve: {
				employee: function () {
					return employee;
				}
			}
		});

		modalAssignInstance.result.then(function (roles) {
			var selectedRole = getSelectedRole(roles),
				userRole = {
					employeeId: employee.employeeId,
					roleIds: selectedRole.selectedRolesId
				};

			authorityRepository.updateRole(userRole).success(function (response) {
				if (response.isSuccessful === true) {
					employee.roles = selectedRole.selectedRolesName;
					notificationFactory.success("DataChangeSuccessfully");
				}
				else {
					notificationFactory.error("", response.message);
				}
			}).error(function (response, status) {
				if (status === 405) {
					ErrorService.errorHandle(status);
				}
				else {
					notificationFactory.error("DataChangeFail");
				}
			});
		});
	};
} ]);

authorityModule.controller('AssignRoleController',
['$scope', '$modalInstance', 'employee', 'authorityRepository', 'notificationFactory',
function ($scope, $modalInstance, employee, authorityRepository, notificationFactory) {
	$scope.employee = employee;

	authorityRepository.getRolesByEmployeeId(employee.employeeId).success(function (roles) {
		$scope.roles = roles;
	}).error(function () {
		notificationFactory.error("DataChangeFail");
	});

	$scope.save = function (roles) {
		$modalInstance.close(roles);
	};

	$scope.cancel = function () {
		$modalInstance.dismiss("cancel");
	};
} ]);