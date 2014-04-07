/*global $, registrationModule*/
'use strict';

registrationModule.filter('enumStatusText', function () {
    var statusEnum = [{ text: "Active", value: 1 }, { text: "Closed", value: 2}];
    return function (input) {
        var statusText;
        $.each(statusEnum, function (i, val) {
            if (statusEnum[i].value === input) {
                statusText = val.text;
            }
        });
        return statusText;
    };
});

registrationModule.filter('targetType', function ($http) {
    var targetType = [];
    $http.get('/api/targetTemplate/GetAllTargetType').success(function (data) {
        targetType = data;
    });
    return function (input) {
        var typeText;
        $.each(targetType, function (i, val) {
            if (targetType[i].targetTypeId === input) {
                typeText = val.name;
            }
        });
        return typeText;
    };
});