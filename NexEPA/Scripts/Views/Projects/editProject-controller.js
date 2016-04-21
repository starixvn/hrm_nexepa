/* global projectsSettingModule */
'use strict';

projectsSettingModule.controller('EditProjectController',
['$scope', '$routeParams', '$location', '$window', '$modal', 'projectUtility', 'projectDataFactory', 'ProjectSettingCache',
'commonFactory', 'tabService', 'notificationFactory', 'ErrorService', 'projectPermissionService', 'projectMembersFactory',
function ($scope, $routeParams, $location, $window, $modal, projectUtility, projectDataFactory,
ProjectSettingCache, commonFactory, tabService, notificationFactory, ErrorService, projectPermissionService, projectMembersFactory) {
    var vm = this;

    vm.projectId = parseInt($routeParams.projectId);
    vm.isEditMode = $routeParams.isEditMode ? true : false;
    vm.cacheListPM = ProjectSettingCache.get("CacheListPM");
    vm.cacheListStatus = ProjectSettingCache.get("CacheListStatus");

    vm.dataTab = {};
    vm.formReadOnly = true;
    vm.pms = [];
    vm.searchProjects = [];
    vm.projectUpdate = {};
    vm.calendar = {
        endDateOpened: false,
        startDateOpened: false
    };
    vm.searchKey = '';

    vm.openPopupStartDate = function ($event) {
        commonFactory.openPopupStartDate($event, $scope);
    };

    vm.openPopupEndDate = function ($event) {
        commonFactory.openPopupEndDate($event, $scope);
    };

    vm.getProjects = function () {
        if (!vm.searchKey) {
            vm.projectUpdate.redmineProjectId = '';
        } else {
            projectDataFactory.getAllProject(vm.searchKey)
                .success(function (projects) {
                    vm.searchProjects = projects;
                })
                .error(function () {
                    notificationFactory.error("ErrorMessage");
                });
        }
    };

    vm.getPMByProject = function (redmineProjectId, isResetPM) {
        projectDataFactory.getPMByProject(redmineProjectId)
            .success(function (pms) {
                vm.pms = pms;

                if (isResetPM && vm.pms && vm.pms.length > 0) {
                    vm.projectUpdate.projectManager = vm.pms[0].employeeId;
                }
            })
            .error(function () {
                notificationFactory.error("ErrorMessage");
            });
    };

    vm.getGroupedMembersByProject = function (projectId) {
        projectMembersFactory.getGroupedMembersByProject(projectId).then(function (members) {
            vm.projectUpdate.memberList = members;
        });
    }

    vm.tabInfoSelect = function () {
        tabService.setProperty(false);
    };

    vm.tabMemberSelect = function () {
        tabService.setProperty(true);
    };

    vm.update = function () {
        var isValid = projectUtility.validateProject(vm.projectUpdate);

        if (isValid) {
            projectDataFactory.updateProject(vm.projectUpdate).then(function (response) {
                if (response.isSuccessful === true) {
                    notificationFactory.success("DataChangeSuccessfully");
                }
                else {
                    notificationFactory.error(null, response.message);
                }
            }, function (response) {
                if (status === 405) {
                    ErrorService.errorHandle(status);
                }
                else {
                    notificationFactory.error("DataChangeFail");
                }
            });
        }
    };

    vm.cancel = function () {
        $window.history.back();
    };

    vm.getProjectManager = function () {
        projectDataFactory.getEmployeeSelectList(3).then(function (data) {
            vm.pms = data;
        });
    };

    vm.getStatus = function () {
        if (vm.cacheListStatus) {
            vm.status = vm.cacheListStatus;
        }
        else {
            projectDataFactory.getProjectStatusSelectList().then(function (data) {
                vm.status = data;
                ProjectSettingCache.put("CacheListStatus", data);
            });
        }
    };

    vm.initialize = function () {
        commonFactory.getDateFormat(vm);

        vm.getStatus();

        projectDataFactory.getProjectById(vm.projectId).then(function (data) {
            if (data.permission === 2) {
                vm.formReadOnly = false;
            }

            vm.projectUpdate = data;
            vm.searchKey = vm.projectUpdate.redmineProjectName;
            projectPermissionService.setProperty(data.permission);

            vm.getPMByProject(vm.projectUpdate.redmineProjectId);
        }, function (status) {
            ErrorService.errorHandle(status);
        });

        if (tabService.getProperty() === true) {
            vm.dataTab.member = true;
        }
        else {
            vm.dataTab.information = true;
        }
    };

    vm.initialize();
}]);