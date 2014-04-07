'use strict';
/*global targetModule*/

targetModule.controller('ViewTargetController', function ($scope, $location, $translate, targetUtility, targetFactory, targetCacheFactory, CommonRepository) {

	// get Form Options
	targetUtility.getFormOptions($scope, CommonRepository);

	var getTarget = function (targetTypeId, startDate, endDate) {
		targetFactory.getTarget(targetTypeId, startDate, endDate).success(function (targetList) {
			$scope.targetList = targetList;
		}).error(function () {
			targetUtility.showErrorMessage($scope);
		});
	};

	// get Target
	getTarget(null, null, null);

	// show start date calendar when click button start date
	$scope.openPopupStartDate = function ($event) {
		targetUtility.openPopupStartDate($event, $scope);
	};

	// show end date calendar when click button end date
	$scope.openPopupEndDate = function ($event) {
		targetUtility.openPopupEndDate($event, $scope);
	};

	// filter data
	$scope.query = function (filterData) {
		if (filterData) {
			var startDate = filterData.startDate ? filterData.startDate.toDateString() : null;
			var endDate = filterData.endDate ? filterData.endDate.toDateString() : null;

			getTarget(filterData.targetTypeId, startDate, endDate);
		}
	};

	// create target
	$scope.create = function () {
		$location.url('Target/CreateTarget');
	};

	// edit target
	$scope.edit = function (targetId) {
		$location.path('Target/EditTarget/' + targetId);
	};
});

targetModule.controller("CreateTargetController",
	function ($scope, $timeout, $location, $translate, targetFactory, targetUtility, targetCacheFactory, CommonRepository) {

		// get Form Options
		targetUtility.getFormOptions($scope, CommonRepository);

		// The onchange function of Form select list
		$scope.formOnChange = function (targetTypeId) {
			targetUtility.resetProjectInformation($scope);

			if (targetTypeId === 2) {
				$scope.formIsProject = true;
				targetUtility.getAllProject($scope);
			}
		};

		// the onchange funtion of Project select list
		$scope.projectOnChange = function (projectId) {
			$scope.isSelectedProject = false;
			$scope.target.milestoneId = null;

			if (projectId) {
				$scope.isSelectedProject = true;
				targetUtility.getMilestone(projectId, $scope);
			}
		};

		// show start date calendar when click button start date
		$scope.openPopupStartDate = function ($event) {
			targetUtility.openPopupStartDate($event, $scope);
		};

		// show end date calendar when click button end date
		$scope.openPopupEndDate = function ($event) {
			targetUtility.openPopupEndDate($event, $scope);
		};

		// save target
		$scope.save = function (target) {
			$scope.submitted = true;

			if (targetUtility.validateTarget(target, $scope.formIsProject) === true) {
				var newTarget = angular.copy(target);

				newTarget.startDate = newTarget.startDate ? newTarget.startDate.toDateString() : null;
				newTarget.endDate = newTarget.endDate ? newTarget.endDate.toDateString() : null;

				targetFactory.createTarget(newTarget).success(function (response) {
					if (response.isSuccessful === true) {
						$scope.success = true;
						$translate("CreatedSuccessfulMessage").then(function (message) {
							$scope.successMessage = message;
						});
						$timeout(function () {
							$location.url('/Target/ViewTarget/');
						}, 2000);
					}
					else {
						$scope.success = false;
						$scope.errorMessage = response.message;
					}
				})
			.error(function () {
				targetUtility.showErrorMessage($scope, $translate);
			});
			}
		};

		// cancel
		$scope.cancel = function () {
			window.history.back();
		};
	});

	targetModule.controller("EditTargetController",
	function ($scope, $timeout, $location, $translate, $routeParams, targetFactory, targetUtility, targetCacheFactory, CommonRepository) {

		var targetId = $routeParams.id;

		// get Target by ID
		targetFactory.getTargetById(targetId).success(function (target) {
			target.startDate = new Date(target.startDate);
			target.endDate = new Date(target.endDate);

			$scope.target = target;

			if (target.targetType && target.targetType.targetTypeId) {
				$scope.target.targetTypeId = target.targetType.targetTypeId;

				if ($scope.target.targetTypeId === 2) {
					$scope.formIsProject = true;
					targetUtility.getAllProject($scope);
				}
			}
			if (target.project && target.project.projectId) {
				$scope.formIsProject = true;
				$scope.target.projectId = target.project.projectId;

				if (target.milestone && target.milestone.milestoneId) {
					$scope.isSelectedProject = true;
					$scope.target.milestoneId = target.milestone.milestoneId;
					targetUtility.getMilestone($scope.target.projectId, $scope);
				}
			}
		}).error(function () {
			targetUtility.showErrorMessage($scope, $translate);
		});

		// get Form options
		targetUtility.getFormOptions($scope, CommonRepository);

		// The onchange function of Form
		$scope.formOnChange = function (targetTypeId) {
			$scope.formIsProject = false;
			$scope.isSelectedProject = false;

			if (targetTypeId === 2) {
				$scope.formIsProject = true;
				$scope.isSelectedProject = true;
				targetUtility.getAllProject($scope);
			}
		};

		// the onchange funtion of Project
		$scope.projectOnChange = function (projectId) {
			$scope.isSelectedProject = false;

			if (projectId) {
				$scope.isSelectedProject = true;
				targetUtility.getMilestone(projectId, $scope);
			}
		};

		// show start date calendar when click button start date
		$scope.openPopupStartDate = function ($event) {
			targetUtility.openPopupStartDate($event, $scope);
		};

		// show end date calendar when click button end date
		$scope.openPopupEndDate = function ($event) {
			targetUtility.openPopupEndDate($event, $scope);
		};

		$scope.save = function (target) {
			$scope.submitted = true;

			if (targetUtility.validateTarget(target, $scope.formIsProject) === true) {
				var newTarget = angular.copy(target);

				if (newTarget.targetTypeId !== 2) {
					newTarget.projectId = null;
					newTarget.milestoneId = null;
				}

				newTarget.startDate = newTarget.startDate ? newTarget.startDate.toDateString() : null;
				newTarget.endDate = newTarget.endDate ? newTarget.endDate.toDateString() : null;

				targetFactory.editTarget(newTarget).success(function (response) {
					if (response.isSuccessful === true) {
						$scope.success = true;
						$translate("UpdatedSuccessfulMessage").then(function (message) {
							$scope.successMessage = message;
						});
						$timeout(function () {
							$location.url('/Target/ViewTarget/');
						}, 2000);
					}
					else {
						$scope.success = false;
						$scope.errorMessage = response.message;
					}
				})
				.error(function () {
					targetUtility.showErrorMessage($scope, $translate);
				});
			}
		};

		// cancel
		$scope.cancel = function () {
			window.history.back();
		};
	});