'use strict';
/*global targetModule*/

targetModule.factory('targetFactory', ['$http', function ($http) {
	return {
		getTarget: function (targetTypeId, startDate, endDate) {
			return $http.get('/api/Target/GetTarget',
				{ params: { targetTypeId: targetTypeId, startDate: startDate, endDate: endDate} });
		},
		getTargetById: function (targetId) {
			return $http.get('/api/Target/GetTargetById', { params: { id: targetId} });
		},
		createTarget: function (target) {
			return $http.post('/api/Target/', target);
		},
		editTarget: function (target) {
			return $http.put('/api/Target/', target);
		},
		getAllProject: function () {
			return $http.get('/api/Target/GetAllProject');
		},
		getMilestoneOfProject: function (projectId) {
			return $http.get('/api/Target/GetMilestoneByProjectId', { params: { projectId: projectId} });
		}
	};
} ]);

targetModule.factory('targetCacheFactory', ['$cacheFactory', function ($cacheFactory) {
	return $cacheFactory('targetCache');
}]);

targetModule.factory('targetUtility', ['$translate','targetFactory','targetCacheFactory',function ($translate, targetFactory, targetCacheFactory) {
	var utility = {};

	utility.getAllProject = function ($scope) {
		targetFactory.getAllProject().success(function (projectList) {
			$scope.projectOptions = projectList;
		}).error(function () {
			utility.showErrorMessage($scope, $translate);
		});
	};

	utility.getMilestone = function (projectId, $scope) {
		targetFactory.getMilestoneOfProject(projectId).success(function (milestoneList) {
			$scope.milestoneOptions = milestoneList;
		}).error(function () {
			utility.showErrorMessage($scope, $translate);
		});
	};

	utility.getFormOptions = function ($scope, CommonRepository) {
		var formOptionsCache = targetCacheFactory.get("formOptions");

		if (formOptionsCache) {
			$scope.formOptions = formOptionsCache;
		}
		else {
			CommonRepository.getFormOptions().then(function (listTargetType) {
				$scope.formOptions = listTargetType;
				targetCacheFactory.put("formOptions", listTargetType);
			});
		}
	};

	utility.validateTarget = function (target, formIsProject) {
		if (target === undefined) {
			return false;
		}
		else {
			if (target.name === undefined) {
				return false;
			}
			else if (target.targetTypeId === undefined || target.targetTypeId === null) {
				return false;
			}
			else if (formIsProject) {
				if (target.projectId === undefined || target.projectId === null) {
					return false;
				}
			}
		}

		return true;
	};

	utility.openPopupStartDate = function ($event, $scope) {
		$event.preventDefault();
		$event.stopPropagation();

		$scope.startDateOpened = true;
		$scope.endDateOpened = false;
	};

	utility.openPopupEndDate = function ($event, $scope) {
		$event.preventDefault();
		$event.stopPropagation();

		$scope.endDateOpened = true;
		$scope.startDateOpened = false;
	};

	utility.showErrorMessage = function ($scope) {
		$scope.hasError = true;
		$translate("ErrorMessage").then(function (errorMessage) {
			$scope.errorMessage = errorMessage;
		});
	};

	utility.resetProjectInformation = function ($scope) {
		$scope.formIsProject = false;
		$scope.isSelectedProject = false;

		$scope.target.projectId = null;
		$scope.target.milestoneId = null;
	};

	return utility;
}]);

