'use strict';
/*global reportModule*/

reportModule.controller('ViewEmployeePerformanceByTeamController',
['$scope', '$route', '$location', 'reportFactory', 'notificationFactory', 'commonFactory', 'employeeRepository',
'employeeEPAFactory', 'ErrorService',
function ($scope, $route, $location, reportFactory, notificationFactory, commonFactory, employeeRepository,
employeeEPAFactory, ErrorService) {

	var date = new Date(),
		year = date.getFullYear(),
		month = date.getMonth(),
		day = date.getDate();

	$scope.filter = {
		fromDate: new Date(year, month - 3, day),
		toDate: new Date(year, month, day),
		groupId: null,
		projectId: null
	};

	$scope.search = {
		searchKey: ''
	};

	$scope.getEmployee = function (searchKey) {
		employeeRepository.getEmployeeByCurrentUserId(searchKey).success(function (employees) {
			$scope.searchEmployees = employees;
		}).error(function () {
			notificationFactory.error("ErrorMessage");
		});
	};

	employeeEPAFactory.getAllProject().then(function (projectList) {
		$scope.projectOptions = projectList;
	});

	commonFactory.getDateFormat($scope);

	employeeRepository.getAllGroup().success(function (groupList) {
		$scope.groupOptions = groupList;
	}).error(function () {
		notificationFactory.error("ErrorMessage");
	});

	$scope.calendar = {
		startDateOpened: false,
		endDateOpened: false
	};

	// show start date calendar when click button start date
	$scope.openPopupStartDate = function ($event) {
		commonFactory.openPopupStartDate($event, $scope);
	};

	// show end date calendar when click button end date
	$scope.openPopupEndDate = function ($event) {
		commonFactory.openPopupEndDate($event, $scope);
	};

	// collapse table project
	$scope.collapseTable = function () {
		$scope.isCollapsed = !$scope.isCollapsed;
	};

	var getEmployeePerformanceByTeamReport = function (filter) {
		var fromDate = filter.fromDate,
			toDate = filter.toDate,
			employeeId = null;

		fromDate = fromDate ? fromDate.toDateString() : null;
		toDate = toDate ? toDate.toDateString() : null;

		if ($scope.employee) {
			employeeId = $scope.employee.employeeId;
		}

		reportFactory.getEmployeePerformanceByTeamReport(filter, fromDate, toDate, employeeId)
		.success(function (report) {
			if (report != null) {
				$scope.reportByProject = report.reportByProject;
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
	};

	getEmployeePerformanceByTeamReport($scope.filter);

	$scope.getReport = function (filter) {
		if (filter) {
			$scope.disabledSubmit = true;
			getEmployeePerformanceByTeamReport(filter);
		}
	};

	$scope.viewEmployeeTendency = function (employeeId) {
		$scope.viewEmployeeTendencyUrl = '/Report/EmployeePerformanceTendency' + '?employeeId=' + employeeId
										+ '&fromDate=' + $scope.filter.fromDate.toDateString()
										+ '&toDate=' + $scope.filter.toDate.toDateString();
	};

	$scope.$watch('search.searchKey', function (newValue) {
		if (newValue === "") {
			$scope.employee = null;
		}
	});

	$scope.reload = function () {
		$route.reload();
	};
} ]);