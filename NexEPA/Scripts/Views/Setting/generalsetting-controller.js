/* global generalSettingModule */
'use strict';

var memberValidation = function (targetTemplateId) {
	var error = $("#memberValid");
	if (targetTemplateId != null) {
		error.hide();
		return true;
	}
	error.show();
	return false;
};

generalSettingModule.controller('GeneralSettingController',
['$scope', '$modal', 'targetTemplateFactory', 'notificationFactory', 'generalSettingFactory', 'ErrorService', '$route',
function ($scope, $modal, targetTemplateFactory, notificationFactory, generalSettingFactory, ErrorService, $route) {
	/**
	* Ajax event Loaded. 
	*/
	// Success get all Target Template data.
	var successGetAllTargetTemplate = function (data) {
		$scope.tgtemplates = data;
		var addModalInstance = $modal.open({
			templateUrl: '/templates/Setting/DefaultTargetTemplate.html',
			controller: "AddDefaultTargetTemplate",
			backdrop: 'static',
			resolve: {
				targetTemplates: function () {
					return $scope.tgtemplates;
				},
				currentTargetTemplate: function () {
					return $scope.currentTargetTemplate;
				}
			}
		});
		addModalInstance.result.then(function (object) {
			generalSettingFactory.setDefaultTargetTemplate(object).then(successSetDefaultTargetTemplate, errorNotification);
		});
	};

	var successGetTargetSetting = function (data) { $scope.targetSettings = data; };

	var successSetDefaultTargetTemplate = function (response) {
		if (response.data !== "") {
			$scope.errors = response.data;
		}
		else {
			notificationFactory.success('DataChangeSuccessfully', '');
			generalSettingFactory.getTargetSetting().then(successGetTargetSetting, errorHandle);
		}
	};
	var errorHandle = function (status) {
		ErrorService.errorHandle(status);
	};

	var errorNotification = function (response) {
		if (response.status === 405) {
			ErrorService.errorHandle(response.status);
		}
		else {
			notificationFactory.error('ErrorMessage');
		}
	};

	$scope.defaultTargetTemplate = function (targetTemplateId, targetTypeId) {
		$scope.currentTargetTemplate = { targetTemplateId: targetTemplateId };
		targetTemplateFactory.getAllTargetTemplate(targetTypeId, false).then(successGetAllTargetTemplate);
	};

	$scope.viewDetailTargetTemplate = function (targetTemplateId) {
		$scope.viewTargetTemplateUrl = '/TargetTemplate/Details/' + targetTemplateId;
	};

	$scope.reload = function () {
		$route.reload();
	};

	/**
	* Initialize data to view Index.
	*/
	generalSettingFactory.getTargetSetting().then(successGetTargetSetting, errorHandle);

} ]);

generalSettingModule.controller('AddDefaultTargetTemplate',
['$scope', '$modalInstance', 'targetTemplates', 'currentTargetTemplate',
function ($scope, $modalInstance, targetTemplates, currentTargetTemplate) {
	$scope.targetTemplates = targetTemplates;
	$scope.currentTargetTemplate = currentTargetTemplate;
	$scope.ok = function (targetTemplateId, targetTypeId) {
		var object = { targetTemplateId: targetTemplateId, targetTypeId: targetTypeId };
		if (memberValidation(targetTemplateId)) {
			$modalInstance.close(object);
		}

	};

	$scope.cancel = function () {
		$modalInstance.dismiss('cancel');
	};
} ]);