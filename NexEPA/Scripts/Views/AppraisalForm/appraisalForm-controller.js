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

	$scope.query = function (filter) {
		if (filter) {
			getAppraisalForm(filter.epaTypeId, filter.keyword, filter.year, filter.month);
		}
	};

	$scope.create = function () {
		$scope.createAppraisalUrl = '/AppraisalForm/Create';
	};

	$scope.reload = function () {
		$route.reload();
	};
} ]);

// Create Appraisal Form controller
epaModule.controller('CreateAppraisalFormController',
['$scope', 'targetEmployeeSettingFactory', 'notificationFactory', 'employeeRepository',
function ($scope, targetEmployeeSettingFactory, notificationFactory, employeeRepository) {

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
} ]);

// Basic Information controller
epaModule.controller('BasicInformationController',
['$scope',
function ($scope) {
	$scope.isCollapsed = false;

	$scope.collapseTable = function () {
		$scope.isCollapsed = !$scope.isCollapsed;
	};
} ]);

// Date Detail controller
epaModule.controller('DateDetailController',
['$scope', function ($scope) {
	$scope.isCollapsed = false;

	$scope.collapseTable = function () {
		$scope.isCollapsed = !$scope.isCollapsed;
	};

	$scope.roles = [
		{
			roleId: 0,
			name: 'Admin'
		},
		{
			roleId: 1,
			name: 'Self'
		},
		{
			roleId: 2,
			name: 'Evaluator'
		},
		{
			roleId: 3,
			name: 'Reviewer'
		}
	];

	$scope.calendar = {
		startDateOpened: '',
		endDateOpened: '',
		alarmDateOpened: ''
	};


	$scope.openPopupStartDate = function ($event, roleId) {
		$event.preventDefault();
		$event.stopPropagation();

		var dateOpened = 'startDateOpened'.concat(roleId);

		$scope.calendar[dateOpened] = true;
	};

	$scope.openPopupEndDate = function ($event, roleId) {
		$event.preventDefault();
		$event.stopPropagation();

		var dateOpened = 'endDateOpened'.concat(roleId);

		$scope.calendar[dateOpened] = true;
	};

	$scope.openPopupAlarmDate = function ($event, roleId) {
		$event.preventDefault();
		$event.stopPropagation();

		var dateOpened = 'alarmDateOpened'.concat(roleId);

		$scope.calendar[dateOpened] = true;
	};
} ]);