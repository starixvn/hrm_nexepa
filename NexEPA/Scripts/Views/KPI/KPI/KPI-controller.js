'use strict';
/*global kpiSettingModule*/

// View KPI
kpiSettingModule.controller("ViewKPIController", ['$scope', '$routeParams', '$location', '$modal', '$route', '$window',
	'KPIRepository', 'KPIGroupRepository', 'CacheFactory', 'KPICommonRepository', 'commonFactory', 'notificationFactory',
	function ($scope, $routeParams, $location, $modal, $route, $window, KPIRepository, KPIGroupRepository, CacheFactory,
		KPICommonRepository, commonFactory, notificationFactory) {
		var groupId = $routeParams.groupId;
		$scope.isViewAll = false;
		$scope.filter = {
			targetTypeId: "",
			kpiGroupId: "",
			active: ""
		};

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

		var getKPI = function (targetTypeId, groupId, isDisabled) {
			KPIRepository.viewKPI(targetTypeId, groupId, isDisabled).success(function (KPIList) {
				$scope.KPIList = KPIList;
				$scope.disabledSubmitButton = false;
			}).error(function () {
				notificationFactory.error("ErrorMessage");
				$scope.disabledSubmitButton = false;
			});
		};

		// get KPI
		getKPI(null, groupId, null);

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
						notificationFactory.success("DeleteSuccess");
					}
					else {
						notificationFactory.error("DeleteFail", response.message);
					}
				}).error(function () {
					notificationFactory.error("ErrorMessage");
				});
			});
		};

		// query data when filter
		$scope.query = function (filter) {
			if (filter) {
				var isDisabled = "";

				if (filter.active) {
					isDisabled = filter.active === 'true' ? false : true;
				}

				getKPI(filter.targetTypeId, filter.groupId, isDisabled);
				$scope.disabledSubmitButton = true;
			}
			else {
				$scope.disabledSubmitButton = false;
			}
		};

		// confirm active
		$scope.confirmActive = function (item) {
			var action = !item.isDisabled ? "deactivate" : "activate";
			var newItem = {
				kpiID: item.kpiId,
				isDisabled: !item.isDisabled
			};
			item.isDisabled = newItem.isDisabled;

			var modalConfirmInstance = $modal.open({
				templateUrl: '/templates/Common/modalConfirmContent.html',
				controller: "ModalConfirmController",
				resolve: {
					name: function () {
						return item.name;
					},
					action: function () {
						return action;
					}
				},
				windowClass: "modal-confirm-center"
			});

			var keyMessage = '';

			modalConfirmInstance.result.then(function () {
				if (newItem) {
					KPIRepository.updateKPIStatus(newItem).success(function (response) {
						if (response.isSuccessful === true) {
							keyMessage = action === "activate" ? "ActivationSuccess" : "DeactivationSuccess";
							notificationFactory.success(keyMessage);
						}
						else {
							keyMessage = action === "activate" ? "ActivationFail" : "DeactivationFail";
							item.isDisabled = !newItem.isDisabled;
							notificationFactory.error(keyMessage);
						}
					}).error(function () {
						keyMessage = action === "activate" ? "ActivationFail" : "DeactivationFail";
						item.isDisabled = !newItem.isDisabled;
						notificationFactory.error(keyMessage);
					});
				}
				else {
					item.isDisabled = !newItem.isDisabled;
				}
			}, function () {
				item.isDisabled = !newItem.isDisabled;
			});
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
	'$timeout', '$location', '$window', 'KPIRepository', 'CacheFactory', 'KPICommonRepository', 'notificationFactory',
	function ($scope, $routeParams, $timeout, $location, $window, KPIRepository, CacheFactory, KPICommonRepository, notificationFactory) {

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
				kpiGroupIds: [parseInt(groupId)]
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

		// validation
		KPICommonRepository.showMessageAfterValidateKPI($scope);

		$scope.save = function (item) {
			$scope.submitted = true;
			$scope.disabledSubmitButton = true;

			if (KPICommonRepository.validateKPI(item) === true) {
				KPIRepository.createKPI(item).success(function (response) {
					if (response.isSuccessful === true) {
						notificationFactory.success("CreatedSuccessfulMessage");
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
						notificationFactory.error('', response.message);
					}

					$scope.disabledSubmitButton = false;
				}).error(function () {
					notificationFactory.error("ErrorMessage");
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
	'$timeout', '$window', 'KPIRepository', 'CacheFactory', 'KPICommonRepository', 'notificationFactory',
	function ($scope, $routeParams, $location, $timeout, $window, KPIRepository, CacheFactory, KPICommonRepository, notificationFactory) {
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
			notificationFactory.error("ErrorMessage");
		});

		// validation
		KPICommonRepository.showMessageAfterValidateKPI($scope);

		$scope.save = function (kpiItem) {
			$scope.submitted = true;
			$scope.disabledSubmitButton = true;

			if (KPICommonRepository.validateKPI(kpiItem) === true) {
				KPIRepository.updateKPI(kpiItem).success(function (response) {
					if (response.isSuccessful === true) {
						notificationFactory.success("UpdatedSuccessfulMessage");
						$timeout(function () {
							$window.history.back();
						}, 2000);
					}
					else {
						notificationFactory.error("", response.message);
						$scope.disabledSubmitButton = false;
					}
				}).error(function () {
					notificationFactory.error("ErrorMessage");
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