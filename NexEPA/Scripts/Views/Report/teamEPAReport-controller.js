'use strict';
/*global reportModule*/

reportModule.controller('TeamEPAReportController',
['$scope', '$route', '$location', 'reportFactory', 'notificationFactory', 'commonFactory', 'employeeRepository',
'employeeEPAFactory', 'ErrorService',
function ($scope, $route, $location, reportFactory, notificationFactory, commonFactory, employeeRepository,
employeeEPAFactory, ErrorService) {
	/*
	|| Variables	
	*/
	var self = this;

	/*
	|| $scope properties
	*/
	$scope.filter = {
		fromDate: commonFactory.calculateFromDate(3, 1),
		toDate: new Date(),
		groupId: null,
		projectId: null,
		employeeIds: ''
	};

	$scope.calendar = {
		startDateOpened: false,
		endDateOpened: false
	};

	$scope.employees = [];
	$scope.projects = [];
	$scope.groups = [];

	$scope.isLoadingButton = false;
	$scope.isSystemAdmin = false;
	$scope.isProjectCollapsed = false;
	$scope.reportByProjects = '';

	/*
	|| General functions
	*/
	$scope.openPopupStartDate = function ($event) {
		commonFactory.openPopupStartDate($event, $scope);
	};

	$scope.openPopupEndDate = function ($event) {
		commonFactory.openPopupEndDate($event, $scope);
	};

	$scope.collapseTable = function () {
		$scope.isProjectCollapsed = !$scope.isProjectCollapsed;
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

		self.checkTheRightToViewTeamEPAReport(function () {
			self.getAllGroup();
			self.getAllProject();

			$scope.getEmployee(function () {
				self.getReportData();
			});
		});
	};

	self.checkTheRightToViewTeamEPAReport = function (hasRightCallBack) {
		reportFactory.checkTheRightToViewTeamEPAReport().success(function () {
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

		return isValid;
	};

	/*
	|| Filter region
	*/
	$scope.getEmployee = function (callBack) {
		var groupId = $scope.filter.groupId;
		var projectId = $scope.filter.projectId;

		employeeRepository.getEmployeeByTeamProject(groupId, projectId).success(function (employees) {
			$scope.employees = employees;
			if (callBack) {
				callBack();
			}
		}).error(function () {
			notificationFactory.error("ErrorMessage");
		});
	};

	self.getAllProject = function () {
		employeeEPAFactory.getAllProject().then(function (projectList) {
			$scope.projects = projectList;
		});
	};

	self.getAllGroup = function () {
		employeeRepository.getAllGroupByActorUser().success(function (groupList) {
			$scope.groups = groupList;
		}).error(function () {
			notificationFactory.error("ErrorMessage");
		});
	};

	$scope.handleReportData = function () {
		var isValid = self.validateFilterOptions();

		if (isValid) {
			self.getReportData();
		}
	};

	self.getReportData = function () {
		$scope.isLoadingButton = true;

		$scope.filter.employeeIds = commonFactory.getSelectedData($scope.employees, "employeeId");

		if ($scope.filter.employeeIds) {
			self.getEmployeePerformanceByTeamReport();
		} else {
			$scope.reportByProjects = [];
			$scope.isLoadingButton = false;
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

	// Run first
	self.load();
} ]);