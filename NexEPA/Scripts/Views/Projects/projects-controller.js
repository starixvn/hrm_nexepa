/* global registrationModule */
'use strict';

projectsSettingModule.controller("ProjectController", function ($scope) {

});

projectsSettingModule.controller("CreateProjectController", function ($scope, selectListDataFactory, projectsFactory, projectFactory, $location) {

    selectListDataFactory.renderPMSelectList().then(function (lstPM) {
        $scope.pms = lstPM;
    });
    selectListDataFactory.renderStatusSelectList().then(function (lstStatus) {
        $scope.status = lstStatus;
    });

    $scope.save = function (project) {
        $scope.errors = [];
        projectsFactory.create(project).$promise.then(
        function () { $location.url('/Home/Index'); },
        function (response) {
            $scope.errors = response.data;
        });
    }
});