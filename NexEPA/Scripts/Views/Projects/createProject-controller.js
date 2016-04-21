/* global projectsSettingModule */
'use strict';

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
        if (value.lenght > 0)
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
                projectUtility.showMessageAfterValidate(this);
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