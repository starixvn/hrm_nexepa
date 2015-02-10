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

	$scope.personalEfforts = {
		data: '',
		projects: '',
		projectMonths: '',
		grandTotalSpentTime: 0
	};

	$scope.personalEffortDetails = {
		data: '',
		projectTrackers: '',
		trackerMonths: '',
		workloadByProjectMonths: '',
		workloadByProjects: '',
		workloadByMonths: '',
		distinctTrackerMonths: '',
		grandTotalWorkload: ''
	};

	$scope.chart = {
		xAxis: {},
		series: null
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
			$scope.employeeId = null;
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

			self.getPersonalEfforts(employeeId);

			self.getPersonalEffortDetails(employeeId);
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

	$scope.getSpentTime = function (projectName, monthName) {
		var data = jQuery.grep($scope.personalEfforts.data, function (item) {
			return item.projectName === projectName && item.monthName === monthName;
		});

		if (data.length > 0) {
			return data[0].spentTime;
		}
	};

	$scope.getDetailNumberIssue = function (projectId, trackerId, year, month) {
		return self.getIssueSpent(projectId, trackerId, year, month, 1);
	};

	$scope.getDetailSpentTime = function (projectId, trackerId, year, month) {
		return self.getIssueSpent(projectId, trackerId, year, month, 2);
	};

	self.getIssueSpent = function (projectId, trackerId, year, month, type) {
		var data = jQuery.grep($scope.personalEffortDetails.data, function (item) {
			return item.projectId === projectId && item.trackerId === trackerId
					&& item.year === year && item.month === month;
		});

		if (data.length > 0) {
			if (type === 1) {
				return data[0].numberIssue;
			} else if (type === 2) {
				return data[0].spentTime;
			}
		}
	};

	$scope.getTotalNumberIssue = function (trackerId, year, month) {
		return self.getTotalIssueSpent(trackerId, year, month, 1);
	};

	$scope.getTotalSpentTime = function (trackerId, year, month) {
		return self.getTotalIssueSpent(trackerId, year, month, 2);
	};

	self.getTotalIssueSpent = function (trackerId, year, month, type) {
		var data = jQuery.grep($scope.personalEffortDetails.trackerMonths, function (item) {
			return item.trackerId === trackerId && item.yearMonth.year === year && item.yearMonth.month === month;
		});

		if (data.length > 0) {
			if (type === 1) {
				return data[0].yearMonth.numberIssue;
			} else if (type === 2) {
				return data[0].yearMonth.spentTime;
			}
		}
	};

	$scope.getWorkload = function (projectId, year, month) {
		var data = jQuery.grep($scope.personalEffortDetails.workloadByProjectMonths, function (item) {
			return item.projectId === projectId && item.year === year && item.month === month;
		});

		if (data.length > 0) {
			return data[0].workload;
		}
	};

	$scope.getSumWorkload = function (projectId) {
		var data = jQuery.grep($scope.personalEffortDetails.workloadByProjects, function (item) {
			return item.projectId === projectId;
		});

		if (data.length > 0) {
			return data[0].workload;
		}
	};

	$scope.getWorkloadByMonth = function (year, month) {
		var data = jQuery.grep($scope.personalEffortDetails.workloadByMonths, function (item) {
			return item.year === year && item.month === month;
		});

		if (data.length > 0) {
			return data[0].workload;
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

	self.getPersonalEfforts = function (employeeId) {
		if (employeeId) {
			reportFactory.getRedminePersonalEffort(employeeId, $scope.filter.fromDate, $scope.filter.toDate).success(function (report) {
				if (report != null) {
					$scope.personalEfforts.data = report.personalEfforts;
					$scope.personalEfforts.projects = report.projects;
					$scope.personalEfforts.projectMonths = report.projectMonths;
					$scope.personalEfforts.grandTotalSpentTime = report.grandTotalSpentTime;
				} else {
					$scope.personalEfforts.data = null;
					$scope.personalEfforts.projects = null;
					$scope.personalEfforts.projectMonths = null;
					$scope.personalEfforts.grandTotalSpentTime = 0;
				}

				self.processChartData();
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

	self.getPersonalEffortDetails = function (employeeId) {
		if (employeeId) {
			reportFactory.getRedminePersonalEffortDetail(employeeId, $scope.filter.fromDate, $scope.filter.toDate).success(function (report) {
				if (report != null) {
					$scope.personalEffortDetails.data = report.personalEffortDetails;
					$scope.personalEffortDetails.projectTrackers = report.projectTrackers;
					$scope.personalEffortDetails.trackerMonths = report.trackerMonths;
					$scope.personalEffortDetails.distinctTrackerMonths = report.distinctTrackerMonths;
					$scope.personalEffortDetails.workloadByProjectMonths = report.workloadByProjectMonths;
					$scope.personalEffortDetails.workloadByProjects = report.workloadByProjects;
					$scope.personalEffortDetails.workloadByMonths = report.workloadByMonths;
					$scope.personalEffortDetails.grandTotalWorkload = report.grandTotalWorkload;
				} else {
					$scope.personalEffortDetails.data = null;
					$scope.personalEffortDetails.projectTrackers = null;
					$scope.personalEffortDetails.trackerMonths = null;
					$scope.personalEffortDetails.distinctTrackerMonths = null;
					$scope.personalEffortDetails.workloadByProjectMonths = null;
					$scope.personalEffortDetails.workloadByProjects = null;
					$scope.personalEffortDetails.workloadByMonths = null;
					$scope.personalEffortDetails.grandTotalWorkload = null;
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

	self.processChartData = function () {
		if ($scope.personalEfforts.data) {
			self.generateXAxis();
			self.generateSeries();
		}
	};

	self.generateSeries = function () {
		var series = [];
		angular.forEach($scope.personalEfforts.projects, function (project) {
			var data = [];
			angular.forEach($scope.personalEfforts.projectMonths, function (month) {
				var spentTime = $scope.getSpentTime(project.projectName, month.monthName);
				data.push(spentTime ? spentTime : 0);
			});

			series.push({
				name: project.projectName,
				data: data
			});
		});

		$scope.chart.series = series;
	};

	self.generateXAxis = function () {
		var months = [];
		angular.forEach($scope.personalEfforts.projectMonths, function (month) {
			months.push(month.monthName);
		});

		$scope.chart.xAxis = {
			categories: months
		};
	};

	self.validateFilterOptions = function () {
		var isValid = true;
		var fromDate = new Date($scope.filter.fromDate.toDateString());
		var toDate = new Date($scope.filter.toDate.toDateString());
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