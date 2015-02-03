'use strict';
/*global reportModule*/

reportModule.controller('ViewEmployeePerformanceTendencyController',
['$scope', '$route', '$routeParams', 'reportFactory', 'employeeRepository', 'notificationFactory', 'ErrorService',
function ($scope, $route, $routeParams, reportFactory, employeeRepository, notificationFactory, ErrorService) {
	var self = this;

	$scope.employeeId = parseInt($routeParams.employeeId);
	$scope.searchEmployees = [];
	$scope.employee = null;

	$scope.isProjectCollapsed = true;
	$scope.isEffortCollapsed = true;
	$scope.isEffortChartCollapsed = true;
	$scope.isEffortDetailCollapsed = true;

	$scope.index = 0;
	$scope.originalComment = '';
	$scope.viewEPADetailUrl = '';
	$scope.report = {
		generalInformation: '',
		historyProjects: ''
	};

	$scope.filter = {
		searchKey: '',
		fromDate: null,
		toDate: null
	};

	$scope.fromOpened = false;
	$scope.toOpened = false;

	$scope.open = function ($event, dateType) {
		$event.preventDefault();
		$event.stopPropagation();

		if (dateType === "from") {
			$scope.fromOpened = true;
			$scope.toOpened = false;
		} else if (dateType === "to") {
			$scope.toOpened = true;
			$scope.fromOpened = false;
		}
	};

	$scope.getEmployee = function (searchKey) {
		if (searchKey !== "") {
			employeeRepository.getEmployeeByCurrentUserId(searchKey, null).success(function (employees) {
				$scope.searchEmployees = employees;
			}).error(function () {
				notificationFactory.error("ErrorMessage");
			});
		} else {
			$scope.employee = null;
		}
	};

	$scope.getReportData = function () {
		self.handleFromToDate();

		var isValid = self.validateFilterOptions();

		if (isValid) {
			var employeeId = null;

			if ($scope.employee) {
				employeeId = $scope.employee.employeeId;
			}
			else if ($scope.employeeId) {
				self.getEmployeeById();

				employeeId = $scope.employeeId;
			}

			self.getReportByProjects(employeeId);
		}
	};

	$scope.reload = function () {
		$route.reload();
	};

	$scope.collapseTable = function (panelName) {
		switch (panelName) {
			case "project":
				$scope.isProjectCollapsed = !$scope.isProjectCollapsed;
				break;
			case "effort":
				$scope.isEffortCollapsed = !$scope.isEffortCollapsed;
				break;
			case "effortChart":
				$scope.isEffortChartCollapsed = !$scope.isEffortChartCollapsed;
				break;
			case "effortDetail":
				$scope.isEffortDetailCollapsed = !$scope.isEffortDetailCollapsed;
				break;
		}
	};

	$scope.EPADetail = function (targetId) {
		if ($scope.employee) {
			$scope.viewEPADetailUrl = '/EmployeeEPA/Index' + '?targetId=' + targetId + '&employeeId=' +
					$scope.employee.employeeId;
		}
		else if ($scope.employeeId) {
			$scope.viewEPADetailUrl = '/EmployeeEPA/Index' + '?targetId=' + targetId + '&employeeId=' +
					$scope.employeeId;
		}
	};

	self.getReportByProjects = function (employeeId) {
		if (employeeId) {
			reportFactory.getEmployeePerformanceTendencyReport(employeeId, $scope.filter.fromDate, $scope.filter.toDate).success(function (report) {
				if (report != null) {
					$scope.report.generalInformation = report.employeeInformation;
					$scope.report.historyProjects = report.historyProjects;
				} else {
					$scope.report.generalInformation = null;
					$scope.report.historyProjects = null;
				}
			}).error(function (response, status) {
				if (status === 405) {
					ErrorService.errorHandle(status);
				}
				else {
					notificationFactory.error("ErrorMessage");
				}
			});
		}
	};

	self.validateFilterOptions = function () {
		var isValid = true;
		var fromDate = $scope.filter.fromDate;
		var toDate = $scope.filter.toDate;
		var maxDate = new Date(fromDate.getFullYear() + 1, fromDate.getMonth(), fromDate.getDate());

		if (fromDate > toDate) {
			isValid = false;
			notificationFactory.error("ValidateFromToDate");
		}
		else if (toDate > maxDate) {
			isValid = false;
			notificationFactory.error("ValidateMaximumOneYear");
		}

		return isValid;
	};

	self.getEmployeeById = function () {
		employeeRepository.getEmployeeById($scope.employeeId).success(function (employee) {
			$scope.employee = employee;
			$scope.filter.searchKey = employee.nickName + ' (' + employee.fullName + ')';
		}).error(function () {
			notificationFactory.error("ErrorMessage");
		});
	};

	self.handleFromToDate = function () {
		var date = new Date(),
			year = date.getFullYear(),
			month = date.getMonth(),
			day = date.getDate();

		if (!$scope.filter.toDate) {
			$scope.filter.toDate = new Date(year, month, day);
		}

		if (!$scope.filter.fromDate) {
			$scope.filter.fromDate = new Date(year, month - 3, day);
		}
	};

	self.load = function () {
		self.handleFromToDate();

		if ($routeParams.fromDate) {
			$scope.filter.fromDate = new Date($routeParams.fromDate);
		}

		if ($routeParams.toDate) {
			$scope.filter.toDate = new Date($routeParams.toDate);
		}

		$scope.getReportData();
	};

	self.load();

} ]);