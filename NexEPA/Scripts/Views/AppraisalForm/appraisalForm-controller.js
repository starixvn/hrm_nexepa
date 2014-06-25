'use strict';
/*global epaModule*/

var init = function ($scope, targetEmployeeSettingFactory, notificationFactory) {
	targetEmployeeSettingFactory.getEPAType().success(function (listEPAType) {
		$scope.listEPAType = listEPAType;
	}).error(function () {
		notificationFactory.error("ErrorMessage");
	});

	// get Year
	var currentDate = new Date(),
			currentYear = currentDate.getFullYear(),
			startYear = currentYear - 3;

	$scope.listYear = [];

	for (var i = 0; i < 5; i++) {
		var year = {
			label: "" + startYear,
			value: startYear
		};
		startYear += 1;
		$scope.listYear.push(year);
	}

	// get Month
	$scope.listMonth = [];
	for (var j = 1; j <= 12; j++) {
		var month = {
			label: "" + j,
			value: j
		};
		$scope.listMonth.push(month);
	}
};

// View Appraisal Form controller
epaModule.controller('ViewAppraisalFormController',
['$scope', 'targetEmployeeSettingFactory', 'notificationFactory', 'employeeRepository', 'appraisalFormFactory',
'ErrorService', '$route', '$modal', 'targetUtility',
function ($scope, targetEmployeeSettingFactory, notificationFactory, employeeRepository, appraisalFormFactory,
ErrorService, $route, $modal, targetUtility) {

	var currentDate = new Date();
	$scope.filter = {
		year: currentDate.getFullYear(),
		month: currentDate.getMonth() + 1
	};
	$scope.appraisalFormList = [];

	var getAppraisalForm = function (epaTypeId, keyword, year, month) {
		appraisalFormFactory.getAppraisalForm(epaTypeId, keyword, year, month).success(function (appraisalFormList) {
			$scope.appraisalFormList = appraisalFormList;
		}).error(function (reaponse, status) {
			if (status === 405) {
				ErrorService.errorHandle(status);
			}
			else {
				notificationFactory.error("ErrorMessage");
			}
		});
	};

	// initialize filter
	init($scope, targetEmployeeSettingFactory, notificationFactory);

	// get all status
	targetUtility.getAllStatus($scope);

	appraisalFormFactory.getAppraisalFormPermission().success(function (policy) {
		$scope.permission = {
			policy : policy
		};
	}).error(function () {
		notificationFactory.error("ErrorMessage");
	});

	getAppraisalForm(null, null, $scope.filter.year, $scope.filter.month);

	$scope.query = function (filter) {
		if (filter) {
			getAppraisalForm(filter.epaTypeId, filter.keyword, filter.year, filter.month);
		}
	};

	$scope.create = function () {
		$scope.createAppraisalUrl = '/AppraisalForm/Create';
	};

	$scope.edit = function (epaId) {
		$scope.editAppraisalUrl = '/AppraisalForm/Edit/' + epaId;
	};

	$scope.openDeletePopup = function (appraisal) {
		var epaId = appraisal.epaId;

		var modalDeleteInstance = $modal.open({
			templateUrl: '/templates/Common/ModalConfirmContent.html',
			controller: "ModalConfirmController",
			resolve: {
				name: function () {
					return "";
				},
				action: function () {
					return "delete";
				}
			},
			windowClass: "modal-confirm-center"
		});

		modalDeleteInstance.result.then(function () {
			appraisalFormFactory.deleteAppraisalForm(epaId).success(function (response) {
				if (response.isSuccessful === true) {
					var index = $scope.appraisalFormList.indexOf(appraisal);
					$scope.appraisalFormList.splice(index, 1);
					notificationFactory.success("DataChangeSuccessfully");
				}
				else {
					notificationFactory.error("DataChangeFail", response.message);
				}
			}).error(function () {
				notificationFactory.error("ErrorMessage");
			});
		});
	};

	$scope.updateStatus = function (appraisal, status) {
		if (appraisal && status) {
			var appraisalStatus = {
				epaId: appraisal.epaId,
				status: status.id
			};

			var modalUpdateStatusInstance = $modal.open({
				templateUrl: '/templates/Common/modalConfirmContent.html',
				controller: "ModalConfirmController",
				resolve: {
					name: function () {
						return "";
					},
					action: function () {
						return status.name;
					}
				},
				windowClass: "modal-confirm-center"
			});

			modalUpdateStatusInstance.result.then(function () {
				appraisalFormFactory.updateStatusAppraisalForm(appraisalStatus).success(function (response) {
					if (response.isSuccessful === true) {
						appraisal.statusName = status.name;
						notificationFactory.success("DataChangeSuccessfully");
					}
					else {
						notificationFactory.error("", response.message);
					}
				}).error(function (response, status) {
					if (status === 405) {
						ErrorService.errorHandle(status);
					}
					else {
						notificationFactory.error("ErrorMessage");
					}
				});
			});
		}
	};

	$scope.reload = function () {
		$route.reload();
	};
} ]);

