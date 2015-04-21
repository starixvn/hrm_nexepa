'use strict';
/*global groupProjectModule*/

groupProjectModule.controller('GroupProjectController',
['$scope', '$location', '$translate', '$modal', '$route', 'groupProjectFactory', 'employeeEPAFactory', 'projectDataFactory',
'commonFactory', 'ErrorService', 'notificationFactory',
function ($scope, $location, $translate, $modal, $route, groupProjectFactory, employeeEPAFactory, projectDataFactory,
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
	projectDataFactory.getAllRedmineProjects().then(function (projectList) {
		$scope.availableProjects = projectList;
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
			if (groupProject) {
				var newGroup = {
					groupProjectId: groupProject.groupProjectId,
					name: groupProject.Name,
					projects: groupProject.Projects
				};

				$scope.groupProjects.push(newGroup);
			}
		});
	};

	/*
	* Update group project.
	*/
	$scope.updateGroupProject = function (group) {
		var modalInstance = $modal.open({
			templateUrl: '/templates/GroupProject/Edit.html',
			controller: "UpdateGroupProjectController",
			backdrop: 'static',
			resolve: {
				groupProject: function () {
					return angular.copy(group);
				},
				projects: function () {
					return angular.copy($scope.availableProjects);
				}
			},
			windowClass: "group-project-model"
		});

		modalInstance.result.then(function (groupProject) {
			if (groupProject) {
				var newGroup = {
					groupProjectId: groupProject.GroupProjectId,
					name: groupProject.Name,
					projects: groupProject.Projects
				};

				var index = $scope.groupProjects.map(function (e) { return e.groupProjectId; }).indexOf(newGroup.groupProjectId);
				$scope.groupProjects[index] = newGroup;
			}
		});
	};

	/*
	* Delete group project.
	*/
	$scope.deleteGroupProject = function (groupProject) {
		var modalInstance = $modal.open({
			templateUrl: '/templates/Common/ModalConfirmContent.html',
			controller: "ModalConfirmController",
			backdrop: 'static',
			resolve: {
				name: function () {
					return groupProject.name;
				},
				action: function () {
					return "delete";
				},
				extraInfo: function () {
					return '';
				}
			},
			windowClass: "modal-confirm-center"
		});

		modalInstance.result.then(function () {
			groupProjectFactory.deleteGroupProject(groupProject.groupProjectId).success(function (response) {
				if (response.isSuccessful === true) {
					notificationFactory.success("DataChangeSuccessfully");
					var index = $scope.groupProjects.map(function (e) { return e.groupProjectId; }).indexOf(groupProject.groupProjectId);
					$scope.groupProjects.splice(index, 1);
				}
				else {
					notificationFactory.error("", response.message);
				}
			}).error(function (response, status) {
				if (status === 405) {
					ErrorService.errorHandle(status);
				}
				else {
					notificationFactory.error("ErrorMessage");
				}
			});
		});
	};

	getGroupProject();
} ]);

groupProjectModule.controller('CreateGroupProjectController',
['$scope', '$modalInstance', 'projects', 'groupProjectFactory', 'ErrorService', 'notificationFactory',
function ($scope, $modalInstance, projects, groupProjectFactory, ErrorService, notificationFactory) {
	$scope.availableProjects = projects;

	$scope.groupProject = {
		Name: '',
		ProjectIds: '',
		Projects: []
	};

	$scope.save = function () {
		if (!$scope.groupProject.Name) {
			notificationFactory.error("NameRequire");
		} else {
			var ids = '';

			angular.forEach($scope.groupProject.Projects, function (item) {
				ids = ids ? ids + ',' + item.id : item.id;
			});

			$scope.groupProject.ProjectIds = ids;

			groupProjectFactory.insertGroupProject($scope.groupProject)
			.success(function (response) {
				if (response.result.isSuccessful === true) {
					notificationFactory.success("DataChangeSuccessfully");

					$scope.groupProject.groupProjectId = response.groupProjectId;
					$modalInstance.close($scope.groupProject);
				}
				else {
					notificationFactory.error("", response.message);
				}
			})
			.error(function (response, status) {
				ErrorService.errorHandle(status);
			});
		}
	};

	$scope.cancel = function () {
		$modalInstance.dismiss("cancel");
	};
} ]);

groupProjectModule.controller('UpdateGroupProjectController',
['$scope', '$modalInstance', 'groupProject', 'projects', 'groupProjectFactory', 'ErrorService', 'notificationFactory',
function ($scope, $modalInstance, groupProject, projects, groupProjectFactory, ErrorService, notificationFactory) {
	$scope.availableProjects = projects;

	$scope.groupProject = {
		GroupProjectId: groupProject.groupProjectId,
		Name: groupProject.name,
		ProjectIds: '',
		Projects: groupProject.projects
	};

	$scope.save = function () {
		if (!$scope.groupProject.Name) {
			notificationFactory.error("NameRequire");
		} else {
			var ids = '';

			angular.forEach($scope.groupProject.Projects, function (item) {
				ids = ids ? ids + ',' + item.id : item.id;
			});

			$scope.groupProject.ProjectIds = ids;

			groupProjectFactory.updateGroupProject($scope.groupProject)
			.success(function (response) {
				if (response.isSuccessful === true) {
					notificationFactory.success("DataChangeSuccessfully");
					$modalInstance.close($scope.groupProject);
				}
				else {
					notificationFactory.error("", response.message);
				}
			})
			.error(function (response, status) {
				ErrorService.errorHandle(status);
			});
		}
	};

	$scope.cancel = function () {
		$modalInstance.dismiss("cancel");
	};
} ]);
