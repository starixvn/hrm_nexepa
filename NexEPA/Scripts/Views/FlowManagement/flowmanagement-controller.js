/* global flowManagementModule */
'use strict';

flowManagementModule.controller("FlowManagementController", ['$scope', 'employeeRepository',
'notificationFactory', '$modal', 'flowManagementService',
function ($scope, employeeRepository, notificationFactory, $modal, flowManagementService) {
	/**
	* Scope Variable. 
	*/
	$scope.admins = [];
	$scope.selfs = [];
	$scope.evaluators = [];
	$scope.reviewers = [];
	$scope.currentSelectedEmployee;
	$scope.showList = false;

	/**
	* Ajax data loaded. 
	*/

	/**
	* Popup Add Member. 
	*/
	// Popup Add New Memeber.

	var addEmployeeToFlow = function (currentList) {
		$modal.open({
			templateUrl: '/templates/FlowManagement/AddEmployee.html',
			controller: "AddEmployeeFlowManagementController",
			backdrop: 'static',
			resolve: {
				currentList: function () {
					return currentList;
				}
			}
		});
	};

	/**
	* Initialize. 
	*/
	var getEmployee = function (keyword, groupId) {
		employeeRepository.getAllEmployee(keyword, groupId).success(function (employees) {
			$scope.employees = employees;
		}).error(function () {
			notificationFactory.error("ErrorMessage");
		});
	};
	getEmployee(null, null);

	// Watch for Employee is added to list.
	$scope.$watch(function () {
		return flowManagementService.getProperty();
	}, function (value) {
		if (value != null) {
			switch (value.currentList) {
				case "adminList":
					$scope.admins.push(value.employee);
					debugger;
					break;
				case "evaluatorList":
					$scope.evaluators.push(value.employee);
					break;
				case "reviewerList":
					$scope.reviewers.push(value.employee);
					break;
			}
		}
	});

	/**
	* Events.
	*/
	$scope.toggleShowList = function () {
		$scope.showList = !$scope.showList;
	};

	$scope.$on('list-sorted', function (ev, val) {
		if (val.sender === "adminList") {
			$scope.admins.splice(val.to, 0, $scope.admins.splice(val.from, 1)[0]);
		}
		if (val.sender === "selfList") {
			$scope.selfs.splice(val.to, 0, $scope.selfs.splice(val.from, 1)[0]);
		}
		if (val.sender === "evaluatorList") {
			$scope.evaluators.splice(val.to, 0, $scope.evaluators.splice(val.from, 1)[0]);
		}
		if (val.sender === "reviewerList") {
			$scope.reviewers.splice(val.to, 0, $scope.reviewers.splice(val.from, 1)[0]);
		}
	});

	$scope.$on('list-created', function (ev, val) {
		employeeRepository.getEmployeeById(parseInt(val.id)).success(function (data) {
			if (val.sender === "adminList") {
				$scope.admins.splice(val.to, 0, data);
			}
			if (val.sender === "selfList") {
				$scope.selfs.splice(val.to, 0, data);
			}
			if (val.sender === "evaluatorList") {
				$scope.evaluators.splice(val.to, 0, data);
			}
			if (val.sender === "reviewerList") {
				$scope.reviewers.splice(val.to, 0, data);
			}
		}).error(function () {
			notificationFactory.error("ErrorMessage");
		});
	});

	var removeItemFromList = function (item, list) {
		var index = list.indexOf(item);
		if (index !== -1) {
			list.splice(index, 1);
		}
	};

	// Remove item from list.
	$scope.removeAdmin = function (admin) {
		removeItemFromList(admin, $scope.admins);
	};

	$scope.removeEvaluator = function (evaluator) {
		removeItemFromList(evaluator, $scope.evaluators);
	};

	$scope.removeReviewer = function (reviewer) {
		removeItemFromList(reviewer, $scope.reviewers);
	};

	// Add item to list
	$scope.addAdmin = function () {
		addEmployeeToFlow("adminList");
	};
	$scope.addEvaluator = function () {
		addEmployeeToFlow("evaluatorList");
	};
	$scope.addReviewer = function () {
		addEmployeeToFlow("reviewerList");
	};

} ]);

flowManagementModule.controller("AddEmployeeFlowManagementController", ['$scope', '$modalInstance',
'projectDataFactory', 'flowManagementService', 'currentList',
function ($scope, $modalInstance, projectDataFactory, flowManagementService, currentList) {
	/**
	* Scope variable.
	*/
	$scope.newEmployee = {};
	$scope.currentList = currentList;
	/**
	* Initialize.
	*/
	$scope.getEmployee = function (searchKey) {
		projectDataFactory.getEmployeeSelectList(null, searchKey).then(function (employees) {
			$scope.searchEmployees = employees;
		});
	};

	/**
	* Events.
	*/
	$scope.cancel = function () {
		$modalInstance.dismiss('cancel');
	};
	$scope.ok = function () {
		flowManagementService.setProperty({ employee: $scope.newEmployee.employee, currentList: $scope.currentList });
		$modalInstance.close();
	};

} ]);