// Create Appraisal Form controller
epaModule.controller('CreateAppraisalFormController',
['$scope', 'targetEmployeeSettingFactory', 'notificationFactory', 'employeeRepository', 'appraisalFormFactory',
'ErrorService', 'commonAppraisalFormFactory', '$timeout', '$window', '$location',
function ($scope, targetEmployeeSettingFactory, notificationFactory, employeeRepository, appraisalFormFactory,
ErrorService, commonAppraisalFormFactory, $timeout, $window, $location) {

	var currentDate = new Date();

	$scope.appraisal = {
		year: currentDate.getFullYear(),
		month: currentDate.getMonth() + 1
	};

	// get all employee
	$scope.getEmployee = function (searchKey) {
		employeeRepository.getAllEmployee(searchKey, null).success(function (employees) {
			$scope.searchEmployees = employees;
		}).error(function () {
			notificationFactory.error("ErrorMessage");
		});
	};

	appraisalFormFactory.getAppraisalFormPermission().success(function (policy) {
		$scope.permission = {
			policy: policy
		};
	}).error(function () {
		notificationFactory.error("ErrorMessage");
	});

	// initialize filter
	init($scope, targetEmployeeSettingFactory, notificationFactory);

	appraisalFormFactory.getAllProcessPeriod().success(function (processList) {
		$scope.processList = processList;
	}).error(function () {
		notificationFactory.error("ErrorMessage");
	});

	commonAppraisalFormFactory.showRequireMessage($scope, notificationFactory);

	$scope.save = function (processTemplate, requireDateViolation, sameProcessDateViolation, differentProcessDateViolation) {
		var violation = requireDateViolation || sameProcessDateViolation || differentProcessDateViolation;
		$scope.disabledSubmit = true;
		$scope.submitted = true;
		$scope.requireDateViolation = requireDateViolation;
		$scope.sameProcessDateViolation = sameProcessDateViolation;
		$scope.differentProcessDateViolation = differentProcessDateViolation;
		$scope.$apply();

		if (!violation) {
			var appraisal = {
				epaTypeId: $scope.appraisal.epaTypeId,
				employeeId: $scope.appraisal.employee.employeeId,
				month: $scope.appraisal.month,
				year: $scope.appraisal.year,
				processDetails: processTemplate
			};

			if (commonAppraisalFormFactory.validateAppraisalForm(appraisal)) {
				appraisalFormFactory.insertAppraisalForm(appraisal).success(function (response) {
					if (response.isSuccessful === true) {
						notificationFactory.success("DataChangeSuccessfully");

						$timeout(function () {
							$location.url('/AppraisalForm/Index/');
						}, 2000);
					}
					else {
						notificationFactory.error("DataChangeFail", response.message);
					}

					$scope.disabledSubmit = false;
				}).error(function (response, status) {
					if (status === 405) {
						ErrorService.errorHandle(status);
					}
					else {
						notificationFactory.error("ErrorMessage");
					}

					$scope.disabledSubmit = false;
				});
			}
		}
		else {
			$scope.disabledSubmit = false;
			$scope.submitted = false;
			$scope.requireViolation = false;
			$scope.sameProcessDateViolation = false;
			$scope.differentProcessDateViolation = false;
			$scope.$apply();
		}
	};

	$scope.back = function () {
		$window.history.back();
	};
} ]);

