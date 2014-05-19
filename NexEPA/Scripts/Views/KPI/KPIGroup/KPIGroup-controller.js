'use strict';
/*global kpiSettingModule*/

// View KPI Group
kpiSettingModule.controller("GetAllKPIGroupController", ['$scope', '$location', '$route',
'$modal', 'KPIGroupRepository', 'KPICommonRepository', 'commonFactory', 'notificationFactory', 'ErrorService',
	function ($scope, $location, $route, $modal, KPIGroupRepository, KPICommonRepository, commonFactory, notificationFactory, ErrorService) {
		$scope.filter = {
			targetTypeId: "",
			active: ""
		};

		KPICommonRepository.getFormOptionsExceptAll().then(function (listTargetType) {
			$scope.formOptions = listTargetType;
		});

		var getAllGroup = function (targetTypeId, isDisabled) {
			KPIGroupRepository.getAll(targetTypeId, isDisabled)
				.success(function (groupList) {
					$scope.KPIGroupList = groupList;
					$scope.disabledSubmitButton = false;
				})
				.error(function (response, status) {
					if (status === 405) {
						ErrorService.errorHandle(status);
					}
					else {
						notificationFactory.error("ErrorMessage");
					}

					$scope.disabledSubmitButton = false;
				});
		};

		getAllGroup(null, null);

		// create new KPI group
		$scope.create = function () {
			$scope.createKPIGroupUrl = '/KPI/CreateNewKPIGroup/';
		};

		// update
		$scope.edit = function (groupId) {
			$scope.editKPIGroupUrl = '/KPI/EditKPIGroup/' + groupId;
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
						notificationFactory.success("DataChangeSuccessfully");
					}
					else {
						notificationFactory.error("DataChangeFail", response.message);
					}
				}).error(function () {
					notificationFactory.error("DataChangeFail");
				});
			});
		};

		// add new KPI
		$scope.addKPI = function (targetTypeId, kpiGroupId) {
			$scope.addKPIUrl = '/KPI/CreateKPI' + '?form=' + targetTypeId + '&groupId=' + kpiGroupId;
		};

		// view KPI
		$scope.viewKPI = function (targetTypeId, kpiGroupId) {
			$scope.viewKPIUrl = '/KPI/ViewKPI' + '?form=' + targetTypeId + '&groupId=' + kpiGroupId;
		};

		// query data when filter
		$scope.query = function (filter) {
			if (filter) {
				var isDisabled = "";

				if (filter.active) {
					isDisabled = filter.active === 'true' ? false : true;
				}

				getAllGroup(filter.targetTypeId, isDisabled);
				$scope.disabledSubmitButton = true;
			}
			else {
				$scope.disabledSubmitButton = false;
			}
		};

		// confirm active group or not
		$scope.confirmActive = function (group) {
			var action = !group.isDisabled ? "deactivate" : "activate";
			var newGroup = {
				kpiGroupId: group.kpiGroupID,
				isDisabled: !group.isDisabled
			};
			group.isDisabled = !group.isDisabled;

			var modalConfirmInstance = $modal.open({
				templateUrl: '/templates/Common/modalConfirmContent.html',
				controller: "ModalConfirmController",
				resolve: {
					name: function () {
						return group.name;
					},
					action: function () {
						return action;
					}
				},
				windowClass: "modal-confirm-center"
			});

			var keyMessage = '';

			modalConfirmInstance.result.then(function () {
				KPIGroupRepository.updateGroupStatus(newGroup).success(function (response) {
					if (response.isSuccessful === true) {
						keyMessage = action === "activate" ? "ActivationSuccess" : "DeactivationSuccess";
						notificationFactory.success(keyMessage);
					}
					else {
						keyMessage = action === "activate" ? "ActivationFail" : "DeactivationFail";
						group.isDisabled = !newGroup.isDisabled;
						notificationFactory.error(keyMessage);
					}

				}).error(function () {
					keyMessage = action === "activate" ? "ActivationFail" : "DeactivationFail";
					notificationFactory.error(keyMessage);
					group.isDisabled = !newGroup.isDisabled;
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

		// Event read more / less text.
		commonFactory.maxLengthDescription($scope);

		$scope.readMore = function (item) {
			commonFactory.readMore(item);
		};

		$scope.readLess = function (item) {
			commonFactory.readLess(item);
		};
	} ]);

// Create New KPI Group
kpiSettingModule.controller("CreateNewKPIGroupController", ['$scope', '$location', '$timeout',
'$translate', '$window', 'KPIGroupRepository', 'KPICommonRepository', 'notificationFactory', 'ErrorService',
function ($scope, $location, $timeout, $translate, $window, KPIGroupRepository, KPICommonRepository, notificationFactory, ErrorService) {

		// get all from options
		KPICommonRepository.getFormOptions().then(function (targetTypeList) {
			$scope.formOptions = targetTypeList;
		});

		// validation
		KPICommonRepository.showMessageAfterValidateKPIGroup($scope);

		//save new KPI group
		$scope.save = function (group) {
			$scope.submitted = true;
			$scope.disabledSubmitButton = true;

			if (KPICommonRepository.validateGroup(group) === true) {
				group.isDisabled = !group.isActive;

				KPIGroupRepository.addGroup(group)
					.success(function (response) {
						if (response.isSuccessful === true) {
							notificationFactory.success("DataChangeSuccessfully");
							$timeout(function () {
								$location.url('/KPI/GetAllKPIGroup/');
							}, 2000);
						}
						else {
							notificationFactory.error("", response.message);
						}

						$scope.disabledSubmitButton = false;
					})
					.error(function (response, status) {
						if (status === 405) {
							ErrorService.errorHandle(status);
						}
						else {
							notificationFactory.error("ErrorMessage");
						}

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
	kpiSettingModule.controller("EditKPIGroupController", ['$scope', '$location', '$routeParams', '$timeout', '$route',
	'$translate', '$window', 'KPIGroupRepository', 'KPICommonRepository', 'notificationFactory', 'ErrorService',
	function ($scope, $location, $routeParams, $timeout, $route, $translate, $window, KPIGroupRepository,
	KPICommonRepository, notificationFactory, ErrorService) {
		var groupId = parseInt($routeParams.id);

		KPIGroupRepository.getGroupById(groupId).success(function (group) {
			$scope.editGroup = group;
			$scope.editGroup.isActive = !group.isDisabled;
		}).error(function (response, status) {
			if (status === 405) {
				ErrorService.errorHandle(status);
			}
			else {
				notificationFactory.error("ErrorMessage");
			}
		});

		KPICommonRepository.getFormOptions().then(function (listTargetType) {
			$scope.formOptions = listTargetType;
		});

		// validation
		KPICommonRepository.showMessageAfterValidateKPIGroup($scope);

		$scope.update = function (group) {
			$scope.submitted = true;
			$scope.disabledSubmitButton = true;

			if (KPICommonRepository.validateGroup(group) === true) {
				var newGroup = angular.copy(group);
				newGroup.isDisabled = !group.isActive;

				KPIGroupRepository.updateGroup(newGroup)
					.success(function (response) {
						if (response.isSuccessful === true) {
							notificationFactory.success("DataChangeSuccessfully");
							$timeout(function () {
								$location.url('/KPI/GetAllKPIGroup/');
							}, 2000);
						}
						else {
							notificationFactory.error("", response.message);
							$scope.disabledSubmitButton = false;
						}
					})
					.error(function () {
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