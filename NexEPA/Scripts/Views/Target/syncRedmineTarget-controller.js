'use strict';
/*global targetModule*/

targetModule.controller('SyncRedmineTargetController',
['targetFactory', 'projectDataFactory', 'commonFactory', 'ErrorService', 'notificationFactory',
function (targetFactory, projectDataFactory, commonFactory, ErrorService, notificationFactory) {
    var vm = this;

    vm.isLoadingGetTargetButton = false;
    vm.filter = {
        fromDate: commonFactory.calculateFromDate(3, 1),
        toDate: new Date(),
        projectId: null
    };

    vm.calendar = {
        startDateOpened: false,
        endDateOpened: false
    };

    vm.openTargetPage = function () {
        window.location.href = "/Target/ViewTarget";
    };

    vm.validateFilterOptions = function () {
        var isValid = commonFactory.validateFromToDate(vm.filter.fromDate, vm.filter.toDate);

        return isValid;
    };

    vm.initialize = function () {
        commonFactory.getDateFormat(vm);
    };

    vm.initialize();
}]);