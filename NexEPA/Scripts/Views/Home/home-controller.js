'use strict';
/*global homeModule*/

homeModule.controller('ViewHomePageController',
['$scope', '$route', 'homeFactory', 'notificationFactory',
function ($scope, $route, homeFactory, notificationFactory) {

	$scope.dashboradInformation = {
		generalInformation: '',
		openProjects: '',
		historyProjects: '',
		listKPIs: ''
	};

	homeFactory.getGeneralInformationOfCurrentEmployee().success(function (dashboardInformation) {
		if(dashboardInformation != null) {
			$scope.dashboradInformation = {
				generalInformation: dashboardInformation.generalInformation,
				openProjects: dashboardInformation.openProjects,
				historyProjects: dashboardInformation.historyProjects,
				listKPIs: dashboardInformation.listKPIs
			};
		}
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

homeModule.controller('HistoryProjectController', ['$scope', '$location', 'homeFactory',
function ($scope, $location, homeFactory) {
	$scope.isCollapsed = false;
	$scope.collapse = {};
	$scope.originalComment = '';
	$scope.employeeId;

	homeFactory.getCurrentEmployeeId().success(function (employeeId) {
		$scope.employeeId = parseInt(employeeId);
	});

	$scope.collapseTable = function () {
		$scope.isCollapsed = !$scope.isCollapsed;
	};

	$scope.EPADetail = function (targetId) {
		$scope.viewEPADetailUrl = '/MyEPA/Index' + '?targetId=' + targetId + '&employeeId=' + $scope.employeeId;
	};
} ]);

homeModule.controller('HomeKPIController', ['$scope', function ($scope) {
	$scope.isCollapsed = false;

	$scope.collapseTable = function () {
		$scope.isCollapsed = !$scope.isCollapsed;
	};
} ]);