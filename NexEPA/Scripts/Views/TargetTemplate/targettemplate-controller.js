/* global targetTemplateModule */
'use strict';

targetTemplateModule.controller('TargetTemplateController', function ($scope, $modal, targetTemplateFactory, notificationFactory) {
	/**
	* Ajax event Loaded. 
	*/
	// Success get all Target Template data.
	var successGetAllTargetTemplate = function (data) { $scope.tgtemplates = data; };

	// Success delete target template.
	var successDeleteTargetTemplate = function () {
		notificationFactory.success();
		return targetTemplateFactory.getAllTargetTemplate().then(successGetAllTargetTemplate);

	};

	// Success get List KPI of each Target Template.
	var succesGetListKPI = function (data) {
		$scope.lstKPI = data;
		var modalInstance = $modal.open({
			templateUrl: '../templates/TargetTemplate/AddKPI.html',
			controller: "ModalViewKPIsController",
			resolve: {
				lstKPI: function () {
					return data;
				}
			}
		});
		modalInstance.result.then(modalConfirmAddKPI);
	};

	// Success add KPI to Target Template.
	var successAddKPI = function () {
		targetTemplateFactory.getAllTargetTemplate().then(successGetAllTargetTemplate);
	};

	// Error notify when action failure.
	var errorNotify = function (data) { notificationFactory.error(data); };

	var modalConfirmAddKPI = function (itemSelected) {
		var objectKPI = {};
		objectKPI.TargetId = $scope.currentTargetId;
		objectKPI.KPIIds = itemSelected.join(',');
		targetTemplateFactory.addKPI(objectKPI).then(successAddKPI, errorNotify);
	};

	/**
	* Initialize data to view Index.
	*/
	targetTemplateFactory.getAllTargetTemplate().then(successGetAllTargetTemplate);

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
	$scope.addKPI = function (targetId) {
		$scope.currentTargetId = targetId;
		targetTemplateFactory.getKPI(targetId, null, null).then(succesGetListKPI);
	};

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

targetTemplateModule.controller('EditTargetTemplateController', function ($scope, $location, $window, $routeParams, targetTemplateFactory) {
	/**
	* Variables. 
	*/
	$scope.errors = [];
	var targetId = $routeParams.targetId;
	/**
	* Ajax event Loaded. 
	*/
	var successGetAllTargetType = function (data) { $scope.tgTypes = data; };
	var successGetTargetTemplateById = function (data) {
		$scope.editTemplate = data;
		$scope.currentTargetTemplateName = data.name;
	};
	var successUpdateTargetTemplate = function () { $location.url('/TargetTemplate/Index'); };
	var errorUpdateTargetTemplate = function (response) { $scope.errors = response; };
	/**
	* Initialize data to view Index.
	*/
	targetTemplateFactory.getAllTargetType().then(successGetAllTargetType);
	targetTemplateFactory.getTargetTemplateById(targetId).then(successGetTargetTemplateById);

	/**
	* Event: Filter 
	*/
	// Submit
	$scope.updateTargetTemplate = function (template) {
		targetTemplateFactory.updateTargetTemplate(template).then(successUpdateTargetTemplate, errorUpdateTargetTemplate);
	};

	// Cancel
	$scope.cancel = function () { $window.history.back(); };
});

targetTemplateModule.controller('ModalViewKPIsController', function ($scope, $modalInstance, lstKPI) {
	$scope.selection = [];
	$scope.lstKPI = lstKPI;
	$scope.lstAllData = [];

	$scope.ok = function () {
		$modalInstance.close($scope.selection);
	};

	$scope.cancel = function () {
		$modalInstance.dismiss('cancel');
	};

	$.each($scope.lstKPI, function (key) {
		var data = $scope.lstKPI[key].kPIGroupItems;
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
