/* global flowManagementModule */
'use strict';

flowManagementModule.controller("FlowManagementController", ['$scope', 'employeeRepository',
'notificationFactory', '$modal', 'flowManagementService', 'ErrorService', 'departmentDataFactory', 'flowDataFactory', '$window',
function ($scope, employeeRepository, notificationFactory, $modal, flowManagementService, ErrorService,
departmentDataFactory, flowDataFactory, $window) {
	/**
	* Scope Variable. 
	*/
	$scope.admins = [];
	$scope.selfs = [{ nickName: "Self"}];
	$scope.evaluators = [];
	$scope.reviewers = [];
	$scope.currentSelectedEmployee;
	$scope.showList = false;
	$scope.EPAFlow = {};
	$scope.flowType = null;

	/**
	* Validation. 
	*/
	var addEmployeeToList = function (listIdentity, employee, position) {
		var list;
		var allowAdd = true;
		switch (listIdentity) {
			case "adminList":
				list = $scope.admins;
				break;
			case "evaluatorList":
				list = $scope.evaluators;
				break;
			case "reviewerList":
				list = $scope.reviewers;
				break;
		}
		$.each(list, function (i) {
			if (list[i].employeeId === employee.employeeId) {
				allowAdd = false;
			}
		});

		if (allowAdd) {
			if (position != null) {
				list.splice(position, 0, employee);
			}
			else {
				list.push(employee);
			}
		}
		else {
			notificationFactory.error('', 'This employee has already exists');
		}
	};

	/**
	* Ajax Event loaded. 
	*/
	var successGetAllDepartment = function (data) {
		$scope.departments = data.item2;
	};

	var successGetFlow = function (data) {
		resetCurrentData();
		if (data !== "null") {
			$.each(data, function (i) {
				var processId = data[i].processId;
				switch (processId) {
					case 2:
						$scope.admins.push(data[i].evaluatorInfo);
						break;
					case 4:
						$scope.evaluators.push(data[i].evaluatorInfo);
						break;
					case 5:
						$scope.reviewers.push(data[i].evaluatorInfo);
						break;
				}
			});
		}
	};

	var successSaveFlow = function () {
		notificationFactory.success('SuccessSaveData', '');
	};

	var successDeleteFlow = function () {
		notificationFactory.success('SuccessDeleteData', '');
		resetCurrentData();
		$scope.EPAFlow = {};
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
	* Popup Add Member. 
	*/
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
	* Initialize data to view. 
	*/

	var resetCurrentData = function () {
		$scope.admins = [];
		$scope.selfs = [{ nickName: "Self"}];
		$scope.evaluators = [];
		$scope.reviewers = [];
	};

	var getEmployee = function (keyword, groupId) {
		employeeRepository.getAllEmployee(keyword, groupId).success(function (employees) {
			$scope.employees = employees;
		}).error(function () {
			notificationFactory.error("ErrorMessage");
		});
	};

	var getEmployeeById = function (sender, employeeId, position) {
		employeeRepository.getEmployeeById(parseInt(employeeId)).success(function (data) {
			addEmployeeToList(sender, data, position);
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
			addEmployeeToList(value.currentList, value.employee);
		}
	});

	departmentDataFactory.getAllDepartment().then(successGetAllDepartment, errorNotification);

	/**
	* Events.
	*/

	// Show or hide list Employee.
	$scope.toggleShowList = function () {
		$scope.showList = !$scope.showList;
	};

	// Sort item in list.
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

	// On Created list after Drag & Drop.
	$scope.$on('list-created', function (ev, val) {
		getEmployeeById(val.sender, val.id, val.to);
	});

	// Remove item from list.
	var removeItemFromList = function (item, list) {
		var index = list.indexOf(item);
		if (index !== -1) {
			list.splice(index, 1);
		}
	};

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

	// Cancel
	$scope.cancel = function () { $window.history.back(); };

	// Delete
	$scope.deleteFlow = function () {
		var deleteModalInstance = $modal.open({
			templateUrl: '/templates/Common/ModalConfirmContent.html',
			controller: "ModalConfirmController",
			resolve: {
				name: function () {
					return "";
				},
				action: function () {
					return "delete";
				}
			}
		});
		deleteModalInstance.result.then(function () {
			flowDataFactory.deleteFlow($scope.EPAFlow.departmentId, $scope.flowType).then(successDeleteFlow, errorNotification);
		});
	};

	//Dropdownlist Change.
	$scope.OnSelectedDepartment = function () {
		$scope.flowType = $scope.EPAFlow.departmentId;
		flowDataFactory.getFlow($scope.EPAFlow.departmentId, $scope.flowType).then(successGetFlow, errorNotification);
	};

	$scope.saveFlow = function () {
		if ($scope.flowType === null) {
			notificationFactory.error('', 'Please select a department!');
		}
		else {
			var model = { departmentId: $scope.EPAFlow.departmentId, flowType: $scope.flowType,
				admins: $scope.admins, evaluators: $scope.evaluators, reviewers: $scope.reviewers
			};
			flowDataFactory.saveFlow(model).then(successSaveFlow, errorNotification);
		}
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