// Basic Information controller
epaModule.controller('BasicInformationController',
['$scope',
function ($scope) {
	$scope.isCollapsed = false;

	$scope.collapseTable = function () {
		$scope.isCollapsed = !$scope.isCollapsed;
	};

	$scope.$watch('search.searchKey', function (newValue) {
		if (!newValue) {
			$scope.appraisal.employee = '';
		}
	});
} ]);

// Date Detail controller
epaModule.controller('DateDetailController',
['$scope', 'commonFactory', '$modal',
function ($scope, commonFactory, $modal) {
	$scope.isCollapsed = false;

	$scope.collapseTable = function () {
		$scope.isCollapsed = !$scope.isCollapsed;
	};

	// get Date Format
	commonFactory.getDateFormat($scope);

	$scope.calendar = {};

	$scope.openPopupDate = function ($event, dateOpened) {
		$event.preventDefault();
		$event.stopPropagation();

		$scope.calendar[dateOpened] = true;
		$scope.$apply();
	};

	$scope.editEvaluator = function (process, evaluatorList) {
		var modalUpdateStatusInstance = $modal.open({
			templateUrl: '/templates/AppraisalForm/EditEvaluator.html',
			controller: "EditEvaluatorController",
			resolve: {
				currentEvaluator: function () {
					return evaluatorList;
				}
			},
			windowClass: "modal-confirm-center"
		});

		modalUpdateStatusInstance.result.then(function (evaluatorList) {
			process.evaluatorList = evaluatorList;
		});
	};
} ]);

// Edit Appraisal Form controller
epaModule.controller('EditAppraisalFormController',
['$scope', '$window', 'targetEmployeeSettingFactory', 'notificationFactory', 'employeeRepository', 'targetUtility',
'appraisalFormFactory', 'ErrorService', '$routeParams', 'commonAppraisalFormFactory', '$timeout', '$location',
function ($scope, $window, targetEmployeeSettingFactory, notificationFactory, employeeRepository, targetUtility,
appraisalFormFactory, ErrorService, $routeParams, commonAppraisalFormFactory, $timeout, $location) {

	var epaId = parseInt($routeParams.id);

	$scope.isEditMode = true;
	$scope.appraisal = {};

	// get all employee
	$scope.getEmployee = function (searchKey) {
		employeeRepository.getAllEmployee(searchKey, null).success(function (employees) {
			$scope.searchEmployees = employees;
		}).error(function () {
			notificationFactory.error("ErrorMessage");
		});
	};

	// get all status
	targetUtility.getAllStatus($scope);

	appraisalFormFactory.getAppraisalFormPermission().success(function (policy) {
		$scope.permission = {
			policy: policy
		};
	}).error(function () {
		notificationFactory.error("ErrorMessage");
	});

	// Initialize
	init($scope, targetEmployeeSettingFactory, notificationFactory);

	appraisalFormFactory.getAppraisalFormById(epaId).success(function (appraisal) {
		$scope.appraisal = {
			epaTypeId: appraisal.epaType.epaTypeId,
			employeeId: appraisal.employee.employeeId,
			code: appraisal.employee.code,
			year: appraisal.year,
			month: appraisal.month,
			status: appraisal.status
		};

		$scope.processList = appraisal.epaProcess;

		$scope.search = {
			searchKey: appraisal.employee.nickName + ' (' + appraisal.employee.fullName + ')'
		};
	}).error(function (response, status) {
		if (status === 405) {
			ErrorService.errorHandle(status);
		}
		else {
			notificationFactory.error("ErrorMessage");
		}
	});

	var getEvaluatorDetail = function (processList) {
		var evaluatorDetails = '<ROOT>';

		angular.forEach(processList, function (process) {
			var evaluatorList = process.evaluatorList;

			angular.forEach(evaluatorList, function (evaluator) {
				var evaluatorPattern = '<Process ProcessId="' + process.processId +
						'" EvaluatorId="' + evaluator.employeeId + '" ></Process>';
					evaluatorDetails += evaluatorPattern;
			});
		});

		evaluatorDetails += '</ROOT>';
		return evaluatorDetails;
	};

	commonAppraisalFormFactory.showRequireMessage($scope, notificationFactory);

	$scope.save = function (processTemplate, requireDateViolation, sameProcessDateViolation, differentProcessDateViolation) {
		var violation = requireDateViolation || sameProcessDateViolation || differentProcessDateViolation;
		$scope.disabledSubmit = true;
		$scope.submitted = true;
		$scope.requireDateViolation = requireDateViolation;
		$scope.sameProcessDateViolation = sameProcessDateViolation;
		$scope.differentProcessDateViolation = differentProcessDateViolation;
		$scope.$apply();

		if (!violation) {
			var appraisal = {
				epaId: epaId,
				status: $scope.appraisal.status,
				processDetails: processTemplate,
				evaluateDetails: getEvaluatorDetail($scope.processList)
			};

			if (epaId && epaId > 0) {
				appraisalFormFactory.updateAppraisalForm(appraisal).success(function (response) {
					if (response.isSuccessful === true) {
						notificationFactory.success("DataChangeSuccessfully");

						$timeout(function () {
							$location.url('/AppraisalForm/Index/');
						}, 2000);
					}
					else {
						notificationFactory.error("DataChangeFail", response.message);
					}

					$scope.disabledSubmit = false;
				}).error(function (response, status) {
					if (status === 405) {
						ErrorService.errorHandle(status);
					}
					else {
						notificationFactory.error("ErrorMessage");
					}

					$scope.disabledSubmit = false;
				});
			}
		}
		else {
			$scope.disabledSubmit = false;
			$scope.submitted = false;
			$scope.sameProcessDateViolation = false;
			$scope.differentProcessDateViolation = false;
			$scope.$apply();
		}
	};

	$scope.back = function () {
		$window.history.back();
	};
} ]);

