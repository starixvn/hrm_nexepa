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
'ErrorService', '$route',
function ($scope, targetEmployeeSettingFactory, notificationFactory, employeeRepository, appraisalFormFactory,
ErrorService, $route) {

	var currentDate = new Date();
	$scope.filter = {
		year: currentDate.getFullYear(),
		month: currentDate.getMonth() + 1
	};
	$scope.appraisalFormList = [];

	var getAppraisalForm = function (epaTypeId, keyword, year, month) {
		appraisalFormFactory.getAppraisalForm(epaTypeId, keyword, year, month).success(function (test) {
			$scope.appraisalFormList = test;
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

	// initialize filter
	init($scope, targetEmployeeSettingFactory, notificationFactory);

	appraisalFormFactory.getAllProcessPeriod().success(function (processList) {
		$scope.processList = processList;
	}).error(function () {
		notificationFactory.error("ErrorMessage");
	});

	commonAppraisalFormFactory.showRequireMessage($scope, notificationFactory);

	$scope.save = function (processTemplate, violation) {
		$scope.disabledSubmit = true;
		$scope.submitted = true;
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
['$scope', 'commonFactory',
function ($scope, commonFactory) {
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

	// Initialize
	init($scope, targetEmployeeSettingFactory, notificationFactory);

	appraisalFormFactory.getAppraisalFormById(epaId).success(function (appraisal) {
		$scope.appraisal = {
			epaTypeId: appraisal.epaType.epaTypeId,
			employeeId: appraisal.employee.employeeId,
			year: appraisal.year,
			month: appraisal.month
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

	commonAppraisalFormFactory.showRequireMessage($scope, notificationFactory);

	$scope.save = function (processTemplate, violation) {
		$scope.disabledSubmit = true;
		$scope.submitted = true;
		$scope.$apply();

		if (!violation) {
			var appraisal = {
				epaId: epaId,
				processDetails: processTemplate
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
			$scope.$apply();
		}
	};

	$scope.back = function () {
		$window.history.back();
	};
} ]);