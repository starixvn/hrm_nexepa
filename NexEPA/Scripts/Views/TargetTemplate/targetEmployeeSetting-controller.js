'use strict';
/* global targetTemplateModule */

targetTemplateModule.controller('ViewTargetEmployeeSettingController',
['$scope', '$modal', '$route', 'targetEmployeeSettingFactory', 'targetTemplateFactory', 'notificationFactory', 'ErrorService',
'departmentDataFactory', 'commonFactory',
function ($scope, $modal, $route, targetEmployeeSettingFactory, targetTemplateFactory, notificationFactory, ErrorService,
departmentDataFactory, commonFactory) {

	targetEmployeeSettingFactory.getPermissionTargetEmployeeSetting().success(function (policy) {
		$scope.permission = {
			policy: policy
		};
	}).error(function () {
		notificationFactory.error("ErrorMessage");
	});

	targetEmployeeSettingFactory.getEPAType().success(function (listEPAType) {
		$scope.listEPAType = listEPAType;
	}).error(function () {
		notificationFactory.error("ErrorMessage");
	});

	targetTemplateFactory.getAllTargetTemplate(1, false).then(function (targetTemplates) {
		$scope.targetTemplates = targetTemplates;
	});

	departmentDataFactory.getAllDepartment().then(function (departments) {
		$scope.departments = departments.item1.children;
	});

	$scope.$watch('departments', function () {
		if ($scope.departments !== undefined) {
			$scope.departmentOptions = commonFactory.generateOptions($scope.departments, [], 0);
		}
	});

	$scope.targetEmployees = [];

	var getTargetEmployeeSetting = function (epaTypeId, departmentId, groupId) {
		targetEmployeeSettingFactory.getTargetEmployeeSetting(epaTypeId, departmentId, groupId)
		.success(function (targetEmployees) {
			$scope.targetEmployees = targetEmployees;
		}).error(function (response, status) {
			if (status === 405) {
				ErrorService.errorHandle(status);
			}
			else {
				notificationFactory.error("ErrorMessage");
			}
		});
	};

	$scope.query = function (epaTypeId, departmentId) {
		if (epaTypeId !== undefined) {
			if (departmentId !== undefined) {
				if (departmentId > 0) {
					getTargetEmployeeSetting(epaTypeId, departmentId, null);
				}
				else {
					var groupId = -departmentId;
					getTargetEmployeeSetting(epaTypeId, null, groupId);
				}
			}
			else {
				getTargetEmployeeSetting(epaTypeId, null, null);
			}
		}
		else {
			$scope.targetEmployees = [];
		}
	};

	$scope.updateTargetTemplate = function (employee, template, epaTypeId) {
		if (employee && template) {
			var targetTemplate = {
				employeeId: employee.employeeId,
				epaTypeId: epaTypeId,
				targetTemplateId: template.targetTemplateId
			};

			var modalUpdateStatusInstance = $modal.open({
				templateUrl: '/templates/Common/modalConfirmContent.html',
				controller: "ModalConfirmController",
				resolve: {
					name: function () {
						return template.name;
					},
					action: function () {
						return "choose";
					}
				},
				windowClass: "modal-confirm-center"
			});

			modalUpdateStatusInstance.result.then(function () {
				targetEmployeeSettingFactory.updateTargetEmployeeSetting(targetTemplate).success(function (response) {
					if (response.isSuccessful === true) {
						employee.name = template.name;
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
						notificationFactory.error("ErrorMessage");
					}
				});
			});
		}
	};

	$scope.reload = function () {
		$route.reload();
	};
} ]);
