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

// Show warning message if total Weight of all KPI in KPI Group is greater than 100 %
var showWarningWeightMessage = function (kpiIds, notificationFactory) {
	var totalWeight = 0;
	var hasWeightError = false;

	if (kpiIds) {
		var numberKPI = kpiIds.length;
		var kpiGroupId = kpiIds[0].KPIGroupId;

		for (var i = 0; i < numberKPI; i++) {
			if (kpiIds[i].KPIGroupId === kpiGroupId) {
				totalWeight += parseInt(kpiIds[i].Weight);

				if (totalWeight > 100 && (!kpiIds[i + 1] || kpiIds[i + 1] && kpiIds[i + 1].KPIGroupId !== kpiGroupId)) {
					var message = 'The total weight of all KPI in the KPI Group "' +
									kpiIds[i].KPIGroupName + '" must be equal or less than 100%!';
					notificationFactory.error('', message);
					hasWeightError = true;
				}
			}
			else {
				kpiGroupId = kpiIds[i].KPIGroupId;
				totalWeight = kpiIds[i].Weight;
			}
		}
	}

	return hasWeightError;
};

targetTemplateModule.controller('TargetTemplateController',
['$scope', '$modal', 'targetTemplateFactory', 'notificationFactory', 'tabService',
function ($scope, $modal, targetTemplateFactory, notificationFactory, tabService) {
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
	// Tab Selected
	$scope.kpiSelected = function (data) {
		tabService.setProperty(data);
	};

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

	// Confirm Active or not
	$scope.confirmActive = function (template) {
		var action = !template.isDisabled ? "deactivate" : "activate";
		var newTemplate = {
			targetTemplateId: template.targetTemplateId,
			isDisabled: !template.isDisabled
		};
		template.isDisabled = !template.isDisabled;

		var modalConfirmInstance = $modal.open({
			templateUrl: '/templates/Common/modalConfirmContent.html',
			controller: "ModalConfirmController",
			resolve: {
				name: function () {
					return template.name;
				},
				action: function () {
					return action;
				}
			},
			windowClass: "modal-confirm-center"
		});

		var keyMessage = '';

		modalConfirmInstance.result.then(function () {
			if (newTemplate) {
				targetTemplateFactory.updateTargetTemplateStatus(newTemplate).then(function (response) {
					if (response.isSuccessful === true) {
						keyMessage = action === "activate" ? "ActivationSuccess" : "DeactivationSuccess";
						notificationFactory.success(keyMessage);
					}
					else {
						keyMessage = action === "activate" ? "ActivationFail" : "DeactivationFail";
						template.isDisabled = !newTemplate.isDisabled;
						notificationFactory.error(keyMessage);
					}
				}, function () {
					keyMessage = action === "activate" ? "ActivationFail" : "DeactivationFail";
					notificationFactory.error(keyMessage);
					template.isDisabled = !newTemplate.isDisabled;
				});
			}
			else {
				template.isDisabled = !newTemplate.isDisabled;
			}
		}, function () {
			template.isDisabled = !newTemplate.isDisabled;
		});
	};
} ]);

