/* global generalSettingModule */
'use strict';

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
				}
			}
		});
		addModalInstance.result.then(function (selectedId) {
			generalSettingFactory.setDefaultTargetTemplate(selectedId).then(successNofication, errorNotification);
		});
	};
	var successNofication = function () { notificationFactory.success('SuccessSaveData', ''); };
	var errorNotification = function (response) { notificationFactory.success('', response); };
	// Popup Add New Memeber.

	$scope.defaultTargetTemplate = function () {
		targetTemplateFactory.getAllTargetTemplate().then(successGetAllTargetTemplate);
		
	};

	/**
	* Initialize data to view Index.
	*/
	

} ]);

generalSettingModule.controller('AddDefaultTargetTemplate',
['$scope', '$modalInstance', 'targetTemplates',
function ($scope, $modalInstance, targetTemplates) {
	$scope.targetTemplates = targetTemplates;
	$scope.ok = function (selectedId) {
		debugger;
		$scope.selectedId = selectedId;
	};

	$scope.cancel = function () {
		$modalInstance.dismiss('cancel');
	};
} ]);