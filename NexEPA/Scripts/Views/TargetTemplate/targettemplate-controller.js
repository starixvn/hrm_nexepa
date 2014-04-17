/* global targetTemplateModule */
'use strict';

var targetTemplateValidation = function (name, targetTypeId) {
	var error = $("#targetTemplateValid");
	if (targetTypeId !== null && (name !== "")) {
		error.hide();
		return true;
	}
	error.show();
	return false;
};

targetTemplateModule.controller('TargetTemplateController', function ($scope, $modal, targetTemplateFactory, notificationFactory) {
	$("#targetTemplateValid").hide();
	/**
	* Ajax event Loaded. 
	*/
	// Success get all Target Template data.
	var successGetAllTargetTemplate = function (data) { $scope.tgtemplates = data; };

	var successGetAllTargetType = function (data) { $scope.tgTypes = data; };

	// Success delete target template.
	var successDeleteTargetTemplate = function () {
		notificationFactory.success();
		return targetTemplateFactory.getAllTargetTemplate().then(successGetAllTargetTemplate);
	};

	var successUpdateTargetTemplate = function () {
		targetTemplateFactory.getAllTargetTemplate().then(successGetAllTargetTemplate);
	};

	var errorUpdateTargetTemplate = function (response) { $scope.errors = response; };

	// Success get List KPI of each Target Template.
	var succesGetListKPI = function (data) {
		$scope.lstKPI = data;
		var modalInstance = $modal.open({
			templateUrl: '../templates/TargetTemplate/AddKPI.html',
			controller: "ModalViewKPIsController",
			windowClass: 'app-modal-window',
			resolve: {
				lstKPI: function () {
					return data;
				},
				editTemplate: function () {
					return $scope.editTemplate;
				},
				tgTypes: function () {
					return $scope.tgTypes;
				}
			}
		});
		modalInstance.result.then(modalConfirmAddKPI);
	};


	// Error notify when action failure.
	var errorNotify = function (data) { notificationFactory.error(data); };

	var modalConfirmAddKPI = function (editTemplate) {
		targetTemplateFactory.updateTargetTemplate(editTemplate).then(successUpdateTargetTemplate, errorUpdateTargetTemplate);
	};

	/**
	* Initialize data to view Index.
	*/
	targetTemplateFactory.getAllTargetTemplate().then(successGetAllTargetTemplate);
	targetTemplateFactory.getAllTargetType().then(successGetAllTargetType);


	/**
	* Event: Filter 
	*/
	// Delete
	$scope.deleteTemplate = function (template) {
		var deleteModalInstance = $modal.open({
			templateUrl: '../templates/Common/ModalConfirmContent.html',
			controller: "ModalConfirmController",
			resolve: {
				name: function () {
					return template.name;
				},
				action: function () {
					return "delete";
				}
			}
		});

		deleteModalInstance.result.then(function () {
			targetTemplateFactory.deleteTargetTemplate(template.targetId).then(successDeleteTargetTemplate, errorNotify);
		});
	};

	// Add KPI Popup
	$scope.editTargetTemplate = function (targetId) {
		targetTemplateFactory.getTargetTemplateById(targetId).then(function (data) {
			$scope.editTemplate = data;
			$scope.currentTargetTemplateName = data.name;
			targetTemplateFactory.getKPI(targetId, null, null).then(succesGetListKPI);
		});

	};

});

targetTemplateModule.controller('ModalViewKPIsController', function ($scope, $modalInstance, lstKPI, editTemplate, tgTypes) {
	$scope.selection = [];
	$scope.lstKPI = lstKPI;
	$scope.editTemplate = editTemplate;
	$scope.tgTypes = tgTypes;
	$scope.lstAllData = [];

	$scope.ok = function (editTemplate) {
		if (targetTemplateValidation(editTemplate.name, editTemplate.targetTypeId)) {
			var itemSelected = $scope.selection;
			editTemplate.kpiIds = itemSelected.join(',');
			$modalInstance.close(editTemplate);
		}
	};

	$scope.cancel = function () {
		$modalInstance.dismiss('cancel');
	};

	$.each($scope.lstKPI, function (key) {
		var data = $scope.lstKPI[key].kpiGroupItems;
		$.each(data, function (k) {
			$scope.lstAllData.push(data[k]);
		});
	});

	$scope.$watch('lstAllData|filter:{selected:true}', function (selectedItems) {
		$scope.selection = selectedItems.map(function (item) {
			return item.kpiId;
		});
	}, true);
});

targetTemplateModule.controller('CreateTargetTemplateController', function ($scope, $location, $window, targetTemplateFactory) {
	/**
	* Variables. 
	*/
	$scope.errors = [];
	/**
	* Ajax event Loaded. 
	*/
	var successGetAllTargetType = function (data) { $scope.tgTypes = data; };
	var successAddTargetTemplate = function () { $location.url('/TargetTemplate/Index'); };
	var errorAddTargetTemplate = function (response) { $scope.errors = response; };
	/**
	* Initialize data to view Index.
	*/
	targetTemplateFactory.getAllTargetType().then(successGetAllTargetType);

	/**
	* Event: Filter 
	*/
	// Submit
	$scope.addTargetType = function (template) {
		targetTemplateFactory.addTargetTemplate(template).then(successAddTargetTemplate, errorAddTargetTemplate);
	};

	// Cancel
	$scope.cancel = function () { $window.history.back(); };
});
