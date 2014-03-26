/* global resourceModule */
'use strict';

resourceModule.controller("ResourceController", function ($scope, $translate) {
    $scope.changeLanguage = function (langKey) {
        $translate.use(langKey);
    };
});
