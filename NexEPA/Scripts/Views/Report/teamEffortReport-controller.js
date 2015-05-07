'use strict';
/*global reportModule*/

reportModule.controller('TeamEffortReportController',
['$scope', '$route', '$location', 'reportFactory', 'notificationFactory', 'commonFactory', 'employeeRepository',
'employeeEPAFactory', 'ErrorService', '$timeout', 'authorityRepository', '$modal', 'projectDataFactory', 'groupProjectFactory',
function ($scope, $route, $location, reportFactory, notificationFactory, commonFactory, employeeRepository,
employeeEPAFactory, ErrorService, $timeout, authorityRepository, $modal, projectDataFactory, groupProjectFactory) {
	/*
	|| Variables	
	*/
	var self = this;

	/*
	|| $scope properties
	*/
	self.generateOptions = function () {
		$scope.filter = {
			fromDate: commonFactory.calculateFromDate(3, 1),
			toDate: new Date(),
			groupId: null,
			isFilterGroup: 'false',
			projectIds: '',
			groupProjectId: '',
			nickNames: ''
		};

		$scope.employees = [];
		$scope.projects = [];
		$scope.groups = [];

		$scope.calendar = {
			startDateOpened: false,
			endDateOpened: false
		};

		$scope.teamChart = {
			xAxis: {},
			series: null,
			reRender: false,
			chartTooltip: self.teamChartTooltip,
			chartDataLabelFormatter: self.teamChartDataLabelFormatter,
			onLoad: self.teamChartOnLoad
		};
		$scope.personalChart = {
			xAxis: {},
			series: null,
			reRender: false
		};
		$scope.personalSeries = [];
		$scope.personalChartTitle = "";
		$scope.isLoadingButton = false;
		$scope.isTeamCollapsed = true;
		$scope.isTeamChartCollapsed = true;
		$scope.isPersonalChartCollapsed = true;

		$scope.teamEfforts = {
			data: '',
			memberProjects: '',
			yearMonths: '',
			memberSubTotals: '',
			memberWorkingDays: '',
			memberWorkloads: '',
			totalEfforts: '',
			averageWorkloads: '',
			grandTotalEffort: '',
			grandAverageWorkload: '',
			subTotalSpentTimeByMembers: '',
			subTotalWorkingDayByMembers: '',
			subTotalWorkloadByMembers: ''
		};

		$scope.spentTimeByMonths = {};
		$scope.subTotalByMonths = {};
		$scope.subTotalSpentTimeByMembers = {};
		$scope.workingDayByMonths = {};
		$scope.subTotalWorkingDayByMembers = {};
		$scope.workloadByMonths = {};
		$scope.subTotalWorkloadByMembers = {};
		$scope.totalSpentTimeByMonths = {};
		$scope.averageWorkloadByMonths = {};
	};

	/*
	|| General functions
	*/
	$scope.openPopupStartDate = function ($event) {
		commonFactory.openPopupStartDate($event, $scope);
	};

	$scope.openPopupEndDate = function ($event) {
		commonFactory.openPopupEndDate($event, $scope);
	};

	$scope.collapseTable = function (panelName) {
		switch (panelName) {
			case "team":
				$scope.isTeamCollapsed = !$scope.isTeamCollapsed;
				break;
			case "teamChart":
				$scope.isTeamChartCollapsed = !$scope.isTeamChartCollapsed;
				if (!$scope.isTeamChartCollapsed) {
					$scope.teamChart.reRender = true;
				}
				break;
			case "personalChart":
				$scope.isPersonalChartCollapsed = !$scope.isPersonalChartCollapsed;
				if (!$scope.isPersonalChartCollapsed) {
					$scope.personalChart.reRender = true;
				}
				break;
		}
	};

	$scope.reload = function () {
		$route.reload();
	};

	self.load = function () {
		self.generateOptions();

		commonFactory.getDateFormat($scope);

		self.checkTheRightToViewTeamEffortReport(function () {
			self.getAllGroup();
			self.getAllGroupProjects();
			self.getAllRedmineProjects();
			$scope.getEmployee();
		});
	};

	self.checkTheRightToViewTeamEffortReport = function (hasRightCallBack) {
		reportFactory.checkTheRightToViewTeamEffortReport().success(function () {
			if (hasRightCallBack) {
				hasRightCallBack();
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

	self.validateFilterOptions = function () {
		var isValid = commonFactory.validateFromToDate($scope.filter.fromDate, $scope.filter.toDate);

		if ($scope.filter.isFilterGroup === 'true' && !$scope.filter.groupProjectId) {
			isValid = false;
			notificationFactory.error("GroupProjectFilterRequire");
		}
		else {
			isValid = true;
		}

		return isValid;
	};

	self.getNickName = function (fullName) {
		var data = jQuery.grep($scope.teamEfforts.memberProjects, function (item) {
			return item.employee.fullName === fullName;
		});

		if (data.length > 0) {
			return data[0].employee.nickName;
		}
	};

	self.getYearMonth = function (monthName) {
		var result = { year: '', month: '' };
		var data = jQuery.grep($scope.teamEfforts.yearMonths, function (item) {
			return item.monthName === monthName;
		});

		if (data.length > 0) {
			result = data[0];
		}
		return result;
	};

	self.teamChartTooltip = function (paramSeries, paramPoint) {
		var fullName = paramSeries.name;
		var monthName = paramPoint.category;
		var spentTime = Math.abs(paramPoint.y);

		var nickName = self.getNickName(fullName);
		var yearMonth = self.getYearMonth(monthName);
		var workingDay = $scope.getWorkingDayByMonth(nickName, yearMonth.year, yearMonth.month);
		var workload = $scope.getWorkloadByMonth(nickName, yearMonth.year, yearMonth.month);
		if (workload && workload > 0) {
			workload = parseFloat(workload * 100).toFixed(2);
		}

		var tooltip = '<b>' + fullName + ', ' + monthName + '</b><br/>' +
							'<ul style="padding-left:25px">' +
								'<li>Spent time: <b>' + spentTime + '</b></li>' +
								'<li>Working day: <b>' + (workingDay ? workingDay : 0) + '</b></li>' +
								'<li>Workload: <b>' + (workload ? workload : 0) + '%</b></li>' +
							'</ul>';

		return tooltip;
	};

	self.teamChartDataLabelFormatter = function (paramSeries, paramPoint) {
		var fullName = paramSeries.name;
		var monthName = paramPoint.category;

		var nickName = self.getNickName(fullName);
		var yearMonth = self.getYearMonth(monthName);

		var workload = $scope.getWorkloadByMonth(nickName, yearMonth.year, yearMonth.month);
		if (paramPoint.y > 0) {
			if (workload && workload > 0) {
				workload = parseFloat(workload * 100).toFixed(2);
			}
			var label = fullName + ' - ' + (workload ? workload : 0) + '%';
			return label;
		}
	};

	self.teamChartOnLoad = function (chart) {
		angular.forEach(chart.series, function (item) {
			angular.forEach(item.data, function (point) {
				if (point.dataLabel) {
					point.dataLabel.attr({ y: point.shapeArgs.y - 5 });

					var fontSize = '10px',
						width = point.shapeArgs.width;

					if (width < 8) {
						fontSize = '4px';
					}
					else if (8 <= width && width <= 13) {
						fontSize = '8px';
					} else if (20 > width && width > 13) {
						fontSize = '9px';
					}

					point.dataLabel.css({ fontSize: fontSize });
				}
			});
		});
	};

	/*
	|| Filter region
	*/
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

	self.getAllGroup = function () {
		employeeRepository.getAllGroup().success(function (groupList) {
			$scope.groups = groupList;
		}).error(function () {
			notificationFactory.error("ErrorMessage");
		});
	};

	$scope.getEmployee = function () {
		var groupId = $scope.filter.groupId;

		if ($scope.filter.isFilterGroup === 'true') {
			var groupProjectId = $scope.filter.groupProjectId;

			employeeRepository.getRedmineEmployeesByTeamGroupProject(groupId, groupProjectId).success(function (employees) {
				$scope.employees = employees;
			}).error(function () {
				notificationFactory.error("ErrorMessage");
			});
		}
		else {
			var projectIds = commonFactory.getSelectedData($scope.projects, 'id');

			employeeRepository.getRedmineEmployeesByTeamProject(groupId, projectIds).success(function (employees) {
				$scope.employees = employees;
			}).error(function () {
				notificationFactory.error("ErrorMessage");
			});
		}
	};

	$scope.fClick = function () {
		$scope.getEmployee();
	};

	$scope.fSelectAll = function () {
		$scope.getEmployee();
	};

	$scope.fReset = function () {
		$scope.getEmployee();
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

		$scope.getEmployee();
	});

	$scope.handleReportData = function () {
		var isValid = self.validateFilterOptions();

		if (isValid) {
			var selectedProjects = commonFactory.getSelectedData($scope.projects);
			var selectedEmployees = commonFactory.getSelectedData($scope.employees);

			if (!$scope.filter.groupId && (selectedEmployees && selectedEmployees.length === $scope.employees.length) &&
				(($scope.filter.isFilterGroup === 'false' && selectedProjects.length === $scope.projects.length) ||
				($scope.filter.isFilterGroup === 'true' && !$scope.filter.groupProjectId))) {
				// warning if show all employees
				self.warningShowAllEmployees();
			} else {
				self.getReportData();
			}
		}
	};

	self.warningShowAllEmployees = function () {
		var warningModal = $modal.open({
			templateUrl: '/templates/Common/modalConfirmContent.html',
			controller: "ModalConfirmController",
			resolve: {
				name: function () {
					return 'all employees';
				},
				action: function () {
					return 'view';
				},
				extraInfo: function () {
					return 'This affects the performance.';
				}
			},
			windowClass: "modal-confirm-center"
		});

		warningModal.result.then(function () {
			self.getReportData();
		});
	};

	self.getReportData = function () {
		$scope.isLoadingButton = true;

		$scope.filter.nickNames = commonFactory.getSelectedData($scope.employees, "nickName");
		$scope.filter.projectIds = commonFactory.getSelectedData($scope.projects, "id");

		if ($scope.filter.nickNames) {
			self.getRedmineTeamEffort();
		} else {
			$scope.teamEfforts.data = [];
			$scope.teamEfforts.memberProjects = [];
			$scope.teamEfforts.yearMonths = [];
			$scope.teamEfforts.memberSubTotals = [];
			$scope.teamEfforts.memberWorkingDays = [];
			$scope.teamEfforts.memberWorkloads = [];
			$scope.teamEfforts.totalEfforts = [];
			$scope.teamEfforts.averageWorkloads = [];
			$scope.teamEfforts.grandTotalEffort = null;
			$scope.teamEfforts.grandAverageWorkload = null;
			$scope.teamEfforts.subTotalSpentTimeByMembers = [];
			$scope.teamEfforts.subTotalWorkingDayByMembers = [];
			$scope.teamEfforts.subTotalWorkloadByMembers = [];

			self.setDefaultPersonalChartTitle();

			$scope.isLoadingButton = false;
		}
	};

	/*
	|| Team Efforts region
	*/
	self.getRedmineTeamEffort = function () {
		reportFactory.getRedmineTeamEffort($scope.filter)
		.success(function (report) {
			if (report != null) {
				$scope.teamEfforts.data = report.teamEfforts;
				$scope.teamEfforts.memberProjects = report.memberProjects;
				$scope.teamEfforts.yearMonths = report.yearMonths;
				$scope.teamEfforts.memberSubTotals = report.memberSubTotals;
				$scope.teamEfforts.subTotalSpentTimeByMembers = report.subTotalSpentTimeByMembers;
				$scope.teamEfforts.memberWorkingDays = report.memberWorkingDays;
				$scope.teamEfforts.subTotalWorkingDayByMembers = report.subTotalWorkingDayByMembers;
				$scope.teamEfforts.memberWorkloads = report.memberWorkloads;
				$scope.teamEfforts.subTotalWorkloadByMembers = report.subTotalWorkloadByMembers;
				$scope.teamEfforts.totalEfforts = report.totalEfforts;
				$scope.teamEfforts.averageWorkloads = report.averageWorkloads;
				$scope.teamEfforts.grandTotalEffort = report.grandTotalEffort;
				$scope.teamEfforts.grandAverageWorkload = report.grandAverageWorkload;

				self.setDefaultPersonalChartTitle();
				self.processChartData();
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
	};

	$scope.getSpentTimeByMonth = function (nickName, projectId, year, month) {
		var data = jQuery.grep($scope.teamEfforts.data, function (item) {
			return item.project.projectId === projectId && angular.lowercase(item.employee.nickName) === angular.lowercase(nickName)
					&& item.yearMonth.year === year && item.yearMonth.month === month;
		});

		if (data.length > 0) {
			return data[0].spentTime;
		}
	};

	$scope.getSubTotalByMonth = function (nickName, year, month) {
		var data = jQuery.grep($scope.teamEfforts.memberSubTotals, function (item) {
			return angular.lowercase(item.employee.nickName) === angular.lowercase(nickName)
					&& item.yearMonth.year === year && item.yearMonth.month === month;
		});

		if (data.length > 0) {
			return data[0].spentTime;
		}
	};

	$scope.getSubTotalSpentTimeByMember = function (nickName) {
		var data = jQuery.grep($scope.teamEfforts.subTotalSpentTimeByMembers, function (item) {
			return angular.lowercase(item.nickName) === angular.lowercase(nickName);
		});

		if (data.length > 0) {
			return data[0].subTotalSpentTime;
		}
	};

	$scope.getWorkingDayByMonth = function (nickName, year, month) {
		var data = jQuery.grep($scope.teamEfforts.memberWorkingDays, function (item) {
			return angular.lowercase(item.employee.nickName) === angular.lowercase(nickName)
					&& item.yearMonth.year === year && item.yearMonth.month === month;
		});

		if (data.length > 0) {
			return data[0].workingDay;
		}
	};

	$scope.getSubTotalWorkingDayByMember = function (nickName) {
		var data = jQuery.grep($scope.teamEfforts.subTotalWorkingDayByMembers, function (item) {
			return angular.lowercase(item.nickName) === angular.lowercase(nickName);
		});

		if (data.length > 0) {
			return data[0].subTotalWorkingDay;
		}
	};

	$scope.getWorkloadByMonth = function (nickName, year, month) {
		var data = jQuery.grep($scope.teamEfforts.memberWorkloads, function (item) {
			return angular.lowercase(item.employee.nickName) === angular.lowercase(nickName)
					&& item.yearMonth.year === year && item.yearMonth.month === month;
		});

		if (data.length > 0) {
			return data[0].workload;
		}
	};

	$scope.getSubTotalWorkloadByMember = function (nickName) {
		var data = jQuery.grep($scope.teamEfforts.subTotalWorkloadByMembers, function (item) {
			return angular.lowercase(item.nickName) === angular.lowercase(nickName);
		});

		if (data.length > 0) {
			return data[0].subTotalWorkload;
		}
	};

	$scope.getTotalSpentTimeByMonth = function (year, month) {
		var data = jQuery.grep($scope.teamEfforts.totalEfforts, function (item) {
			return item.yearMonth.year === year && item.yearMonth.month === month;
		});

		if (data.length > 0) {
			return data[0].total;
		}
	};

	$scope.getAverageWorkloadByMonth = function (year, month) {
		var data = jQuery.grep($scope.teamEfforts.averageWorkloads, function (item) {
			return item.yearMonth.year === year && item.yearMonth.month === month;
		});

		if (data.length > 0) {
			return data[0].workload;
		}
	};

	/*
	|| Team Effort Chart region
	*/
	self.processChartData = function () {
		if ($scope.teamEfforts.data) {
			self.generateXAxis();
			self.generateTeamSeries();
			self.generatePersonalSeries();
		}
	};

	self.generateTeamSeries = function () {
		var series = [];
		angular.forEach($scope.teamEfforts.memberProjects, function (member) {
			var data = [];
			angular.forEach($scope.teamEfforts.yearMonths, function (month) {
				var spentTime = $scope.getSubTotalByMonth(member.employee.nickName, month.year, month.month);
				data.push(spentTime ? spentTime : 0);
			});

			series.push({
				name: member.employee.fullName,
				data: data
			});
		});

		$scope.teamChart.series = series;
	};

	/*
	|| Personal Effort Chart region
	*/
	self.setDefaultPersonalChartTitle = function () {
		if ($scope.teamEfforts.data.length === 0) {
			$scope.personalChartTitle = "Member's effort in chart";
		}
	};

	self.generatePersonalSeries = function () {
		if ($scope.teamEfforts.memberProjects.length > 0) {
			angular.forEach($scope.teamEfforts.memberProjects, function (member) {
				var series = [],
				nickName = angular.lowercase(member.employee.nickName);

				angular.forEach(member.projects, function (project) {
					var data = [];
					angular.forEach($scope.teamEfforts.yearMonths, function (month) {
						var spentTime = $scope.getSpentTimeByMonth(nickName, project.projectId, month.year, month.month);
						data.push(spentTime ? spentTime : 0);
					});

					series.push({
						name: project.projectName,
						data: data
					});
				});

				$scope.personalSeries[nickName] = series;
			});

			var member = $scope.teamEfforts.memberProjects[0];
			self.getPersonalChartData(member.employee.nickName, member.employee.fullName);
		}
	};

	$scope.drillDownChart = function (e) {
		var data = jQuery.grep($scope.teamEfforts.memberProjects, function (item) {
			return item.employee.fullName === e.point.series.name;
		});

		if (data.length > 0) {
			var member = data[0];
			$scope.renderPersonalChart(member.employee.nickName, member.employee.fullName);
		}
	};

	$scope.renderPersonalChart = function (nickName, fullName) {
		if ($scope.isPersonalChartCollapsed) {
			$scope.isPersonalChartCollapsed = false;
		}
		self.getPersonalChartData(nickName, fullName);

		$timeout(function () {
			commonFactory.scrollToBottom(500);
		}, 100);
	};

	self.getPersonalChartData = function (nickName, fullName) {
		nickName = angular.lowercase(nickName);

		$scope.personalChartTitle = fullName + "'s effort in chart";
		$scope.personalChart.series = $scope.personalSeries[nickName];
	};

	self.generateXAxis = function () {
		var months = [];
		angular.forEach($scope.teamEfforts.yearMonths, function (month) {
			months.push(month.monthName);
		});

		var xAxis = { categories: months };

		$scope.teamChart.xAxis = xAxis;
		$scope.personalChart.xAxis = xAxis;
	};

	// Run first
	self.load();
} ]);