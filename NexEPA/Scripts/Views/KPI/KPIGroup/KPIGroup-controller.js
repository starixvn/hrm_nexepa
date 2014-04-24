'use strict';
/*global kpiSettingModule*/

// View KPI Group
kpiSettingModule.controller("GetAllKPIGroupController", ['$scope', '$location', '$route',
'$modal', 'CacheFactory', 'KPIGroupRepository', 'KPICommonRepository', 'commonFactory',
	function ($scope, $location, $route, $modal, CacheFactory, KPIGroupRepository, KPICommonRepository, commonFactory) {
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
					commonFactory.showErrorMessageBox($scope, "ErrorMessage");
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
						var index = $scope.KPIGroupList.indexOf(group);
						$scope.KPIGroupList.splice(index, 1);
						commonFactory.showInfoMessageBox($scope, "DeleteSuccess");
					}
					else {
						commonFactory.showErrorMessageBox($scope, "DeleteFail", response.message);
					}
				}).error(function () {
					commonFactory.showErrorMessageBox($scope, "DeleteFail");
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

			var keyMessage = '';

			modalConfirmInstance.result.then(function () {
				KPIGroupRepository.updateGroup(newGroup).success(function (response) {
					if (response.isSuccessful === true) {
						keyMessage = action === "activate" ? "ActivationSuccess" : "DeactivationSuccess";
					}
					else {
						keyMessage = action === "activate" ? "ActivationFail" : "DeactivationFail";
					}

					commonFactory.showInfoMessageBox($scope, keyMessage);
				}).error(function () {
					keyMessage = action === "activate" ? "ActivationFail" : "DeactivationFail";
					commonFactory.showInfoMessageBox($scope, keyMessage);
				});
			}, function () {
				group.isDisabled = !newGroup.isDisabled;
			});
		};

		// filter active group or not
		$scope.activeFilter = 'all';

		$scope.filterActive = function (group) {
			if ($scope.activeFilter === 'all') {
				return true;
			}
			else if ($scope.activeFilter === 'active') {
				if (!group.isDisabled) {
					return true;
				}
			}
			else if ($scope.activeFilter === 'inactive') {
				if (group.isDisabled) {
					return true;
				}
			}

			return false;
		};

		// reload
		$scope.reload = function () {
			$route.reload();
		};
	} ]);

// Create New KPI Group
	kpiSettingModule.controller("CreateNewKPIGroupController", ['$scope', '$location', '$timeout',
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
							KPICommonRepository.showErrorMessage($scope, "ErrorMessage", response.message);
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
	} ]);

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
							KPICommonRepository.showErrorMessage($scope, "ErrorMessage", response.message);
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
	}]);