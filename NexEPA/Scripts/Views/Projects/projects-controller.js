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

projectsSettingModule.controller('CreateProjectController',
['$scope', '$location', '$window', 'projectDataFactory', 'projectMembersFactory', 'ProjectSettingCache', 'commonFactory', '$timeout',
'notificationFactory', 'projectUtility', 'sharedProperties',
function ($scope, $location, $window, projectDataFactory, projectMembersFactory, ProjectSettingCache, commonFactory, $timeout,
notificationFactory, projectUtility, sharedProperties) {
    /**
	* Variables. 
	*/
    var cacheListPM = ProjectSettingCache.get("CacheListPM");
    var cacheListStatus = ProjectSettingCache.get("CacheListStatus");
    $scope.errors = [];
    $scope.search = {
        searchKey: ''
    };
    $("#projectValid").hide();

    $scope.project = {
        name: '',
        description: '12312'
    };

    /**
	* Ajax event. 
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

    // Success add new project callback.
    var successAddProject = function () {
        $scope.disabledSubmit = false;
        notificationFactory.success('DataChangeSuccessfully', '');
        $timeout(function () {
            $location.url('/Projects/Index');
        }, 2000);
    };

    // Errors occur during Add process.
    var errorAddProject = function () {
        $scope.disabledSubmit = false;
    };

    //Get projects - search in auto
    $scope.getProjects = function (searchKey) {
        projectDataFactory.getAllProject(searchKey)
            .success(function (projects) {
                $scope.searchProjects = projects;
            })
            .error(function () {
                notificationFactory.error("ErrorMessage");
            });
    };

    $scope.getPMByProject = function (projectId) {
        projectDataFactory.getPMByProject(projectId)
            .success(function (pms) {
                $scope.pms = pms;
            })
            .error(function () {
                notificationFactory.error("ErrorMessage");
            });
    };

    $scope.getProjectRedmine = function (projectId) {
        projectDataFactory.getProjectRedmineById(projectId)
            .success(function (project) {
                //$scope.projectRedmine = project;
                //$('#txtDescription').val(project.description);
                //$('#startDate').val($.format.date(project.createdOn, "dd MMM, yyyy"));
                $scope.project = project;
            })
            .error(function () {
                notificationFactory.error("ErrorMessage");
            });
    }

    $scope.$watch('project.description', function (value) {
        if(value.lenght > 0)
        $scope.$apply();
    });

    $scope.getMembersByProject = function (projectId) {
        projectMembersFactory.getMembersByProject(projectId).then(function (data) {
            sharedProperties.setMembersProject(data);
        });
    };

    /**
    * Initialize data to view Create Project. 
    */
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

    // get Date Format
    commonFactory.getDateFormat($scope);

    /**
    * Events 
    */
    projectUtility.showMessageAfterValidate($scope);

    // Save
    $scope.save = function (project) {
        $scope.disabledSubmit = true;
        $scope.submitted = true;

        var newProject = angular.copy(project);
        newProject.memberList = sharedProperties.getMembersProject();

        if (newProject) {
            if (newProject.startDate != null) {
                var selectedStartDate = new Date(newProject.startDate);
                newProject.startDate = selectedStartDate ? selectedStartDate.toDateString() : null;
            }
            if (newProject.endDate != null) {
                var selectedEndDate = new Date(newProject.endDate);
                newProject.endDate = selectedEndDate ? selectedEndDate.toDateString() : null;
            }

            if (projectUtility.validateProject(newProject)) {
                projectDataFactory.addProject(newProject).then(successAddProject, errorAddProject);
            }
            else {
                $scope.disabledSubmit = false;
            }
        }
        else {
            $scope.disabledSubmit = false;
        }
    };

    // Cancel
    $scope.cancel = function () { $window.history.back(); };

    // show start date calendar when click button start date
    $scope.openPopupStartDate = function ($event) {
        commonFactory.openPopupStartDate($event, $scope);
    };

    // show end date calendar when click button end date
    $scope.openPopupEndDate = function ($event) {
        commonFactory.openPopupEndDate($event, $scope);
    };

}]);

