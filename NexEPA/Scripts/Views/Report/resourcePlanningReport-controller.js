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

	$scope.reportData = {};
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

		return isValid;
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
			$scope.reportData = {
				detailResourcePlannings: []
			};

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

	$scope.getDateStatus = function (nickName, workDate) {
		var result = '';

		var data = jQuery.grep($scope.reportData.resourceDateStatuses, function (item) {
			return item.employee.nickName === nickName && workDate === item.dateStatus.workDate;
		});

		if (data.length > 0) {
			var dateStatus = data[0];

			if (dateStatus.dateStatus.isHoliday) {
				result = 'Holiday';
			} else if (dateStatus.dateStatus.isOffDay) {
				result = 'Off';
			} else if (dateStatus.dateStatus.isLeaveDay) {
				result = 'Leave';
			}
		}

		return result;
	};

	$scope.getTopLevelWorkingClass = function (nickName, workDate) {
		var dateStatus = $scope.getDateStatus(nickName, workDate);
		if (dateStatus === '') {
			var data = jQuery.grep($scope.reportData.detailResourcePlannings, function (item) {
				return item.employee.nickName === nickName && item.issue.startDate <= workDate && workDate <= item.issue.dueDate;
			});

			if (data.length > 0) {
				return "top-level-working";
			}
		}
	};

	$scope.getSubLevelWorkingClass = function (nickName, projectId, workDate) {
		var dateStatus = $scope.getDateStatus(nickName, workDate);
		if (dateStatus === '') {
			var data = jQuery.grep($scope.reportData.detailResourcePlannings, function (item) {
				return item.employee.nickName === nickName && item.project.projectId === projectId &&
					item.issue.startDate <= workDate && workDate <= item.issue.dueDate;
			});

			if (data.length > 0) {
				return "sub-level-working";
			}
		}
	};
	//#endregion

	// Run first
	self.load();
} ]);