/*global registrationModule*/
'use strict';

registrationModule.service("ErrorService", function ($location) {
    return {
        errorHandle: function (status) {
            if (status === 405) {
                $location.path('/Home/AccessDenied');
            }
        }
    };
});