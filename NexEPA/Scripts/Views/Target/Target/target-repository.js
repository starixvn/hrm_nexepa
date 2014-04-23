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
			return $http.put('/api/Target/', target);
		},
		deleteTarget: function (targetId) {
			return $http['delete']('/api/Target', { params: { targetId: targetId} });
		},
		getAllProject: function () {
			return $http.get('/api/Core/GetAllProject');
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
		getKPIByTargetId: function (targetId, groupId) {
			return $http.get('/api/Target/GetKPIByTargetId', { params: { targetId: targetId, groupId: groupId } });
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

			$scope.calendar.startDateOpened = true;
			$scope.calendar.endDateOpened = false;
		};

		utility.openPopupEndDate = function ($event, $scope) {
			$event.preventDefault();
			$event.stopPropagation();

			$scope.calendar.endDateOpened = true;
			$scope.calendar.startDateOpened = false;
		};

		utility.showErrorMessage = function ($scope) {
			$scope.hasError = true;
			$translate("ErrorMessage").then(function (errorMessage) {
				$scope.errorMessage = errorMessage;
			});
		};

		utility.showErrorMessageOfMember = function ($scope) {
			$scope.targetMemberSuccess = false;
			$translate("ErrorMessage").then(function (errorMessage) {
				$scope.memberErrorMessage = errorMessage;
			});
		};

		utility.showErrorMessageOfTargetInformations = function ($scope) {
			$scope.targetInfoSuccess = false;
			$translate("ErrorMessage").then(function (errorMessage) {
				$scope.infoErrorMessage = errorMessage;
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
				$translate("TargetInformations").then(function (message) {
					$scope.targetInformationsHeading = message;
					targetCacheFactory.put("targetInformationsHeading", message);
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

// search employee auto-complete directive
targetModule.directive('searchAutoComplete', ['$timeout','employeeRepository', function ($timeout, employeeRepository) {
		return {
			scope: { searchKey: "&searchKey", employee: "=employee", projectId: "&projectId" },
			link: function (scope, iElement) {
				iElement.autocomplete({
					source: function (request, response) {
						var searchKey = scope.searchKey(),
							projectId = scope.projectId();

						employeeRepository.getEmployeeByProjectId(projectId, searchKey).success(function (employeeList) {
							var data = [];
							angular.forEach(employeeList, function (employee) {
								data.push(
								{ value: employee.nickName + ' (' + employee.fullName + ')',
									id: employee.employeeId,
									employee: employee
								});
							});

							response(data);
						});
					},
					select: function (event, ui) {
						scope.employee = ui.item.employee;
						$timeout(function () {
							iElement.trigger('input');
						}, 0);
					}
				});
			}
		};
}]);

