'use strict';
/*global reportModule*/

reportModule.controller('ViewEmployeePerformanceTendencyController',
['$scope', '$route', 'reportFactory', 'employeeRepository', 'notificationFactory',
function ($scope, $route, reportFactory, employeeRepository, notificationFactory) {

	$scope.report = {
		generalInformation: '',
		historyProjects: ''
	};

	$scope.search = {
		searchKey: ''
	};

	// get all employee by target Id 
	$scope.getEmployee = function (searchKey) {
		employeeRepository.getAllEmployee(searchKey, null).success(function (employees) {
			$scope.searchEmployees = employees;
		}).error(function () {
			notificationFactory.error("ErrorMessage");
		});
	};

	// get Employee Performance Tendency report
	$scope.getReport = function (employee) {
		if (employee) {
			var employeeId = employee.employeeId;
			reportFactory.getEmployeePerformanceTendencyReport(employeeId).success(function (report) {
				$scope.search.searchKey = '';

				if (report != null && report.length >= 2) {
					$scope.report.generalInformation = report[0];
					$scope.report.historyProjects = report[1];
				}
			}).error(function () {
				notificationFactory.error("ErrorMessage");
			});
		}
	};

	$scope.reload = function () {
		$route.reload();
	};
} ]);

reportModule.controller('EmployeeGeneralInformationController', [function () {

} ]);

reportModule.controller('EmployeeHistoryProjectController', ['$scope', '$location', 'homeFactory',
function ($scope, $location, homeFactory) {
	$scope.isCollapsed = false;
	$scope.collapse = {};
	$scope.isMilestoneCollapsed = false;
	$scope.originalComment = '';
	$scope.employeeId;

	homeFactory.getCurrentEmployeeId().success(function (employeeId) {
		$scope.employeeId = parseInt(employeeId);
	});

	$scope.collapseTable = function () {
		$scope.isCollapsed = !$scope.isCollapsed;
	};

	$scope.showHideMilestone = function (historyProject) {
		var projectId = historyProject.project.projectId;
		var property = 'isCollapsed' + projectId;
		$scope.collapse[property] = !$scope.collapse[property];
	};

	$scope.EPADetail = function (targetId) {
		$location.path('/MyEPA/Index').search('targetId', targetId).search('employeeId', $scope.employeeId);
	};
} ]);