'use strict';
/*global targetModule*/

targetModule.factory('targetFactory', ['$http', function ($http) {
    return {
        getTarget: function (targetTypeId, startDate, endDate) {
            return $http.get('/api/Target/GetTarget',
                { params: { targetTypeId: targetTypeId, startDate: startDate, endDate: endDate } });
        },
        getTargetByProjectId: function (projectId) {
            return $http.get('/api/Target/GetTargetByProjectId',
                { params: { projectId: projectId || '' } });
        },
        getTargetById: function (targetId) {
            return $http.get('/api/Target/GetTargetById', { params: { targetId: targetId } });
        },
        createTarget: function (target) {
            return $http.post('/api/Target/InsertTarget', target);
        },
        editTarget: function (target) {
            return $http.put('/api/Target/Put', target);
        },
        updateTargetStatus: function (targetStatus) {
            return $http.put('/api/Target/UpdateStatus', targetStatus);
        },
        deleteTarget: function (targetId) {
            return $http['delete']('/api/Target', { params: { targetId: targetId } });
        },
        getAllProject: function (projectId) {
            return $http.get('/api/Core/GetAllProject', { params: { IncludeProjectId: projectId } });
        },
        getMilestoneOfProject: function (projectId) {
            return $http.get('/api/Target/GetMilestoneByProjectId', { params: { projectId: projectId } });
        },
        getAllStatus: function () {
            return $http.get('/api/Core/GetAllStatus');
        },
        getEmployeeByTargetId: function (targetId) {
            return $http.get('/api/Target/GetEmployeeByTargetId', { params: { targetId: targetId } });
        },
        insertTargetEmployee: function (employee) {
            return $http.post('/api/Target/InsertTargetEmployee', employee);
        },
        deleteTargetEmployee: function (targetId, employeeIds) {
            return $http['delete']('/api/Target/DeleteTargetEmployee',
                { params: { targetId: targetId, employeeIds: employeeIds } });
        },
        getKPIByTargetId: function (targetId, groupId) {
            return $http.get('/api/Target/GetKPIByTargetId', { params: { targetId: targetId, groupId: groupId } });
        },
        getProjectOrMilestoneDateById: function (projectId, miletoneId) {
            return $http.get('/api/Target/GetProjectOrMilestoneDateById', { params: { projectId: projectId, milestoneId: miletoneId } });
        },
        getKPIGroupByTargetId: function (targetId) {
            return $http.get('/api/Target/GetKPIGroupByTargetId', { params: { targetId: targetId } });
        },
        getPermission: function () {
            return $http.get('/api/Target/GetPermission');
        },
        getEmployeeByKPI: function (targetId, kpiGroupId, kpiId) {
            return $http.get('/api/Target/GetEmployeeByKPI',
                { params: { targetId: targetId, kpiGroupId: kpiGroupId, kpiId: kpiId } });
        },
        updateKPIScore: function (kpiScore) {
            return $http.put('/api/Target/UpdateKPIScore', kpiScore);
        },        
        getUserPermissionOnTarget: function (targetId) {
            return $http.get('/api/EmployeeEPA/GetUserPermissonOnTarget', { params: { targetId: targetId } });
        },
        getRedmineProjectTarget: function (filter) {
            return $http.get('/api/Target/GetRedmineProjectTarget', {
                params: {
                    fromDate: filter.fromDate.toDateString() || '',
                    toDate: filter.toDate.toDateString() || '',
                    projectIds: filter.projectIds || ''
                }
            });
        },
        getRedmineFeatureInfo: function (redmineFeatureId) {
            return $http.get('/api/Target/GetRedmineFeatureInfo', {
                params: {
                    redmineFeatureId: redmineFeatureId
                }
            });
        },
        syncTargetFromRedmineFeature: function (targets) {
            return $http.post('/api/Target/SyncTargetFromRedmineFeature', targets);
        },
        getSyncTargetPermission: function () {
            return $http.get('/api/Target/GetSyncTargetPermission');
        }
    };
}]);

targetModule.factory('targetCacheFactory', ['$cacheFactory', function ($cacheFactory) {
    return $cacheFactory('targetCache');
}]);

