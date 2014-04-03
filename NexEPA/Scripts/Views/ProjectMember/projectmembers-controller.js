/* global projectsSettingModule */
'use strict';

var memberValidation = function (selected) {
    var error = $("#memberValid");
    if (selected.length > 0) {
        error.hide();
        return true;
    }
    error.show();
    return false;
};

projectsSettingModule.controller("ProjectMembersController",
function ($scope, $routeParams, $location, projectService, projectMembersFactory, notificationFactory, projectDataFactory) {
    /**
    * Scope variable.
    */
    $("#memberValid").hide();
    var projectIdInUrl = $routeParams.projectId;
    $scope.currentProject = [];

    /**
    * Ajax event loaded. 
    */
    var successGetMembers = function (data) { $scope.members = data; };
    var successGetMilestones = function (data) { $scope.milestones = data; };
    // Get project by Id
    var successGetProjectById = function (data) {
        projectService.setProperty(data);
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

    /**
    * Events.
    */
    $scope.filterMember = function (isShowDeletedMember, milestone) {
        projectMembersFactory.getMember(projectIdInUrl, isShowDeletedMember, milestone).then(successGetMembers);
    };

    $scope.deleteMember = function (member) {
        member.projectId = projectIdInUrl;
        projectMembersFactory.deleteMember(member).then(successDeleteMember, errorNotification);
    };
});

projectsSettingModule.controller("AddProjectMembersController",
function ($scope, $location, $window, projectService, projectMembersFactory, projectDataFactory, ProjectSettingCache) {
    /**
    * Scope variable.
    */
    $scope.currentProject = projectService.getProperty();
    var projectId = $scope.currentProject.projectId;
    var cacheListEmployee = ProjectSettingCache.get("CacheListEmployee");
    $scope.roles = [];
    $scope.selection = [];

    /**
    * Ajax event loaded. 
    */
    var successGetProjectRoles = function (data) { $scope.roles = data; };

    var successGetEmployeeSelectList = function (data) {
        $scope.members = data;
        ProjectSettingCache.put("CacheListEmployee", data);
    };

    var successAddProjectMember = function () { $location.url('/ProjectMembers/View/' + projectId); };
    var errorAddProjectMember = function (response) { $scope.errors = response; };

    /**
    * Initialize data to view Add Member.
    */
    // Check for list Employee has cache data.
    if (cacheListEmployee) {
        $scope.members = cacheListEmployee;
    }
    else {
        projectDataFactory.getEmployeeSelectList(null).then(successGetEmployeeSelectList);
    }

    projectMembersFactory.getProjectRole().then(successGetProjectRoles);

    /**
    * Events.
    */
    $scope.addMember = function (employeeId) {
        var selected = $scope.selection;
        var object = {};
        if (memberValidation(selected)) {
            var projectRolesIds = selected.join(',');
            $scope.errors = [];
            object.projectId = projectId;
            object.projectRoleIds = projectRolesIds;
            object.employeeId = employeeId;
            projectMembersFactory.addMember(object).then(successAddProjectMember, errorAddProjectMember);
        }
    };

    // Watch for checkboxlist change.
    $scope.$watch('roles|filter:{selected:true}', function (selectedItems) {
        $scope.selection = selectedItems.map(function (item) {
            return item.projectRoleId;

        });
    }, true);

    $scope.cancel = function () {
        $window.history.back();
    };
});

projectsSettingModule.controller("EditProjectMembersController",
function ($scope, $routeParams, projectMembersFactory, $location, $window, projectDataFactory, projectService, ProjectSettingCache) {
    /**
    * Scope variable.
    */
    $scope.currentProject = projectService.getProperty();
    var projectId = $scope.currentProject.projectId;
    var employeeId = $routeParams.employeeId;
    var cacheListEmployee = ProjectSettingCache.get("CacheListEmployee");
    var lstRoles = [];
    $scope.errors = [];
    $scope.roles = [];
    $scope.selection = [];
    $scope.editMember = [];

    /**
    * Ajax event loaded. 
    */
    var successGetProjectRoles = function (data) { $scope.roles = data; };

    var successGetEmployeeSelectList = function (data) {
        $scope.members = data;
        ProjectSettingCache.put("CacheListEmployee", data);
    };

    var successUpdateMember = function () { $location.url('/ProjectMembers/View/' + projectId); };
    var errorUpdateMember = function (response) { $scope.errors = response; };

    var successGetRolesOfMember = function (data) {
        if (data.length > 0) {
            $scope.editMember = data[0];
            $.each(data, function (key) {
                var object = {};
                object.projectRoleId = data[key].projectRoleId;
                object.name = data[key].name;
                lstRoles.push(object);
            });
            $.each($scope.roles, function (key) {
                $.each(lstRoles, function (k) {
                    if (lstRoles[k].projectRoleId === $scope.roles[key].projectRoleId) {
                        $scope.roles[key].selected = true;
                    }
                });
            });
        }
        else {
            $scope.editMember.employeeId = parseInt(employeeId);
        }
    };

    /**
    * Initialize data to view Edit Member.
    */
    // Check for list Employee has cache data.
    if (cacheListEmployee) {
        $scope.members = cacheListEmployee;
    }
    else {
        projectDataFactory.getEmployeeSelectList(null).then(successGetEmployeeSelectList);
    }

    projectMembersFactory.getProjectRole().then(successGetProjectRoles);
    projectMembersFactory.getMemberRoles(projectId, employeeId).then(successGetRolesOfMember);

    /**
    * Events.
    */
    // Watch for checkboxlist change.
    $scope.$watch('roles|filter:{selected:true}', function (selectedItems) {
        $scope.selection = selectedItems.map(function (item) {
            return item.projectRoleId;
        });
    }, true);

    // Update Member.
    $scope.updateMember = function (employeeId) {
        var selected = $scope.selection;
        var object = {};
        if (memberValidation(selected)) {
            var projectRolesIds = selected.join(',');
            object.projectId = projectId;
            object.projectRoleIds = projectRolesIds;
            object.employeeId = employeeId;
            projectMembersFactory.addMember(object).then(successUpdateMember, errorUpdateMember);
        }
    };

    // Cancel
    $scope.cancel = function () {
        $window.history.back();
    };
});