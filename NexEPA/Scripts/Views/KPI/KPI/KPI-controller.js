'use strict';
/*global kpiSettingModule*/

// View KPI
kpiSettingModule.controller("ViewKPIController", ['$scope', '$routeParams', '$location', '$modal', '$route', '$window',
	'KPIRepository', 'KPIGroupRepository', 'CacheFactory', 'KPICommonRepository', 'commonFactory',
	function ($scope, $routeParams, $location, $modal, $route, $window, KPIRepository, KPIGroupRepository, CacheFactory,
		KPICommonRepository, commonFactory) {
		var groupId = $routeParams.groupId;
		$scope.isViewAll = false;
		$scope.isActiveFilter = true;
		$scope.isCollapsed = true;

		if (groupId) {
			$scope.back = function () {
				$window.history.back();
			};
		}
		else {
			$scope.isViewAll = true;
			var formOptionsCache = CacheFactory.get("formOptions");

			if (formOptionsCache) {
				$scope.formOptions = formOptionsCache;
			}
			else {
				KPICommonRepository.getFormOptions().then(function (targetTypeList) {
					$scope.formOptions = targetTypeList;
					CacheFactory.put("formOptions", targetTypeList);
				});
			}

			KPIGroupRepository.getAll().success(function (groupList) {
				$scope.groupOptions = groupList;
			});
		}

		var getKPI = function (targetTypeId, groupId) {
			KPIRepository.viewKPI(targetTypeId, groupId).success(function (KPIList) {
				$scope.KPIList = KPIList;
				$scope.disabledSubmitButton = false;
			}).error(function () {
				KPICommonRepository.showErrorMessage($scope, "ErrorMessage");
				$scope.disabledSubmitButton = false;
			});
		};

		// get KPI
		getKPI(null, groupId);

		// add new KPI
		$scope.create = function () {
			$location.path('/KPI/CreateKPI/');
		};

		// edit KPI
		$scope.edit = function (kpiId) {
			$location.url('/KPI/EditKPI/' + kpiId);
		};

		// delete KPI
		$scope.openDeletePopup = function (item) {
			var kpiId = item.kpiId,
				kpiName = item.name;

			var modalDeleteInstance = $modal.open({
				templateUrl: '/templates/Common/ModalConfirmContent.html',
				controller: "ModalConfirmController",
				resolve: {
					name: function () {
						return kpiName;
					},
					action: function () {
						return "delete";
					}
				},
				windowClass: "modal-confirm-center"
			});

			modalDeleteInstance.result.then(function () {
				KPIRepository.deleteKPI(kpiId).success(function (response) {
					if (response.isSuccessful === true) {
						var index = $scope.KPIList.indexOf(item);
						$scope.KPIList.splice(index, 1);
						commonFactory.showInfoMessageBox($scope, "DeleteSuccess");
					}
					else {
						commonFactory.showErrorMessageBox($scope, "DeleteFail", response.message);
					}
				}).error(function () {
					commonFactory.showErrorMessageBox($scope, "ErrorMessage");
				});
			});
		};

		// query data when filter
		$scope.query = function (targetTypeId, groupId) {
			getKPI(targetTypeId, groupId);
			$scope.disabledSubmitButton = true;
		};

		// confirm active
		$scope.confirmActive = function (item) {
			var action = !item.isDisabled ? "deactivate" : "activate";
			var newItem = item;
			newItem.isDisabled = !item.isDisabled;

			var modalConfirmInstance = $modal.open({
				templateUrl: '/templates/Common/modalConfirmContent.html',
				controller: "ModalConfirmController",
				resolve: {
					name: function () {
						return newItem.name;
					},
					action: function () {
						return action;
					}
				},
				windowClass: "modal-confirm-center"
			});

			var keyMessage = '';

			modalConfirmInstance.result.then(function () {
				KPIRepository.updateKPI(newItem).success(function (response) {
					if (response.isSuccessful === true) {
						keyMessage = action === "activate" ? "ActivationSuccess" : "DeactivationSuccess";
					}
					else {
						keyMessage = action === "activate" ? "ActivationFail" : "DeactivationFail";
						item.isDisabled = !newItem.isDisabled;
					}

					commonFactory.showInfoMessageBox($scope, keyMessage);
				}).error(function () {
					keyMessage = action === "activate" ? "ActivationFail" : "DeactivationFail";
					commonFactory.showInfoMessageBox($scope, keyMessage);
					item.isDisabled = !newItem.isDisabled;
				});
			}, function () {
				item.isDisabled = !newItem.isDisabled;
			});
		};

		// filter active KPI or not
		$scope.activeFilter = 'all';

		$scope.filterActive = function (item) {
			if ($scope.activeFilter === 'all') {
				return true;
			}
			else if ($scope.activeFilter === 'active') {
				if (!item.isDisabled) {
					return true;
				}
			}
			else if ($scope.activeFilter === 'inactive') {
				if (item.isDisabled) {
					return true;
				}
			}

			return false;
		};

		// reload
		$scope.reload = function () {
			if ($scope.isViewAll) {
				$route.reload();
			}
			else {
				$location.url('/KPI/ViewKPI/');
			}
		};
	} ]);

