'use strict';
/*global reportModule*/

reportModule.controller('ViewEmployeePerformanceTendencyController',
['$scope', '$route', '$routeParams', 'reportFactory', 'employeeRepository', 'notificationFactory', 'ErrorService', 'authorityRepository',
'commonFactory', 'groupProjectFactory', 'projectDataFactory',
function ($scope, $route, $routeParams, reportFactory, employeeRepository, notificationFactory, ErrorService, authorityRepository,
commonFactory, groupProjectFactory, projectDataFactory) {
	/*
	|| variables
	*/
	var self = this;

	self.generateOptions = function () {
		$scope.employeeId = parseInt($routeParams.employeeId);
		$scope.searchEmployees = [];
		$scope.employee = null;

		$scope.isProjectCollapsed = true;
		$scope.isEffortCollapsed = true;
		$scope.isEffortChartCollapsed = true;
		$scope.isEffortDetailCollapsed = true;

		$scope.isLoadingButton = false;
		$scope.isOnlyMember = false;
		$scope.currentEmployeeId = null;
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
			grandTotalWorkload: '',
			grandTotalWorkingDay: '',
			grandTotalIssueSpent: {
				numberIssue: '',
				spentTime: ''
			},
			projectMonthSubTotals: ''
		};

		$scope.chart = {
			xAxis: {},
			series: null,
			reRender: false
		};

		$scope.filter = {
			searchKey: '',
			fromDate: null,
			toDate: null,
			isFilterGroup: 'false',
			projectIds: '',
			groupProjectId: ''
		};

		$scope.fromOpened = false;
		$scope.toOpened = false;
		$scope.effortInFiguresTitle = "My effort in figures";
		$scope.effortInChartTitle = "My effort in chart";
		$scope.effortDetailInFiguresTitle = "My effort detail in figures";
		$scope.isProject = 'true';
		$scope.isGroupProject = 'false';

		$scope.spentTimes = {};
		$scope.detailNumberIssues = {};
		$scope.detailSpentTimes = {};
		$scope.subTotalIssues = {};
		$scope.subTotalSpentTimes = {};
		$scope.workloads = {};
		$scope.sumWorkloads = {};
		$scope.totalNumberIssues = {};
		$scope.totalSpentTimes = {};
		$scope.grandSubTotalIssues = {};
		$scope.grandSubTotalSpentTimes = {};
	};

	/*
	|| general functions
	*/
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

	$scope.getReportData = function () {
		self.checkRightToViewPersonReportOfEmployee(function () {
			var isValid = self.validateFilterOptions();

			if (isValid) {
				var employeeId = self.getEmployeeId();

				self.setPanelTitle();

				self.getReportByProjects(employeeId);

				self.getPersonalEfforts(employeeId);

				self.getPersonalEffortDetails(employeeId);
			}
		});
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
				if (!$scope.isEffortChartCollapsed) {
					$scope.chart.reRender = true;
				}
				break;
			case "effortDetail":
				$scope.isEffortDetailCollapsed = !$scope.isEffortDetailCollapsed;
				break;
		}
	};

	$scope.$watch('filter.isFilterGroup', function (value) {
		if (value === 'true') {
			angular.forEach($scope.projects, function (project) {
				project.ticked = false;
			});
		}
		else {
			$scope.filter.groupProjectId = '';
		}
	});

	self.getEmployeeId = function () {
		var employeeId = null;

		// Get employeeId of member 
		if ($scope.isOnlyMember === true) {
			employeeId = $scope.currentEmployeeId;
			self.getEmployeeById(employeeId);
		}
		// Get employeeId from Employee filter
		else if ($scope.employee) {
			employeeId = $scope.employee.employeeId;
		}
		// Get employeeId from employeeId query string
		else if ($scope.employeeId) {
			employeeId = $scope.employeeId;
			self.getEmployeeById(employeeId);

			$scope.employeeId = null;
		}

		return employeeId;
	};

	self.checkIfOnlyMember = function (callback) {
		authorityRepository.isOnlyMember().success(function (result) {
			if (result) {
				$scope.isOnlyMember = result.isOnlyMember;
				$scope.currentEmployeeId = result.employeeId;

				if (angular.isFunction(callback)) {
					callback();
				}
			}
		}).error(function (response, status) {
			if (status === 405) {
				ErrorService.errorHandle(status);
			}
			else {
				notificationFactory.error("ErrorMessage");
			}
		});
	};

	self.checkRightToViewPersonReportOfEmployee = function (hasRightCallBack) {
		if ($scope.employeeId) {
			// Check the right
			if ($scope.isOnlyMember === true && $scope.employeeId !== $scope.currentEmployeeId) {
				notificationFactory.error("PersonReportHasNoRight");
				$scope.employeeId = null;
			} else {
				authorityRepository.checkRightToViewPersonReportOfEmployee($scope.employeeId).success(function (result) {
					if (result) {
						if (result.hasViewRight === false) {
							notificationFactory.error("PersonReportHasNoRight");
							$scope.employeeId = null;
						} else {
							// Has the right to view this employee
							if (angular.isFunction(hasRightCallBack)) {
								hasRightCallBack();
							}
						}
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
		} else {
			// Not check the right
			if (angular.isFunction(hasRightCallBack)) {
				hasRightCallBack();
			}
		}
	};

	self.validateFilterOptions = function () {
		var isValid = true;

		isValid = commonFactory.validateFromToDate($scope.filter.fromDate, $scope.filter.toDate);

		if (!$scope.employeeId && !$scope.isOnlyMember && $scope.filter.searchKey.length === 0) {
			isValid = false;
			notificationFactory.error("EmployeeFilterRequire");
		} else if ($scope.filter.isFilterGroup === 'true' && $scope.filter.groupProjectId === '') {
			isValid = false;
			notificationFactory.error("GroupProjectFilterRequire");
		}

		return isValid;
	};

	self.getEmployeeById = function (employeeId) {
		employeeRepository.getEmployeeById(employeeId).success(function (employee) {
			$scope.employee = employee;
			$scope.filter.searchKey = employee.nickName + ' (' + employee.fullName + ')';
			self.setPanelTitle();
		}).error(function () {
			notificationFactory.error("ErrorMessage");
		});
	};

	self.setPanelTitle = function () {
		if ($scope.employee) {
			if ($scope.employee.employeeId !== $scope.currentEmployeeId) {
				$scope.effortInFiguresTitle = $scope.employee.nickName + "'s effort in figures";
				$scope.effortInChartTitle = $scope.employee.nickName + "'s effort in chart";
				$scope.effortDetailInFiguresTitle = $scope.employee.nickName + "'s effort detail in figures";
			} else {
				$scope.effortInFiguresTitle = "My effort in figures";
				$scope.effortInChartTitle = "My effort in chart";
				$scope.effortDetailInFiguresTitle = "My effort detail in figures";
			}
		}
	};

	self.handleFromToDate = function () {
		if (!$scope.filter.toDate) {
			$scope.filter.toDate = new Date();
		}

		if (!$scope.filter.fromDate) {
			$scope.filter.fromDate = commonFactory.calculateFromDate(3, 1);
		}
	};

	self.getAllGroupProjects = function () {
		groupProjectFactory.getGroupProject().success(function (listGroupProject) {
			$scope.groupProjects = listGroupProject;
		}).error(function (response, status) {
			if (status === 405) {
				ErrorService.errorHandle(status);
			}
			else {
				notificationFactory.error("ErrorMessage");
			}
		});
	};

	self.getAllRedmineProjects = function () {
		projectDataFactory.getAllRedmineProjects().then(function (projectList) {
			$scope.projects = projectList;
		});
	};

	self.load = function () {
		self.generateOptions();
		self.getAllGroupProjects();
		self.getAllRedmineProjects();

		self.checkIfOnlyMember(function () {
			self.handleFromToDate();

			if ($routeParams.fromDate) {
				$scope.filter.fromDate = new Date($routeParams.fromDate);
			}

			if ($routeParams.toDate) {
				$scope.filter.toDate = new Date($routeParams.toDate);
			}

			if ($scope.employeeId || $scope.isOnlyMember === true) {
				$scope.getReportData();
			}
		});
	};

	/*
	|| Filter region
	*/
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

	/*
	|| By projects panel
	*/
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

	$scope.EPADetail = function (targetId) {
		var employeeId = self.getEmployeeId();
		var queryString = '?targetId=' + targetId + '&employeeId=' + employeeId;

		if ($scope.isOnlyMember === true) {
			$scope.viewEPADetailUrl = '/MyEPA/Index' + queryString;
		} else {
			$scope.viewEPADetailUrl = '/EmployeeEPA/Index' + queryString;
		}
	};

	/*
	|| Personal effort panel
	*/
	self.getPersonalEfforts = function (employeeId) {
		if (employeeId) {
			$scope.filter.projectIds = commonFactory.getSelectedData($scope.projects, "id");

			reportFactory.getRedminePersonalEffort(employeeId, $scope.filter.fromDate, $scope.filter.toDate,
				$scope.filter.isFilterGroup, $scope.filter.projectIds, $scope.filter.groupProjectId)
			.success(function (report) {
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
				var spentTime = $scope.getSpentTime(project.projectId, month.monthName);
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

		$scope.chart.xAxis = { categories: months };
	};

	$scope.getSpentTime = function (projectId, monthName) {
		var data = jQuery.grep($scope.personalEfforts.data, function (item) {
			return item.projectId === projectId && item.monthName === monthName;
		});

		if (data.length > 0) {
			return data[0].spentTime;
		}
	};

	/*
	|| Personal effort detail panel
	*/
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

	$scope.getSubTotalIssue = function (projectId, year, month) {
		return self.getSubTotalIssueSpent(projectId, year, month, 1);
	};

	$scope.getSubTotalSpentTime = function (projectId, year, month) {
		return self.getSubTotalIssueSpent(projectId, year, month, 2);
	};

	self.getSubTotalIssueSpent = function (projectId, year, month, type) {
		var data = jQuery.grep($scope.personalEffortDetails.projectMonthSubTotals, function (item) {
			return item.projectId === projectId && item.year === year && item.month === month;
		});

		if (data.length > 0) {
			if (type === 1) {
				return data[0].numberIssue;
			} else if (type === 2) {
				return data[0].spentTime;
			}
		}
	};

	$scope.getGrandSubTotalIssue = function (year, month) {
		return self.getGrandSubTotalIssueSpent(year, month, 1);
	};

	$scope.getGrandSubTotalSpentTime = function (year, month) {
		return self.getGrandSubTotalIssueSpent(year, month, 2);
	};

	self.getGrandSubTotalIssueSpent = function (year, month, type) {
		var data = jQuery.grep($scope.personalEffortDetails.trackerMonths, function (item) {
			return item.yearMonth.year === year && item.yearMonth.month === month;
		});

		var result = 0;

		if (data.length > 0) {
			if (type === 1) {
				angular.forEach(data, function (item) {
					result += item.yearMonth.numberIssue;
				});
			} else if (type === 2) {
				angular.forEach(data, function (item) {
					result += item.yearMonth.spentTime;
				});
			}

			return result;
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

	self.getPersonalEffortDetails = function (employeeId) {
		if (employeeId) {
			$scope.isLoadingButton = true;
			$scope.filter.projectIds = commonFactory.getSelectedData($scope.projects, "id");

			reportFactory.getRedminePersonalEffortDetail(employeeId, $scope.filter.fromDate, $scope.filter.toDate,
				$scope.filter.isFilterGroup, $scope.filter.projectIds, $scope.filter.groupProjectId)
			.success(function (report) {
				if (report != null) {
					$scope.personalEffortDetails.data = report.personalEffortDetails;
					$scope.personalEffortDetails.projectTrackers = report.projectTrackers;
					$scope.personalEffortDetails.projectMonthSubTotals = report.projectMonthSubTotals;
					$scope.personalEffortDetails.trackerMonths = report.trackerMonths;
					$scope.personalEffortDetails.distinctTrackerMonths = report.distinctTrackerMonths;
					$scope.personalEffortDetails.workloadByProjectMonths = report.workloadByProjectMonths;
					$scope.personalEffortDetails.workloadByProjects = report.workloadByProjects;
					$scope.personalEffortDetails.workloadByMonths = report.workloadByMonths;
					$scope.personalEffortDetails.grandTotalWorkload = report.grandTotalWorkload;
					$scope.personalEffortDetails.grandTotalWorkingDay = report.grandTotalWorkingDay;
					$scope.personalEffortDetails.grandTotalIssueSpent = self.calculateGrandTotalIssueSpent();
				} else {
					$scope.personalEffortDetails.data = null;
					$scope.personalEffortDetails.projectTrackers = null;
					$scope.personalEffortDetails.trackerMonths = null;
					$scope.personalEffortDetails.distinctTrackerMonths = null;
					$scope.personalEffortDetails.workloadByProjectMonths = null;
					$scope.personalEffortDetails.workloadByProjects = null;
					$scope.personalEffortDetails.workloadByMonths = null;
					$scope.personalEffortDetails.grandTotalWorkload = null;
					$scope.personalEffortDetails.grandTotalWorkingDay = null;
					$scope.personalEffortDetails.grandTotalIssueSpent = null;
				}

				$scope.isLoadingButton = false;
			}).error(function (response, status) {
				if (status === 405) {
					ErrorService.errorHandle(status);
				}
				else {
					notificationFactory.error("ErrorMessage");
				}

				$scope.isLoadingButton = false;
			});
		}
	};

	self.calculateGrandTotalIssueSpent = function () {
		var numberIssue = 0,
			spentTime = 0;
		angular.forEach($scope.personalEffortDetails.trackerMonths, function (item) {
			numberIssue += item.yearMonth.numberIssue;
			spentTime += item.yearMonth.spentTime;
		});

		return { numberIssue: numberIssue, spentTime: spentTime };
	};

	/*
	|| load(): run first time
	*/
	self.load();

} ]);