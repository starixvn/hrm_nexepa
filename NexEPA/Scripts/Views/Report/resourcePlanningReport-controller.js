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
	$scope.isCollapsed = false;

	$scope.reportData = null;
	$scope.collapses = {};
	$scope.topLevelWorkingClasses = {};
	$scope.dateStatuses = {};
	$scope.subLevelWorkingClasses = {};

	$scope.isSystemAdmin = false;
	//#endregion

	//#region General functions
	$scope.openPopupStartDate = function ($event) {
		commonFactory.openPopupStartDate($event, $scope);
	};

	$scope.openPopupEndDate = function ($event) {
		commonFactory.openPopupEndDate($event, $scope);
	};

	$scope.collapseTable = function () {
		$scope.isCollapsed = !$scope.isCollapsed;
	};

	$scope.reload = function () {
		$route.reload();
	};

	self.load = function () {
		self.checkIfSystemAdmin();
		commonFactory.getDateFormat($scope);
		self.getAllGroup();
		self.getAllRedmineProjects();
		$scope.getEmployee();
	};

	self.validateFilterOptions = function () {
		var isValid = true,
			fromDate = $scope.filter.fromDate,
			toDate = $scope.filter.toDate;

		if (!fromDate) {
			isValid = false;
			notificationFactory.error("FromRequire");
		} else if (!toDate) {
			isValid = false;
			notificationFactory.error("ToRequire");
		} else {
			fromDate = new Date(fromDate.toDateString());
			toDate = new Date(toDate.toDateString());
			var maxDate = new Date(fromDate.getFullYear(), fromDate.getMonth() + 2, fromDate.getDate());

			if (fromDate > toDate) {
				isValid = false;
				notificationFactory.error("ValidateFromToDate");
			}
			else if (toDate > maxDate) {
				isValid = false;
				notificationFactory.error("ValidateMaximumTwoMonth");
			}
		}

		if (!$scope.isSystemAdmin && !$scope.filter.groupId && !$scope.filter.projectId) {
			isValid = false;
			notificationFactory.error("TeamOrProjectRequire");
		}

		return isValid;
	};

	self.checkIfSystemAdmin = function () {
		authorityRepository.isSystemAdmin().success(function (result) {
			$scope.isSystemAdmin = angular.fromJson(result);
		}).error(function () {
			notificationFactory.error("ErrorMessage");
		});
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
			var selectedEmployees = commonFactory.getSelectedData($scope.employees);

			if (!$scope.filter.groupId && !$scope.filter.projectId && selectedEmployees.length === $scope.employees.length) {
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

		if ($scope.filter.nickNames) {
			self.getRedmineResourcePlanning();
		} else {
			$scope.reportData = {};

			$scope.isLoadingButton = false;
		}
	};
	//#endregion

	//#region Resource Planning
	self.getRedmineResourcePlanning = function () {
		reportFactory.getRedmineResourcePlanning($scope.filter)
		.success(function (report) {
			if (report != null) {
				$scope.reportData = report;
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
	//#endregion

	// Run first
	self.load();
} ]);