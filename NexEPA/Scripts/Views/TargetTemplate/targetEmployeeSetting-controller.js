'use strict';
/* global targetTemplateModule */

targetTemplateModule.controller('ViewTargetEmployeeSettingController',
['$scope', '$modal', 'targetEmployeeSettingFactory', 'targetTemplateFactory', 'notificationFactory', 'ErrorService',
'departmentDataFactory',
function ($scope, $modal, targetEmployeeSettingFactory, targetTemplateFactory, notificationFactory, ErrorService,
departmentDataFactory) {

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

	var createLabel = function (departmentItem, level) {
		var label = '',
			space = '',
			levelTemp = level;

		if (level > 0) {
			while (levelTemp > 0) {
				label = label + " " + space;
				space = String.fromCharCode(160) + String.fromCharCode(160) + String.fromCharCode(160);
				levelTemp--;
			}

			label = label + "└── " + departmentItem.name;
		}
		else {
			label = departmentItem.name;
		}

		return label;
	};

	var generateOptions = function (departments, options, level) {
		angular.forEach(departments, function (departmentItem) {
			options.push({ label: createLabel(departmentItem, level), value: departmentItem.departmentId });
			generateOptions(departmentItem.children, options, level + 1);
		});

		return options;
	};

	$scope.$watch('departments', function () {
		if ($scope.departments !== undefined) {
			$scope.departmentOptions = generateOptions($scope.departments, [], 0);
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
} ]);
