/* global projectsSettingModule */
'use strict';

projectsSettingModule.controller('ProjectTargetListController',
['$routeParams', '$modal', 'targetFactory', 'targetUtility', 'employeeEPAFactory', 'notificationFactory', 'ErrorService',
function ($routeParams, $modal, targetFactory, targetUtility, employeeEPAFactory, notificationFactory, ErrorService) {
    var vm = this;

    vm.projectId = parseInt($routeParams.projectId);
    vm.targets = [];
    vm.permissionMemberEPA = {
        policy: ''
    };
    vm.permission = {
        policy: ''
    };
    vm.statusOptions = [];
    vm.currentUser = {};

    vm.getTargets = function () {
        targetFactory.getTargetByProjectId(vm.projectId).success(function (targets) {
            vm.targets = targets;
        }).error(function () {
            notificationFactory.error("ErrorMessage");
        });
    };

    vm.editTargetUrl = function (target) {
        var targetId = target.targetId,
            projectId = target.project ? target.project.projectId : null;
        var editTargetUrl = '/Target/EditTarget/' + targetId + '?projectId=' + projectId + '&isEditMode';

        return editTargetUrl;
    };

    vm.viewTargetEmployee = function (target) {
        var targetId = target.targetId,
            projectId = target.project ? target.project.projectId : null;
        var viewTargetEmployeeUrl = '/Target/TargetEmployee/' + targetId + '?projectId=' + projectId + '&isEditMode&viewMembers';

        return viewTargetEmployeeUrl;
    };

    vm.viewTargetKPI = function (target) {
        var targetId = target.targetId,
            projectId = target.project ? target.project.projectId : null;
        var viewTargetKPIUrl = '/Target/EditTarget/' + targetId + '?projectId=' + projectId + '&isEditMode&viewKPIs';

        return viewTargetKPIUrl;
    };

    vm.viewOpenEPA = function (createdDate, targetId, projectManagerId) {
        var viewOpenEPAUrl = '';

        if (vm.currentUser) {
            var isShowAll = false;

            if (vm.currentUser.userId !== projectManagerId) {
                isShowAll = true;
            }

            createdDate = new Date(createdDate);
            viewOpenEPAUrl = '/MemberEPA/ViewMember/?targetId=' + targetId + '&status=0&isShowAll=' + isShowAll +
                                '&createdDate=' + createdDate.toDateString();
        }

        return viewOpenEPAUrl;
    };

    vm.viewAllEPA = function (createdDate, targetId, projectManagerId) {
        var viewAllEPAUrl = '';

        if (vm.currentUser) {
            var isShowAll = false;

            if (vm.currentUser.userId !== projectManagerId) {
                isShowAll = true;
            }

            createdDate = new Date(createdDate);
            viewAllEPAUrl = '/MemberEPA/ViewMember/?targetId=' + targetId + '&isShowAll=' + isShowAll +
                                '&createdDate=' + createdDate.toDateString();
        }

        return viewAllEPAUrl;
    };

    vm.updateStatus = function (target, status) {
        if (target && status) {
            var targetStatus = {
                targetId: target.targetId,
                status: status.id
            };

            var modalUpdateStatusInstance = $modal.open({
                templateUrl: '/templates/Common/modalConfirmContent.html',
                controller: "ModalConfirmController",
                resolve: {
                    name: function () {
                        return "";
                    },
                    action: function () {
                        return status.name;
                    },
                    extraInfo: function () {
                        return '';
                    }
                },
                windowClass: "modal-confirm-center"
            });

            modalUpdateStatusInstance.result.then(function () {
                targetFactory.updateTargetStatus(targetStatus).success(function (response) {
                    if (response.isSuccessful === true) {
                        target.statusName = status.name;
                        notificationFactory.success("DataChangeSuccessfully");
                    }
                    else {
                        notificationFactory.error("", response.message);
                    }
                }).error(function (response, status) {
                    if (status === 405) {
                        ErrorService.errorHandle(status);
                    }
                    else {
                        notificationFactory.error("ErrorMessage");
                    }
                });
            });
        }
    };

    vm.initialize = function () {
        employeeEPAFactory.getCurrentUser().then(function (currentUser) {
            vm.currentUser = currentUser;
        }, function () {
            notificationFactory.error("ErrorMessage");
        });

        employeeEPAFactory.getPermission().then(function (policy) {
            vm.permissionMemberEPA.policy = policy;
        });

        targetFactory.getPermission().success(function (policy) {
            vm.permission.policy = policy;
        }).error(function () {
            notificationFactory.error("ErrorMessage");
        });

        targetUtility.getAllStatus(vm);

        vm.getTargets();
    };

    vm.initialize();
}]);
