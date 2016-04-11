/*global $, registrationModule*/
'use strict';

registrationModule.filter('enumStatusText', ['$http', function ($http) {
    var statusEnum = [];
    $http.get('/api/Project/RenderProjectStatusSelectList').success(function (data) {
        statusEnum = data;
    });
    return function (input) {
        var statusText;
        $.each(statusEnum, function (i, val) {
            if (statusEnum[i].id === input) {
                statusText = val.name;
            }
        });
        return statusText;
    };
}]);

registrationModule.filter('targetType', ['$http', function ($http) {
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
}]);

registrationModule.filter('customDate', ['$filter', function ($filter) {
    return function (input) {
        if (input == null) { return ""; }
        var _date = $filter('date')(new Date(input), 'dd MMM, yyyy');
        return _date;
    };
}]);

registrationModule.filter('epaStatus', function () {
    return function (input) {
        var statusText;
        switch (input) {
            case 0:
                statusText = "Open";
                break;
            case 1:
                statusText = "Closed";
                break;
        }
        return statusText;
    };
});

registrationModule.filter('customNumber', ['$filter', function ($filter) {
    return function (input) {
        if (input == null || input === 0) { return ""; }
        var _number = $filter('number')(input, 2);
        return _number;
    };
}]);

registrationModule.filter('customPercent', ['$filter', function ($filter) {
    return function (input, digits) {
        if (input == null || input === 0) { return ""; }

        var percent = $filter('number')(input * 100, digits) + '%';
        return percent;
    };
}]);