projectsSettingModule.controller('ViewDetailAndEditProjectController',
['$scope', '$routeParams', '$location', '$window', '$modal', 'projectDataFactory', 'ProjectSettingCache',
'commonFactory', 'tabService', 'notificationFactory', 'ErrorService', 'projectPermissionService', 'projectMembersFactory', 'sharedProperties',
function ($scope, $routeParams, $location, $window, $modal, projectDataFactory,
ProjectSettingCache, commonFactory, tabService, notificationFactory, ErrorService, projectPermissionService, projectMembersFactory, sharedProperties) {
    /**
    * Scope variables. 
    */
    var projectId = parseInt($routeParams.projectId);
    $scope.isEditMode = $routeParams.isEditMode ? true : false;
    $scope.errors = [];
    $scope.dataTab = {};
    var cacheListPM = ProjectSettingCache.get("CacheListPM");
    var cacheListStatus = ProjectSettingCache.get("CacheListStatus");
    $scope.formReadOnly = true;
    var viewDetailMode = false;

    /**
    * Ajax event. 
    */
    // Get data to ProjectManager - Select List
    var successGetEmployeeSelectList = function (data) { $scope.pms = data; };

    // Get data to Status - Select List
    var successGetProjectStatusSelectList = function (data) {
        $scope.status = data;
        ProjectSettingCache.put("CacheListStatus", data);
    };

    // Get project by Id
    var successGetProjectById = function (data) {

        if (window.location.href.indexOf("Detail") > -1) {
            viewDetailMode = true;
        }
        if (data.permission === 2 && !viewDetailMode) {
            $scope.formReadOnly = false;
        }
        $scope.projectUpdate = data;
        projectPermissionService.setProperty(data.permission);
    };

    // Update Project success callback.
    var successUpdateProject = function () { notificationFactory.success('DataChangeSuccessfully', ''); };

    // Update Project failure callback.
    var errorUpdateProject = function (response) {
        $scope.errors = response;
    };

    var errorHandle = function (status) {
        ErrorService.errorHandle(status);
    };

    //Get projects
    $scope.getProjects = function (searchKey) {
        projectDataFactory.getAllProject(searchKey)
            .success(function (projects) {
                $scope.searchProjects = projects;
            })
            .error(function () {
                notificationFactory.error("ErrorMessage");
            });
    };

    $scope.getProjectRedmine = function (projectId) {
        projectDataFactory.getProjectRedmineById(projectId)
            .success(function (project) {
                $scope.project = project;
            })
            .error(function () {
                notificationFactory.error("ErrorMessage");
            });
    }

    $scope.getPMByProject = function (projectId) {
        projectDataFactory.getPMByProject(projectId)
            .success(function (pms) {
                $scope.pms = pms;
            })
            .error(function () {
                notificationFactory.error("ErrorMessage");
            });
    };

    $scope.getMembersByProject = function (projectId) {
        projectMembersFactory.getMembersByProject(projectId).then(function (data) {
            sharedProperties.setMembersProject(data);
        });
    }

    /**
    * Initialize data to view Edit Project. 
    */
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

    // Get Selected Tab in Edit Project Information Page.
    if (tabService.getProperty() === true) {
        $scope.dataTab.member = true;
    }
    else {
        $scope.dataTab.information = true;
    }

    projectDataFactory.getProjectById(projectId).then(successGetProjectById, errorHandle);

    // get Date Format
    commonFactory.getDateFormat($scope);

    var successGetPermission = function (data) {
        $scope.permissionProject = {
            policy: parseInt(data)
        };
    };

    projectDataFactory.getPermission().then(successGetPermission, errorHandle);

    /**
    * Events 
    */
    // Tab Select
    $scope.tabInfoSelect = function () {
        tabService.setProperty(false);
    };

    $scope.tabMemberSelect = function () {
        tabService.setProperty(true);
    };

    // Update
    $scope.update = function (project) {
        project.memberList = sharedProperties.getMembersProject();
        projectDataFactory.updateProject(project).then(successUpdateProject, errorUpdateProject);
    };

    // Cancel
    $scope.cancel = function () {
        $window.history.back();
    };

    $scope.calendar = {
        endDateOpened: false,
        startDateOpened: false
    };

    // show start date calendar when click button start date
    $scope.openPopupStartDate = function ($event) {
        commonFactory.openPopupStartDate($event, $scope);
    };

    // show end date calendar when click button end date
    $scope.openPopupEndDate = function ($event) {
        commonFactory.openPopupEndDate($event, $scope);
    };

    // Edit project
    $scope.editProject = function () {
        //$scope.formReadOnly = false;
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