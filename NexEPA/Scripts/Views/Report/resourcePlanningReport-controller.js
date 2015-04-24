'use strict';
/*global reportModule*/

reportModule.controller('ResourcePlanningReportController',
['$scope', '$route', '$location', 'reportFactory', 'notificationFactory', 'commonFactory', 'employeeRepository',
'employeeEPAFactory', 'ErrorService', '$timeout', 'authorityRepository', '$modal', 'projectDataFactory', 'groupProjectFactory',
function ($scope, $route, $location, reportFactory, notificationFactory, commonFactory, employeeRepository,
employeeEPAFactory, ErrorService, $timeout, authorityRepository, $modal, projectDataFactory, groupProjectFactory) {
	var self = this;
	/*jshint ignore:start */
	// moment is the third party library.
	var fromDate = moment().subtract(1, 'months');
	var toDate = moment().add(1, 'months');

	//#region $scope properties
	$scope.filter = {
		fromDate: new Date(fromDate),
		toDate: new Date(toDate),
		groupId: null,
		isFilterGroup: 'false',
		projectIds: '',
		groupProjectId: '',
		nickNames: ''
	};
	/*jshint ignore:end */

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
		self.getAllGroupProjects();
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

			/*jshint ignore:start */
			// moment is the third party library.
			var maxDate = moment(fromDate).add(61, "days");
			/*jshint ignore:end */

			if (fromDate > toDate) {
				isValid = false;
				notificationFactory.error("ValidateFromToDate");
			}
			/*jshint ignore:start */
			// moment is the third party library.
			else if (toDate > maxDate) {
				isValid = false;
				notificationFactory.error("ValidateMaximumTwoMonth");
			}
			/*jshint ignore:end */
		}

		if (!$scope.isSystemAdmin && !$scope.filter.groupId && !$scope.filter.projectId) {
			isValid = false;
			notificationFactory.error("TeamOrProjectRequire");
		} else if ($scope.filter.isFilterGroup === 'true' && $scope.filter.groupProjectId === '') {
			isValid = false;
			notificationFactory.error("GroupProjectFilterRequire");
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
		var groupId = $scope.filter.groupId;

		if ($scope.filter.isFilterGroup === 'true') {
			var groupProjectId = $scope.filter.groupProjectId;

			employeeRepository.getRedmineEmployeesByTeamGroupProject(groupId, groupProjectId).success(function (employees) {
				$scope.employees = employees;
			}).error(function () {
				notificationFactory.error("ErrorMessage");
			});
		} else {
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
		$scope.filter.projectIds = commonFactory.getSelectedData($scope.projects, "id");

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