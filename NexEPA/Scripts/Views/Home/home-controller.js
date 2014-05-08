'use strict';
/*global homeModule*/

homeModule.controller('ViewHomePageController', ['$scope', '$route', 'homeFactory', 'notificationFactory',
	function ($scope, $route, homeFactory, notificationFactory) {

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
	} ]);

homeModule.controller('OpenProjectController', ['$scope', function ($scope) {
	$scope.isCollapsed = false;

	$scope.collapseTable = function () {
		$scope.isCollapsed = !$scope.isCollapsed;
	};
} ]);

homeModule.controller('HistoryProjectController', ['$scope', '$location', function ($scope, $location) {
	$scope.isCollapsed = false;
	$scope.collapse = {};
	$scope.isMilestoneCollapsed = false;

	$scope.collapseTable = function () {
		$scope.isCollapsed = !$scope.isCollapsed;
	};

	$scope.showHideMilestone = function (historyProject) {
		var projectId = historyProject.project.projectId;
		var property = 'isCollapsed' + projectId;
		$scope.collapse[property] = !$scope.collapse[property];
	};

	$scope.EPADetail = function (targetId) {
		var employeeId = 248; // TODO : get employeeId of current user
		$location.path('/MyEPA/Index').search('targetId', targetId).search('employeeId', employeeId);
	};
} ]);

homeModule.controller('HomeKPIController', ['$scope', function ($scope) {
	$scope.isCollapsed = false;

	$scope.collapseTable = function () {
		$scope.isCollapsed = !$scope.isCollapsed;
	};
} ]);