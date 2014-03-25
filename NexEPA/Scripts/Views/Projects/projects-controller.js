/* global projectsSettingModule */
'use strict';

var dateValidation = function () {
    var startDate = new Date($('#startDate').val());
    var endDate = new Date($('#endDate').val());
    if (startDate !== "" && endDate !== "") {
        if (endDate < startDate) {
            $("form .form-group:eq(1)").before("<div id='dateValid' class='form-group'>"
                + "<div class='col-sm-offset-2 col-sm-10 ng-scope ng-binding' style='color:red;'> "
                + "End date must be greater than Start date.</div></div>");
            return false;
        }
        else {
            $.fn.exists = function () {
                return this.length !== 0;
            };
            var dateErrors = $("#dateValid");
            if (dateErrors.exists()) {
                dateErrors.remove();
                return true;
            }
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

projectsSettingModule.controller("ProjectController", function ($scope, selectListDataFactory, dataFilterFactory, projectService) {

    $scope.projects = dataFilterFactory.filterData(null).then(function (proData) {
        $scope.projects = proData;
    });

    selectListDataFactory.renderPMSelectList().then(function (lstPM) {
        $scope.pms = lstPM;
    });
    selectListDataFactory.renderStatusSelectList().then(function (lstStatus) {
        $scope.status = lstStatus;
    });
    selectListDataFactory.renderProjectPeriodSelectList().then(function (lstPeriod) {
        $scope.periods = lstPeriod;
    });

    $scope.getProjectList = function (filter) {
        dataFilterFactory.filterData(filter).then(function (lstEmployee) {
            $scope.projects = lstEmployee;
        });
    };

    $scope.editProject = function (currObj) {
        projectService.setProperty(currObj);
    };

});

projectsSettingModule.controller("CreateProjectController", function ($scope, selectListDataFactory, projectsFactory, projectFactory, $location) {

    selectListDataFactory.renderPMSelectList().then(function (lstPM) {
        $scope.pms = lstPM;
    });
    selectListDataFactory.renderStatusSelectList().then(function (lstStatus) {
        $scope.status = lstStatus;
    });

    $scope.save = function (project) {
        var checkDateValidation = dateValidation();
        if (checkDateValidation) {
            $scope.errors = [];
            projectsFactory.create(project).$promise.then(
            function () { $location.url('/Home/Index'); },
            function (response) {
                $scope.errors = response.data;
            });
        }
    };

});

projectsSettingModule.controller("UpdateProjectController", function ($scope, projectService, selectListDataFactory, projectFactory, $location) {

    selectListDataFactory.renderPMSelectList().then(function (lstPM) {
        $scope.pms = lstPM;
    });
    selectListDataFactory.renderStatusSelectList().then(function (lstStatus) {
        $scope.projectstatus = lstStatus;

    });

    $scope.projectUpdate = projectService.getProperty();

    $scope.update = function (project) {
        var checkDateValidation = dateValidation();
        if (checkDateValidation) {
            $scope.errors = [];
            projectFactory.update(project).$promise.then(
            function () { $location.url('/Project/Index'); },
            function (response) {
                $scope.errors = response.data;
            });
        }
    };
});


projectsSettingModule.controller('IndexCtrl', function ($scope, milestoneFactory, notificationFactory, projectService) {
    $scope.currentProject = projectService.getProperty();

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

        return milestoneFactory.getMilestone($scope.currentProject.projectId).success(getMilestoneSuccessCallback).error(errorCallback);
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


    milestoneFactory.getMilestone($scope.currentProject.projectId).success(getMilestoneSuccessCallback).error(errorCallback);

    $scope.addMilestone = function () {
        $scope.milestone.projectId = $scope.currentProject.projectId;
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
