/* global epaModule */
'use strict';

var calculateScore = function ($scope, kpiInfo) {
    var totalGroupScore = 0;
    var totalGroupWeight = 0;
    $scope.finalScore = 0;

    $.each(kpiInfo, function (item) {
        var kpiGroupItem = kpiInfo[item].kpiGroupItems;

        var kpiGroupWeight = kpiInfo[item].kpiGroupWeight;
        var totalItemScore = 0;
        var totalItemWeight = 0;
        $.each(kpiGroupItem, function (i) {
            var currentItem = kpiGroupItem[i];
            if (currentItem.score > 0) {
                totalItemWeight += currentItem.weight;
                totalItemScore += currentItem.score * currentItem.weight;
            }
        });

        var kpiGroupScore = (totalItemScore / totalItemWeight) * kpiGroupWeight;
        if (isNaN(kpiGroupScore)) {
            totalGroupScore += 0;
        }
        else {
            totalGroupScore += kpiGroupScore;
            totalGroupWeight += kpiGroupWeight;
        }

        kpiInfo[item].kpiGroupScore = kpiGroupScore;
    });

    $.each(kpiInfo, function (item) {
        var currentKPIInfo = kpiInfo[item];
        currentKPIInfo.groupFinalScore = (currentKPIInfo.kpiGroupScore / totalGroupWeight).toFixed(1);
    });

    $scope.finalScore = (totalGroupScore / totalGroupWeight).toFixed(1);

    if (isNaN($scope.finalScore)) {
        $scope.finalScore = 0;
    }
};

