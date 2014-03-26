/* global projectsSettingModule */
'use strict';

var dateValidation = function () {
    var startDate = new Date($('#startDate').val());
    var endDate = new Date($('#endDate').val());
    var dateWarning = $("#dateValid");
    if (startDate !== "" && endDate !== "") {
        if (endDate < startDate) {
            dateWarning.show();
            return false;
        }
        else {
            dateWarning.hide();
        }
    }
    return true;
};

var dateValidation1 = function () {
    var startDate = new Date($('#start1').val());
    var endDate = new Date($('#end1').val());
    if (startDate !== "" && endDate !== "") {
        if (endDate < startDate) {
            alert("End date must be greater than Start date.");
            return false;
        }
    }
    return true;
};

projectsSettingModule.controller("ProjectController",
function ($scope, $routeParams, projectDataFactory, dataFilterFactory, projectFactory, projectsFactory, $location) {
    //$scope.project = null;
    

    var loadSelectList = function () {
        projectDataFactory.renderPMSelectList().then(function (lstPM) {
            $scope.pms = lstPM;
        });
        projectDataFactory.renderStatusSelectList().then(function (lstStatus) {
            $scope.status = lstStatus;
        });
    };

    //view Index.
    $scope.initViewIndex = function () {
        $scope.projects = dataFilterFactory.filterData(null).then(function (lstProject) {
            $scope.projects = lstProject;
        });
    };

    $scope.initViewFilter = function () {
        loadSelectList();
        projectDataFactory.renderProjectPeriodSelectList().then(function (lstPeriod) {
            $scope.periods = lstPeriod;
        });
    };

    $scope.getProjectList = function (filter) {
        dataFilterFactory.filterData(filter).then(function (lstProject) {
            $scope.projects = lstProject;
        });
    };

    //view Add
    $scope.initViewAdd = function () {
        loadSelectList();
    };

    $scope.save = function (project) {
        var checkDateValidation = dateValidation();
        if (checkDateValidation) {
            $scope.errors = [];
            projectsFactory.create(project).$promise.then(
            function () { $location.url('/Projects/Index'); },
            function (response) {
                $scope.errors = response.data;
            });
        }
    };

    //view Edit
    $scope.initViewEdit = function () {
        var projectId = $routeParams.projectId;
        loadSelectList();
        projectDataFactory.getProjectById(projectId).then(function (project) {
            $scope.projectUpdate = project;
        });
    };

//    $scope.editProject = function (projectId) {
//        projectService.setProperty(projectId);
//    };

    $scope.update = function (project) {
        var checkDateValidation = dateValidation();
        if (checkDateValidation) {
            $scope.errors = [];
            projectsFactory.update(project).$promise.then(
            function () { $location.url('/Projects/Index'); },
            function (response) {
                $scope.errors = response.data;
            });
        }
    };

});

projectsSettingModule.controller('MilestoneCtrl', function ($scope, $routeParams, milestoneFactory, notificationFactory) {
    var projectId = $routeParams.projectId;

    $scope.milestones = [];
    $scope.addMode = false;

    $scope.toggleAddMode = function () {
        $scope.addMode = !$scope.addMode;
    };

    $scope.toggleEditMode = function (milestone) {
        milestone.editMode = !milestone.editMode;
    };

    var getMilestoneSuccessCallback = function (data) {
        $scope.milestones = data;
    };

    var successCallback = function () {
        notificationFactory.success();

        return milestoneFactory.getMilestone(projectId).success(getMilestoneSuccessCallback).error(errorCallback);
    };

    var successPostCallback = function (data, status, headers, config) {
        successCallback(data, status, headers, config).success(function () {
            $scope.toggleAddMode();
            $scope.milestone = {};
        });
    };

    var errorCallback = function (data) {
        notificationFactory.error(data.ExceptionMessage);
    };

    milestoneFactory.getMilestone(projectId).success(getMilestoneSuccessCallback).error(errorCallback);

    $scope.addMilestone = function () {
        $scope.milestone.projectId = projectId;
        milestoneFactory.addMilestone($scope.milestone).success(successPostCallback).error(errorCallback);
    };

    $scope.deleteMilestone = function (milestone) {
        milestoneFactory.deleteMilestone(milestone.milestoneId).success(successCallback).error(errorCallback);
    };

    $scope.updateMilestone = function (milestone) {
        var validate = dateValidation1();
        if (validate) {
            milestoneFactory.updateMilestone(milestone).success(successCallback).error(errorCallback);
        }

    };
});
