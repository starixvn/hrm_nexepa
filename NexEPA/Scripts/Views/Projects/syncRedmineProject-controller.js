/* global projectsSettingModule */
'use strict';

projectsSettingModule.controller('SyncRedmineProjectController',
['projectDataFactory', 'notificationFactory', 'ErrorService',
function (projectDataFactory, notificationFactory, ErrorService) {
    var vm = this;

    vm.projects = null;

    vm.filter = {
        monthPeriod: 3
    };
    vm.isLoadingButton = false;
    vm.parentProjects = [];

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

    vm.syncProjects = function (selectedProjects) {
        if (selectedProjects.length > 0) {
            projectDataFactory.syncProjectsFromRedmine(selectedProjects).success(function (response) {
                if (response.isSuccessful === true) {
                    notificationFactory.success("SyncProjectSuccess");

                    vm.getRedmineProjects();
                }
                else {
                    notificationFactory.error("SyncProjectFailed", response.message);
                }
            }).error(function (response, status) {
                if (status === 405) {
                    ErrorService.errorHandle(status);
                }
                else {
                    notificationFactory.error("SyncProjectFailed");
                }
            });
        }
    };

    vm.getTopParentRedmineProjects = function () {
        projectDataFactory.getTopParentRedmineProjects().success(function (parentProjects) {
            vm.parentProjects = parentProjects;
        }).error(function (response, status) {
            if (status === 405) {
                ErrorService.errorHandle(status);
            }
            else {
                notificationFactory.error("ErrorMessage");
            }
        });
    };

    vm.initialize = function () {
        vm.getTopParentRedmineProjects();
    };

    vm.initialize();
}]);