epaModule.controller('EmployeeEPAController', ['$scope', 'employeeEPAFactory', 'notificationFactory',
'$routeParams', '$window', 'ErrorService', '$modal',
function ($scope, employeeEPAFactory, notificationFactory, $routeParams, $window, ErrorService, $modal) {
    /**
    * Scope variables. 
    */
    $scope.max = 5;
    $scope.isReadonly = false;
    var targetId = $routeParams.targetId;
    var employeeId = $routeParams.employeeId;
    var currentEPAStatus;
    var newEPAStatus;
    $scope.showMenuContext = true;
    $scope.isPMPage = true;

    /**
    * Function. 
    */

    var checkStatus = function (status) {
        if (status === 1) {
            $scope.isReadonly = true;
            $scope.hoveringOver = null;
            $scope.hoveringLeave = null;
        }
        else {
            $scope.isReadonly = false;
            // Rating Star Hover.
            $scope.hoveringOver = function (value, object) {
                object.percent = value;
            };

            // Rating Star Mouse Leave
            $scope.hoveringLeave = function (item) {
                item.percent = item.score;
                calculateScore($scope, $scope.employeeEPA.kpiInfo);
            };
        }
    };

    /**
    * Ajax event. 
    */
    var successAddOrUpdateEPA = function (response) {
        if (response.data !== "") {
            notificationFactory.error('', response.data);
        }
        else {
            notificationFactory.success('DataChangeSuccessfully', '');
            $scope.dataSaved = true;
        }
    };

    var errorNotify = function (response) {
        if (response.status === 405) {
            ErrorService.errorHandle(response.status);
        }
        else {
            notificationFactory.error('ErrorMessage');
        }
    };

    var successGetEmployeeEPA = function (data) {
        if (data !== "null") {
            if (data.kpiInfo.length > 0) {
                $.each(data.kpiInfo, function (i) {
                    var currentKPIInfo = data.kpiInfo[i];
                    var totalGroupItems = currentKPIInfo.kpiGroupItems.length;
                    var averageMark = 100 / totalGroupItems;
                    if (currentKPIInfo.isSpreadKPI && totalGroupItems > 0) {
                        var currentKPIGroup = currentKPIInfo.kpiGroupItems;
                        $.each(currentKPIGroup, function (k) {
                            currentKPIGroup[k].weight = averageMark;
                        });
                    }
                });
            }

            $scope.employeeEPA = data;
            $scope.finalScore = data.finalScore;
            var roleArray = [];
            $.each(data.projectRoles, function (i) {
                roleArray.push(data.projectRoles[i].name);
            });
            $scope.employeeRole = roleArray.join(", ");

            calculateScore($scope, data.kpiInfo);
        }
        else {
            notificationFactory.error('EPANotAvailable');
        }
    };

    var successUpdateEPAStatus = function (response) {
        if (response.data !== "") {
            $scope.employeeEPA.status = currentEPAStatus;
            checkStatus(currentEPAStatus);
            notificationFactory.error('', response.data);
        }
        else {
            notificationFactory.success('DataChangeSuccessfully', '');
            checkStatus(newEPAStatus);
        }
    };

    var errorGetEmployeeEPA = function (status) {
        notificationFactory.error('', status);
        ErrorService.errorHandle(status);
    };

    /**
    * Initialize data to view Edit Project. 
    */
    employeeEPAFactory.getEmployeeEPA(targetId, employeeId).then(successGetEmployeeEPA, errorGetEmployeeEPA);

    employeeEPAFactory.getPermission().then(function (policy) {
        $scope.permission = {
            policy: parseInt(policy)
        };
    });

    /**
    * Events 
    */

    $scope.$watchCollection('[employeeEPA,permission]', function (newVals) {
        if (newVals[0] && newVals[1]) {
            if ($scope.employeeEPA.permission === 2 && $scope.permission.policy === 2) {
                checkStatus($scope.employeeEPA.status);
            }
            else {
                $scope.isReadonly = true;
                $scope.showMenuContext = false;
            }
        }
    });

    $scope.generalCommentValid = true;

    $scope.save = function (employeeEPAResult) {
        $scope.itemCommentValid = true;

        if (employeeEPAResult.comment === "" || employeeEPAResult.comment === null) {
            $scope.generalCommentValid = false;
        }
        else {
            $scope.generalCommentValid = true;
        }

        $.each(employeeEPAResult.kpiInfo, function (item) {
            var kpiGroupItem = employeeEPAResult.kpiInfo[item].kpiGroupItems;
            $.each(kpiGroupItem, function (i) {
                var currentItem = kpiGroupItem[i];
                if (currentItem.isRequireComment && currentItem.score > 0 &&
                    (currentItem.comment === "" || currentItem.comment === null)) {
                    currentItem.checkNullComment = true;
                    $scope.itemCommentValid = false;
                }
                else {
                    currentItem.checkNullComment = false;
                }
            });
        });

        if ($scope.generalCommentValid && $scope.itemCommentValid) {
            employeeEPAResult.finalScore = $scope.finalScore;
            employeeEPAResult.targetId = targetId;
            employeeEPAFactory.addEmployeeEPA(employeeEPAResult).then(successAddOrUpdateEPA, errorNotify);
        }
        else {
            notificationFactory.error('EPARequiredComment');
        }
    };

    //Cancel Rating
    $scope.cancelRating = function (item) {
        item.score = '';
        item.percent = 0;
        calculateScore($scope, $scope.employeeEPA.kpiInfo);
    };

    // Back to previous history link.
    $scope.cancel = function () { $window.history.back(); };

    // EPA Status Context Menu
    $scope.epaStatus = [{ name: 'Open', id: 0 }, { name: 'Closed', id: 1 }];
    $scope.toggleDropdown = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope.status.isopen = !$scope.status.isopen;
    };

    $scope.updateEPAStatus = function (employeeEPA, status) {
        var updateEPAModalInstance = $modal.open({
            templateUrl: '/templates/Common/ModalConfirmContent.html',
            controller: "ModalConfirmController",
            resolve: {
                name: function () {
                    return "";
                },
                action: function () {
                    return "change status";
                },
                extraInfo: function () {
                    return '';
                }
            },
            windowClass: "modal-confirm-center"
        });
        updateEPAModalInstance.result.then(function () {
            currentEPAStatus = employeeEPA.status;
            newEPAStatus = status;
            employeeEPA.status = status;
            employeeEPA.targetId = targetId;
            employeeEPAFactory.updateEPAStatus(employeeEPA).then(successUpdateEPAStatus, errorNotify);
        });
    };

}]);

