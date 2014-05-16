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
			$scope.backToList = function () {
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
			$scope.createKPIUrl = '/KPI/CreateKPI/';
		};

		// edit KPI
		$scope.edit = function (kpiId) {
			$scope.editKPIUrl = '/KPI/EditKPI/' + kpiId;
			//$location.url('/KPI/EditKPI/' + kpiId);
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

				getKPI(filter.targetTypeId, filter.kpiGroupId, isDisabled);
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

		// Event read more / less text.
		commonFactory.maxLengthDescription($scope);

		$scope.readMore = function (item) {
			commonFactory.readMore(item);
		};

		$scope.readLess = function (item) {
			commonFactory.readLess(item);
		};
	} ]);

// Create New KPI
	kpiSettingModule.controller('CreateNewKPIController', ['$scope', '$routeParams',
	'$timeout', '$location', '$window', 'KPIRepository', 'CacheFactory', 'KPICommonRepository', 'notificationFactory',
	function ($scope, $routeParams, $timeout, $location, $window, KPIRepository, CacheFactory, KPICommonRepository, notificationFactory) {

		var currentChosenGroups = [];

		var formOptionsCache = CacheFactory.get("formOptions");

		if (formOptionsCache) {
			$scope.formOptions = formOptionsCache;
		}
		else {
			KPICommonRepository.getFormOptions().then(function (targetTypeList) {
				$scope.formOptions = targetTypeList;
				$scope.item = {
					targetTypeId: targetTypeList[0].targetTypeId
				};
				$scope.getGroupOptions(targetTypeList[0].targetTypeId);
				CacheFactory.put("formOptions", targetTypeList);
			});
		}

		var targetTypeId = $routeParams.form;
		var groupId = $routeParams.groupId;
		$scope.isCreatedFromKPI = false;

		$scope.getGroupOptions = function (targetTypeId) {
			if (targetTypeId === 0) {
				targetTypeId = null;
			}

			KPICommonRepository.getGroupByTargetTypeId(targetTypeId).then(function (groupList) {
				if (currentChosenGroups) {
					$.each(currentChosenGroups, function (index, group) {
						if (!checkExist(groupList, group.kpiGroupID)) {
							groupList.push(group);
						}
					});
				}

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

		// check if group exist in list group
		var checkExist = function (groupList, kpiGroupId) {
			var hasExist = false;

			$.each(groupList, function (index, group) {
				if (group.kpiGroupID === kpiGroupId) {
					hasExist = true;
					return false;
				}
			});

			return hasExist ? true : false;
		};

		// onchange function when select KPI Group
		$scope.getSelectedOrDeselectedGroup = function (event, param) {
			var index, selectedGroup, kpiGroupId;

			if (param.selected) {
				index = parseInt(param.selected),
				selectedGroup = $scope.groupOptions[index],
				kpiGroupId = selectedGroup.kpiGroupID;

				if (currentChosenGroups) {
					if (!checkExist(currentChosenGroups, kpiGroupId)) {
						currentChosenGroups.push(selectedGroup);
					}
				}
			}
			else if (param.deselected) {
				index = parseInt(param.deselected),
				selectedGroup = $scope.groupOptions[index],
				kpiGroupId = selectedGroup.kpiGroupID;

				if (currentChosenGroups) {
					var indexInCurrentChosen = currentChosenGroups.indexOf(selectedGroup);
					currentChosenGroups.splice(indexInCurrentChosen, 0);
				}

				if (selectedGroup.targetTypeId !== $scope.item.targetTypeId) {
					$scope.groupOptions.splice(index, 0);
					$scope.$digest();
				}
			}
		};

		// form select on change
		$scope.getGroupByTargetType = function (targetTypeId) {
			if (targetTypeId !== undefined) {
				$scope.getGroupOptions(targetTypeId);
			}
		};

		$scope.$watch('item.hasComment', function (newValue) {
			if (newValue === false) {
				$scope.item.isRequireComment = false;
			}
		});

		// validation
		KPICommonRepository.showMessageAfterValidateKPI($scope);

		$scope.save = function (item, isCreateAndContinued) {
			$scope.submitted = true;
			$scope.disabledSubmitButton = true;

			if (KPICommonRepository.validateKPI(item) === true) {
				KPIRepository.createKPI(item).success(function (response) {
					if (response.isSuccessful === true) {
						notificationFactory.success("CreatedSuccessfulMessage");

						if (isCreateAndContinued) {
							$scope.submitted = false;
							item.name = null;
							item.description = null;
							item.isRequireComment = false;
							item.hasComment = false;
						}
						else {
							$timeout(function () {
								if (!$scope.isCreatedFromKPI) {
									$window.history.back();
								}
								else {
									$location.url('/KPI/ViewKPI/');
								}
							}, 2000);
						}
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

		KPIRepository.getKPIById(kpiId).success(function (kpiItem) {
			$scope.item = kpiItem;
			KPICommonRepository.getGroupByTargetTypeId(null).then(function (groupList) {
				$scope.groupOptions = groupList;
			});
		}).error(function () {
			notificationFactory.error("ErrorMessage");
		});

		$scope.$watch('item.hasComment', function (newValue) {
			if (newValue === false) {
				$scope.item.isRequireComment = false;
			}
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