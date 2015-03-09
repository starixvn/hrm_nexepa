'use strict';
/*global kpiSettingModule*/

// View KPI Group Controller
kpiSettingModule.controller("GetAllKPIGroupController",
['$scope', '$location', '$route', '$modal', 'KPIGroupRepository', 'KPICommonRepository', 'commonFactory',
'notificationFactory', 'ErrorService', 'KPIRepository',
function ($scope, $location, $route, $modal, KPIGroupRepository, KPICommonRepository, commonFactory,
notificationFactory, ErrorService, KPIRepository) {

	$scope.filter = {
		targetTypeId: "",
		active: ""
	};

	/**
	* Get Information
	*/
	KPIGroupRepository.getPermission().success(function (policy) {
		$scope.permission = {
			policy : policy
		};
	}).error(function () {
		notificationFactory.error("ErrorMessage");
	});

	KPIRepository.getPermission().success(function (policy) {
		$scope.permissionKPI = {
			policy: policy
		};
	}).error(function () {
		notificationFactory.error("ErrorMessage");
	});

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

	/**
	* Declare some actions of $scope
	*/

	$scope.create = function () {
		$scope.createKPIGroupUrl = '/KPI/CreateNewKPIGroup/';
	};

	$scope.edit = function (groupId) {
		$scope.editKPIGroupUrl = '/KPI/EditKPIGroup/' + groupId;
	};

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
				},
				extraInfo: function () {
					return '';
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

	$scope.addKPI = function (targetTypeId, kpiGroupId) {
		$scope.addKPIUrl = '/KPI/CreateKPI' + '?form=' + targetTypeId + '&groupId=' + kpiGroupId;
	};

	$scope.viewKPI = function (targetTypeId, kpiGroupId) {
		$scope.viewKPIUrl = '/KPI/ViewKPI' + '?form=' + targetTypeId + '&groupId=' + kpiGroupId;
	};

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
				},
				extraInfo: function () {
					return '';
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

// Create New KPI Group Controller
kpiSettingModule.controller("CreateNewKPIGroupController",
['$scope', '$location', '$timeout', '$translate', '$window', 'KPIGroupRepository', 'KPICommonRepository',
'notificationFactory', 'ErrorService',
function ($scope, $location, $timeout, $translate, $window, KPIGroupRepository, KPICommonRepository,
notificationFactory, ErrorService) {

	KPICommonRepository.getFormOptions().then(function (targetTypeList) {
		$scope.formOptions = targetTypeList;
	});

	KPICommonRepository.showMessageAfterValidateKPIGroup($scope);

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
kpiSettingModule.controller("EditKPIGroupController",
['$scope', '$location', '$routeParams', '$timeout', '$route', '$translate', '$window', 'KPIGroupRepository',
'KPICommonRepository', 'notificationFactory', 'ErrorService',
function ($scope, $location, $routeParams, $timeout, $route, $translate, $window, KPIGroupRepository,
KPICommonRepository, notificationFactory, ErrorService) {

	var groupId = parseInt($routeParams.id);

	KPIGroupRepository.getPermission().success(function (policy) {
		$scope.permission = {
			policy: policy
		};
	}).error(function () {
		notificationFactory.error("ErrorMessage");
	});

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