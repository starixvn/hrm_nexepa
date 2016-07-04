/* global projectsSettingModule, moment */
'use strict';

projectsSettingModule.controller('CreateProjectController',
['$scope', '$routeParams', '$window', 'projectUtility', 'projectDataFactory', 'ProjectSettingCache',
'commonFactory', 'tabService', 'notificationFactory', 'ErrorService', 'projectMembersFactory',
function ($scope, $routeParams, $window, projectUtility, projectDataFactory, ProjectSettingCache,
commonFactory, tabService, notificationFactory, ErrorService, projectMembersFactory) {
    var vm = this;

    vm.cacheListPM = ProjectSettingCache.get("CacheListPM");
    vm.cacheListStatus = ProjectSettingCache.get("CacheListStatus");

    vm.dataTab = {
        information: true,
        member: false
    };
    vm.formReadOnly = true;
    vm.pms = [];
    vm.searchProjects = [];
    vm.project = {
        projectId: '',
        projectManager: '',
        redmineProjectId: '',
        redmineProjectName: '',
        startDate: '',
        endDate: '',
        memberList: [],
        description: '',
        name: '',
        status: 1
    };
    vm.calendar = {
        endDateOpened: false,
        startDateOpened: false
    };
    vm.searchKey = '';
    vm.isAddMode = true;


    vm.openPopupStartDate = function ($event) {
        commonFactory.openPopupStartDate($event, vm);
    };

    vm.openPopupEndDate = function ($event) {
        commonFactory.openPopupEndDate($event, vm);
    };

    vm.getProjects = function () {
        if (!vm.searchKey) {
            vm.project.redmineProjectId = '';
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
                    vm.project.projectManager = vm.pms[0].employeeId;
                }
            })
            .error(function () {
                notificationFactory.error("ErrorMessage");
            });
    };

    vm.getGroupedMembersByProject = function (projectId) {
        projectMembersFactory.getGroupedMembersByProject(projectId).then(function (members) {
            vm.project.memberList = members;
        });
    };

    vm.add = function () {
        var isValid = projectUtility.validateProject(vm.project);

        if (isValid) {
            if (vm.project.startDate) {
                vm.project.startDate = moment(vm.project.startDate).format('DD MMM, YYYY');
            }
            if (vm.project.endDate) {
                vm.project.endDate = moment(vm.project.endDate).format('DD MMM, YYYY');
            }

            projectDataFactory.addProject(vm.project).then(function (response) {
                if (response.isSuccessful === true) {
                    notificationFactory.success("DataChangeSuccessfully");
                    location.href = "/Projects/Index";
                }
                else {
                    notificationFactory.error(null, response.message);
                }
            }, function () {
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

        vm.getProjectManager();

        projectDataFactory.getPermission().then(function (data) {
            if (data === "2") {
                vm.formReadOnly = false;
            }
        }, function (status) {
            ErrorService.errorHandle(status);
        });
    };

    vm.initialize();
}]);