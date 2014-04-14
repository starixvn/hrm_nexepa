'use strict';
/*global kpiSettingModule*/

// View KPI
kpiSettingModule.controller("ViewKPIController",
	function ($scope, $routeParams, $location, $modal, $route, KPIRepository, KPIGroupRepository, CacheFactory, CommonRepository) {
		var groupId = $routeParams.groupId;
		$scope.isViewAll = false;

		if (groupId) {
			$scope.back = function () {
				window.history.back();
			};
		}
		else {
			$scope.isViewAll = true;
			var formOptionsCache = CacheFactory.get("formOptions");

			if (formOptionsCache) {
				$scope.formOptions = formOptionsCache;
			}
			else {
				CommonRepository.getFormOptions().then(function (targetTypeList) {
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
				CommonRepository.showErrorMessage($scope, "ErrorMessage");
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
		$scope.openDeletePopup = function (kpiId, kpiName) {
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
				}
			});

			modalDeleteInstance.result.then(function () {
				KPIRepository.deleteKPI(kpiId).success(function (response) {
					if (response.isSuccessful === true) {
						$scope.success = true;
						$route.reload();
					}
					else {
						$scope.success = false;
						$scope.errorMessage = response.message;
					}
				}).error(function () {
					CommonRepository.showErrorMessage($scope, "ErrorMessage");
				});
			});
		};

		// query data when filter
		$scope.query = function (targetTypeId, groupId) {
			getKPI(targetTypeId, groupId);
			$scope.disabledSubmitButton = true;
		};
	});

// Create New KPI
	kpiSettingModule.controller('CreateNewKPIController',
	function ($scope, $routeParams, $timeout, $location, KPIRepository, CacheFactory, CommonRepository) {
		var formOptionsCache = CacheFactory.get("formOptions");

		if (formOptionsCache) {
			$scope.formOptions = formOptionsCache;
		}
		else {
			CommonRepository.getFormOptions().then(function (targetTypeList) {
				$scope.formOptions = targetTypeList;
				CacheFactory.put("formOptions", targetTypeList);
			});
		}

		var targetTypeId = $routeParams.form;
		var groupId = $routeParams.groupId;
		$scope.isCreatedFromKPI = false;

		var getGroupOptions = function (targetTypeId) {
			CommonRepository.getGroupByTargetTypeId(targetTypeId).then(function (groupList) {
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

			if (CommonRepository.validateKPI(item) === true) {
				KPIRepository.createKPI(item).success(function (response) {
					if (response.isSuccessful === true) {
						$scope.success = true;
						CommonRepository.showSuccessMessage($scope, "CreatedSuccessfulMessage");
						$timeout(function () {
							if (!$scope.isCreatedFromKPI) {
								window.history.back();
							}
							else {
								$location.url('/KPI/ViewKPI/');
							}
						}, 2000);
					}
					else {
						$scope.success = false;
						$scope.errorMessage = response.message;
						$scope.disabledSubmitButton = false;
					}
				}).error(function () {
					CommonRepository.showErrorMessage($scope, "ErrorMessage");
					$scope.disabledSubmitButton = false;
				});
			}
			else {
				$scope.disabledSubmitButton = false;
			}
		};

		$scope.cancel = function () {
			window.history.back();
		};
	});

// Edit KPI
	kpiSettingModule.controller("EditKPIController",
	function ($scope, $routeParams, $location, $timeout, KPIRepository, CacheFactory, CommonRepository) {
		var kpiId = $routeParams.id;
		var formOptionsCache = CacheFactory.get("formOptions");

		if (formOptionsCache) {
			$scope.formOptions = formOptionsCache;
		}
		else {
			CommonRepository.getFormOptions().then(function (targetTypeList) {
				$scope.formOptions = targetTypeList;
				CacheFactory.put("formOptions", targetTypeList);
			});
		}

		KPIRepository.getKPIById(kpiId).success(function (kpiItem) {
			$scope.item = kpiItem;
			CommonRepository.getGroupByTargetTypeId(kpiItem.targetTypeId).then(function (groupList) {
				$scope.groupOptions = groupList;
			});
		}).error(function () {
			CommonRepository.showErrorMessage($scope, "ErrorMessage");
		});

		$scope.save = function (kpiItem) {
			$scope.submitted = true;
			$scope.disabledSubmitButton = true;

			if (CommonRepository.validateKPI(kpiItem) === true) {
				KPIRepository.editKPI(kpiItem).success(function (response) {
					if (response.isSuccessful === true) {
						$scope.success = true;
						CommonRepository.showSuccessMessage($scope, "UpdatedSuccessfulMessage");
						$timeout(function () {
							window.history.back();
						}, 2000);
					}
					else {
						$scope.success = false;
						$scope.errorMessage = response.message;
						$scope.disabledSubmitButton = false;
					}
				}).error(function () {
					CommonRepository.showErrorMessage($scope, "ErrorMessage");
					$scope.disabledSubmitButton = false;
				});
			}
			else {
				$scope.disabledSubmitButton = false;
			}
		};

		$scope.cancel = function () {
			window.history.back();
		};
	});