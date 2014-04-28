'use strict';
/*global targetModule*/

targetModule.controller('ViewTargetController',
['$scope', '$location', '$translate', '$modal', '$route', 'targetUtility', 'targetFactory',
'targetCacheFactory', 'KPICommonRepository', 'commonFactory', 'ErrorService', 'notificationFactory',
	function ($scope, $location, $translate, $modal, $route,
		targetUtility, targetFactory, targetCacheFactory, KPICommonRepository, commonFactory, ErrorService, notificationFactory) {

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
					notificationFactory.error("ErrorMessage");
				}

				$scope.submitted = false;
			});
		};

		// get Target
		getTarget(null, $scope.filter.startDate, $scope.filter.endDate);

		$scope.calendar = {
			startDateOpened: false,
			endDateOpened: false
		};

		// show start date calendar when click button start date
		$scope.openPopupStartDate = function ($event) {
			commonFactory.openPopupStartDate($event, $scope);
		};

		// show end date calendar when click button end date
		$scope.openPopupEndDate = function ($event) {
			commonFactory.openPopupEndDate($event, $scope);
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
		$scope.edit = function (target) {
			var targetId = target.targetId,
				projectId = target.project ? target.project.projectId : null;

			$location.path('Target/EditTarget/' + targetId).search("projectId", projectId).search("isEditMode", true);
		};

		// delete target
		$scope.openDeletePopup = function (target) {
			var targetId = target.targetId;
			var targetName = target.name;

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
						var index = $scope.targetList.indexOf(target);
						$scope.targetList.splice(index, 1);
						notificationFactory.error("DeleteSuccess");
					}
					else {
						notificationFactory.error("DeleteFail", response.message);
					}
				}).error(function (response, status) {
					if (status === 405) {
						ErrorService.errorHandle(status);
					}
					else {
						notificationFactory.error("DeleteFail");
					}
				});
			});
		};

		$scope.reload = function () {
			$route.reload();
		};

		// view target employee
		$scope.viewTargetEmployee = function (targetId) {
			$location.path("/Target/TargetEmployee/" + targetId).search("viewMembers", true);
		};

		// view target KPI
		$scope.viewTargetKPI = function (targetId) {
			$location.path("/Target/TargetKPI/" + targetId).search("viewKPIs", true);
		};

		// view detail of Target
		$scope.viewDetailTarget = function (targetId) {
			$location.path("/Target/TargetDetail/" + targetId);
		};
	} ]);

	targetModule.controller("CreateTargetController",
['$scope', '$timeout', '$location', '$translate', '$window',
'targetFactory', 'targetUtility', 'targetCacheFactory', 'KPICommonRepository', 'commonFactory', 'ErrorService',
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
			commonFactory.openPopupStartDate($event, $scope);
		};

		// show end date calendar when click button end date
		$scope.openPopupEndDate = function ($event) {
			commonFactory.openPopupEndDate($event, $scope);
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
	}]);

