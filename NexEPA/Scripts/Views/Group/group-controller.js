/* global registrationModule */
'use strict';

registrationModule.controller("GroupController", function ($scope, groupRepository) {
    groupRepository.get().then(function (groups) {
        $scope.groups = groups;
    });
});