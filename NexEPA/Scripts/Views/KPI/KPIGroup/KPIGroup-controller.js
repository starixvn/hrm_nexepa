'use strict';
/*global kpiSettingModule*/

var calculateTotalWeightAndShowWarningMessage = function (group, $scope, $translate, KPICommonRepository) {
	if (group.targetTypeId && group.weight) {
		KPICommonRepository.getGroupByTargetTypeId(group.targetTypeId).then(function (groupList) {
			var totalWeight = group.weight;

			for (var i = 0; i < groupList.length; i++) {
				totalWeight += groupList[i].weight;
			}

			if (totalWeight > 100) {
				KPICommonRepository.showErrorMessage($scope, "WeightWarningMessage");
			}
			else {
				$scope.success = true;
				$scope.errorMessage = "";
			}
		});

		return $scope;
	}
};

// View KPI Group
kpiSettingModule.controller("GetAllKPIGroupController",['$scope', '$location', '$route', 
'$modal', 'CacheFactory', 'KPIGroupRepository', 'KPICommonRepository',
	function ($scope, $location, $route, $modal, CacheFactory, KPIGroupRepository, KPICommonRepository) {
		var formOptionsCache = CacheFactory.get("formOptions");

		if (formOptionsCache) {
			$scope.formOptions = formOptionsCache;
		}
		else {
			KPICommonRepository.getFormOptions().then(function (listTargetType) {
				$scope.formOptions = listTargetType;
				CacheFactory.put("formOptions", listTargetType);
			});
		}

		var getAllGroup = function (targetTypeId) {
			KPIGroupRepository.getAll(targetTypeId)
				.success(function (groupList) {
					$scope.KPIGroupList = groupList;
					$scope.disabledSubmitButton = false;
				})
				.error(function () {
					KPICommonRepository.showErrorMessage($scope, "ErrorMessage");
					$scope.disabledSubmitButton = false;
				});
		};

		getAllGroup(null);

		// create new KPI group
		$scope.create = function () {
			$location.url('/KPI/CreateNewKPIGroup/');
		};

		// update
		$scope.edit = function (groupId) {
			$location.url('/KPI/EditKPIGroup/' + groupId);
		};

		// delete
		$scope.openDeletePopup = function (group) {
			var groupId = group.kpiGroupID,
				groupName = group.name;

			var modalDeleteInstance = $modal.open({
				templateUrl: '/templates/Common/ModalConfirmContent.html',
				controller: "ModalConfirmController",
				resolve: {
					name: function () {
						return groupName;
					},
					action: function () {
						return "delete";
					}
				},
				windowClass: "modal-confirm-center"
			});

			modalDeleteInstance.result.then(function () {
				KPIGroupRepository.deleteGroup(groupId).success(function (response) {
					if (response.isSuccessful === true) {
						$scope.success = true;
						$route.reload();
					}
					else {
						$scope.success = false;
						$scope.errorMessage = response.message;
					}
				}).error(function () {
					KPICommonRepository.showErrorMessage($scope, "ErrorMessage");
				});
			});
		};

		// add new KPI
		$scope.addKPI = function (targetTypeId, kpiGroupId) {
			$location.search('form', targetTypeId).search('groupId', kpiGroupId).path("/KPI/CreateKPI/");
		};

		// view KPI
		$scope.viewKPI = function (targetTypeId, kpiGroupId) {
			$location.search('form', targetTypeId).search('groupId', kpiGroupId).path("/KPI/ViewKPI/");
		};

		// query data when filter
		$scope.query = function (targetTypeId) {
			$scope.disabledSubmitButton = true;
			getAllGroup(targetTypeId);
		};

		// confirm active group or not
		$scope.confirmActive = function (group) {
			var action = !group.isDisabled ? "deactivate" : "activate";
			var newGroup = group;
			newGroup.isDisabled = !group.isDisabled;

			var modalConfirmInstance = $modal.open({
				templateUrl: '/templates/Common/modalConfirmContent.html',
				controller: "ModalConfirmController",
				resolve: {
					name: function () {
						return newGroup.name;
					},
					action: function () {
						return action;
					}
				},
				windowClass: "modal-confirm-center"
			});

			modalConfirmInstance.result.then(function () {
				KPIGroupRepository.updateGroup(newGroup).success(function (response) {
					if (response.isSuccessful === true) {
						$scope.success = true;
					}
					else {
						$scope.success = false;
						$scope.errorMessage = response.message;
					}
				}).error(function () {
					KPICommonRepository.showErrorMessage($scope, "ErrorMessage");
				});
			}, function () {
				group.isDisabled = !newGroup.isDisabled;
			});
		};

		// reload
		$scope.reload = function () {
			$route.reload();
		};
	}]);

