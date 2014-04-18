'use strict';
/*global targetModule*/

targetModule.controller('ViewTargetController',
	function ($scope, $location, $translate, $modal, $route,
		targetUtility, targetFactory, targetCacheFactory, KPICommonRepository, commonFactory, ErrorService) {

		// get Form Options
		targetUtility.getFormOptions($scope, KPICommonRepository);

		// get Date Format
		commonFactory.getDateFormat($scope);

		// get Calendar Image
		commonFactory.getSourceCalendarImage($scope);

		// set default date is today
		$scope.filter = {
			startDate: new Date(),
			endDate: new Date()
		};

		var getTarget = function (targetTypeId, startDate, endDate) {
			startDate = startDate ? startDate.toDateString() : null;
			endDate = endDate ? endDate.toDateString() : null;

			targetFactory.getTarget(targetTypeId, startDate, endDate).success(function (targetList) {
				$scope.targetList = targetList;
				$scope.submitted = false;
			}).error(function (response, status) {
				if (status === 405) {
					ErrorService.errorHandle(status);
				}
				else {
					targetUtility.showErrorMessage($scope);
				}
			});
		};

		// get Target
		getTarget(null, $scope.filter.startDate, $scope.filter.endDate);

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
			$scope.submitted = true;

			if (filterData) {
				getTarget(filterData.targetTypeId, filterData.startDate, filterData.endDate);
			}
			else {
				$scope.submitted = false;
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

		// delete target
		$scope.openDeletePopup = function (targetId, targetName) {
			var modalDeleteInstance = $modal.open({
				templateUrl: '/templates/Common/modalConfirmContent.html',
				controller: "ModalConfirmController",
				resolve: {
					name: function () {
						return targetName;
					},
					action: function () {
						return "delete";
					}
				},
				windowClass: "modal-confirm-center"
			});

			modalDeleteInstance.result.then(function () {
				targetFactory.deleteTarget(targetId).success(function (response) {
					if (response.isSuccessful === true) {
						$scope.hasError = false;
						$route.reload();
					}
					else {
						$scope.hasError = true;
						$scope.errorMessage = response.message;
					}
				}).error(function (response, status) {
					if (status === 405) {
						ErrorService.errorHandle(status);
					}
					else {
						targetUtility.showErrorMessage($scope);
					}
				});
			});
		};

		$scope.reload = function () {
			$route.reload();
		};

		// view target employee
		$scope.viewTargetEmployee = function (targetId) {
			$location.path("/Target/TargetEmployee/" + targetId);
		};

		// view target KPI
		$scope.viewTargetKPI = function (targetId) {
			$location.path("/Target/TargetKPI/" + targetId);
		};
	});

targetModule.controller("CreateTargetController",
	function ($scope, $timeout, $location, $translate, $window,
		targetFactory, targetUtility, targetCacheFactory, KPICommonRepository, commonFactory, ErrorService) {

		// get Form Options
		targetUtility.getFormOptions($scope, KPICommonRepository);

		// get Date Format
		commonFactory.getDateFormat($scope);

		// get Calendar Image
		commonFactory.getSourceCalendarImage($scope);

		// The onchange function of Form dropdown list
		$scope.formOnChange = function (targetTypeId) {
			targetUtility.resetProjectInformation($scope);

			if (targetTypeId === 2) {
				$scope.formIsProject = true;
				targetUtility.getAllProject($scope);
			}
		};

		// the onchange funtion of Project dropdown list
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
			$scope.disabledSubmit = true;

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
						$scope.disabledSubmit = false;
					}
				})
				.error(function (response, status) {
					if (status === 405) {
						ErrorService.errorHandle(status);
					}
					else {
						targetUtility.showErrorMessage($scope);
					}

					$scope.disabledSubmit = false;
				});
			}
			else {
				$scope.disabledSubmit = false;
			}
		};

		// cancel
		$scope.cancel = function () {
			$window.history.back();
		};

		// back to view
		$scope.backToView = function () {
			$location.url('/Target/ViewTarget/');
		};
});

