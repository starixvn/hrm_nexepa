/* global projectsSettingModule */
'use strict';

projectsSettingModule.controller('SyncRedmineProjectController',
['projectDataFactory', 'notificationFactory',
function (projectDataFactory, notificationFactory) {
    var vm = this;

    vm.projects = null;

    vm.filter = {
        monthPeriod: 3
    };
    vm.isLoadingButton = false;

    vm.reload = function () {
        window.location.reload();
    };

    vm.getRedmineProjects = function () {
        projectDataFactory.getRedmineProject(vm.filter).then(function (projects) {
            vm.projects = projects;
        }, function () {
            notificationFactory.error("ErrorMessage");
        });
    };

    vm.initialize = function () {
        
    };

    vm.initialize();
}]);