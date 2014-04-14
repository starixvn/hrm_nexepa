/*global registrationModule*/
'use strict';

registrationModule.controller("ModalConfirmController", function ($scope, $modalInstance, name, action) {
    $scope.name = name;
    $scope.action = action;

    $scope.ok = function () {
        $modalInstance.close();
    };

    $scope.cancel = function () {
        $modalInstance.dismiss("cancel");
    };
});