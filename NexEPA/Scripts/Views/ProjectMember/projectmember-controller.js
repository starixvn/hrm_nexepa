/* global projectsSettingModule */
'use strict';

var projectMemberValidation = function (employeeId, selected, joinDate) {
	var error = $("#memberValid");
	if (selected.length > 0 && employeeId != null && joinDate !== undefined) {
		error.hide();
		return true;
	}
	error.show();
	return false;
};

projectsSettingModule.controller("ProjectMembersController", ['$scope', '$routeParams', '$location', '$modal', '$window',
'ProjectSettingCache', 'projectMilestoneService', 'projectMembersFactory', 'notificationFactory', 'projectDataFactory',
function ($scope, $routeParams, $location, $modal, $window, ProjectSettingCache, projectMilestoneService,
projectMembersFactory, notificationFactory, projectDataFactory) {
	/**
	* Scope variable.
	*/
	$("#memberValid").hide();
	var projectIdInUrl = $routeParams.projectId;
	$scope.currentProject = [];
	$scope.roles = [];

	/**
	* Ajax event loaded. 
	*/
	var successGetMembers = function (data) {
		$scope.members = data;
	};
	var successGetMilestones = function (data) { $scope.milestones = data; };
	// Get project by Id
	var successGetProjectById = function (data) {
		$scope.currentProject = data;
	};

	var successDeleteMember = function () {
		notificationFactory.success('SuccessDeleteData', '');
		return projectMembersFactory.getMember(projectIdInUrl, null, null).then(successGetMembers, errorNotification);
	};
	var errorNotification = function (data) { notificationFactory.error('', data); };

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
			notificationFactory.success('SuccessSaveData', '');
			projectMembersFactory.getMember(projectIdInUrl, null, null).then(successGetMembers);
		};

		// Popup Add New Memeber.
		var populateModalInstance = function (roles) {
			var addModalInstance = $modal.open({
				templateUrl: '/templates/ProjectMembers/Create.html',
				controller: "AddProjectMembersController",
				backdrop: 'static',
				resolve: {
					roles: function () {
						return roles;
					},
					projectId: function () {
						return projectIdInUrl;
					}
				}
			});
			addModalInstance.result.then(function (member) {
				if (member.joinDate != null) {
					var selectedStartDate = new Date(member.joinDate);
					member.joinDate = selectedStartDate ? selectedStartDate.toDateString() : null;
				}
				projectMembersFactory.addMember(member).then(successAddProjectMember, errorNotification);
			});
		};

		/**
		* Initialize data to view Add.
		*/
		projectMembersFactory.getProjectRole().then(function (data) {
			$scope.roles = data;
			populateModalInstance(data);
		});
	};

	// Update Member.
	$scope.updateMember = function (member) {
		var successUpdateProjectMember = function () {
			notificationFactory.success('SuccessSaveData', '');
			projectMembersFactory.getMember(projectIdInUrl, null, null).then(successGetMembers);
		};

		// Popup Edit Member.
		var populateModalInstance = function (roles) {
			var editMember = member;
			editMember.projectId = parseInt(projectIdInUrl);
			var addModalInstance = $modal.open({
				templateUrl: '/templates/ProjectMembers/Edit.html',
				controller: "EditProjectMembersController",
				backdrop: 'static',
				resolve: {
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
		projectMembersFactory.getProjectRole(projectIdInUrl, member.employeeId).then(function (data) {
			$scope.roles = data;
			populateModalInstance(data);
		});
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
['$scope', '$modalInstance', 'commonFactory', 'roles', 'projectId', 'projectDataFactory',
function ($scope, $modalInstance, commonFactory, roles, projectId, projectDataFactory) {
	$scope.selection = [];
	$scope.roles = roles;
	$scope.calendar = { opened: false };

	// get Date Format
	commonFactory.getDateFormat($scope);

	$scope.ok = function (employeeId, joinDate) {
		var selected = $scope.selection;
		var member = {};
		var check = projectMemberValidation(employeeId, selected, joinDate);
		if (check) {
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

	// Search member
	$scope.getEmployee = function (searchKey) {
		projectDataFactory.getEmployeeSelectList(null, searchKey).then(function (employees) {
			$scope.searchEmployees = employees;
		});
	};
} ]);

projectsSettingModule.controller("EditProjectMembersController",
['$scope', '$modalInstance', 'roles', 'editMember', 'commonFactory', 'projectDataFactory',
function ($scope, $modalInstance, roles, editMember, commonFactory, projectDataFactory) {
	$scope.roles = roles;
	$scope.selection = [];
	$scope.editMember = editMember;
	$scope.calendar = { opened: false };
	$scope.searchKey = editMember.nickName + " (" + editMember.fullName + ")";

	// get Date Format
	commonFactory.getDateFormat($scope);

	$scope.ok = function (member) {
		var selected = $scope.selection;
		if (projectMemberValidation(member.employeeId, selected, member.joinDate)) {
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

	// Search member
	$scope.getEmployee = function (searchKey) {
		projectDataFactory.getEmployeeSelectList(null, searchKey).then(function (employees) {
			$scope.searchEmployees = employees;
		});
	};
} ]);
