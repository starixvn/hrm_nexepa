'use strict';
/*global groupProjectModule*/

groupProjectModule.controller('GroupProjectController',
['$scope', '$location', '$translate', '$modal', '$route', 'groupProjectFactory', 'employeeEPAFactory',
'commonFactory', 'ErrorService', 'notificationFactory',
function ($scope, $location, $translate, $modal, $route, groupProjectFactory, employeeEPAFactory,
commonFactory, ErrorService, notificationFactory) {
	/**
	* Get Information
	*/
	groupProjectFactory.getPermission().success(function (policy) {
		$scope.permission = {
			policy: policy
		};
	}).error(function () {
		notificationFactory.error("ErrorMessage");
	});

	employeeEPAFactory.getCurrentUser().then(function (currentUser) {
		$scope.currentUser = currentUser;
	}, function () {
		notificationFactory.error("ErrorMessage");
	});

	var getGroupProject = function () {
		groupProjectFactory.getGroupProject().success(function (groupProjects) {
			$scope.groupProjects = groupProjects;
		}).error(function (response, status) {
			if (status === 405) {
				ErrorService.errorHandle(status);
			}
			else {
				notificationFactory.error("ErrorMessage");
			}
		});
	};

	getGroupProject();
} ]);
