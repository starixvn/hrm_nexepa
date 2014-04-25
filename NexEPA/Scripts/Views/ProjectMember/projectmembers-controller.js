/* global projectsSettingModule */
'use strict';

var memberValidation = function (employeeId, selected, joinDate) {
	var error = $("#memberValid");
	if (selected.length > 0 && employeeId != null && joinDate != null) {
		error.hide();
		return true;
	}
	error.show();
	return false;
};

projectsSettingModule.controller("ProjectMembersController", ['$scope', '$routeParams', '$location', '$modal', '$window',
'ProjectSettingCache', 'projectMilestoneService', 'projectMembersFactory', 'notificationFactory', 'projectDataFactory',
function ($scope, $routeParams, $location, $modal,  $window, ProjectSettingCache, projectMilestoneService,
projectMembersFactory, notificationFactory, projectDataFactory) {
	/**
	* Scope variable.
	*/
	$("#memberValid").hide();
	var projectIdInUrl = $routeParams.projectId;
	var cacheListEmployee = ProjectSettingCache.get("CacheListEmployee");
	$scope.currentProject = [];
	$scope.roles = [];
	
	/**
	* Ajax event loaded. 
	*/
	var successGetMembers = function (data) { $scope.members = data; };
	var successGetMilestones = function (data) { $scope.milestones = data; };
	// Get project by Id
	var successGetProjectById = function (data) {
		$scope.currentProject = data;
	};

	var successDeleteMember = function () {
		notificationFactory.success();
		return projectMembersFactory.getMember(projectIdInUrl, null, null).then(successGetMembers, errorNotification);
	};
	var errorNotification = function (data) { notificationFactory.error(data); };

	/**
	* Initialize data to view Index.
	*/
	projectMembersFactory.getMember(projectIdInUrl, null, null).then(successGetMembers);
	projectMembersFactory.getMilestone(projectIdInUrl).then(successGetMilestones);
	projectDataFactory.getProjectById(projectIdInUrl).then(successGetProjectById);

	// Watch for Milestone list change in MilestoneController.
	$scope.$watch(function () {
		return projectMilestoneService.getProperty();
	}, function (value) {
		$scope.milestones = value;
	});

	/**
	* Events.
	*/
	// Filter Data Display on Grid.
	$scope.filterMember = function (isShowDeletedMember, milestone) {
		projectMembersFactory.getMember(projectIdInUrl, isShowDeletedMember, milestone).then(successGetMembers);
	};

	$scope.cancel = function () { $window.history.back(); };

	// Add New Member.
	$scope.addMember = function () {
		/**
		* Ajax event loaded. 
		*/
		var successAddProjectMember = function () {
			notificationFactory.success();
			projectMembersFactory.getMember(projectIdInUrl, null, null).then(successGetMembers);
		};

		// Popup Add New Memeber.
		var populateModalInstance = function (lstMembers, roles) {
			var addModalInstance = $modal.open({
				templateUrl: '/templates/ProjectMembers/Create.html',
				controller: "AddProjectMembersController",
				resolve: {
					lstMembers: function () {
						return lstMembers;
					},
					roles: function () {
						return roles;
					},
					projectId: function () {
						return projectIdInUrl;
					}
				}
			});
			addModalInstance.result.then(function (member) {
				projectMembersFactory.addMember(member).then(successAddProjectMember, errorNotification);
			});
		};

		var successGetEmployeeSelectList = function (data) {
			$scope.lstMembers = data;
			ProjectSettingCache.put("CacheListEmployee", data);
			projectMembersFactory.getProjectRole().then(function (data) {
				$scope.roles = data;
				populateModalInstance($scope.lstMembers, data);
			});
		};

		/**
		* Initialize data to view Add.
		*/
		if (cacheListEmployee) {
			$scope.lstMembers = cacheListEmployee;
			projectMembersFactory.getProjectRole().then(function (data) {
				$scope.roles = data;
				populateModalInstance($scope.lstMembers, data);
			});
		}
		else {
			projectDataFactory.getEmployeeSelectList(null).then(successGetEmployeeSelectList);
		}
	};

	// Update Member.
	$scope.updateMember = function (employeeId, joinDate) {
		var successUpdateProjectMember = function () {
			notificationFactory.success();
			projectMembersFactory.getMember(projectIdInUrl, null, null).then(successGetMembers);
		};
		var successGetEmployeeSelectList = function (data) {
			$scope.lstMembers = data;
			ProjectSettingCache.put("CacheListEmployee", data);
			projectMembersFactory.getProjectRole(projectIdInUrl, employeeId).then(function (data) {
				$scope.roles = data;
				populateModalInstance($scope.lstMembers, data);
			});
		};

		// Popup Edit Member.
		var populateModalInstance = function (lstMembers, roles) {
			var editMember = {};
			editMember.employeeId = employeeId;
			editMember.joinDate = joinDate;
			editMember.projectId = projectIdInUrl;
			var addModalInstance = $modal.open({
				templateUrl: '/templates/ProjectMembers/Edit.html',
				controller: "EditProjectMembersController",
				resolve: {
					lstMembers: function () {
						return lstMembers;
					},
					roles: function () {
						return roles;
					},
					editMember: function () {
						return editMember;
					}
				}
			});
			addModalInstance.result.then(function (member) {
				projectMembersFactory.addMember(member).then(successUpdateProjectMember, errorNotification);
			});
		};

		/**
		* Initialize data to view Edit.
		*/
		if (cacheListEmployee) {
			$scope.lstMembers = cacheListEmployee;
			projectMembersFactory.getProjectRole(projectIdInUrl, employeeId).then(function (data) {
				$scope.roles = data;
				populateModalInstance($scope.lstMembers, data);
			});
		}
		else {
			projectDataFactory.getEmployeeSelectList(null).then(successGetEmployeeSelectList);
		}
	};

	// Delete Member.
	$scope.deleteMember = function (member) {
		member.projectId = projectIdInUrl;
		var deleteModalInstance = $modal.open({
			templateUrl: '/templates/Common/ModalConfirmContent.html',
			controller: "ModalConfirmController",
			resolve: {
				name: function () {
					return member.fullName;
				},
				action: function () {
					return "delete";
				}
			}
		});
		deleteModalInstance.result.then(function () {
			projectMembersFactory.deleteMember(member).then(successDeleteMember, errorNotification);
		});
	};
} ]);

