/* global departmentModule */
'use strict';

departmentModule.controller("ManageDepartmentController",
['$scope', '$window', 'departmentDataFactory', 'notificationFactory', 'projectDataFactory', 'commonFactory',
function ($scope, $window, departmentDataFactory, notificationFactory, projectDataFactory, commonFactory) {
	/**
	* Scope Variable. 
	*/
	var selectedNode;
	var departmentValid = function (code, name, managerId) {
		var error = $("#departmentValid");
		if (code !== "" && name !== "" && managerId > 0) {
			error.hide();
			return true;
		}
		error.show();
		return false;
	};

	var resetCurrentData = function () {
		$scope.addMode = false;
		$scope.editMode = false;
		$scope.newNode = {};
		$scope.detailsNode = {};
		$scope.selectedNode = {};
		$("#txtManager").val(null);
	};

	/**
	* Ajax event Loaded. 
	*/
	var successGetAllDepartment = function (data) {
		$scope.department = data.item1;
		$scope.departments = data.item2;
	};
	var errorNotification = function (response) {
		if (response != null) {
			notificationFactory.error('', response);
		}
		else {
			notificationFactory.error('ErrorMessage', '');
		}
	};

	var successAddNode = function () {
		notificationFactory.success('SuccessSaveData', '');
		resetCurrentData();
		departmentDataFactory.getAllDepartment().then(successGetAllDepartment, errorNotification);
	};

	var errorAddNode = function (response) { notificationFactory.error('', response); };

	/**
	* Initialize data to view Index.
	*/
	departmentDataFactory.getAllDepartment().then(successGetAllDepartment, errorNotification);
	commonFactory.getDateFormat($scope);

	/**
	* Events.
	*/

	// Search member
	$scope.getEmployee = function (searchKey) {
		projectDataFactory.getEmployeeSelectList(null, searchKey).then(function (employees) {
			$scope.searchEmployees = employees;
		});
	};

	// Cancel
	$scope.cancel = function () { resetCurrentData(); };

	// show start date calendar when click button start date
	$scope.openPopupStartDate = function ($event) {
		commonFactory.openPopupStartDate($event, $scope);
	};

	// show end date calendar when click button end date
	$scope.openPopupEndDate = function ($event) {
		commonFactory.openPopupEndDate($event, $scope);
	};


	$scope.menuOptions = function (currentItem) {
		selectedNode = currentItem;
		return [
				['Add Child', function () {
					$scope.selectedNode = selectedNode;
					$scope.addMode = true;
					$scope.editMode = false;
				} ],
				['Delete', function ($itemScope) {
					$scope.player.gold += $itemScope.item.cost;
				} ]
			];
	};

	$scope.addChild = function (newNode) {
		if (newNode == null) {
			$("#departmentValid").show();
		}
		else {
			var departmentId = $scope.selectedNode.departmentId;
			var managerId = newNode.manager ? newNode.manager.employeeId : null;

			var allowAdd = departmentValid(newNode.code, newNode.name, managerId);
			if (allowAdd) {
				var model = { name: newNode.name, code: newNode.code, manager: newNode.manager, parentId: departmentId };
				departmentDataFactory.addDepartment(model).then(successAddNode, errorAddNode);
			}
		}
	};

	$scope.viewDetails = function (currentItem) {
		$scope.detailsNode = currentItem;
		$scope.detailsNode.searchKey = currentItem.manager.nickName + " (" + currentItem.manager.fullName + ")";
		$scope.addMode = false;
		$scope.editMode = true;
		$scope.isDisabled = true;

	};

	$scope.toggleEditMode = function () {
		$scope.isDisabled = false;
	};

	$scope.editChild = function (detailsNode) {
		var allowEdit = departmentValid(detailsNode.code, detailsNode.name, detailsNode.manager.employeeId);
		if (allowEdit) {
			var model = { name: detailsNode.name, code: detailsNode.code, manager: detailsNode.manager,
				parentId: detailsNode.parentId, departmentId: detailsNode.departmentId, isDisabled: detailsNode.isDisabled
			};
			departmentDataFactory.updateDepartment(model).then(successAddNode, errorAddNode);
		}
	};
} ]);