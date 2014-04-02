'use strict';
/*global registrationModule*/


registrationModule.controller("GetAllKPIGroupController",
	function ($scope, $location, $route, $modal, CacheFactory, KPIGroupRepository, CommonRepository) {
		var formOptionsCache = CacheFactory.get("formOptions");

		if (formOptionsCache) {
			$scope.formOptions = formOptionsCache;
		}
		else {
			CommonRepository.getFormOptions().then(function (listTargetType) {
				$scope.formOptions = listTargetType;
				CacheFactory.put("formOptions", listTargetType);
			});
		}

		var getAllGroup = function (targetTypeId) {
			KPIGroupRepository.getAll(targetTypeId)
				.success(function (groupList) {
					$scope.KPIGroupList = groupList;
				})
				.error(function (httpResponse, status) {
					if (status === 404) {
						$scope.hasError = true;
						$scope.errorMessage = "Sorry, an error occurred while processing your request, please notify the administrators!";
					}
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
		$scope.openDeletePopup = function (groupId, groupName) {
			var modalDeleteInstance = $modal.open({
				templateUrl: '/templates/KPI/modalConfirmContent.html',
				controller: "ModalConfirmController",
				resolve: {
					name: function () {
						return groupName;
					},
					action: function () {
						return "delete";
					}
				}
			});

			modalDeleteInstance.result.then(function () {
				KPIGroupRepository.deleteGroup(groupId).success(function (response) {
					if (response.isSuccessful === true) {
						$scope.hasError = false;
						$route.reload();
					}
					else {
						$scope.hasError = true;
						$scope.errorMessage = response.message;
					}
				}).error(function (httpResponse, status) {
					if (status === 404) {
						$scope.hasError = true;
						$scope.errorMessage = "Sorry, an error occurred while processing your request, please notify the administrators!";
					}
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
			getAllGroup(targetTypeId);
		};
	});

registrationModule.controller("CreateNewKPIGroupController",
	function ($scope, $location, $timeout, KPIGroupRepository, CacheFactory, CommonRepository) {
		var formOptionsCache = CacheFactory.get("formOptions");

		if (formOptionsCache) {
			$scope.formOptions = formOptionsCache;
		}
		else {
			CommonRepository.getFormOptions().then(function (listTargetType) {
				$scope.formOptions = listTargetType;
				CacheFactory.put("formOptions", listTargetType);
			});
		}

		//save new KPI group
		$scope.save = function (group) {
			$scope.submitted = true;

			if (CommonRepository.validateGroup(group) === true) {
				group.isDisabled = !group.isActive;

				KPIGroupRepository.addGroup(group)
					.success(function (response) {
						if (response.isSuccessful === true) {
							$scope.success = true;
							$scope.saveSuccessMessage = "Created successful!";
							$timeout(function () {
								$location.url('/KPI/GetAllKPIGroup/');
							}, 2000);
						}
						else {
							$scope.success = false;
							$scope.saveErrorMessage = response.message;
						}
					})
					.error(function (httpResponse, status) {
						if (status === 404) {
							$scope.hasError = true;
							$scope.errorMessage = "Sorry, an error occurred while processing your request, please notify the administrators!";
						}
					});
			}
		};

		$scope.cancel = function () {
			window.history.back();
		};
});

registrationModule.controller("EditKPIGroupController",
	function ($scope, $location, $routeParams, $timeout, $route, KPIGroupRepository, CacheFactory, CommonRepository) {
		var groupId = $routeParams.id;

		KPIGroupRepository.getGroupById(groupId).success(function (group) {
			$scope.editGroup = group;
		});

		var formOptionsCache = CacheFactory.get("formOptions");

		if (formOptionsCache) {
			$scope.formOptions = formOptionsCache;
		}
		else {
			CommonRepository.getFormOptions().then(function (listTargetType) {
				$scope.formOptions = listTargetType;
				CacheFactory.put("formOptions", listTargetType);
			});
		}

		$scope.update = function (group) {
			$scope.submitted = true;

			if (CommonRepository.validateGroup(group) === true) {
				var newGroup = group;
				newGroup.isDisabled = !group.isActive;

				KPIGroupRepository.updateGroup(newGroup)
					.success(function (response) {
						if (response.isSuccessful === true) {
							$scope.success = true;
							$scope.updateSuccessMessage = "Updated successful!";
							$timeout(function () {
								$location.url('/KPI/GetAllKPIGroup/');
							}, 2000);
						}
						else {
							$scope.success = false;
							$scope.updateErrorMessage = response.message;
						}
					})
					.error(function (httpResponse, status) {
						if (status === 404) {
							$scope.hasError = true;
							$scope.errorMessage = "Sorry, an error occurred while processing your request, please notify the administrators!";
						}
					});
			}
		};

		$scope.cancel = function () {
			window.history.back();
		};
});