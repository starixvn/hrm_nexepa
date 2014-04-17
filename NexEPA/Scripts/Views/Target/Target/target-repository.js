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
			return $http.post('/api/Target/', target);
		},
		editTarget: function (target) {
			return $http.put('/api/Target/', target);
		},
		deleteTarget: function (targetId) {
			return $http['delete']('/api/Target', { params: { targetId: targetId} });
		},
		getAllProject: function () {
			return $http.get('/api/Target/GetAllProject');
		},
		getMilestoneOfProject: function (projectId) {
			return $http.get('/api/Target/GetMilestoneByProjectId', { params: { projectId: projectId} });
		},
		getAllStatus: function () {
			return $http.get('/api/Target/GetAllStatus');
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
		getKPIByTargetId: function (targetId) {
			return $http.get('/api/Target/GetKPIByTargetId', { params: { targetId: targetId } });
		}
	};
} ]);

targetModule.factory('targetCacheFactory', ['$cacheFactory', function ($cacheFactory) {
	return $cacheFactory('targetCache');
}]);

targetModule.factory('targetUtility', ['$translate', 'targetFactory', 'targetCacheFactory',
		function ($translate, targetFactory, targetCacheFactory) {
			var utility = {};

			utility.getAllProject = function ($scope) {
				targetFactory.getAllProject().success(function (projectList) {
					$scope.projectOptions = projectList;
				}).error(function () {
					utility.showErrorMessage($scope);
				});
			};

			utility.getMilestone = function (projectId, $scope) {
				targetFactory.getMilestoneOfProject(projectId).success(function (milestoneList) {
					$scope.milestoneOptions = milestoneList;
				}).error(function () {
					utility.showErrorMessage($scope);
				});
			};

			utility.getFormOptions = function ($scope, KPICommonRepository) {
				var formOptionsCache = targetCacheFactory.get("formOptions");

				if (formOptionsCache) {
					$scope.formOptions = formOptionsCache;
				}
				else {
					KPICommonRepository.getFormOptions().then(function (listTargetType) {
						$scope.formOptions = listTargetType;
						targetCacheFactory.put("formOptions", listTargetType);
					});
				}
			};

			utility.getAllStatus = function ($scope) {
				targetFactory.getAllStatus().success(function (statusList) {
					$scope.statusOptions = statusList;
				}).error(function () {
					utility.showErrorMessage($scope);
				});
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
		} ]);

// search employee auto-complete directive
targetModule.directive('searchAutoComplete', function ($timeout, employeeRepository) {
	return {
		scope: { searchKey: "&searchKey", employeeId: "=employeeId" },
		link: function (scope, iElement) {
			iElement.autocomplete({
				source: function (request, response) {
					var searchKey = scope.searchKey();
					employeeRepository.getAllEmployee(searchKey, null).success(function (employeeList) {
						var data = [];
						angular.forEach(employeeList, function (employee) {
							data.push(
								{ value: employee.nickName + ' (' + employee.fullName + ')',
									id: employee.employeeId
								});
						});
					response(data);
					});
				},
				select: function (event, ui) {
					scope.employeeId = ui.item.id;
					$timeout(function () {
						iElement.trigger('input');
					}, 0);
				}
			});
		}
	};
});

