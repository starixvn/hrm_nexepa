/* global projectsSettingModule */
'use strict';

projectsSettingModule.controller("ProjectMembersController",
['$scope', '$routeParams', '$location', '$modal', '$window', 'ProjectSettingCache', 'projectMilestoneService',
'projectMembersFactory', 'notificationFactory', 'projectDataFactory', 'projectPermissionService', 'projectMemberService', 'sharedProperties',
function ($scope, $routeParams, $location, $modal, $window, ProjectSettingCache, projectMilestoneService,
projectMembersFactory, notificationFactory, projectDataFactory, projectPermissionService, projectMemberService, sharedProperties) {
    var vm = this;

    vm.parentController = {};
    vm.projectIdInUrl = $routeParams.projectId;
    vm.isShowDeletedMember = false;
    vm.milestoneId = '';
    vm.roles = [];
    vm.showMembersFilter = true;

    vm.filterMember = function () {
        projectMembersFactory.getMember(vm.projectIdInUrl, vm.isShowDeletedMember, vm.milestoneId).then(function (members) {
            vm.parentController.project.memberList = members;
        });
    };

    vm.cancel = function () {
        $window.history.back();
    };

    vm.addMember = function () {
        $modal.open({
            templateUrl: '/templates/ProjectMembers/Create.html',
            controller: "AddProjectMembersController as addProjectMembersCtrl",
            backdrop: 'static',
            resolve: {
                roles: function () {
                    return angular.copy(vm.roles);
                },
                members: function () {
                    return vm.parentController.project.memberList;
                }
            },
            windowClass: "modal-confirm-center"
        });
    };

    vm.updateMember = function (member) {
        var addModalInstance = $modal.open({
            templateUrl: '/templates/ProjectMembers/Edit.html',
            controller: "EditProjectMembersController as editProjectMemberCtrl",
            backdrop: 'static',
            resolve: {
                roles: function () {
                    return angular.copy(vm.roles);
                },
                editMember: function () {
                    return angular.copy(member);
                }
            },
            windowClass: "modal-confirm-center"
        });
        addModalInstance.result.then(function (editMember) {
            member.member = editMember.member;
            member.roles = editMember.roles;
            member.roleText = vm.getRoleText(member.roles);
        });
    };

    vm.deleteMember = function (index, member) {
        var displayName = member.member.nickName + ' (' + member.member.fullName + ')';

        var deleteModalInstance = $modal.open({
            templateUrl: '/templates/Common/ModalConfirmContent.html',
            controller: "ModalConfirmController",
            resolve: {
                name: function () {
                    return displayName;
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
        deleteModalInstance.result.then(function () {
            vm.parentController.project.memberList.splice(index, 1);
        });
    };

    vm.getRoleText = function (roles) {
        var roleText = '';

        if (roles && roles.length > 0) {
            var roleNames = $.map(roles, function (item) { return item.name; });

            roleText = roleNames.join(', ');
        }

        return roleText;
    };

    vm.setParentController = function (parent) {
        vm.parentController = parent;

        vm.showMembersFilter = !vm.parentController.isAddMode;

        if (vm.showMembersFilter) {
            $scope.$watch(function () {
                return projectMilestoneService.getProperty();
            }, function (value) {
                vm.milestones = value;
            });
        }
    };

    vm.initialize = function () {
        projectMembersFactory.getProjectRole().then(function (roles) {
            vm.roles = roles;
        });
    };

    vm.initialize();
}]);

projectsSettingModule.controller('AddProjectMembersController',
['$scope', '$modalInstance', 'commonFactory', 'roles', 'members', 'projectDataFactory', 'notificationFactory', 'projectMemberUtility',
function ($scope, $modalInstance, commonFactory, roles, members, projectDataFactory, notificationFactory, projectMemberUtility) {
    var vm = this;

    vm.roles = angular.copy(roles);
    vm.members = members;
    vm.calendar = { opened: false };
    vm.newMember = {};
    vm.searchKey = '';
    vm.searchEmployees = [];
    vm.employee = '';

    vm.getNewMember = function () {
        return {
            member: {
                joinDate: new Date(),
                employeeId: '',
                nickName: '',
                fullName: ''
            },
            roles: []
        };
    };

    vm.ok = function (isContinue) {
        var selectedRoles = $.grep(vm.roles, function (role) { return role.selected; });
        vm.newMember.roles = selectedRoles;
        if (vm.employee) {
            vm.newMember.member.employeeId = vm.employee.employeeId;
            vm.newMember.member.nickName = vm.employee.nickName;
            vm.newMember.member.fullName = vm.employee.fullName;
        }
        var isValid = projectMemberUtility.validate(vm.newMember) && !projectMemberUtility.checkDuplicateMember(vm.members, vm.newMember);

        if (isValid) {
            vm.members.push(vm.newMember);

            if (isContinue) {
                vm.searchKey = '';
                vm.employee = '';
                vm.newMember = vm.getNewMember();
                vm.roles = angular.copy(roles);
            } else {
                $modalInstance.close();
            }
        }
    };

    vm.cancel = function () {
        $modalInstance.dismiss('cancel');
    };

    // show start date calendar when click button start date
    vm.open = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();
        vm.calendar.opened = true;
    };

    // Search member
    vm.getEmployee = function () {
        if (!vm.searchKey) {
            vm.newMember.member.employeeId = '';
            vm.employee = '';
        } else {
            projectDataFactory.getEmployeeSelectList(null, vm.searchKey).then(function (employees) {
                vm.searchEmployees = employees;
            });
        }
    };

    vm.initialize = function () {
        vm.newMember = vm.getNewMember();

        commonFactory.getDateFormat(vm);
    };

    vm.initialize();
}]);

projectsSettingModule.controller("EditProjectMembersController",
['$scope', '$modalInstance', 'roles', 'editMember', 'commonFactory', 'projectMemberUtility',
function ($scope, $modalInstance, roles, editMember, commonFactory, projectMemberUtility) {
    var vm = this;

    vm.roles = roles;
    vm.editMember = editMember;
    vm.calendar = { opened: false };
    vm.searchKey = editMember.member.nickName + " (" + editMember.member.fullName + ")";

    vm.ok = function () {
        var selectedRoles = $.grep(vm.roles, function (role) { return role.selected; });
        vm.editMember.roles = selectedRoles;
        var isValid = projectMemberUtility.validate(vm.editMember);

        if (isValid) {
            $modalInstance.close(vm.editMember);
        }
    };

    vm.cancel = function () {
        $modalInstance.dismiss('cancel');
    };

    vm.open = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();
        vm.calendar.opened = true;
    };

    vm.initialize = function () {
        commonFactory.getDateFormat(vm);

        var roleIds = $.map(editMember.roles, function (role) { return role.projectRoleId; });

        angular.forEach(vm.roles, function (role) {
            if (roleIds.indexOf(role.projectRoleId) !== -1) {
                role.selected = true;
            }
        });
    };

    vm.initialize();
}]);
