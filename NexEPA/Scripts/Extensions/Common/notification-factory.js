/*global registrationModule, toastr*/
'use strict';

registrationModule.factory('notificationFactory', function () {
    return {
        success: function () {
            toastr.success("Success");
        },
        error: function (text) {
            toastr.error(text.join("<br />"), "Error!");
        }
    };
});