'use strict';
/*global groupProjectModule*/

groupProjectModule.controller('GroupProjectController',
['$scope', '$location', '$translate', '$modal', '$route', 'groupProjectFactory', 'employeeEPAFactory',
'commonFactory', 'ErrorService', 'notificationFactory',
function ($scope, $location, $translate, $modal, $route, groupProjectFactory, employeeEPAFactory,
commonFactory, ErrorService, notificationFactory) {
	/*
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

	/*
	* Get all projects.
	*/
	groupProjectFactory.getAllProject().success(function (projects) {
		$scope.availableProjects = projects;
	}, function () {
		notificationFactory.error("ErrorMessage");
	});

	/*
	* Get group project.
	*/
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

	/*
	* Create group project.
	*/
	$scope.createGroupProject = function () {
		var modalInstance = $modal.open({
			templateUrl: '/templates/GroupProject/Create.html',
			controller: "CreateGroupProjectController",
			backdrop: 'static',
			resolve: {
				projects: function () {
					return angular.copy($scope.availableProjects);
				}
			},
			windowClass: "group-project-model"
		});

		modalInstance.result.then(function (groupProject) {
			groupProjectFactory.insertGroupProject(groupProject)
			.success(function (response) {
				if (response.isSuccessful === true) {
					notificationFactory.success("DataChangeSuccessfully");

					getGroupProject();
				}
				else {
					notificationFactory.error("", response.message);
				}
			})
			.error(function (response, status) {
				ErrorService.errorHandle(status);
			});
		});
	};

	getGroupProject();
} ]);

groupProjectModule.controller('CreateGroupProjectController',
['$scope', '$modalInstance', 'projects', 'groupProjectFactory', 'notificationFactory',
function ($scope, $modalInstance, projects, groupProjectFactory, notificationFactory) {
	$scope.availableProjects = projects;

	$scope.groupProject = {
		Name: '',
		Projects: []
	};

	$scope.save = function () {
		if (!$scope.groupProject.Name) {
			notificationFactory.error("NameRequire");
		} else {
			angular.forEach($scope.groupProject.Projects, function (item) {
				delete item.$$hashKey;
			});

			$modalInstance.close($scope.groupProject);
		}
	};

	$scope.cancel = function () {
		$modalInstance.dismiss("cancel");
	};
} ]);
