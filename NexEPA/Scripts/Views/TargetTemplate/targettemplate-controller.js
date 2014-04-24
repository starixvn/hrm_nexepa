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

targetTemplateModule.controller('TargetTemplateController',
['$scope', '$modal', 'targetTemplateFactory', 'notificationFactory',
function ($scope, $modal, targetTemplateFactory, notificationFactory) {
	$("#targetTemplateValid").hide();
	/**
	* Ajax event Loaded. 
	*/
	// Success get all Target Template data.
	var successGetAllTargetTemplate = function (data) { $scope.tgtemplates = data; debugger; };

	var successGetAllTargetType = function (data) { $scope.tgTypes = data; };

	// Success delete target template.
	var successDeleteTargetTemplate = function () {
		notificationFactory.success();
		return targetTemplateFactory.getAllTargetTemplate().then(successGetAllTargetTemplate);
	};

	// Error notify when action failure.
	var errorNotify = function (data) { notificationFactory.error(data); };

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
} ]);

targetTemplateModule.controller('EditTargetTemplateController', 
['$scope', '$routeParams', '$window', '$location', 'targetTemplateFactory',
function ($scope, $routeParams, $window, $location, targetTemplateFactory) {
	/**
	* Variables. 
	*/
	var targetId = $routeParams.targetId;
	$scope.selection = [];
	$scope.lstAllData = [];

	/**
	* Ajax event Loaded. 
	*/
	var succesGetListKPI = function (data) {
		$scope.lstKPI = data;

		$.each($scope.lstKPI, function (key) {
			var data = $scope.lstKPI[key].kpiGroupItems;
			$.each(data, function (k) {
				$scope.lstAllData.push(data[k]);
			});
		});
	};

	var successGetAllTargetType = function (data) { $scope.tgTypes = data; };

	var successUpdateTargetTemplate = function () {
		$location.url('/TargetTemplate/Index');
	};

	var errorUpdateTargetTemplate = function (response) { $scope.errors = response; };

	/**
	* Initialize data to view Edit.
	*/
	targetTemplateFactory.getAllTargetType().then(successGetAllTargetType);
	targetTemplateFactory.getTargetTemplateById(targetId).then(function (data) {
		$scope.editTemplate = data;
		$scope.currentTargetTemplateName = data.name;
		targetTemplateFactory.getKPI(targetId, null, null, false).then(succesGetListKPI);
	});

	// What for all list checkbox change.
	$scope.$watch('lstAllData|filter:{selected:true}', function (selectedItems) {
		$scope.selection = selectedItems.map(function (item) {
			return item.kpiId;
		});
	}, true);

	/**
	* Events. 
	*/
	$scope.cancel = function () { $window.history.back(); };

	$scope.update = function (editTemplate) {
		if (targetTemplateValidation(editTemplate.name, editTemplate.targetTypeId)) {
			var itemSelected = $scope.selection;
			editTemplate.kpiIds = itemSelected.join(',');
			targetTemplateFactory.updateTargetTemplate(editTemplate).then(successUpdateTargetTemplate, errorUpdateTargetTemplate);
		}
	};
}]);


targetTemplateModule.controller('CreateTargetTemplateController',
['$scope', '$location', '$window', 'targetTemplateFactory',
function ($scope, $location, $window, targetTemplateFactory) {
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
	* Events 
	*/
	// Submit
	$scope.addTargetType = function (template) {
		targetTemplateFactory.addTargetTemplate(template).then(successAddTargetTemplate, errorAddTargetTemplate);
	};

	// Cancel
	$scope.cancel = function () { $window.history.back(); };
}]);


targetTemplateModule.controller('DetailTargetTemplateController',
['$scope', '$routeParams', '$window', 'targetTemplateFactory',
 function ($scope, $routeParams, $window, targetTemplateFactory) {
	var targetId = $routeParams.targetId;
	var succesGetListKPI = function (data) {
		$scope.lstKPI = data;
	};

	var successGetAllTargetType = function (data) { $scope.tgTypes = data; };

	targetTemplateFactory.getAllTargetType().then(successGetAllTargetType);

	targetTemplateFactory.getTargetTemplateById(targetId).then(function (data) {
		$scope.editTemplate = data;
		$scope.currentTargetTemplateName = data.name;
		targetTemplateFactory.getKPI(targetId, null, null, true).then(succesGetListKPI);
	});

	// Back
	$scope.back = function () { $window.history.back(); };
}]);