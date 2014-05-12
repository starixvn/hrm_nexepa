'use strict';
/*global homeModule*/

homeModule.controller('ViewHomePageController',
	['$scope', '$route', '$window', 'homeFactory', 'notificationFactory', 'employeeRepository',
	function ($scope, $route, $window, homeFactory, notificationFactory, employeeRepository) {

		$scope.dashboradInformation = {
			generalInformation: null,
			openProjects: null,
			historyProjects: null,
			listKPIs: null
		};

		homeFactory.getGeneralInformationOfCurrentEmployee().success(function (dashboardInformationList) {
			$scope.dashboradInformation = {
				generalInformation: dashboardInformationList[0],
				openProjects: dashboardInformationList[1],
				historyProjects: dashboardInformationList[2],
				listKPIs: dashboardInformationList[3]
			};
		}).error(function () {
			notificationFactory.error("ErrorMessage");
		});

		$scope.reload = function () {
			$route.reload();
		};

		// login as another user to test
		homeFactory.getIsSimulatedLoginUser().success(function (isSimulatedLoginUser) {
			$scope.isSimulatedLoginUser = isSimulatedLoginUser;
		}).error(function () {
			notificationFactory.error("ErrorMessage");
		});

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

		$scope.loginAsAnotherUser = function (employee) {
			homeFactory.loginAsAnotherUser(employee.domainUsername).success(function (code) {
				if (parseInt(code) === 0) {
					$window.location.reload();
				}
			}).error(function () {
				notificationFactory.error("ErrorMessage");
			});
		};
	} ]);

homeModule.controller('OpenProjectController', ['$scope', function ($scope) {
	$scope.isCollapsed = false;

	$scope.collapseTable = function () {
		$scope.isCollapsed = !$scope.isCollapsed;
	};
} ]);

homeModule.controller('HistoryProjectController', ['$scope', '$location', 'homeFactory', function ($scope, $location, homeFactory) {
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

homeModule.controller('HomeKPIController', ['$scope', function ($scope) {
	$scope.isCollapsed = false;

	$scope.collapseTable = function () {
		$scope.isCollapsed = !$scope.isCollapsed;
	};
} ]);