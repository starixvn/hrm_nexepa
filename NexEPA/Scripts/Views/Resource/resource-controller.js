/* global registrationModule */
'use strict';

registrationModule.controller("ResourceController", ['$scope', '$translate', function ($scope, $translate) {
    $scope.changeLanguage = function (langKey) {
        $translate.use(langKey);
    };
}]);
