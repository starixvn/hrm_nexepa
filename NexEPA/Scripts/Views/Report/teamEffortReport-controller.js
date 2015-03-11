'use strict';
/*global reportModule*/

reportModule.controller('TeamEffortReportController',
['$scope', '$route', '$location', 'reportFactory', 'notificationFactory', 'commonFactory', 'employeeRepository',
'employeeEPAFactory', 'ErrorService', '$timeout', 'authorityRepository', '$modal', 'projectDataFactory',
function ($scope, $route, $location, reportFactory, notificationFactory, commonFactory, employeeRepository,
employeeEPAFactory, ErrorService, $timeout, authorityRepository, $modal, projectDataFactory) {
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
	self.generateOptions = function () {
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
		$scope.personalChartTitle = "";
		$scope.isSystemAdmin = false;

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
			self.getAllGroupByActorUser();
			self.getAllRedmineProjects();

			self.isSystemAdmin(function () {
				$scope.getEmployee(function () {
					if (!$scope.isSystemAdmin) {
						self.getReportData();
					}
				});
			});
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

	self.isSystemAdmin = function (callBack) {
		authorityRepository.isSystemAdmin().success(function (result) {
			$scope.isSystemAdmin = angular.fromJson(result);
			if (callBack) {
				callBack();
			}
		}).error(function () {
			notificationFactory.error("ErrorMessage");
		});
	};

	self.validateFilterOptions = function () {
		var isValid = commonFactory.validateFromToDate($scope.filter.fromDate, $scope.filter.toDate);

		return isValid;
	};

	/*
	|| Filter region
	*/
	$scope.getEmployee = function (callBack) {
		var groupId = $scope.filter.groupId;

		employeeRepository.getEmployeeByTeamProject(groupId, null).success(function (employees) {
			$scope.employees = employees;

			if (callBack) {
				callBack();
			}
		}).error(function () {
			notificationFactory.error("ErrorMessage");
		});
	};

	self.getAllRedmineProjects = function () {
		projectDataFactory.getAllRedmineProjects().then(function (projectList) {
			$scope.projects = projectList;
		});
	};

	self.getAllGroupByActorUser = function () {
		employeeRepository.getAllGroupByActorUser().success(function (groupList) {
			$scope.groups = groupList;
		}).error(function () {
			notificationFactory.error("ErrorMessage");
		});
	};

	$scope.handleReportData = function () {
		var isValid = self.validateFilterOptions();

		if (isValid) {
			if ($scope.isSystemAdmin) {
				var selectedEmployees = commonFactory.getSelectedItems($scope.employees);

				if (!$scope.filter.groupId && !$scope.filter.projectId && selectedEmployees.length === $scope.employees.length) {
					// warning if show all employees
					self.warningShowAllEmployees();
				} else {
					self.getReportData();
				}
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
		$scope.filter.nickNames = commonFactory.getNickNames($scope.employees);

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
			$scope.personalChartTitle = "Member's effort";
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