targetModule.factory('targetUtility',
['$translate', 'targetFactory', 'targetCacheFactory', 'notificationFactory',
function ($translate, targetFactory, targetCacheFactory, notificationFactory) {
    var utility = {};

    utility.getAllProject = function ($scope, projectId) {
        targetFactory.getAllProject(projectId).success(function (projectList) {
            $scope.projectOptions = projectList;
        }).error(function () {
            notificationFactory.error("ErrorMessage");
        });
    };

    utility.getMilestone = function (projectId, $scope) {
        targetFactory.getMilestoneOfProject(projectId).success(function (milestoneList) {
            $scope.milestoneOptions = milestoneList;
        }).error(function () {
            notificationFactory.error("ErrorMessage");
        });
    };

    utility.getFormOptions = function ($scope, KPICommonRepository) {
        var formOptionsCache = targetCacheFactory.get("formOptions");

        if (formOptionsCache) {
            $scope.formOptions = formOptionsCache;
        }
        else {
            KPICommonRepository.getFormOptionsExceptAll().then(function (listTargetType) {
                $scope.formOptions = listTargetType;
                targetCacheFactory.put("formOptions", listTargetType);
            });
        }
    };

    utility.getAllStatus = function ($scope) {
        targetFactory.getAllStatus().success(function (statusList) {
            $scope.statusOptions = statusList;
        }).error(function () {
            notificationFactory.error("ErrorMessage");
        });
    };

    utility.validateTarget = function (target) {
        if (!target) {
            return false;
        }
        else {
            if (!target.name) {
                return false;
            }
            if (!target.projectId) {
                return false;
            }
            if (!target.projectManager) {
                return false;
            }
        }

        return true;
    };

    utility.showMessageAfterValidate = function ($scope) {
        $scope.$watchCollection('[submitted, createForm.$invalid, createForm.targetName.$error.required, \
            createForm.targetProjectSelect.$error.required, createForm.targetProjectManagerSelect.$error.required ]',
            function (newVals) {
                if (newVals[0] && newVals[1]) {
                    if (newVals[4]) {
                        notificationFactory.error("ProjectManagerRequire");
                    }
                    if (newVals[3]) {
                        notificationFactory.error("ProjectRequire");
                    }
                    if (newVals[2]) {
                        notificationFactory.error("NameRequire");
                    }

                    $scope.submitted = false;
                }
            });
    };

    utility.validateTargetRating = function (employees, kpiItem) {
        var violation = false;

        angular.forEach(employees, function (employee) {
            if (employee.kpiItem) {
                if (employee.kpiItem.score > 0 && !employee.kpiItem.comment && kpiItem.isRequireComment) {
                    violation = true;
                }
            }
        });

        return violation;
    };

    utility.showMessageAfterValidateTargetRating = function (scope) {
        scope.$watchCollection('[submitted, violationRating]', function (newVals) {
            if (newVals[0] && newVals[1]) {
                notificationFactory.error("CommentRequire");
            }

            scope.submitted = false;
        });
    };

    utility.resetProjectInformation = function ($scope) {
        $scope.formIsProject = false;
        $scope.isSelectedProject = false;

        $scope.target.projectId = null;
        $scope.target.milestoneId = null;
    };

    utility.getTabHeading = function ($scope) {
        var targetInformationsHeadingCache = targetCacheFactory.get("targetInformationsHeading");

        if (targetInformationsHeadingCache) {
            $scope.targetInformationsHeading = targetInformationsHeadingCache;
        }
        else {
            $translate("TargetInformation").then(function (message) {
                $scope.targetInformationHeading = message;
                targetCacheFactory.put("targetInformationHeading", message);
            });
        }

        var targetMembersHeadingCache = targetCacheFactory.get("targetMembersHeading");

        if (targetMembersHeadingCache) {
            $scope.targetMembersHeading = targetMembersHeadingCache;
        }
        else {
            $translate("TargetMembers").then(function (message) {
                $scope.targetMembersHeading = message;
                targetCacheFactory.put("targetMembersHeading", message);
            });
        }

        var targetKPIHeadingCache = targetCacheFactory.get("targetKPIHeading");

        if (targetKPIHeadingCache) {
            $scope.targetKPIHeading = targetKPIHeadingCache;
        }
        else {
            $translate("TargetKPI").then(function (message) {
                $scope.targetKPIHeading = message;
                targetCacheFactory.put("targetKPIHeading", message);
            });
        }
    };

    utility.findProjectManagerName = function (projectManagerId, scope) {
        var projectManagerName = '';

        if (scope.projectManagerOptions) {
            angular.forEach(scope.projectManagerOptions, function (projectManager) {
                if (projectManager.employeeId === projectManagerId) {
                    projectManagerName = projectManager.nickName + ' (' + projectManager.fullName + ')';
                    return false;
                }
            });
        }

        return projectManagerName;
    };

    utility.calculateScoreTarget = function (epaInfo) {
        var totalGroupScore = 0,
            totalGroupWeight = 0,
            finalScore = 0;

        angular.forEach(epaInfo, function (kpiGroup) {
            var kpiGroupWeight = kpiGroup.kpiGroupWeight,
                totalItemScore = 0,
                totalItemWeight = 0,
                isSpreadKPI = kpiGroup.isSpreadKPI,
                averageWeight = isSpreadKPI && kpiGroup.kpiItems ? 100 / kpiGroup.kpiItems.length : 0;

            angular.forEach(kpiGroup.kpiItems, function (kpiItem) {
                if (kpiItem.score > 0) {
                    if (!isSpreadKPI) {
                        totalItemWeight += kpiItem.weight;
                        totalItemScore += kpiItem.score * 2 * kpiItem.weight;
                    }
                    else {
                        totalItemWeight += averageWeight;
                        totalItemScore += kpiItem.score * 2 * averageWeight;
                    }
                }
            });

            var groupScore = (totalItemScore / totalItemWeight) * kpiGroupWeight;

            if (isNaN(groupScore)) {
                totalGroupScore += 0;
            }
            else {
                totalGroupScore += groupScore;
                totalGroupWeight += kpiGroupWeight;
            }
        });

        finalScore = ((totalGroupScore / (totalGroupWeight * 2)) * 10).toFixed(1);

        if (isNaN(finalScore)) {
            finalScore = 0;
        }

        return finalScore;
    };
    return utility;
}]);


