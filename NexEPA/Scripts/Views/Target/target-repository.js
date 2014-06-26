'use strict';
/*global targetModule*/

targetModule.factory('targetFactory', ['$http', function ($http) {
	return {
		getTarget: function (targetTypeId, startDate, endDate) {
			return $http.get('/api/Target/GetTarget',
				{ params: { targetTypeId: targetTypeId, startDate: startDate, endDate: endDate} });
		},
		getTargetById: function (targetId) {
			return $http.get('/api/Target/GetTargetById', { params: { targetId: targetId} });
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
			return $http['delete']('/api/Target', { params: { targetId: targetId} });
		},
		getAllProject: function (projectId) {
			return $http.get('/api/Core/GetAllProject', { params: { IncludeProjectId: projectId} });
		},
		getMilestoneOfProject: function (projectId) {
			return $http.get('/api/Target/GetMilestoneByProjectId', { params: { projectId: projectId} });
		},
		getAllStatus: function () {
			return $http.get('/api/Core/GetAllStatus');
		},
		getEmployeeByTargetId: function (targetId) {
			return $http.get('/api/Target/GetEmployeeByTargetId', { params: { targetId: targetId} });
		},
		insertTargetEmployee: function (employee) {
			return $http.post('/api/Target/InsertTargetEmployee', employee);
		},
		deleteTargetEmployee: function (targetId, employeeId) {
			return $http['delete']('/api/Target/DeleteTargetEmployee',
				{ params: { targetId: targetId, employeeId: employeeId} });
		},
		getKPIByTargetId: function (targetId, groupId) {
			return $http.get('/api/Target/GetKPIByTargetId', { params: { targetId: targetId, groupId: groupId} });
		},
		getProjectOrMilestoneDateById: function (projectId, miletoneId) {
			return $http.get('/api/Target/GetProjectOrMilestoneDateById', { params: { projectId: projectId, milestoneId: miletoneId} });
		},
		getKPIGroupByTargetId: function (targetId) {
			return $http.get('/api/Target/GetKPIGroupByTargetId', { params: { targetId: targetId} });
		},
		getPermission: function () {
			return $http.get('/api/Target/GetPermission');
		}
	};
} ]);

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
			else if (!target.projectId) {
				return false;
			}
		}

		return true;
	}; 

	utility.showMessageAfterValidate = function ($scope) {
		$scope.$watchCollection('[submitted, createForm.$invalid, createForm.targetName.$error.required, \
			createForm.targetProjectSelect.$error.required ]', function (newVals) {
				if (newVals[0] && newVals[1]) {
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

	return utility;
} ]);