targetModule.controller("EditTargetController",
	function ($scope, $timeout, $location, $translate, $routeParams, $window,
			targetFactory, targetUtility, targetCacheFactory, KPICommonRepository, commonFactory, ErrorService) {

		var targetId = $routeParams.id;

		// get Target by ID
		targetFactory.getTargetById(targetId).success(function (target) {
			target.startDate = target.startDate !== "" ? new Date(target.startDate) : "";
			target.endDate = target.endDate !== "" ? new Date(target.endDate) : "";

			$scope.target = target;

			if (target.targetType && target.targetType.targetTypeId) {
				$scope.target.targetTypeId = target.targetType.targetTypeId;
			}

			if (target.project && target.project.projectId) {
				$scope.formIsProject = true;
				targetUtility.getAllProject($scope);
				$scope.target.projectId = target.project.projectId;

				if (target.milestone && target.milestone.milestoneId) {
					$scope.isSelectedProject = true;
					$scope.target.milestoneId = target.milestone.milestoneId;
					targetUtility.getMilestone($scope.target.projectId, $scope);
				}
			}
		}).error(function () {
			targetUtility.showErrorMessage($scope);
		});

		// get Form options
		targetUtility.getFormOptions($scope, KPICommonRepository);

		// get Date Format
		commonFactory.getDateFormat($scope);

		// get Calendar Image
		commonFactory.getSourceCalendarImage($scope);

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

		// get all status
		targetUtility.getAllStatus($scope);

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

		// update target
		$scope.save = function (target) {
			$scope.submitted = true;
			$scope.disabledSubmit = true;

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
						$scope.disabledSubmit = false;
					}
				})
				.error(function (response, status) {
					if (status === 405) {
						ErrorService.errorHandle(status);
					}
					else {
						targetUtility.showErrorMessage($scope);
					}

					$scope.disabledSubmit = false;
				});
			}
			else {
				$scope.disabledSubmit = false;
			}
		};

		// cancel
		$scope.cancel = function () {
			$window.history.back();
		};

		// back to view
		$scope.backToView = function () {
			$location.url('/Target/ViewTarget/');
		};
});

// Target Employee Controller
targetModule.controller("TargetEmployeeController",
	function ($scope, $route, $routeParams, $modal, $window, targetFactory, targetUtility, ErrorService) {
		var targetId = $routeParams.id;

		// get all employee by target Id
		targetFactory.getEmployeeByTargetId(targetId).success(function (employees) {
			$scope.employees = employees;
		}).error(function () {
			if (status === 405) {
				ErrorService.errorHandle(status);
			}
			else {
				targetUtility.showErrorMessage($scope);
			} 
		});

		// add employee
		$scope.addEmployee = function (employeeId) {
			var employee = {
				targetId: targetId,
				employeeId: employeeId
			};
			targetFactory.insertTargetEmployee(employee).success(function (response) {
				if (response.isSuccessful) {
					$route.reload();
				}
				else {
					$scope.hasError = true;
					$scope.errorMessage = response.message;
				}
			}).error(function (response, status) {
				if (status === 405) {
					ErrorService.errorHandle(status);
				}
				else {
					targetUtility.showErrorMessage($scope);
				}
			});
		};

		// delete employee
		$scope.openDeletePopup = function (employee) {
			var modalDeleteInstance = $modal.open({
				templateUrl: '/templates/Common/modalConfirmContent.html',
				controller: "ModalConfirmController",
				resolve: {
					name: function () {
						return employee.fullName;
					},
					action: function () {
						return "delete";
					}
				},
				windowClass: "modal-confirm-center"
			});

			modalDeleteInstance.result.then(function () {
				targetFactory.deleteTargetEmployee(targetId, employee.employeeId).success(function (response) {
					if (response.isSuccessful === true) {
						$scope.hasError = false;
						$route.reload();
					}
					else {
						$scope.hasError = true;
						$scope.errorMessage = response.message;
					}
				}).error(function (response, status) {
					if (status === 405) {
						ErrorService.errorHandle(status);
					}
					else {
						targetUtility.showErrorMessage($scope);
					}
				});
			});
		};

		$scope.cancel = function () {
			$window.history.back();
		};
});

// Target KPI controller
targetModule.controller("TargetKPIController",
	function ($scope, $route, $routeParams, $modal, $window, targetFactory, targetUtility, KPIGroupRepository, ErrorService) {
		var targetId = $routeParams.id;

		// get all KPI group
		KPIGroupRepository.getAll().success(function (groupList) {
			$scope.groupOptions = groupList;
		});

		// get KPI of Target
		var getKPIByTargetId = function (targetId, groupId) {
			$scope.disabledSubmitButton = true;

			targetFactory.getKPIByTargetId(targetId, groupId).success(function (listKPI) {
				$scope.listKPI = listKPI;
				$scope.disabledSubmitButton = false;
			}).error(function (response, status) {
				if (status === 405) {
					ErrorService.errorHandle(status);
				}
				else {
					targetUtility.showErrorMessage($scope);
				}

				$scope.disabledSubmitButton = false;
			});
		};

		// get all KPI of Target when no filter
		getKPIByTargetId(targetId, null);

		// filter by group
		$scope.filter = function (kpiGroupId) {
			getKPIByTargetId(targetId, kpiGroupId);
		};

		$scope.cancel = function () {
			$window.history.back();
		};
	});