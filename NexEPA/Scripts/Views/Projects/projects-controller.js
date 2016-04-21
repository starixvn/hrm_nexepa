/* global projectsSettingModule */
'use strict';

projectsSettingModule.controller('ProjectController',
['$scope', '$location', 'projectDataFactory', 'ProjectSettingCache', 'ErrorService', 'tabService',
'notificationFactory', '$modal', 'linkToEPAService', 'commonFactory', 'employeeEPAFactory',
function ($scope, $location, projectDataFactory, ProjectSettingCache, ErrorService, tabService,
notificationFactory, $modal, linkToEPAService, commonFactory, employeeEPAFactory) {
    /**
    * Scope Variable. 
    */
    $scope.createProjectPermission = false;
    $scope.entrySelected = 20;
    /**
    * Project Setting Cache. 
    */
    var cacheListPM = ProjectSettingCache.get("CacheListPM");
    var cacheListStatus = ProjectSettingCache.get("CacheListStatus");
    var cacheListPeriod = ProjectSettingCache.get("CacheListPeriod");
    /**
    * Ajax event Loaded. 
    */
    // Get data to ProjectManager - Select List
    var successGetEmployeeSelectList = function (data) {
        $scope.pms = data;
        ProjectSettingCache.put("CacheListPM", data);
    };

    // Get data to Status - Select List
    var successGetProjectStatusSelectList = function (data) {
        $scope.status = data;
        ProjectSettingCache.put("CacheListStatus", data);
    };

    // Get data to Period - Select List
    var successGetPeriodSelectList = function (data) {
        $scope.periods = data;
        ProjectSettingCache.put("CacheListPeriod", data);
    };

    // Filter data by criteria.
    var successFilterData = function (data) {
        $scope.projects = data;
        commonFactory.tablePagination($scope, data, $scope.entrySelected);
        $scope.totalEntries = data.length;
    };

    var errorHandle = function (status) {
        ErrorService.errorHandle(status);
    };

    var successGetPermission = function (data) {
        $scope.permission = {
            policy: parseInt(data)
        };
    };

    var errorNotification = function (response) {
        if (response !== null || response !== "") {
            notificationFactory.error('', response);
        }
        else {
            notificationFactory.error("ErrorMessage");
        }
    };

    /**
    * Initialize data to view Index.
    */
    linkToEPAService.setProperty(null);
    tabService.setProperty(null);
    projectDataFactory.getPermission().then(successGetPermission, errorNotification);
    //projectDataFactory.filterData(null).then(successFilterData, errorHandle);
    commonFactory.numberEntryList($scope);

    // Check for list ProjectManager has cache data.
    if (cacheListPM) {
        $scope.pms = cacheListPM;
    }
    else {
        projectDataFactory.getEmployeeSelectList(3).then(successGetEmployeeSelectList);
    }

    // Check for list Status has cache data.
    if (cacheListStatus) {
        $scope.status = cacheListStatus;
    }
    else {
        projectDataFactory.getProjectStatusSelectList().then(successGetProjectStatusSelectList);
    }

    // Check for list Periods has cache data.
    if (cacheListPeriod) {
        $scope.periods = cacheListPeriod;
    }
    else {
        projectDataFactory.getProjectPeriodSelectList().then(successGetPeriodSelectList);
    }

    employeeEPAFactory.getCurrentUser().then(function (currentUser) {
        $scope.currentUser = currentUser;
        //check project manager
        var checkPM = false;
        var roles = currentUser.roles;
        $.each(roles, function (index, value) {
            if (value.name === 'PM') {
                checkPM = true;
            }
        });
        if (checkPM === true) {
            $scope.filter = {
                projectManager: currentUser.userId
            };
            projectDataFactory.filterData($scope.filter).then(successFilterData, errorHandle);
        }
        else {
            projectDataFactory.filterData(null).then(successFilterData, errorHandle);
        }
    }, function () {
        notificationFactory.error("ErrorMessage");
    });

    /**
    * Event: Filter 
    */
    $scope.getProjectList = function (filter) {
        projectDataFactory.filterData(filter).then(successFilterData);
    };

    // View detail of project
    $scope.detailProject = function (project, isMemberSelected) {
        tabService.setProperty(isMemberSelected);
    };

    $scope.updateProjectStatus = function (project, status) {
        var projectStatus = {
            projectId: project.projectId,
            status: status.id
        };

        var updateModalInstance = $modal.open({
            templateUrl: '/templates/Common/ModalConfirmContent.html',
            controller: "ModalConfirmController",
            resolve: {
                name: function () {
                    return project.name;
                },
                action: function () {
                    return "change status";
                },
                extraInfo: function () {
                    return '';
                }
            },
            windowClass: "modal-confirm-center"
        });

        updateModalInstance.result.then(function () {
            projectDataFactory.updateProjectStatus(projectStatus).then(function () {
                project.status = status.id;
                notificationFactory.success('DataChangeSuccessfully', '');
            }, errorNotification);
        });
    };

    // Delete Milestone.
    $scope.deleteProject = function (project) {
        var deleteModalInstance = $modal.open({
            templateUrl: '/templates/Common/ModalConfirmContent.html',
            controller: "ModalConfirmController",
            resolve: {
                name: function () {
                    return project.name;
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
            projectDataFactory.deleteProject(project.projectId).then(function () {
                var index = $scope.projects.indexOf(project);
                $scope.projects.splice(index, 1);
                commonFactory.tablePagination($scope, $scope.projects, $scope.entrySelected);
                notificationFactory.success("DataChangeSuccessfully");
            }, errorNotification);
        });
    };

    $scope.toggleDropdown = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope.status.isopen = !$scope.status.isopen;
    };

    $scope.$watch("currentUser", function (newValue) {
        if (newValue) {
            var roles = newValue.roles;

            $.each(roles, function (index, value) {
                if (value.name === 'SA') {
                    $scope.showAllMemberEPA = true;
                }
            });
        }
    });

    $scope.linkToEPA = function (startDate, endDate, projectId, status) {
        var objectQuery = { startDate: startDate, endDate: endDate, projectId: projectId, status: status, isShowAll: $scope.showAllMemberEPA };
        linkToEPAService.setProperty(objectQuery);
    };

    $scope.showEntries = function () {
        commonFactory.tablePagination($scope, $scope.projects, $scope.entrySelected);
    };
}]);

projectsSettingModule.controller('MilestoneController',
['$scope', '$routeParams', '$modal', 'projectMilestoneService', 'projectDataFactory',
'milestoneFactory', 'notificationFactory', 'commonFactory', 'ProjectSettingCache',
function ($scope, $routeParams, $modal, projectMilestoneService, projectDataFactory, milestoneFactory, notificationFactory,
commonFactory, ProjectSettingCache) {
    /**
    * Scope variables. 
    */
    var projectId = $routeParams.projectId;
    $scope.isEditMode = $routeParams.isEditMode ? true : false;
    $scope.milestones = [];
    $scope.milestoneErrors = [];
    $scope.addMode = false;
    var cacheListStatus = ProjectSettingCache.get("CacheListStatus");

    /**
    * Ajax event. 
    */
    // Notify error popup when action is failure.
    var errorNotification = function () {
        notificationFactory.error('ErrorMessage');
    };

    // Notify success popup when action is succeed.
    var successNotification = function () {
        notificationFactory.success('DataChangeSuccessfully', '');
        return milestoneFactory.getMilestone(projectId).then(successGetMilestones, errorNotification);
    };

    // Get Milestone
    var successGetMilestones = function (data) {
        $scope.milestones = data;
        projectMilestoneService.setProperty(data);
        $scope.milestoneErrors = {};
    };

    // Callback Success for Post data.
    var successAddMilestone = function (data, status, headers, config) {
        successNotification(data, status, headers, config).then(function () {
            $scope.toggleAddMode();
            $scope.milestone = {};
        });
    };

    var errorAddUpdateMilestone = function (data) {
        $scope.milestoneErrors = data;
    };

    var successDeleteMilestone = function () {
        notificationFactory.success('DataChangeSuccessfully', '');
        return milestoneFactory.getMilestone(projectId).then(successGetMilestones, errorNotification);
    };

    // Get data to Status - Select List
    var successGetMilestoneStatusSelectList = function (data) {
        $scope.milestoneStatus = data;
        ProjectSettingCache.put("CacheListStatus", data);
    };

    /**
    * Switch mode Add/Update/Normal
    */
    $scope.toggleAddMode = function () {
        $scope.addMode = !$scope.addMode;
        if ($scope.addMode === false) {
            $scope.milestoneErrors = {};
        }
    };

    $scope.toggleEditMode = function (milestone) {
        milestone.editMode = !milestone.editMode;
        if (milestone.editMode === false) {
            milestoneFactory.getMilestone(projectId).then(successGetMilestones, errorNotification);
        }
    };

    /**
    * Initialize data to view Project Milestone 
    */
    milestoneFactory.getMilestone(projectId).then(successGetMilestones, errorNotification);

    // Check for list Status has cache data.
    if (cacheListStatus) {
        $scope.milestoneStatus = cacheListStatus;
    }
    else {
        projectDataFactory.getProjectStatusSelectList().then(successGetMilestoneStatusSelectList);
    }

    // get Date Format
    commonFactory.getDateFormat($scope);
    /**
    * Events 
    */

    // show start date calendar when click button start date in Add Mode
    $scope.openPopupAddModeStartDate = function ($event) {
        commonFactory.openPopupAddModeStartDate($event, $scope);
    };

    // show end date calendar when click button end date in Add Mode
    $scope.openPopupAddModeEndDate = function ($event) {
        commonFactory.openPopupAddModeEndDate($event, $scope);
    };

    // show start date calendar when click button start date in Edit Mode
    $scope.openPopupEditModeStartDate = function ($event, milestoneId) {
        commonFactory.openPopupEditModeStartDate($event, $scope, milestoneId);
    };

    // show end date calendar when click button end date in Edit Mode.
    $scope.openPopupEditModeEndDate = function ($event, milestoneId) {
        commonFactory.openPopupEditModeEndDate($event, $scope, milestoneId);
    };

    // Add Milestone.
    $scope.addMilestone = function () {
        if ($scope.milestone == null) {
            var errorMessage = "Please fill out all required (*) fields.";
            $scope.milestoneErrors = errorMessage.split(',');
        }
        else {
            var milestone = $scope.milestone;
            if (milestone.startDate != null) {
                var selectedStartDate = new Date(milestone.startDate);
                milestone.startDate = selectedStartDate ? selectedStartDate.toDateString() : null;
            }
            if (milestone.endDate != null) {
                var selectedEndDate = new Date(milestone.endDate);
                milestone.endDate = selectedEndDate ? selectedEndDate.toDateString() : null;
            }
            milestone.projectId = projectId;
            milestoneFactory.addMilestone(milestone).then(successAddMilestone, errorAddUpdateMilestone);
        }
    };
    // Update Milestone.
    $scope.updateMilestone = function (milestone) {
        milestoneFactory.updateMilestone(milestone).then(successNotification, errorAddUpdateMilestone);
    };
    // Delete Milestone.
    $scope.deleteMilestone = function (milestone) {
        var deleteModalInstance = $modal.open({
            templateUrl: '/templates/Common/ModalConfirmContent.html',
            controller: "ModalConfirmController",
            resolve: {
                name: function () {
                    return milestone.name;
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
            milestoneFactory.deleteMilestone(milestone.milestoneId).then(successDeleteMilestone, errorNotification);
        });
    };
}]);

projectsSettingModule.controller('ProjectManagementController', [function () {
    var vm = this;

    vm.membersProject = [];
}]);