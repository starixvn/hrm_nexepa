'use strict';
/*global reportModule*/

reportModule.controller('ViewEmployeePerformanceTendencyController',
['$scope', '$route', '$routeParams', 'reportFactory', 'employeeRepository', 'notificationFactory',
function ($scope, $route, $routeParams, reportFactory, employeeRepository, notificationFactory) {

	var employeeId = parseInt($routeParams.employeeId);

	$scope.report = {
		generalInformation: '',
		historyProjects: ''
	};

	$scope.search = {
		searchKey: ''
	};

	// get all employee by target Id 
	$scope.getEmployee = function (searchKey) {
		employeeRepository.getAllEmployee(searchKey, null).success(function (employees) {
			$scope.searchEmployees = employees;
		}).error(function () {
			notificationFactory.error("ErrorMessage");
		});
	};

	var getEmployeePerformanceTendencyReport = function (employeeId) {
		reportFactory.getEmployeePerformanceTendencyReport(employeeId).success(function (report) {
			$scope.search.searchKey = '';

			if (report != null) {
				$scope.report.generalInformation = report.employeeInformation;
				$scope.report.historyProjects = report.historyProjects;
			}
		}).error(function () {
			notificationFactory.error("ErrorMessage");
		});
	};

	// get Employee Performance Tendency report
	var getEmployeeReport = function (employee, employeeId) {
		if (employee) {
			employeeId = employee.employeeId;
			getEmployeePerformanceTendencyReport(employeeId);
		}
		else if (employeeId) {
			getEmployeePerformanceTendencyReport(employeeId);
		}
	};

	$scope.getReport = function (employee) {
		getEmployeeReport(employee);
	};

	getEmployeeReport(null, employeeId);

	$scope.reload = function () {
		$route.reload();
	};
} ]);

reportModule.controller('EmployeeGeneralInformationController', function () {
} );

reportModule.controller('EmployeeHistoryProjectController', ['$scope', '$location', 'homeFactory',
function ($scope, $location, homeFactory) {
	$scope.isCollapsed = false;
	$scope.collapse = {};
	$scope.isMilestoneCollapsed = false;
	$scope.originalComment = '';
	$scope.employeeId;

	homeFactory.getCurrentEmployeeId().success(function (employeeId) {
		$scope.employeeId = parseInt(employeeId);
	});

	$scope.collapseTable = function () {
		$scope.isCollapsed = !$scope.isCollapsed;
	};

	$scope.showHideMilestone = function (historyProject) {
		var projectId = historyProject.project.projectId;
		var property = 'isCollapsed' + projectId;
		$scope.collapse[property] = !$scope.collapse[property];
	};

	$scope.EPADetail = function (targetId) {
		$scope.viewEPADetailUrl = '/EmployeeEPA/Index' + '?targetId=' + targetId + '&employeeId=' + $scope.employeeId;
	};
} ]);

// View Employee Performance By Team Controller
reportModule.controller('ViewEmployeePerformanceByTeamController',
['$scope', '$route', '$location', 'reportFactory', 'notificationFactory', 'commonFactory', 'employeeRepository', 'employeeEPAFactory',
function ($scope, $route, $location, reportFactory, notificationFactory, commonFactory, employeeRepository, employeeEPAFactory) {

	var fromDate = new Date();
	fromDate.setMonth(fromDate.getMonth() - 3);

	$scope.filter = {
		fromDate: fromDate,
		toDate: new Date(),
		groupId: null,
		projectId: null
	};

	$scope.search = {
		searchKey: ''
	};

	// get all employee by target Id 
	$scope.getEmployee = function (searchKey) {
		employeeRepository.getAllEmployee(searchKey, null).success(function (employees) {
			$scope.searchEmployees = employees;
		}).error(function () {
			notificationFactory.error("ErrorMessage");
		});
	};

	// get all project to filter
	employeeEPAFactory.getAllProject().then(function (projectList) {
		$scope.projectOptions = projectList;
	});

	// get Date Format
	commonFactory.getDateFormat($scope);

	// get all Group
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

	// Get employee performance by team report
	var getEmployeePerformanceByTeamReport = function (filter) {
		var fromDate = filter.fromDate,
			toDate = filter.toDate,
			employeeId = null;

		fromDate = fromDate ? fromDate.toDateString() : null;
		toDate = toDate ? toDate.toDateString() : null;

		if ($scope.employee) {
			employeeId = $scope.employee.employeeId;
		}

		reportFactory.getEmployeePerformanceByTeamReport(filter, fromDate, toDate, employeeId).success(function (report) {
			if (report != null) {
				$scope.reportByProject = report.reportByProject;
			}

			$scope.disabledSubmit = false;
		}).error(function () {
			notificationFactory.error("ErrorMessage");
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
		$scope.viewEmployeeTendencyUrl = '/Report/EmployeePerformanceTendency' + '?employeeId=' + employeeId;
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