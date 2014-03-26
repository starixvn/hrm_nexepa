/* global projectsSettingModule */
'use strict';

projectsSettingModule.controller("ProjectMembersController", function ($scope, $routeParams, projectMembersFactory) {
    var projectId = $routeParams.projectId;
    $scope.initViewIndex = function () {
        projectMembersFactory.getMember(projectId, null, null).then(function (lstMember) {
            $scope.members = lstMember;
        });
    };

    $scope.initViewAdd = function () {
        projectMembersFactory.getProjectRole().then(function (lstProjectRole) {
            $scope.roles = lstProjectRole;
        });
    };
});