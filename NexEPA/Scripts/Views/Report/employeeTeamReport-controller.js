'use strict';
/*global reportModule*/

reportModule.controller('ViewEmployeePerformanceByTeamController',
['$scope', '$route', '$location', 'reportFactory', 'notificationFactory', 'commonFactory', 'employeeRepository',
'employeeEPAFactory', 'ErrorService', '$timeout',
function ($scope, $route, $location, reportFactory, notificationFactory, commonFactory, employeeRepository,
employeeEPAFactory, ErrorService, $timeout) {
	/*
	|| Variables	
	*/
	var self = this,
		date = new Date(),
		year = date.getFullYear(),
		month = date.getMonth(),
		day = date.getDate();

	/*
	|| $scope properties
	*/
	$scope.filter = {
		fromDate: new Date(year, month - 3, day),
		toDate: new Date(year, month, day),
		groupId: null,
		projectId: null,
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
		reRender: false
	};
	$scope.personalChart = {
		xAxis: {},
		series: null,
		reRender: false
	};
	$scope.personalSeries = [];
	$scope.personalChartTitle = "Personal effort";

	$scope.isProjectCollapsed = true;
	$scope.isTeamCollapsed = true;
	$scope.isTeamChartCollapsed = true;
	$scope.isPersonalChartCollapsed = true;

	$scope.reportByProjects = '';
	$scope.teamEfforts = {
		data: '',
		memberProjects: '',
		yearMonths: '',
		memberSubTotals: '',
		memberWorkingDays: '',
		memberWorkloads: '',
		totalEfforts: '',
		grandTotalEffort: '',
		subTotalSpentTimeByMembers: '',
		subTotalWorkingDayByMembers: '',
		subTotalWorkloadByMembers: ''
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
			case "project":
				$scope.isProjectCollapsed = !$scope.isProjectCollapsed;
				break;
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

	$scope.viewEmployeeTendency = function (employeeId) {
		$scope.viewEmployeeTendencyUrl = '/Report/EmployeePerformanceTendency' + '?employeeId=' + employeeId
										+ '&fromDate=' + $scope.filter.fromDate.toDateString()
										+ '&toDate=' + $scope.filter.toDate.toDateString();
	};

	$scope.reload = function () {
		$route.reload();
	};

	self.load = function () {
		commonFactory.getDateFormat($scope);

		self.getAllGroup();

		self.getAllProject();
	};

	self.validateFilterOptions = function () {
		var isValid = true;

		if (!$scope.filter.fromDate) {
			isValid = false;
			notificationFactory.error("FromRequire");
		} else if (!$scope.filter.toDate) {
			isValid = false;
			notificationFactory.error("ToRequire");
		} else {
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
		}

		if (isValid && !$scope.filter.groupId && !$scope.filter.projectId) {
			isValid = false;
			notificationFactory.error("TeamOrProjectRequire");
		}

		return isValid;
	};

	/*
	|| Filter region
	*/
	$scope.getEmployee = function () {
		var groupId = $scope.filter.groupId;
		var projectId = $scope.filter.projectId;

		if (groupId || projectId) {
			employeeRepository.getEmployeeByTeamProject(groupId, projectId).success(function (employees) {
				$scope.employees = employees;
			}).error(function () {
				notificationFactory.error("ErrorMessage");
			});
		} else {
			$scope.employees = [];
		}
	};

	self.getAllProject = function () {
		employeeEPAFactory.getAllProject().then(function (projectList) {
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

	$scope.getReportData = function () {
		var isValid = self.validateFilterOptions();

		if (isValid) {
			$scope.filter.nickNames = commonFactory.getNickNames($scope.employees);

			if ($scope.filter.nickNames) {
				self.getEmployeePerformanceByTeamReport();

				self.getRedmineTeamEffort();
			} else {
				$scope.personalChartTitle = "Member's effort";
				$scope.reportByProjects = [];

				$scope.teamEfforts.data = [];
				$scope.teamEfforts.memberProjects = [];
				$scope.teamEfforts.yearMonths = [];
				$scope.teamEfforts.memberSubTotals = [];
				$scope.teamEfforts.memberWorkingDays = [];
				$scope.teamEfforts.memberWorkloads = [];
				$scope.teamEfforts.totalEfforts = [];
				$scope.teamEfforts.grandTotalEffort = null;
				$scope.teamEfforts.subTotalSpentTimeByMembers = [];
				$scope.teamEfforts.subTotalWorkingDayByMembers = [];
				$scope.teamEfforts.subTotalWorkloadByMembers = [];
			}
		}
	};

	/*
	|| By Projects region
	*/
	self.getEmployeePerformanceByTeamReport = function () {
		reportFactory.getEmployeePerformanceByTeamReport($scope.filter)
		.success(function (report) {
			if (report != null) {
				$scope.reportByProjects = report.reportByProject;
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
				$scope.teamEfforts.grandTotalEffort = report.grandTotalEffort;

				self.processChartData();
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

	$scope.renderPersonalChart = function (nickName, fullName) {		
		self.getPersonalChartData(nickName, fullName);
		if ($scope.isPersonalChartCollapsed) {
			$scope.collapseTable("personalChart");
		}

		$timeout(function () {
			commonFactory.scrollToBottom();
		}, 400);
	};

	self.getPersonalChartData = function (nickName, fullName) {
		nickName = angular.lowercase(nickName);

		$scope.personalChartTitle = fullName + "'s effort";
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