// Create New KPI
	kpiSettingModule.controller('CreateNewKPIController', ['$scope', '$routeParams',
	'$timeout', '$location', '$window', 'KPIRepository', 'CacheFactory', 'KPICommonRepository',
	function ($scope, $routeParams, $timeout, $location, $window, KPIRepository, CacheFactory, KPICommonRepository) {

		var formOptionsCache = CacheFactory.get("formOptions");

		if (formOptionsCache) {
			$scope.formOptions = formOptionsCache;
		}
		else {
			KPICommonRepository.getFormOptions().then(function (targetTypeList) {
				$scope.formOptions = targetTypeList;
				CacheFactory.put("formOptions", targetTypeList);
			});
		}

		var targetTypeId = $routeParams.form;
		var groupId = $routeParams.groupId;
		$scope.isCreatedFromKPI = false;

		var getGroupOptions = function (targetTypeId) {
			KPICommonRepository.getGroupByTargetTypeId(targetTypeId).then(function (groupList) {
				$scope.groupOptions = groupList;
			});
		};

		if (targetTypeId !== undefined) {
			getGroupOptions(targetTypeId);

			$scope.item = {
				targetTypeId: parseInt(targetTypeId),
				kpiGroupId: parseInt(groupId)
			};
		}
		else {
			$scope.isCreatedFromKPI = true;
		}

		// form select on change
		$scope.getGroupByTargetType = function (targetTypeId) {
			if (targetTypeId !== undefined) {
				getGroupOptions(targetTypeId);
			}
			else {
				$scope.groupOptions = [];
			}

			$scope.item.kpiGroupId = "";
		};

		$scope.save = function (item) {
			$scope.submitted = true;
			$scope.disabledSubmitButton = true;

			if (KPICommonRepository.validateKPI(item) === true) {
				KPIRepository.createKPI(item).success(function (response) {
					if (response.isSuccessful === true) {
						KPICommonRepository.showSuccessMessage($scope, "CreatedSuccessfulMessage");
						$timeout(function () {
							if (!$scope.isCreatedFromKPI) {
								$window.history.back();
							}
							else {
								$location.url('/KPI/ViewKPI/');
							}
						}, 2000);
					}
					else {
						KPICommonRepository.showErrorMessage($scope, "ErrorMessage", response.message);
						$scope.disabledSubmitButton = false;
					}
				}).error(function () {
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

// Edit KPI
	kpiSettingModule.controller("EditKPIController", ['$scope', '$routeParams', '$location',
	'$timeout', '$window', 'KPIRepository', 'CacheFactory', 'KPICommonRepository',
	function ($scope, $routeParams, $location, $timeout, $window, KPIRepository, CacheFactory, KPICommonRepository) {
		var kpiId = $routeParams.id;
		var formOptionsCache = CacheFactory.get("formOptions");

		if (formOptionsCache) {
			$scope.formOptions = formOptionsCache;
		}
		else {
			KPICommonRepository.getFormOptions().then(function (targetTypeList) {
				$scope.formOptions = targetTypeList;
				CacheFactory.put("formOptions", targetTypeList);
			});
		}

		KPIRepository.getKPIById(kpiId).success(function (kpiItem) {
			$scope.item = kpiItem;
			KPICommonRepository.getGroupByTargetTypeId(kpiItem.targetTypeId).then(function (groupList) {
				$scope.groupOptions = groupList;
			});
		}).error(function () {
			KPICommonRepository.showErrorMessage($scope, "ErrorMessage");
		});

		$scope.save = function (kpiItem) {
			$scope.submitted = true;
			$scope.disabledSubmitButton = true;

			if (KPICommonRepository.validateKPI(kpiItem) === true) {
				KPIRepository.updateKPI(kpiItem).success(function (response) {
					if (response.isSuccessful === true) {
						KPICommonRepository.showSuccessMessage($scope, "UpdatedSuccessfulMessage");
						$timeout(function () {
							$window.history.back();
						}, 2000);
					}
					else {
						KPICommonRepository.showErrorMessage($scope, "ErrorMessage", response.message);
						$scope.disabledSubmitButton = false;
					}
				}).error(function () {
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