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

projectsSettingModule.controller("ProjectMembersController",
['$scope', '$routeParams', '$location', '$modal', '$window', 'ProjectSettingCache', 'projectMilestoneService',
'projectMembersFactory', 'notificationFactory', 'projectDataFactory', 'projectPermissionService', 'projectMemberService',
function ($scope, $routeParams, $location, $modal, $window, ProjectSettingCache, projectMilestoneService,
projectMembersFactory, notificationFactory, projectDataFactory, projectPermissionService, projectMemberService) {
	/**
	* Scope variable.
	*/
	$("#memberValid").hide();
	var projectIdInUrl = $routeParams.projectId;
	$scope.roles = [];

	/**
	* Ajax event loaded. 
	*/
	var successGetMembers = function (data) {
		$scope.members = data;
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

	// Watch for Milestone list change in MilestoneController.
	$scope.$watch(function () {
		return projectMilestoneService.getProperty();
	}, function (value) {
		$scope.milestones = value;
	});

	$scope.$watch(function () {
		return projectPermissionService.getProperty();
	}, function (value) {
		$scope.permission = value;
	});

	// Watch for Milestone list change in MilestoneController.
	$scope.$watch(function () {
		return projectMemberService.getProperty();
	}, function (value) {
		$scope.members = value;
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
				projectMembersFactory.updateMember(member).then(successUpdateProjectMember, errorNotification);
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
		var displayName = member.nickName + ' (' + member.fullName + ')';
		var deleteModalInstance = $modal.open({
			templateUrl: '/templates/Common/ModalConfirmContent.html',
			controller: "ModalConfirmController",
			resolve: {
				name: function () {
					return displayName;
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
'projectMembersFactory', 'notificationFactory', 'projectMemberService',
function ($scope, $modalInstance, commonFactory, roles, projectId, projectDataFactory, projectMembersFactory,
notificationFactory, projectMemberService) {
	$scope.selection = [];
	$scope.roles = roles;
	$scope.calendar = { opened: false };
	$scope.newMember = { joinDate: new Date() };

	// get Date Format
	commonFactory.getDateFormat($scope);
	var errorNotification = function (data) { notificationFactory.error('', data); };
	var successAddProjectMember = function () {
		notificationFactory.success('DataChangeSuccessfully', '');
		$scope.newMember = { joinDate: new Date() };
		$.each($scope.roles, function (i) {
			$scope.roles[i].selected = false;
		});
		$("#txtEmployee").val('');
		projectMembersFactory.getMember(projectId, null, null).then(function (data) {
			projectMemberService.setProperty(data);
		});
	};

	$scope.ok = function (isContinue) {
		var newMember = $scope.newMember;
		var employeeId = newMember.employee ? newMember.employee.employeeId : null;
		var selected = $scope.selection;
		var check = projectMemberValidation(employeeId, selected, newMember.joinDate);
		if (check) {
			var projectRolesIds = selected.join(',');
			$scope.errors = [];
			var member = { projectId: projectId, projectRoleIds: projectRolesIds,
				employeeId: employeeId, joinDate: newMember.joinDate
			};
			if (isContinue) {
				projectMembersFactory.addMember(member).then(successAddProjectMember, errorNotification);

			}
			else {
				$modalInstance.close(member);
			}

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
