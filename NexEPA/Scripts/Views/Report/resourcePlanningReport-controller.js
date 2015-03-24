'use strict';
/*global reportModule*/

reportModule.controller('ResourcePlanningReportController',
['$scope', '$route', '$location', 'reportFactory', 'notificationFactory', 'commonFactory', 'employeeRepository',
'employeeEPAFactory', 'ErrorService', '$timeout', 'authorityRepository', '$modal', 'projectDataFactory',
function ($scope, $route, $location, reportFactory, notificationFactory, commonFactory, employeeRepository,
employeeEPAFactory, ErrorService, $timeout, authorityRepository, $modal, projectDataFactory) {
	var self = this;

	//#region $scope properties
	$scope.filter = {
		fromDate: commonFactory.calculateFromDate(1),
		toDate: commonFactory.calculateLastDateOfCurrentMonth(),
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

	$scope.isLoadingButton = false;
	$scope.isTeamCollapsed = true;
	//#endregion

	//#region General functions
	$scope.openPopupStartDate = function ($event) {
		commonFactory.openPopupStartDate($event, $scope);
	};

	$scope.openPopupEndDate = function ($event) {
		commonFactory.openPopupEndDate($event, $scope);
	};

	$scope.collapseTable = function () {
		$scope.isTeamCollapsed = !$scope.isTeamCollapsed;
	};

	$scope.reload = function () {
		$route.reload();
	};

	self.load = function () {
		commonFactory.getDateFormat($scope);
		self.getAllGroup();
		self.getAllRedmineProjects();
		$scope.getEmployee();
	};

	self.validateFilterOptions = function () {
		var isValid = commonFactory.validateFromToDate($scope.filter.fromDate, $scope.filter.toDate);

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
	//#endregion

	//#region Filter
	$scope.getEmployee = function () {
		var groupId = $scope.filter.groupId,
			projectId = $scope.filter.projectId;

		employeeRepository.getRedmineEmployeesByTeamProject(groupId, projectId).success(function (employees) {
			$scope.employees = employees;
		}).error(function () {
			notificationFactory.error("ErrorMessage");
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

	$scope.handleReportData = function () {
		var isValid = self.validateFilterOptions();

		if (isValid) {

			//			var selectedEmployees = commonFactory.getSelectedItems($scope.employees);

			//			if (!$scope.filter.groupId && !$scope.filter.projectId && selectedEmployees.length === $scope.employees.length) {
			//				// warning if show all employees
			//				self.warningShowAllEmployees();
			//			} else {
			self.getReportData();
			//			}
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

		$scope.filter.nickNames = commonFactory.getNickNames($scope.employees);

		if ($scope.filter.nickNames) {
			self.getRedmineResourcePlanning();
		} else {
			//generate empty data

			$scope.isLoadingButton = false;
		}
	};
	//#endregion

	//#region Resource Planning
	self.getRedmineResourcePlanning = function () {
		reportFactory.getRedmineResourcePlanning($scope.filter)
		.success(function (report) {
			if (report != null) {
				// set report data
				report = null;
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
	//#endregion

	// Run first
	self.load();
} ]);