projectsSettingModule.controller('AddProjectMembersController',
['$scope', '$modalInstance', 'commonFactory', 'lstMembers', 'roles', 'projectId',
function ($scope, $modalInstance, commonFactory, lstMembers, roles, projectId) {
	$scope.selection = [];
	$scope.lstMembers = lstMembers;
	$scope.roles = roles;
	$scope.calendar = { opened: false };

	// Get calendar button image.
	commonFactory.getSourceCalendarImage($scope);

	// get Date Format
	commonFactory.getDateFormat($scope);

	$scope.ok = function (employeeId, joinDate) {
		var selected = $scope.selection;
		var member = {};
		if (memberValidation(employeeId, selected, joinDate)) {
			var projectRolesIds = selected.join(',');
			$scope.errors = [];
			member.projectId = projectId;
			member.projectRoleIds = projectRolesIds;
			member.employeeId = employeeId;
			member.joinDate = joinDate;
			$modalInstance.close(member);
		}
	};

	$scope.cancel = function () {
		$modalInstance.dismiss('cancel');
	};

	// show start date calendar when click button start date
	$scope.open = function ($event) {
		$event.preventDefault();
		$event.stopPropagation();
		$scope.calendar.opened = true;
	};

	// Watch for checkboxlist change.
	$scope.$watch('roles|filter:{selected:true}', function (selectedItems) {
		$scope.selection = selectedItems.map(function (item) {
			return item.projectRoleId;
		});
	}, true);
} ]);

projectsSettingModule.controller("EditProjectMembersController",
['$scope', '$modalInstance', 'lstMembers', 'roles', 'editMember', 'commonFactory',
function ($scope, $modalInstance, lstMembers, roles, editMember, commonFactory) {
	$scope.lstMembers = lstMembers;
	$scope.roles = roles;
	$scope.selection = [];
	$scope.editMember = editMember;
	$scope.calendar = { opened: false };

	// Get calendar button image.
	commonFactory.getSourceCalendarImage($scope);

	// get Date Format
	commonFactory.getDateFormat($scope);

	$scope.ok = function (member) {
		var selected = $scope.selection;
		if (memberValidation(member.employeeId, selected, member.joinDate)) {
			var projectRolesIds = selected.join(',');
			$scope.errors = [];
			member.projectRoleIds = projectRolesIds;
			$modalInstance.close(member);
		}
	};

	$scope.cancel = function () {
		$modalInstance.dismiss('cancel');
	};

	// show start date calendar when click button start date
	$scope.open = function ($event) {
		$event.preventDefault();
		$event.stopPropagation();
		$scope.calendar.opened = true;
	};

	// Watch for checkboxlist change.
	$scope.$watch('roles|filter:{selected:true}', function (selectedItems) {
		$scope.selection = selectedItems.map(function (item) {
			return item.projectRoleId;
		});
	}, true);
}]);
