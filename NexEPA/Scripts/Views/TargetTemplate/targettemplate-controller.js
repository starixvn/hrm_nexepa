/* global targetTemplateModule */
'use strict';

// Show warning message if total Weight of all KPI in KPI Group is greater than 100 %
var showWarningWeightMessage = function (kpiIds, notificationFactory) {
	var totalWeight = 0;
	var hasWeightError = false;

	if (kpiIds && kpiIds.length > 0) {
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
['$scope', '$modal', 'targetTemplateFactory', 'notificationFactory', 'tabService', 'commonFactory', 'ErrorService',
function ($scope, $modal, targetTemplateFactory, notificationFactory, tabService, commonFactory, ErrorService) {
	/**
	* Ajax event Loaded. 
	*/
	// Success get all Target Template data.
	var successGetAllTargetTemplate = function (data) { $scope.tgtemplates = data; };

	var successGetAllTargetType = function (data) { $scope.tgTypes = data; };

	// Success delete target template.
	var successDeleteTargetTemplate = function (response) {
		if (response.data !== "") {
			notificationFactory.error('', response.data);
		}
		else {
			notificationFactory.success('SuccessDeleteData', '');
			return targetTemplateFactory.getAllTargetTemplate(null, null).then(successGetAllTargetTemplate);
		}
	};

	// Error notify when action failure.
	var errorNotification = function (response) {
		if (response.status === 405) {
			ErrorService.errorHandle(response.status);
		}
		else {
			notificationFactory.error('ErrorMessage');
		}
	};

	var errorHandle = function (status) {
		ErrorService.errorHandle(status);
	};
	/**
	* Initialize data to view Index.
	*/
	targetTemplateFactory.getAllTargetTemplate(null, null).then(successGetAllTargetTemplate, errorHandle);
	targetTemplateFactory.getAllTargetType().then(successGetAllTargetType, errorHandle);
	commonFactory.maxLengthDescription($scope);
	tabService.setProperty(null);

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
			targetTemplateFactory.deleteTargetTemplate(template.targetTemplateId).then(successDeleteTargetTemplate, errorNotification);
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
			}

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

	// Event read more / less text.
	$scope.readMore = function (tgtemplate) {
		commonFactory.readMore(tgtemplate);
	};
	$scope.readLess = function (tgtemplate) {
		commonFactory.readLess(tgtemplate);
	};

} ]);