epaModule.controller('MyEPAController', ['$scope', 'employeeEPAFactory', 'notificationFactory',
'$routeParams', '$window', 'ErrorService',
function ($scope, employeeEPAFactory, notificationFactory, $routeParams, $window, ErrorService) {
    /**
    * Scope variables. 
    */
    $scope.max = 5;
    $scope.isReadonly = true;
    $scope.showMenuContext = false;
    var targetId = $routeParams.targetId;
    var employeeId = $routeParams.employeeId;
    $scope.generalCommentValid = true;
    $scope.isPMPage = false;

    /**
    * Ajax event. 
    */
    var successGetEmployeeEPA = function (data) {
        if (data !== "null") {
            if (data.kpiInfo.length > 0) {
                $.each(data.kpiInfo, function (i) {
                    var currentKPIInfo = data.kpiInfo[i];
                    var totalGroupItems = currentKPIInfo.kpiGroupItems.length;
                    var averageMark = 100 / totalGroupItems;
                    if (currentKPIInfo.isSpreadKPI && totalGroupItems > 0) {
                        var currentKPIGroup = currentKPIInfo.kpiGroupItems;
                        $.each(currentKPIGroup, function (k) {
                            currentKPIGroup[k].weight = averageMark;
                        });
                    }
                });
            }

            $scope.employeeEPA = data;
            $scope.finalScore = data.finalScore;
            var roleArray = [];
            $.each(data.projectRoles, function (i) {
                roleArray.push(data.projectRoles[i].name);
            });
            $scope.employeeRole = roleArray.join(", ");

            calculateScore($scope, data.kpiInfo);
        }
        else {
            notificationFactory.error('EPANotAvailable');
        }
    };

    var errorHandle = function (status) {
        ErrorService.errorHandle(status);
    };

    /**
    * Initialize data to view. 
    */
    employeeEPAFactory.getMyEPA(targetId, employeeId).then(successGetEmployeeEPA, errorHandle);

    /**
    * Events 
    */

    $scope.cancel = function () { $window.history.back(); };

}]);

// View EPA of Current Member Controller
epaModule.controller('ViewCurrentMemberEPAController',
['$scope', '$location', '$route', 'employeeEPAFactory', 'commonFactory', 'ErrorService', 'notificationFactory',
function ($scope, $location, $route, employeeEPAFactory, commonFactory, ErrorService, notificationFactory) {

    employeeEPAFactory.getAllProject().then(function (projectList) {
        $scope.projectOptions = projectList;
    });

    employeeEPAFactory.getAllStatus().then(function (statusList) {
        $scope.statusOptions = statusList;
    });

    var successGetEPA = function (listEPA) {
        $scope.listEPA = listEPA;
        $scope.submitted = false;
    };

    var errorHandle = function (response, status) {
        if (status === 405) {
            ErrorService.errorHandle(status);
        }
        else {
            notificationFactory.error('', "ErrorMessage");
        }

        $scope.submitted = false;
    };

    var getEPAOfCurrentMember = function (projectId, status) {
        employeeEPAFactory.getEPAOfCurrentMember(projectId, status).then(successGetEPA, errorHandle);
    };

    getEPAOfCurrentMember(null, null);

    $scope.EPADetail = function (epaItem) {
        if (epaItem.target) {
            var targetId = epaItem.target.targetId,
                employeeId = epaItem.employeeId;
            $scope.viewEPADetailUrl = '/MyEPA/Index' + '?targetId=' + targetId + '&employeeId=' + employeeId;
        }
    };

    $scope.query = function (filter) {
        $scope.submitted = true;

        if (filter) {
            getEPAOfCurrentMember(filter.projectId, filter.status);
        }
        else {
            $scope.submitted = false;
        }
    };

    $scope.reload = function () {
        $route.reload();
    };

}]);

