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
	$scope.selfs = [{ nickName: "Self", employeeId: -1}];
	$scope.evaluators = [];
	$scope.reviewers = [];

	$scope.personalAdmins = [];
	$scope.personalEmployees = [];
	$scope.personalEvaluators = [];
	$scope.personalReviewers = [];

	$scope.showList = true;
	$scope.EPAFlow = {};
	$scope.flowType = null;
	$scope.selectedEmployee = false;

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
			case "personalAdminList":
				list = $scope.personalAdmins;
				break;
			case "personalEmployeeList":
				list = $scope.personalEmployees;
				break;
			case "personalEvaluatorList":
				list = $scope.personalEvaluators;
				break;
			case "personalReviewerList":
				list = $scope.personalReviewers;
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

	var createLabel = function (departmentItem, level) {
		var label = '',
			space = '',
			levelTemp = level;

		if (level > 0) {
			while (levelTemp > 0) {
				label = label + " " + space;
				space = String.fromCharCode(160) + String.fromCharCode(160) + String.fromCharCode(160);
				levelTemp--;
			}

			label = label + departmentItem.name;
		}
		else {
			label = departmentItem.name;
		}

		return label;
	};

	var generateOptions = function (departments, options, level) {
		angular.forEach(departments, function (departmentItem) {
			options.push({ label: createLabel(departmentItem, level), value: departmentItem.departmentId });
			generateOptions(departmentItem.children, options, level + 1);
		});

		return options;
	};

	var successGetAllDepartment = function (data) {
		$scope.departments = data.item2;
		$scope.departmentOptions = generateOptions(data.item1.children, [], 0);
	};

	var successGetFlow = function (data) {
		resetCurrentDepartmentData();
		if (data.length > 0) {
			$.each(data, function (i) {
				var processId = data[i].processId;
				switch (processId) {
					case 2:
						$scope.admins.push(data[i].evaluatorInfo);
						break;
					case 3:
						var object;
						if (data[i].evaluatorInfo.employeeId === -1) {
							object = { nickName: "Self", employeeId: -1 };
						}
						else {
							object = data[i].evaluatorInfo;
						}
						$scope.selfs.push(object);
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
		else {
			$scope.selfs = [{ nickName: "Self", employeeId: -1}];
		}
	};

	var successSaveFlow = function () {
		notificationFactory.success('SuccessSaveData', '');
	};

	var successDeleteFlow = function () {
		notificationFactory.success('SuccessDeleteData', '');
		resetCurrentDepartmentData();
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

	var resetCurrentDepartmentData = function () {
		$scope.admins = [];
		$scope.selfs = [];
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
	$scope.toggleDepartmentDraggableList = function () {
		$scope.showList = !$scope.showList;
	};

	// Sort item in list.
	$scope.$on('list-sorted', function (ev, val) {
		var sender = val.sender;
		switch (sender) {
			case "adminList":
				$scope.admins.splice(val.to, 0, $scope.admins.splice(val.from, 1)[0]);
				break;
			case "selfList":
				$scope.selfs.splice(val.to, 0, $scope.selfs.splice(val.from, 1)[0]);
				break;
			case "evaluatorList":
				$scope.evaluators.splice(val.to, 0, $scope.evaluators.splice(val.from, 1)[0]);
				break;
			case "reviewerList":
				$scope.reviewers.splice(val.to, 0, $scope.reviewers.splice(val.from, 1)[0]);
				break;
			case "personalAdminList":
				$scope.personalAdmins.splice(val.to, 0, $scope.personalAdmins.splice(val.from, 1)[0]);
				break;
			case "personalEmployeeList":
				$scope.personalEmployees.splice(val.to, 0, $scope.personalEmployees.splice(val.from, 1)[0]);
				break;
			case "personalEvaluatorList":
				$scope.personalEvaluators.splice(val.to, 0, $scope.personalEvaluators.splice(val.from, 1)[0]);
				break;
			case "personalReviewerList":
				$scope.personalReviewers.splice(val.to, 0, $scope.personalReviewers.splice(val.from, 1)[0]);
				break;
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
		if ($scope.EPAFlow.departmentId == null) {
			notificationFactory.error('', 'Please select a department!');
		}
		else {
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
		}
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
				admins: $scope.admins, evaluators: $scope.evaluators, reviewers: $scope.reviewers, selfs: $scope.selfs
			};
			flowDataFactory.saveFlow(model).then(successSaveFlow, errorNotification);
		}
	};

	// TAB PERSONAL.
	$scope.tabPersonal = function () {
		/**
		* Scope variable.
		*/
		$scope.DepartmentList = {};
		$scope.personalDraggable = true;
		$scope.showDeleteButton = false;

		$scope.moveItem = function (item, from, to) {
			$.each(from, function (i) {
				var currentItem = from[i];
				if (currentItem.employeeId === parseInt(item)) {
					from.splice(i, 1);
					to.push(currentItem);
					return false;
				}
			});
		};
		$scope.moveAll = function (from, to) {
			$.each(from, function (i) {
				to.push(from[i]);
			});
			from.length = 0;
		};

		var getEmployeeFlowList = function () {
			employeeRepository.getEmployeeInDepartment($scope.DepartmentList.departmentId).success(function (data) {
				$scope.defaultSettingFlowEmployees = data.item1;
				$scope.personalSettingFlowEmployees = data.item2;
			}).error(function () {
				notificationFactory.error("ErrorMessage");
			});
		};

		var removeAllPersonalFlowData = function () {
			$scope.personalAdmins = [];
			$scope.personalEmployees = [];
			$scope.personalEvaluators = [];
			$scope.personalReviewers = [];
		};

		var successGetPersonalFlow = function (data) {
			removeAllPersonalFlowData();
			if (data.length > 0) {
				$scope.showDeleteButton = true;
				$.each(data, function (i) {
					var processId = data[i].processId;
					switch (processId) {
						case 2:
							$scope.personalAdmins.push(data[i].evaluatorInfo);
							break;
						case 3:
							$scope.personalEmployees.push(data[i].evaluatorInfo);
							break;
						case 4:
							$scope.personalEvaluators.push(data[i].evaluatorInfo);
							break;
						case 5:
							$scope.personalReviewers.push(data[i].evaluatorInfo);
							break;
					}
				});
			}
			else {
				$scope.showDeleteButton = false;
				getEmployeeById("personalEmployeeList", $scope.currentSelectedEmployee);
			}
		};

		var successDeletePersonalFlow = function () {
			notificationFactory.success('SuccessDeleteData', '');
			removeAllPersonalFlowData();
			$scope.currentSelectedEmployee = null;
			getEmployeeFlowList();
		};

		var successSavePersonalFlow = function () {
			notificationFactory.success('SuccessSaveData', '');
			$scope.showDeleteButton = true;
			getEmployeeFlowList();
		};

		/**
		* Events.
		*/
		// Add item to list
		$scope.addPersonalAdmin = function () {
			addEmployeeToFlow("personalAdminList");
		};
		$scope.addPersonalEvaluator = function () {
			addEmployeeToFlow("personalEvaluatorList");
		};
		$scope.addPersonalReviewer = function () {
			addEmployeeToFlow("personalReviewerList");
		};

		$scope.toggleDepartmentDragableList = function () {
			$scope.personalDraggable = !$scope.personalDraggable;
		};

		$scope.removePersonalAdmin = function (admin) {
			removeItemFromList(admin, $scope.personalAdmins);
		};

		$scope.removePersonalEvaluator = function (evaluator) {
			removeItemFromList(evaluator, $scope.personalEvaluators);
		};

		$scope.removePersonalReviewer = function (reviewer) {
			removeItemFromList(reviewer, $scope.personalReviewers);
		};

		$scope.OnSelectedPersonalDepartment = function () {
			getEmployeeFlowList();
		};

		$scope.setEmployeeFlow = function (item) {
			var employeeId = parseInt(item[0]);
			$scope.currentSelectedEmployee = employeeId;
			flowDataFactory.getPersonalFlow(employeeId).then(successGetPersonalFlow, errorNotification);
		};

		$scope.savePersonalFlow = function () {
			if ($scope.personalEmployees.length === 0) {
				notificationFactory.error('', 'Please select an employee!');
			}
			else {
				if ($scope.personalAdmins.length === 0 || $scope.personalEvaluators.lenth === 0 || $scope.personalReviewers.length === 0) {
					notificationFactory.error('', 'Admin list, Evaluator list and Reviewer list can not be null');
				}
				else {
					var model = { admins: $scope.personalAdmins, evaluators: $scope.personalEvaluators,
						reviewers: $scope.personalReviewers, personal: $scope.personalEmployees
					};

					flowDataFactory.savePersonalFlow(model).then(successSavePersonalFlow, errorNotification);
				}
			}
		};

		$scope.deletePersonalFlow = function () {
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
				var flowType = 0;
				flowDataFactory.deleteFlow($scope.currentSelectedEmployee, flowType).then(successDeletePersonalFlow, errorNotification);
			});
		};

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