targetTemplateModule.controller('EditTargetTemplateController',
['$scope', '$routeParams', '$window', '$location', 'targetTemplateFactory', '$modal', 'tabService',
'notificationFactory', '$timeout', 'ErrorService', 'targetTemplateUtility',
function ($scope, $routeParams, $window, $location, targetTemplateFactory, $modal, tabService,
notificationFactory, $timeout, ErrorService, targetTemplateUtility) {
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
			var currentItem = $scope.lstKPI[key].kpiGroupItems;
			$.each(currentItem, function (k) {
				$scope.lstAllData.push(currentItem[k]);
			});
		});
	};

	var errorHandle = function (status) {
		ErrorService.errorHandle(status);
	};

	var successGetAllTargetType = function (data) { $scope.tgTypes = data; };

	var successUpdateTargetTemplate = function (response) {
		if (response.data !== "") {
			notificationFactory.error('', response.data);
		}
		else {
			notificationFactory.success('SuccessSaveData', '');
		}
	};

	var successRemoveKPIGroup = function (response) {
		if (response.data !== "") {
			notificationFactory.error('', response.data);
		}
		else {
			notificationFactory.success('SuccessSaveData', '');
			targetTemplateFactory.getKPI(targetTemplateId, null, null, false).then(succesGetListKPI);
		}
	};

	var errorNotification = function (response) {
		if (response.status === 405) {
			ErrorService.errorHandle(response.status);
		}
		else {
			notificationFactory.error('ErrorMessage');
		}
	};

	/**
	* Initialize data to view Edit.
	*/
	targetTemplateFactory.getAllTargetType().then(successGetAllTargetType);
	targetTemplateFactory.getTargetTemplateById(targetTemplateId).then(function (data) {
		$scope.editTemplate = data;
		$scope.currentTargetTemplateName = data.name;
		targetTemplateFactory.getKPI(targetTemplateId, null, null, false).then(succesGetListKPI);
	}, errorHandle);

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
			var object = {};
			if (item.isSpreadKPI) {
				object = { KPIId: item.kpiId, KPIGroupId: item.kpiGroupId, KPIGroupName: item.kpiGroupName, Weight: 0 };
			}
			else {
				object = { KPIId: item.kpiId, KPIGroupId: item.kpiGroupId,
					KPIGroupName: item.kpiGroupName, Weight: parseInt(item.weight)
				};
			}

			return object;
		});
	}, true);

	/**
	* Events. 
	*/
	targetTemplateUtility.showMessageAfterValidate($scope);

	$scope.cancel = function () { $window.history.back(); };

	$scope.update = function (editTemplate) {
		$scope.submitted = true;

		if (targetTemplateUtility.targetTemplateValidation(editTemplate)) {
			var selectedKPIs = $scope.selection;
			var checkSelectedKPIValid = true;
			var arrayNumber = [];

			var dataTable = $("table#tblKPI tbody tr.row-data");
			$.each(selectedKPIs, function (i) {
				var currentItem = selectedKPIs[i];
				if (currentItem.Weight == null || isNaN(currentItem.Weight)) {
					checkSelectedKPIValid = false;
				}
			});

			for (var i = 0; i < dataTable.length; i++) {
				var tableRowTR = $(dataTable[i]).children().eq(0);
				var itemChecked = $(dataTable[i]).find(".check-group").first();
				var itemId = parseInt(tableRowTR.attr("item-id"));
				var groupId = parseInt(tableRowTR.attr("group-id"));
				if (itemChecked[0].checked === true) {
					arrayNumber.push({ itemId: itemId, groupId: groupId });
				}
			}

			if (checkSelectedKPIValid) {
				if (showWarningWeightMessage(editTemplate.kpiIds, notificationFactory) === false) {
					var orderNo = 1;
					if (arrayNumber != null) {
						$.each(arrayNumber, function (i) {
							var currentArray = arrayNumber[i];
							$.each(selectedKPIs, function (j) {
								var currentKPI = selectedKPIs[j];
								if ((currentKPI.KPIId === currentArray.itemId) && (currentKPI.KPIGroupId === currentArray.groupId)) {
									selectedKPIs[j].displayOrder = orderNo;
								}
							});
							orderNo++;
						});

					}
					editTemplate.kpiIds = selectedKPIs;
					targetTemplateFactory.updateTargetTemplate(editTemplate).then(successUpdateTargetTemplate, errorNotification);
				}
			}
			else {
				notificationFactory.error('', 'All weight of selected KPIs is required!');
			}
		}
		else {
			if (tabService.getProperty() === true) {
				notificationFactory.error('', 'Re-check your general information tab!');
			}
		}
	};

	$scope.addKPIGroup = function () {
		var successAddKPIGroups = function (response) {
			if (response.data !== "") {
				notificationFactory.error('', response.data);
			}
			else {
				notificationFactory.success('SuccessSaveData', '');
				targetTemplateFactory.getKPI(targetTemplateId, null, null, false).then(succesGetListKPI);
			}
		};
		var successGetAllKPIGroup = function (data) {
			var addModalInstance = $modal.open({
				templateUrl: '/templates/TargetTemplate/AddKPIGroup.html',
				controller: "AddKPIGroup",
				windowClass: 'app-modal-window',
				backdrop: 'static',
				resolve: {
					KPIGroups: function () {
						return data;
					}
				}
			});
			addModalInstance.result.then(function (model) {
				model.TargetTemplateId = targetTemplateId;
				targetTemplateFactory.addKPIGroups(model).then(successAddKPIGroups, errorNotification);
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

	$scope.tabInfoSelect = function () {
		tabService.setProperty(false);
	};

	$scope.tabKPISelect = function () {
		tabService.setProperty(true);
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

	$scope.removeKPIGroup = function (groupId, groupName) {
		var model = { groupId: groupId, targetTemplateId: targetTemplateId };
		var deleteModalInstance = $modal.open({
			templateUrl: '/templates/Common/ModalConfirmContent.html',
			controller: "ModalConfirmController",
			resolve: {
				name: function () {
					return groupName;
				},
				action: function () {
					return "delete";
				}
			}
		});
		deleteModalInstance.result.then(function () {
			targetTemplateFactory.removeKPIGroup(model).then(successRemoveKPIGroup, errorNotification);
		});
	};

	$scope.setPriority = function (event) {
		var currentItem = $(event.target);
		var row = currentItem.parents("table#tblKPI tbody tr.row-data:first");
		var firstItem = currentItem.parents('table#tblKPI tbody').find('tr.row-data:first');
		var lastItem = currentItem.parents('table#tblKPI tbody').find('tr:last');
		if (currentItem.is(".kpiUp")) {
			if (currentItem.parents('tr')[0] !== firstItem[0]) {
				row.insertBefore(row.prev());
			}
		} else if (currentItem.is(".kpiDown")) {
			row.insertAfter(row.next());
		} else if (currentItem.is(".kpiBottom")) {
			if (currentItem.parents('tr')[0] !== lastItem[0]) {
				row.insertAfter(lastItem);
			}
		} else if (currentItem.is(".kpiTop")) {
			if (currentItem.parents('tr')[0] !== firstItem[0]) {
				row.insertBefore(firstItem);
			}
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
		var checkSelectedGroupValid = true;
		$.each(selectedItems, function (i) {
			var currentItem = selectedItems[i];
			if (currentItem.weight == null || isNaN(currentItem.weight)) {
				checkSelectedGroupValid = false;
			}
			totalWeight += parseInt(currentItem.weight);
		});

		if (!checkSelectedGroupValid) {
			notificationFactory.error('', 'All weight of selected KPIs is required!');
		}
		else {
			if (totalWeight > 100) {
				notificationFactory.error('', 'Total Groups weight must be equal or less than 100');
			}
			else {
				var arrayNumber = [];
				var orderNo = 1;
				var dataTable = $("table#tblKPIGroup tbody tr");
				for (var i = 0; i < dataTable.length; i++) {
					var tableRowTR = $(dataTable[i]).children().eq(0);
					var groupChecked = $(dataTable[i]).find(".check-group").first();
					var rowId = parseInt(tableRowTR.attr("data-id"));
					if (groupChecked[0].checked === true) {
						arrayNumber.push(rowId);
					}
				}
				if (arrayNumber != null) {
					$.each(arrayNumber, function (i) {
						$.each(selectedItems, function (k) {
							var currentItem = selectedItems[k];
							if (currentItem.kpiGroupId === arrayNumber[i]) {
								selectedItems[k].displayOrder = orderNo;
							}
						});
						orderNo++;
					});
				}
				var model = { KPIGroups: selectedItems };
				$modalInstance.close(model);
			}
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
			object.kpiGroupId = item.kpiGroupId;
			object.weight = item.weight;
			object.isSpreadKPI = item.isSpreadKPI;
			return object;
		});
	}, true);

} ]);

targetTemplateModule.controller('CreateTargetTemplateController',
['$scope', '$location', '$window', 'targetTemplateFactory', 'notificationFactory', '$timeout', 'ErrorService',
'targetTemplateUtility',
function ($scope, $location, $window, targetTemplateFactory, notificationFactory, $timeout, ErrorService,
targetTemplateUtility) {
	/**
	* Variables. 
	*/
	$scope.errors = [];

	/**
	* Ajax event Loaded. 
	*/
	var successGetAllTargetType = function (data) { $scope.tgTypes = data; };

	var successAddTargetTemplate = function (response) {
		if (response.data !== "") {
			notificationFactory.error('', response.data);
		}
		else {
			$location.url('/TargetTemplate/Index');
		}

	};
	var errorAddTargetTemplate = function (response) {
		if (response.status === 405) {
			ErrorService.errorHandle(response.status);
		}
		else {
			notificationFactory.error('ErrorMessage');
		}
	};

	/**
	* Initialize data to view Index.
	*/
	targetTemplateFactory.getAllTargetType().then(successGetAllTargetType);

	/**
	* Events 
	*/
	targetTemplateUtility.showMessageAfterValidate($scope);

	// Submit
	$scope.addTargetTemplate = function (template) {
		$scope.submitted = true;
		if (targetTemplateUtility.targetTemplateValidation(template)) {
			targetTemplateFactory.addTargetTemplate(template).then(successAddTargetTemplate, errorAddTargetTemplate);
		}
	};

	// Cancel
	$scope.cancel = function () { $window.history.back(); };
} ]);


targetTemplateModule.controller('DetailTargetTemplateController',
['$scope', '$routeParams', '$window', 'targetTemplateFactory', 'ErrorService',
function ($scope, $routeParams, $window, targetTemplateFactory, ErrorService) {
	/**
	* Variables. 
	*/
	var targetTemplateId = $routeParams.targetTemplateId;

	/**
	* Ajax event Loaded. 
	*/
	var succesGetListKPI = function (data) {
		$scope.lstKPI = data;
	};

	var errorHandle = function (status) {
		ErrorService.errorHandle(status);
	};

	/**
	* Initialize data to view Details.
	*/
	targetTemplateFactory.getTargetTemplateById(targetTemplateId).then(function (data) {
		$scope.editTemplate = data;
		$scope.currentTargetTemplateName = data.name;
		targetTemplateFactory.getKPI(targetTemplateId, null, null, true).then(succesGetListKPI);
	}, errorHandle);

	// Back
	$scope.back = function () { $window.history.back(); };
} ]);