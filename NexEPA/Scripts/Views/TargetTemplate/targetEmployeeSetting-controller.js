'use strict';
/* global targetTemplateModule */

targetTemplateModule.controller('ViewTargetEmployeeSettingController',
['$scope', '$modal', 'targetEmployeeSettingFactory', 'targetTemplateFactory', 'notificationFactory', 'ErrorService',
'departmentDataFactory',
function ($scope, $modal, targetEmployeeSettingFactory, targetTemplateFactory, notificationFactory, ErrorService,
departmentDataFactory) {

	targetEmployeeSettingFactory.getEPAType().success(function (listEPAType) {
		$scope.listEPAType = listEPAType;
	}).error();

	targetTemplateFactory.getAllTargetTemplate(1, false).then(function (targetTemplates) {
		$scope.targetTemplates = targetTemplates;
	});

	departmentDataFactory.getAllDepartment().then(function (departments) {
		$scope.departments = departments;
	});

//	var generateOptions = function (departments) {
//		var
//	};

//	$scope.$watch('departments', function () {
//		$scope.departmentOptions = generateOptions($scope.departments);
//	});

	var getTargetEmployeeSetting = function (epaTypeId, departmentId) {
		targetEmployeeSettingFactory.getTargetEmployeeSetting(epaTypeId, departmentId)
		.success(function (targetEmployees) {
			$scope.targetEmployees = targetEmployees;
		}).error();
	};

	$scope.query = function (epaTypeId, departmentId) {
		getTargetEmployeeSetting(epaTypeId, departmentId);
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
