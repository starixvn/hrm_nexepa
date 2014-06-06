/* global departmentModule */
'use strict';

departmentModule.controller("ManageDepartmentController",
['$scope', '$window', 'departmentDataFactory', 'notificationFactory', 'projectDataFactory', 'commonFactory',
function ($scope, $window, departmentDataFactory, notificationFactory, projectDataFactory, commonFactory) {
	/**
	* Scope Variable. 
	*/
	var selectedNode;
	$scope.hasTreeEvent = false;
	$scope.addMode = false;
	$scope.editMode = false;
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
		$scope.newNode = {};
		$scope.newMember = {};
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
	$scope.bindTreeEvent = function () {
		if (!$scope.hasTreeEvent) {
			$('.tree li:has(ul)').addClass('parent_li').find(' > span').attr('title', 'Collapse this branch');
			$('.tree li.parent_li > span').on('click', function (e) {
				var children = $(this).parent('li.parent_li').find(' > ul > li');
				if (children.is(":visible")) {
					children.hide('fast');
					$(this).attr('title', 'Expand this branch').find(' > i').addClass('glyphicon-plus').removeClass('glyphicon-minus');
				} else {
					children.show('fast');
					$(this).attr('title', 'Collapse this branch').find(' > i').addClass('glyphicon-minus').removeClass('glyphicon-plus');
				}
				e.stopPropagation();
			});
			$scope.hasTreeEvent = true;
		}
	};
	// Search member
	$scope.getEmployee = function (searchKey) {
		projectDataFactory.getEmployeeSelectList(null, searchKey).then(function (employees) {
			$scope.searchEmployees = employees;
		});
	};

	// Cancel
	$scope.cancel = function () { $window.history.back(); };

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
					debugger;
					$scope.addMode = true;
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
			var managerId;
			var newMember = $scope.newMember;
			if (newMember != null) {
				managerId = newMember.employee ? newMember.employee.employeeId : null;
			}

			var allowAdd = departmentValid(newNode.code, newNode.name, managerId);
			if (allowAdd) {
				var manager = { employeeId: managerId };
				var model = { name: newNode.name, code: newNode.code, manager: manager, parentId: departmentId };
				departmentDataFactory.addDepartment(model).then(successAddNode, errorAddNode);

			}
		}
	};

	$scope.cancelAddChild = function () {
		resetCurrentData();
	};

	$scope.detailsNode = function () {
	
	};

} ]);