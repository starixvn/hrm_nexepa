/* global projectsSettingModule */
'use strict';

projectsSettingModule.controller('SyncRedmineProjectController',
['$modal', 'projectDataFactory', 'notificationFactory', 'ErrorService', 'commonFactory',
function ($modal, projectDataFactory, notificationFactory, ErrorService, commonFactory) {
    var vm = this;

    vm.projects = null;
    vm.parentProjects = [];
    vm.subProjects = [];
    vm.filter = {
        monthPeriod: 6,
        projectGroupId: '',
        projectIds: ''
    };
    vm.isLoadingGetProjectButton = false;
    vm.isLoadingSyncButton = false;

    vm.reload = function () {
        window.location.reload();
    };

    vm.getRedmineProjects = function (showLoading) {
        if (showLoading) {
            vm.isLoadingGetProjectButton = true;
        }

        vm.filter.projectIds = commonFactory.getOnlySelectedData(vm.subProjects, 'projectId');

        projectDataFactory.getRedmineProject(vm.filter).then(function (projects) {
            if (showLoading) {
                vm.isLoadingGetProjectButton = false;
            }

            vm.projects = projects;
        }, function () {
            if (showLoading) {
                vm.isLoadingGetProjectButton = false;
            }

            notificationFactory.error("ErrorMessage");
        });
    };

    vm.syncProjects = function (selectedProjects) {
        if (selectedProjects.length > 0) {
            projectDataFactory.syncProjectsFromRedmine(selectedProjects).success(function (response) {
                vm.isLoadingSyncButton = false;

                if (response.isSuccessful === true) {
                    notificationFactory.success("SyncProjectSuccess");

                    vm.getRedmineProjects(false);
                }
                else {
                    notificationFactory.error("SyncProjectFailed", response.message);
                }
            }).error(function (response, status) {
                vm.isLoadingSyncButton = false;

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

    vm.getSubRedmineProjects = function () {
        projectDataFactory.getSubRedmineProjects(vm.filter.projectGroupId).success(function (subProjects) {
            vm.subProjects = subProjects;
        }).error(function (response, status) {
            if (status === 405) {
                ErrorService.errorHandle(status);
            }
            else {
                notificationFactory.error("ErrorMessage");
            }
        });
    };

    vm.viewProjectMembers = function (projectId) {
        var modalInstance = $modal.open({
            templateUrl: '/templates/Projects/ViewProjectMember.html',
            controller: "ViewProjectMemberController as viewProjectMemberCtrl",
            backdrop: 'static',
            resolve: {
                projectId: function () {
                    return projectId;
                }
            },
            windowClass: "view-project-member"
        });
    };

    vm.initialize = function () {
        vm.getTopParentRedmineProjects();

        vm.getSubRedmineProjects();
    };

    vm.initialize();
}]);