targetTemplateModule.controller('EditTargetTemplateController',
['$scope', '$routeParams', '$window', '$location', 'targetTemplateFactory', '$modal', 'tabService', 'notificationFactory', '$timeout',
function ($scope, $routeParams, $window, $location, targetTemplateFactory, $modal, tabService, notificationFactory, $timeout) {
	/**
	* Variables. 
	*/
	var targetTemplateId = $routeParams.targetTemplateId;
	$scope.selection = [];
	$scope.lstAllData = [];
	$scope.dataTab = {};
	$scope.checkAll = false;

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
		notificationFactory.success('SuccessSaveData', '');
		$timeout(function () {
			$location.url('/TargetTemplate/Index');
		}, 3000);
	};

	var errorUpdateTargetTemplate = function (response) { $scope.errors = response; };

	/**
	* Initialize data to view Edit.
	*/
	targetTemplateFactory.getAllTargetType().then(successGetAllTargetType);
	targetTemplateFactory.getTargetTemplateById(targetTemplateId).then(function (data) {
		$scope.editTemplate = data;
		$scope.currentTargetTemplateName = data.name;
		targetTemplateFactory.getKPI(targetTemplateId, null, null, false).then(succesGetListKPI);
	});

	// Get Selected Tab in Edit Target Template Page.
	if (tabService.getProperty() === true) {
		$scope.dataTab.kpi = true;
	}
	else {
		$scope.dataTab.information = true;
	}

	// What for all list checkbox change.
	$scope.$watch('lstAllData|filter:{selected:true}', function (selectedItems) {
		$scope.selection = selectedItems.map(function (item) {
			var object = { KPIId: item.kpiId, KPIGroupId: item.kpiGroupId, KPIGroupName: item.kpiGroupName, Weight: parseInt(item.weight) };
			return object;
		});
	}, true);

	/**
	* Events. 
	*/
	$scope.cancel = function () { $window.history.back(); };

	$scope.update = function (editTemplate) {
		if (targetTemplateValidation(editTemplate.name, editTemplate.targetTypeId)) {
			editTemplate.kpiIds = $scope.selection;
			if (showWarningWeightMessage(editTemplate.kpiIds, notificationFactory) === false) {
				targetTemplateFactory.updateTargetTemplate(editTemplate).then(successUpdateTargetTemplate, errorUpdateTargetTemplate);
			}
		}
	};

	$scope.addKPIGroup = function () {
		var successAddKPIGroups = function () { notificationFactory.success(); };
		var errorAddKPIGroups = function (data) { notificationFactory.error(data); };
		var successGetAllKPIGroup = function (data) {
			var addModalInstance = $modal.open({
				templateUrl: '/templates/TargetTemplate/AddKPIGroup.html',
				controller: "AddKPIGroup",
				windowClass: 'app-modal-window',
				resolve: {
					KPIGroups: function () {
						return data;
					}
				}
			});
			addModalInstance.result.then(function (selectedGroup) {
				var object = {};
				object.TargetTemplateId = targetTemplateId;
				object.KPIGroups = selectedGroup;
				targetTemplateFactory.addKPIGroups(object).then(successAddKPIGroups, errorAddKPIGroups);
				// Refresh List KPI. 
				targetTemplateFactory.getKPI(targetTemplateId, null, null, false).then(succesGetListKPI);
			});
		};

		/**
		* Initialize Data. 
		*/
		targetTemplateFactory.getSelectedKPIGroupByTargetTemplate(targetTemplateId).then(successGetAllKPIGroup);
	};

	$scope.checkAllGroup = function (status, kpi) {
		angular.forEach(kpi.kpiGroupItems, function (item) {
			item.selected = !status;
		});
	};
	$scope.checkAll = function (status) {
		angular.forEach($scope.lstKPI, function (kpi) {
			kpi.groupSelected = !status;
			angular.forEach(kpi.kpiGroupItems, function (item) {
				item.selected = !status;
			});
		});
	};

	$scope.scoreSpread = function (kpi) {
		var kpiGroupItems = kpi.kpiGroupItems;
		var totalItemChecked = 0;
		$.each(kpiGroupItems, function (i) {
			if (kpiGroupItems[i].selected) {
				totalItemChecked += 1;
			}
		});

		if (totalItemChecked > 0) {
			var average = 100 / totalItemChecked;
			$.each(kpiGroupItems, function (i) {
				var item = kpiGroupItems[i];
				if (item.selected) {
					item.weight = average;
				}
			});
		}
	};

} ]);

targetTemplateModule.controller('AddKPIGroup', ['$scope', '$modalInstance', 'KPIGroups', 'notificationFactory',
function ($scope, $modalInstance, KPIGroups, notificationFactory) {
	/**
	* Variables. 
	*/
	$scope.selectedAll = false;
	$scope.KPIGroups = KPIGroups;
	$scope.selection = [];

	/**
	* Event. 
	*/
	$scope.ok = function () {
		var selectedItems = $scope.selection;
		var totalWeight = 0;
		$.each(selectedItems, function (i) {
			totalWeight += parseInt(selectedItems[i].weight);
		});

		if (totalWeight > 100) {
			notificationFactory.error('', 'Total Weight must be equal or less than 100');
		}
		else {
			$modalInstance.close($scope.selection);
		}


	};

	$scope.cancel = function () {
		$modalInstance.dismiss('cancel');
	};

	/**
	* Check all KPI Group. 
	*/
	$scope.checkAll = function (status) {
		angular.forEach($scope.KPIGroups, function (item) {
			item.selected = !status;
		});
	};

	$scope.$watch('KPIGroups|filter:{selected:true}', function (selectedItems) {
		$scope.selection = selectedItems.map(function (item) {
			var object = {};
			object.kpiGroupId = item.kpiGroupID;
			object.weight = item.weight;
			return object;
		});
	}, true);


} ]);


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
	$scope.addTargetTemplate = function (template) {
		if (template == null) {
			targetTemplateValidation(null, null);
		}
		else {
			if (targetTemplateValidation(template.name, template.targetTypeId)) {
				targetTemplateFactory.addTargetTemplate(template).then(successAddTargetTemplate, errorAddTargetTemplate);
			}
		}
	};

	// Cancel
	$scope.cancel = function () { $window.history.back(); };
} ]);


targetTemplateModule.controller('DetailTargetTemplateController',
['$scope', '$routeParams', '$window', 'targetTemplateFactory',
function ($scope, $routeParams, $window, targetTemplateFactory) {
	var targetTemplateId = $routeParams.targetTemplateId;
	var succesGetListKPI = function (data) {
		$scope.lstKPI = data;
	};

	var successGetAllTargetType = function (data) { $scope.tgTypes = data; };

	targetTemplateFactory.getAllTargetType().then(successGetAllTargetType);

	targetTemplateFactory.getTargetTemplateById(targetTemplateId).then(function (data) {
		$scope.editTemplate = data;
		$scope.currentTargetTemplateName = data.name;
		targetTemplateFactory.getKPI(targetTemplateId, null, null, true).then(succesGetListKPI);
	});

	// Back
	$scope.back = function () { $window.history.back(); };
} ]);