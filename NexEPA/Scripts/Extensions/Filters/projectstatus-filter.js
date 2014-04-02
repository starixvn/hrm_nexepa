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