// View Detail of Target or Edit Target Controller
	targetModule.controller("TargetDetailAndEditController",
	['$scope', '$timeout', '$location', '$translate', '$routeParams', '$window', '$route', '$modal',
	'targetFactory', 'targetUtility', 'targetCacheFactory', 'KPICommonRepository', 
	'KPIGroupRepository', 'commonFactory', 'ErrorService', 'notificationFactory',
	function ($scope, $timeout, $location, $translate, $routeParams, $window, $route, $modal,
			targetFactory, targetUtility, targetCacheFactory, KPICommonRepository, KPIGroupRepository,
			commonFactory, ErrorService, notificationFactory) {

		var targetId = $routeParams.id,
			projectId = $routeParams.projectId,
			isEditMode = $routeParams.isEditMode ? true : false;

		$scope.projectId = projectId;

		if (!isEditMode) {
			var isMemberSelect = $routeParams.viewMembers ? true : false;
			var isKPISelect = $routeParams.viewKPIs ? true : false;

			if (isMemberSelect || isKPISelect) {
				$scope.tabGeneralSelect = false;
				$scope.tabMemberSelect = $routeParams.viewMembers ? true : false;
				$scope.tabKPISelect = $routeParams.viewKPIs ? true : false;
			}
		}

		// get Tab Heading
		targetUtility.getTabHeading($scope);

		// TAB : GENERAL INFORMATION
		// get Target by ID
		targetFactory.getTargetById(targetId).success(function (target) {
			if (isEditMode) {
				target.startDate = target.startDate !== "" ? new Date(target.startDate) : "";
				target.endDate = target.endDate !== "" ? new Date(target.endDate) : "";
			}

			$scope.target = target;

			if (isEditMode) {
				if (target.targetType && target.targetType.targetTypeId) {
					$scope.target.targetTypeId = target.targetType.targetTypeId;
				}

				if (target.project && target.project.projectId) {
					$scope.formIsProject = true;
					targetUtility.getAllProject($scope);
					$scope.target.projectId = target.project.projectId;

					$scope.isSelectedProject = true;
					targetUtility.getMilestone($scope.target.projectId, $scope);

					if (target.milestone && target.milestone.milestoneId) {
						$scope.target.milestoneId = target.milestone.milestoneId;
					}
				}
			}
		}).error(function () {
			targetUtility.showErrorMessageOfTargetInformations($scope);
		});

		if (isEditMode) {
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

			$scope.calendar = {
				startDateOpened: false,
				endDateOpened: false
			};

			// show start date calendar when click button start date
			$scope.openPopupStartDate = function ($event) {
				commonFactory.openPopupStartDate($event, $scope);
			};

			// show end date calendar when click button end date
			$scope.openPopupEndDate = function ($event) {
				commonFactory.openPopupEndDate($event, $scope);
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
							$scope.targetInfoSuccess = true;
							$translate("UpdatedSuccessfulMessage").then(function (message) {
								$scope.infoSuccessMessage = message;
							});
							$timeout(function () {
								$scope.targetInfoSuccess = false;
							}, 2000);
						}
						else {
							$scope.targetInfoSuccess = false;
							$scope.infoErrorMessage = response.message;
						}
						$scope.disabledSubmit = false;
					})
			.error(function (response, status) {
				if (status === 405) {
					ErrorService.errorHandle(status);
				}
				else {
					targetUtility.showErrorMessageOfTargetInformations($scope);
				}

				$scope.disabledSubmit = false;
			});
				}
				else {
					$scope.disabledSubmit = false;
				}
			};
		}

		// TAB : MEMBERS 
		$scope.search = {
			searchKey: ''
		};

		// get all employee by target Id 
		targetFactory.getEmployeeByTargetId(targetId).success(function (employees) {
			$scope.employees = employees;
		}).error(function () {
			if (status === 405) {
				ErrorService.errorHandle(status);
			}
			else {
				targetUtility.showErrorMessageOfMember($scope);
			}
		});

		if (isEditMode) {
			// add employee
			$scope.addEmployee = function (employee) {
				$scope.disabledSubmit = true;

				var employeeInsert = {
					targetId: targetId,
					employeeId: employee.employeeId
				};

				targetFactory.insertTargetEmployee(employeeInsert).success(function (response) {
					if (response.isSuccessful) {
						$scope.search.searchKey = '';
						employee.groupName = employee.group.name;
						$scope.employees.push(employee);
					}
					else {
						notificationFactory.error('', response.message);
					}

					$scope.disabledSubmit = false;
				}).error(function (response, status) {
					if (status === 405) {
						ErrorService.errorHandle(status);
					}
					else {
						notificationFactory.error("ErrorMessage");
					}

					$scope.disabledSubmit = false;
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
							var index = $scope.employees.indexOf(employee);
							$scope.employees.splice(index, 1);
							notificationFactory.success("DeleteSuccess");
						}
						else {
							notificationFactory.error("DeleteFail", response.message);
						}
					}).error(function (response, status) {
						if (status === 405) {
							ErrorService.errorHandle(status);
						}
						else {
							notificationFactory.error("ErrorMessage");
						}
					});
				});
			};
		}

		// TAB : KPIs
		if (!isEditMode) {
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
		}

		// Edit Target
		$scope.editTarget = function () {
			$location.url('Target/EditTarget/' + targetId).search("isEditMode", true);
		};

		$scope.cancel = function () {
			$window.history.back();
		};

		$scope.backToView = function () {
			$location.url('Target/ViewTarget');
		};
	}]);
