/* global projectsSettingModule */
'use strict';

projectsSettingModule.controller('ViewProjectMemberController',
['$modalInstance', 'projectId', 'projectName', 'projectMembersFactory', 'notificationFactory',
function ($modalInstance, projectId, projectName, projectMembersFactory, notificationFactory) {
    var vm = this;

    vm.projectId = projectId;
    vm.projectName = projectName;
    vm.projectMembers = [];

    vm.getProjectMembers = function () {
        projectMembersFactory.getGroupedMembersByProject(vm.projectId).then(function (projectMembers) {
            vm.projectMembers = projectMembers;
        }, function () {
            notificationFactory.error("ErrorMessage");
        });
    };

    vm.cancel = function () {
        $modalInstance.dismiss("cancel");
    };

    vm.initialize = function () {
        vm.getProjectMembers();
    };

    vm.initialize();
}]);
