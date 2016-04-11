/* global projectsSettingModule */
'use strict';

projectsSettingModule.controller('ViewProjectMemberController',
['$modalInstance', 'projectId',
function ($modalInstance, projectId) {
    var vm = this;

    vm.projectId = projectId;

    vm.cancel = function () {
        $modalInstance.dismiss("cancel");
    };
}]);