// View EPA of Member by Manager Controller
epaModule.controller('ViewMemberByManagerEPAController',
['$scope', '$routeParams', '$route', 'employeeEPAFactory', 'ErrorService', 'commonFactory', 'notificationFactory',
'$modal', 'employeeRepository', 'linkToEPAService',
function ($scope, $routeParams, $route, employeeEPAFactory, ErrorService, commonFactory, notificationFactory,
$modal, employeeRepository, linkToEPAService) {
    var createdDate = $routeParams.createdDate ? new Date($routeParams.createdDate) : new Date();

    var startDate = moment(createdDate).subtract(3, 'months').toDate(),
        endDate = moment(createdDate).add(1, 'months').toDate(),
        targetId = $routeParams.targetId ? parseInt($routeParams.targetId) : null,
        status = $routeParams.status ? parseInt($routeParams.status) : null,
        isShowAll = $routeParams.isShowAll === "true" ? true : false;

    var objectQuery = linkToEPAService.getProperty();

    $scope.filter = {
        fromDate: commonFactory.calculateFromDate(3, 1),
        toDate: new Date(),
        teamId: null,
        targetId: null,
        status: null,
        isShowAll: null,
        projectId: null,
        employeeId: ''
    };

    if (targetId) {
        $scope.filter.fromDate = startDate;
        $scope.filter.toDate = endDate;
        $scope.filter.targetId = targetId;
        $scope.filter.status = status;
        $scope.filter.isShowAll = isShowAll;
    } else if (objectQuery != null) {
        $scope.filter.fromDate = objectQuery.startDate ? new Date(objectQuery.startDate) : new Date();
        $scope.filter.toDate = objectQuery.endDate ? new Date(objectQuery.endDate) : null;
        $scope.filter.projectId = objectQuery.projectId;
        $scope.filter.status = objectQuery.status;
        $scope.filter.isShowAll = objectQuery.isShowAll;
    }

    $scope.isShowAll = false;
    $scope.isLoadingButton = false;
    $scope.sumRow = 0;

    $scope.dateFormat = "dd MMM, yyyy";

    $scope.calendar = {
        startDateOpened: false,
        endDateOpened: false
    };

    /*
    ** Open calendar.
    */
    $scope.openPopupStartDate = function ($event) {
        commonFactory.openPopupStartDate($event, $scope);
    };

    $scope.openPopupEndDate = function ($event) {
        commonFactory.openPopupEndDate($event, $scope);
    };

    $scope.collapse = function (epa) {
        if (epa.isCollapse) {
            epa.isCollapse = false;
            $scope.sumRow -= epa.epAs.length;
        }
        else {
            epa.isCollapse = true;
            $scope.sumRow += epa.epAs.length;
        }
    };

    /*
    ** Get current user.
    */
    employeeEPAFactory.getCurrentUser().then(function (currentUser) {
        $scope.currentUser = currentUser;
    }, function () {
        notificationFactory.error("ErrorMessage");
    });

    /*
    ** Get isShowFullEPAComment.
    */
    employeeEPAFactory.getIsShowFullEPAComment().then(function (isShowFullEPAComment) {
        $scope.isShowFullEPAComment = isShowFullEPAComment === "true" ? true : false;
    }, function () {
        notificationFactory.error("ErrorMessage");
    });

    $scope.$watch("currentUser", function (newValue) {
        if (newValue) {
            var roles = newValue.roles;

            var roleNames = roles.map(function (e) { return e.name; });

            $.each(roles, function (index, value) {
                if (value.name === 'PM') {
                    // only PM can see Target filter
                    $scope.showTargetFilter = true;

                    /*
                    ** Get all target.
                    */
                    employeeEPAFactory.getTargetToFilter().then(function (targetList) {
                        $scope.targetOptions = targetList;
                    });
                }
                if (value.name === 'SA') {
                    // only SA can see checkbox Show all member EPA
                    $scope.showAllMemberEPA = true;
                }
            });

            if (roleNames.indexOf('PM') !== -1 && roleNames.indexOf('SA') !== -1) {
                $scope.isShowAll = true;
            }
            else {
                $scope.isShowAll = false;
            }
        }
    });

    /*
    ** Get permission.
    */
    employeeEPAFactory.getPermission().then(function (policy) {
        $scope.permission = {
            policy: policy
        };
    });

    /*
    ** Get all project.
    */
    employeeEPAFactory.getAllProject().then(function (projectList) {
        $scope.projectOptions = projectList;
    });

    /*
    ** Get all status.
    */
    employeeEPAFactory.getAllStatus().then(function (statusList) {
        $scope.statusOptions = statusList;
    });

    /*
    ** Get all group by current user.
    */
    employeeRepository.getAllGroupByActorUser().success(function (groupList) {
        $scope.teamOptions = groupList;
    }).error(function () {
        notificationFactory.error("ErrorMessage");
    });

    var successGetEPA = function (listEPA) {
        angular.forEach(listEPA, function (epa) {
            epa.isCollapse = false;
        });

        $scope.listEPA = listEPA;
        $scope.sumRow = listEPA.length;
        $scope.submitted = false;
        $scope.isLoadingButton = false;
    };

    var errorHandle = function (status) {
        if (status === 405) {
            ErrorService.errorHandle(status);
        }
        else {
            notificationFactory.error("ErrorMessage");
        }

        $scope.submitted = false;
        $scope.isLoadingButton = false;
    };

    /*
    ** Get EPA of Employee by Manager.
    */
    var getEPAOfEmployeeByManager = function (fromDate, toDate, teamId, targetId, projectId, status, employeeId, isShowAll) {
        employeeEPAFactory.getEPAOfEmployeeByManager(fromDate, toDate, teamId, targetId, projectId, status, employeeId, isShowAll)
            .then(successGetEPA, errorHandle);
    };

    /*
    ** Get employee when group and project change.
    */
    $scope.getEmployee = function (teamId, projectId) {
        employeeRepository.getEmployeeByTeamProject(teamId, projectId).success(function (employees) {
            $scope.employeeOptions = employees;
        }).error(function () {
            notificationFactory.error("ErrorMessage");
        });
    };


    var validateFromToDate = function (fromDate, toDate) {
        var isValid = true;

        if (!fromDate) {
            isValid = false;
            notificationFactory.error("FromRequire");
        } else if (toDate) {
            fromDate = new Date(fromDate.toDateString());
            toDate = new Date(toDate.toDateString());
            var maxDate = new Date(fromDate.getFullYear() + 1, fromDate.getMonth(), fromDate.getDate());

            if (fromDate > toDate) {
                isValid = false;
                notificationFactory.error("ValidateFromToDate");
            }
            else if (toDate > maxDate) {
                isValid = false;
                notificationFactory.error("ValidateMaximumOneYear");
            }
        }

        return isValid;
    };

    /*
    ** Query.
    */
    $scope.query = function (filter) {
        var isValid = validateFromToDate(filter.fromDate, filter.toDate);
        if (isValid && filter) {
            $scope.submitted = true;
            $scope.isLoadingButton = true;
            filter.employeeId = commonFactory.getSelectedData($scope.employeeOptions, "employeeId");

            getEPAOfEmployeeByManager(filter.fromDate, filter.toDate, filter.teamId,
            filter.targetId, filter.projectId, filter.status, filter.employeeId, filter.isShowAll);
        }
    };

    var loadData = function () {
        $scope.getEmployee($scope.filter.teamId, $scope.filter.projectId);
        $scope.query($scope.filter);
    };

    loadData();

    /*
    ** Link to EPA Detail.
    */
    $scope.EPADetail = function (epaItem) {
        if (epaItem.target) {
            var targetId = epaItem.target.targetId,
            employeeId = epaItem.employeeId;
            $scope.viewEPADetailUrl = '/EmployeeEPA/Index' + '?targetId=' + targetId + '&employeeId=' + employeeId;
        }
    };

    /*
    ** Link to Person Report.
    */
    $scope.PersonReport = function (epaItem) {
        if (epaItem.employeeId) {
            $scope.viewPersonReportUrl = '/Report/EmployeePerformanceTendency?employeeId=' + epaItem.employeeId;
        }
    };

    // EPA Status Context Menu
    $scope.epaStatus = [{ name: 'Open', id: 0 }, { name: 'Closed', id: 1 }];
    $scope.toggleDropdown = function ($event) {
        $event.preventDefault();
        $event.stopPropagation();
        $scope.status.isopen = !$scope.status.isopen;
    };

    $scope.updateEPAStatus = function (epaItem, status) {
        var updateStatus = angular.copy(epaItem);
        updateStatus.statusName = status.name;
        updateStatus.status = status.id;
        updateStatus.targetId = epaItem.target.targetId;

        var updateEPAModalInstance = $modal.open({
            templateUrl: '/templates/Common/ModalConfirmContent.html',
            controller: "ModalConfirmController",
            resolve: {
                name: function () {
                    return "";
                },
                action: function () {
                    return "change status";
                },
                extraInfo: function () {
                    return '';
                }
            },
            windowClass: "modal-confirm-center"
        });

        updateEPAModalInstance.result.then(function () {
            employeeEPAFactory.updateEPAStatus(updateStatus).then(function (response) {
                if (response.data !== "") {
                    notificationFactory.error('', response.data);
                }
                else {
                    epaItem.statusName = status.name;
                    notificationFactory.success('DataChangeSuccessfully', '');
                }
            }, function (response) {
                notificationFactory.error('', response);
            });
        });
    };

    $scope.reload = function () {
        $route.reload();
    };
}]);
