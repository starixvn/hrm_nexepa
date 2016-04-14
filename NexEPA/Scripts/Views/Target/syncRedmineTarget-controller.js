'use strict';
/*global targetModule*/

targetModule.controller('SyncRedmineTargetController',
['targetFactory', 'commonFactory', 'ErrorService', 'notificationFactory',
function (targetFactory, commonFactory, ErrorService, notificationFactory) {
    var vm = this;

    vm.isLoadingGetTargetButton = false;
    vm.filter = {
        fromDate: commonFactory.calculateFromDate(3, 1),
        toDate: new Date(),
        projectIds: null
    };
    vm.calendar = {
        startDateOpened: false,
        endDateOpened: false
    };
    vm.projects = [];
    vm.projectTargets = null;

    vm.openTargetPage = function () {
        window.location.href = "/Target/ViewTarget";
    };

    vm.validateFilterOptions = function () {
        var isValid = commonFactory.validateFromToDate(vm.filter.fromDate, vm.filter.toDate);

        return isValid;
    };

    vm.getAllProjects = function () {
        targetFactory.getAllProject().success(function (projects) {
            vm.projects = projects;
        }).error(function () {
            notificationFactory.error("ErrorMessage");
        });
    };

    vm.getProjectTarget = function () {
        vm.filter.projectIds = commonFactory.getSelectedData(vm.projects, "id");

        targetFactory.getRedmineProjectTarget(vm.filter).success(function (projectTargets) {
            vm.projectTargets = projectTargets;

            vm.addNewTargetRowsAndHandleOldFeatureId();
        }).error(function () {
            notificationFactory.error("ErrorMessage");
        });
    };

    vm.getNewTarget = function (projectId) {
        return {
            targetId: 0,
            name: '',
            startDate: '',
            endDate: '',
            syncedDate: '',
            redmineFeatureId: '',
            projectId: projectId
        };
    };

    vm.addNewTargetRow = function (projectTarget) {
        if (projectTarget) {
            var target = vm.getNewTarget(projectTarget.project.projectId);

            projectTarget.targets.push(target);
        }
    };

    vm.handleOldFeatureId = function (targets) {
        angular.forEach(targets, function (target) {
            target.oldRedmineFeatureId = target.redmineFeatureId;
        });
    };

    vm.addNewTargetRowsAndHandleOldFeatureId = function () {
        if (vm.projectTargets) {
            angular.forEach(vm.projectTargets, function (projectTarget) {
                vm.addNewTargetRow(projectTarget);

                projectTarget.isCollapse = true;

                vm.handleOldFeatureId(projectTarget.targets);
            });
        }
    };

    vm.setTargetInfo = function (target) {
        if (target.redmineFeatureId && target.redmineFeatureId !== target.oldRedmineFeatureId) {
            targetFactory.getRedmineFeatureInfo(target.redmineFeatureId).success(function (feature) {
                feature = angular.fromJson(feature);
                target.oldRedmineFeatureId = target.redmineFeatureId;

                if (feature) {
                    target.name = feature.issueName;
                    target.startDate = feature.startDate;
                    target.endDate = feature.dueDate;
                } else {
                    var message = 'Couldn\'t find the feature with id "' + target.redmineFeatureId + '"';

                    notificationFactory.error(null, message);
                }
            }).error(function () {
                notificationFactory.error("ErrorMessage");
            });
        }
    };

    vm.selectTargets = function (projectTarget) {
        projectTarget.checked = !projectTarget.checked;

        angular.forEach(projectTarget.targets, function (target) {
            target.checked = projectTarget.checked;
        });
    };

    vm.selectTarget = function (projectTarget, target) {
        target.checked = !target.checked;

        var matchedTargets = $.grep(projectTarget.targets, function (item) {
            return item.checked === target.checked;
        });

        if (matchedTargets.length === projectTarget.targets.length) {
            projectTarget.checked = target.checked;
        } else {
            projectTarget.checked = false;
        }
    };

    vm.initialize = function () {
        commonFactory.getDateFormat(vm);

        vm.getAllProjects();
    };

    vm.initialize();
}]);