// Create New KPI Group
	kpiSettingModule.controller("CreateNewKPIGroupController",['$scope', '$location', '$timeout', 
	'$translate', '$window', 'KPIGroupRepository', 'CacheFactory', 'KPICommonRepository',
	function ($scope, $location, $timeout, $translate, $window, KPIGroupRepository, CacheFactory, KPICommonRepository) {
		var formOptionsCache = CacheFactory.get("formOptions");

		if (formOptionsCache) {
			$scope.formOptions = formOptionsCache;
		}
		else {
			KPICommonRepository.getFormOptions().then(function (listTargetType) {
				$scope.formOptions = listTargetType;
				CacheFactory.put("formOptions", listTargetType);
			});
		}

		//save new KPI group
		$scope.save = function (group) {
			$scope.submitted = true;
			$scope.disabledSubmitButton = true;

			if (KPICommonRepository.validateGroup(group) === true) {
				group.isDisabled = !group.isActive;

				KPIGroupRepository.addGroup(group)
					.success(function (response) {
						if (response.isSuccessful === true) {
							KPICommonRepository.showSuccessMessage($scope, "CreatedSuccessfulMessage");
							$timeout(function () {
								$location.url('/KPI/GetAllKPIGroup/');
							}, 2000);
						}
						else {
							$scope.success = false;
							$scope.errorMessage = response.message;
							$scope.disabledSubmitButton = false;
						}
					})
					.error(function () {
						KPICommonRepository.showErrorMessage($scope, "ErrorMessage");
						$scope.disabledSubmitButton = false;
					});
			}
			else {
				$scope.disabledSubmitButton = false;
			}
		};

		$scope.cancel = function () {
			$window.history.back();
		};

		// weight on change
		$scope.weightOnChange = function (group) {
			calculateTotalWeightAndShowWarningMessage(group, $scope, $translate, KPICommonRepository);
		};

		// form on change
		$scope.formOnChange = function (group) {
			calculateTotalWeightAndShowWarningMessage(group, $scope, $translate, KPICommonRepository);
		};
	}]);

// Edit KPI Group
	kpiSettingModule.controller("EditKPIGroupController",['$scope', '$location', 
	'$routeParams', '$timeout', '$route', '$translate', '$window', 'KPIGroupRepository', 'CacheFactory', 'KPICommonRepository',
	function ($scope, $location, $routeParams, $timeout, $route, $translate, $window, KPIGroupRepository, CacheFactory, KPICommonRepository) {
		var groupId = $routeParams.id;

		KPIGroupRepository.getGroupById(groupId).success(function (group) {
			$scope.editGroup = group;
		});

		var formOptionsCache = CacheFactory.get("formOptions");

		if (formOptionsCache) {
			$scope.formOptions = formOptionsCache;
		}
		else {
			KPICommonRepository.getFormOptions().then(function (listTargetType) {
				$scope.formOptions = listTargetType;
				CacheFactory.put("formOptions", listTargetType);
			});
		}

		$scope.update = function (group) {
			$scope.submitted = true;
			$scope.disabledSubmitButton = true;

			if (KPICommonRepository.validateGroup(group) === true) {
				var newGroup = group;
				newGroup.isDisabled = !group.isActive;

				KPIGroupRepository.updateGroup(newGroup)
					.success(function (response) {
						if (response.isSuccessful === true) {
							KPICommonRepository.showSuccessMessage($scope, "UpdatedSuccessfulMessage");
							$timeout(function () {
								$location.url('/KPI/GetAllKPIGroup/');
							}, 2000);
						}
						else {
							$scope.success = false;
							$scope.errorMessage = response.message;
							$scope.disabledSubmitButton = false;
						}
					})
					.error(function () {
						KPICommonRepository.showErrorMessage($scope, "ErrorMessage");
						$scope.disabledSubmitButton = false;
					});
			}
			else {
				$scope.disabledSubmitButton = false;
			}
		};

		$scope.cancel = function () {
			$window.history.back();
		};

		// weight on change
		$scope.weightOnChange = function (group) {
			calculateTotalWeightAndShowWarningMessage(group, $scope, $translate, KPICommonRepository);
		};

		// form on change
		$scope.formOnChange = function (group) {
			calculateTotalWeightAndShowWarningMessage(group, $scope, $translate, KPICommonRepository);
		};
	}]);