epaModule.controller("EditEvaluatorController",
['$scope', 'currentEvaluator', 'departmentDataFactory', 'employeeRepository', '$modalInstance', 'commonFactory',
'notificationFactory',
function ($scope, currentEvaluator, departmentDataFactory, employeeRepository, $modalInstance, commonFactory,
notificationFactory) {
	$scope.currentEvaluator = angular.copy(currentEvaluator);

	departmentDataFactory.getAllDepartment().then(function (departments) {
		$scope.departments = departments.item1.children;
	});

	$scope.$watch('departments', function () {
		if ($scope.departments !== undefined) {
			$scope.departmentOptions = commonFactory.generateOptions($scope.departments, [], 0);
		}
	});

	var removeExistEmployees = function (employees, currentEvaluator) {
		angular.forEach(currentEvaluator, function (evaluator) {
			for (var i = 0; i < employees.length; i++) {
				var employee = employees[i];

				if (employee.employeeId === evaluator.employeeId) {
					employees.splice(i, 1);
				}
			}
		});

		return employees;
	};

	$scope.defaultEmployees = [];

	$scope.getEmployee = function (departmentId) {
		employeeRepository.getEmployeeInDepartment(departmentId).success(function (data) {
			$scope.defaultEmployees = removeExistEmployees(data.item1, $scope.currentEvaluator);
		}).error(function () {
			notificationFactory.error("ErrorMessage");
		});
	};

	var moveEmployee = function (employeeIds, from, to) {
		angular.forEach(employeeIds, function (employeeId) {
			for (var index = 0; index < from.length; index++) {
				var employee = from[index];

				if (employee.employeeId === employeeId) {
					to.push(employee);
					from.splice(index, 1);
				}
			}
		});
	};

	var moveAllEmployee = function (from, to) {
		angular.forEach(from, function (employee) {
			to.push(employee);
		});

		from.length = 0;
	};

	$scope.moveItem = function (employeeIds, from, to) {
		moveEmployee(employeeIds, from, to);
	};

	$scope.moveAll = function (from, to) {
		moveAllEmployee(from, to);
	};

	$scope.ok = function () {
		$modalInstance.close($scope.currentEvaluator);
	};

	$scope.cancel = function () {
		$modalInstance.dismiss("cancel");
	};
} ]);