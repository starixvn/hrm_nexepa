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
['$scope', '$modal', 'targetTemplateFactory', 'notificationFactory', 'generalSettingFactory',
function ($scope, $modal, targetTemplateFactory, notificationFactory, generalSettingFactory) {
	/**
	* Ajax event Loaded. 
	*/
	// Success get all Target Template data.
	var successGetAllTargetTemplate = function (data) {
		$scope.tgtemplates = data;
		var addModalInstance = $modal.open({
			templateUrl: '/templates/Setting/DefaultTargetTemplate.html',
			controller: "AddDefaultTargetTemplate",
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

	var successGetTargetSetting = function (data) { $scope.targetSettings = data;};
	var successSetDefaultTargetTemplate = function () {
		notificationFactory.success('SuccessSaveData', '');
		generalSettingFactory.getTargetSetting().then(successGetTargetSetting, errorNotification);
	};
	var errorNotification = function (response) { notificationFactory.success('', response); };
	// Popup Add New Memeber.

	$scope.defaultTargetTemplate = function (targetTemplateId, targetTypeId) {
		$scope.currentTargetTemplate = { targetTemplateId: targetTemplateId };
		targetTemplateFactory.getAllTargetTemplate(targetTypeId).then(successGetAllTargetTemplate);

	};

	/**
	* Initialize data to view Index.
	*/
	generalSettingFactory.getTargetSetting().then(successGetTargetSetting, errorNotification);

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