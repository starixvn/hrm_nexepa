/* global departmentModule */
'use strict';

departmentModule.controller("ManageDepartmentController",
['$scope', '$window', '$modal', 'departmentDataFactory', 'notificationFactory', 'projectDataFactory','commonFactory', 'ErrorService',
function ($scope, $window, $modal, departmentDataFactory, notificationFactory, projectDataFactory, commonFactory, ErrorService) {
	/**
	* Scope Variable. 
	*/
	var selectedNode;
	$scope.hasTreeEvent = false;
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

	// find Children of a Node
	var findChild = function (data) {
		$.each(data.children, function (i) {
			var currentItem = data.children[i];
			if (currentItem != null) {
				$scope.exclusiveItem.push(currentItem.departmentId);
				findChild(currentItem);
			}
		});
	};

	/**
	* Ajax event Loaded. 
	*/
	var successGetAllDepartment = function (data) {
		$scope.department = data.item1;
		$scope.departments = data.item2;
		resetCurrentData();
		$scope.hasTreeEvent = false;
	};
	var errorNotification = function (response) {
		if (response.status === 405) {
			ErrorService.errorHandle(response.status);
		}
		else {
			notificationFactory.error('ErrorMessage');
		}
	};

	var successAddNode = function () {
		notificationFactory.success('SuccessSaveData', '');
		departmentDataFactory.getAllDepartment().then(successGetAllDepartment, errorNotification);
	};

	var errorAddNode = function (response) { notificationFactory.error('', response); };

	var successDeleteNode = function (response) {
		if (response.data !== "") {
			notificationFactory.error('', response.data);
		}
		else {
			notificationFactory.success('SuccessSaveData', '');
		}
		departmentDataFactory.getAllDepartment().then(successGetAllDepartment, errorNotification);
	};

	var errorDeleteNode = function (response) {
		if (response.status === 405) {
			ErrorService.errorHandle(status);
		}
		else {
			notificationFactory.error("ErrorMessage");
		}
	};

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
	$scope.cancel = function () {
		resetCurrentData();
	};

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
				['Delete', function () {
					var deleteModalInstance = $modal.open({
						templateUrl: '/templates/Common/ModalConfirmContent.html',
						controller: "ModalConfirmController",
						resolve: {
							name: function () {
								return selectedNode.name;
							},
							action: function () {
								return "delete node";
							}
						}
					});
					deleteModalInstance.result.then(function () {
						departmentDataFactory.deleteDepartment(selectedNode.departmentId).then(successDeleteNode, errorDeleteNode);
					});

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
		$scope.exclusiveItem = [];
		findChild($scope.detailsNode);
	};

	// check child node and hide it in dropdown list - screen Add.
	$scope.hideChildNode = function (data) {
		var check = $.inArray(data.departmentId, $scope.exclusiveItem);
		if (check !== -1) {
			return true;
		}
		return false;
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

	$scope.treeEvent = function () {
		if (!$scope.hasTreeEvent) {
			$('.tree li:has(ul)').addClass('parent_li').find(' > span').attr('title', 'Collapse this branch');
			$('.tree li.parent_li > span').on('click', function (e) {
				var children = $(this).parent('li.parent_li').find(' > ul > li');
				if (children.is(":visible")) {
					children.hide('fast');
					$(this).attr('title', 'Expand this branch').find(' > i').addClass('glyphicon-plus-sign').removeClass('glyphicon-chevron-right');
				} else {
					children.show('fast');
					$(this).attr('title', 'Collapse this branch').find(' > i').addClass('glyphicon-chevron-right').removeClass('glyphicon-plus-sign');
				}
				e.stopPropagation();
			});
			$scope.hasTreeEvent = true;
		}

	};

} ]);