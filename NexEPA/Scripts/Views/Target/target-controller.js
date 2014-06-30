'use strict';
/*global targetModule*/

targetModule.controller('ViewTargetController',
['$scope', '$location', '$translate', '$modal', '$route', 'targetUtility', 'targetFactory', 'targetCacheFactory',
'KPICommonRepository', 'commonFactory', 'ErrorService', 'notificationFactory', 'employeeEPAFactory',
function ($scope, $location, $translate, $modal, $route, targetUtility, targetFactory, targetCacheFactory,
KPICommonRepository, commonFactory, ErrorService, notificationFactory, employeeEPAFactory) {
	/**
	* Get Information
	*/
	targetFactory.getPermission().success(function (policy) {
		$scope.permission = {
			policy: policy
		};
	}).error(function () {
		notificationFactory.error("ErrorMessage");
	});

	employeeEPAFactory.getPermission().then(function (policy) {
		$scope.permissionMemberEPA = {
			policy: policy
		};
	});

	employeeEPAFactory.getCurrentUser().then(function (currentUser) {
		$scope.currentUser = currentUser;
	}, function () {
		notificationFactory.error("ErrorMessage");
	});

	targetUtility.getFormOptions($scope, KPICommonRepository);
	commonFactory.getDateFormat($scope);
	targetUtility.getAllStatus($scope);

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

	getTarget(null, $scope.filter.startDate, $scope.filter.endDate);

	$scope.calendar = {
		startDateOpened: false,
		endDateOpened: false
	};

	/**
	* Declare some actions
	*/

	// show start date calendar when click button start date
	$scope.openPopupStartDate = function ($event) {
		commonFactory.openPopupStartDate($event, $scope);
	};

	// show end date calendar when click button end date
	$scope.openPopupEndDate = function ($event) {
		commonFactory.openPopupEndDate($event, $scope);
	};

	$scope.query = function (filterData) {
		$scope.submitted = true;

		if (filterData) {
			getTarget(filterData.targetTypeId, filterData.startDate, filterData.endDate);
		}
		else {
			$scope.submitted = false;
		}
	};

	$scope.create = function () {
		$scope.createTargetUrl = '/Target/CreateTarget';
	};

	$scope.edit = function (target) {
		var targetId = target.targetId,
			projectId = target.project ? target.project.projectId : null;
		$scope.editTargetUrl = '/Target/EditTarget/' + targetId + '?projectId=' + projectId + '&isEditMode';
	};

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
					notificationFactory.success("DataChangeSuccessfully");
				}
				else {
					notificationFactory.error("DataChangeFail", response.message);
				}
			}).error(function (response, status) {
				if (status === 405) {
					ErrorService.errorHandle(status);
				}
				else {
					notificationFactory.error("DataChangeFail");
				}
			});
		});
	};

	$scope.reload = function () {
		$route.reload();
	};

	$scope.viewTargetEmployee = function (target) {
		var targetId = target.targetId,
			projectId = target.project ? target.project.projectId : null;
		$scope.viewTargetEmployeeUrl = '/Target/TargetEmployee/' + targetId + '?projectId=' + projectId + '&viewMembers';
	};

	$scope.viewTargetKPI = function (target) {
		var targetId = target.targetId,
			projectId = target.project ? target.project.projectId : null;
		$scope.viewTargetKPIUrl = '/Target/TargetKPI/' + targetId + '?projectId=' + projectId + '&viewKPIs';
	};

	$scope.viewDetailTarget = function (target) {
		var targetId = target.targetId,
			projectId = target.project ? target.project.projectId : null;
		$scope.viewDetailTargetUrl = '/Target/TargetDetail/' + targetId + '?projectId=' + projectId;
	};

	$scope.updateStatus = function (target, status) {
		if (target && status) {
			var targetStatus = {
				targetId: target.targetId,
				status: status.id
			};

			var modalUpdateStatusInstance = $modal.open({
				templateUrl: '/templates/Common/modalConfirmContent.html',
				controller: "ModalConfirmController",
				resolve: {
					name: function () {
						return "";
					},
					action: function () {
						return status.name;
					}
				},
				windowClass: "modal-confirm-center"
			});

			modalUpdateStatusInstance.result.then(function () {
				targetFactory.updateTargetStatus(targetStatus).success(function (response) {
					if (response.isSuccessful === true) {
						target.statusName = status.name;
						notificationFactory.success("DataChangeSuccessfully");
					}
					else {
						notificationFactory.error("", response.message);
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
		}
	};

	$scope.viewOpenEPA = function (targetId, projectManagerId) {
		if ($scope.currentUser) {
			var isShowAll = false;

			if ($scope.currentUser.userId !== projectManagerId) {
				isShowAll = true;
			}

			$scope.viewOpenEPAUrl = '/MemberEPA/ViewMember/?targetId=' + targetId + '&status=0&isShowAll=' + isShowAll;
		}
	};

	$scope.viewAllEPA = function (targetId, projectManagerId) {
		if ($scope.currentUser) {
			var isShowAll = false;

			if ($scope.currentUser.userId !== projectManagerId) {
				isShowAll = true;
			}

			$scope.viewAllEPAUrl = '/MemberEPA/ViewMember/?targetId=' + targetId + '&isShowAll=' + isShowAll;
		}
	};
} ]);

// Create Target Controller
targetModule.controller("CreateTargetController",
['$scope', '$timeout', '$location', '$translate', '$window', 'targetFactory', 'targetUtility', 'targetCacheFactory',
'KPICommonRepository', 'commonFactory', 'ErrorService', 'notificationFactory', 'projectDataFactory',
function ($scope, $timeout, $location, $translate, $window, targetFactory, targetUtility, targetCacheFactory,
KPICommonRepository, commonFactory, ErrorService, notificationFactory, projectDataFactory) {

	var targetDate = {
		projectStartDate: null,
		projectEndDate: null,
		milestoneStartDate: null,
		milestoneEndDate: null
	};

	targetUtility.getFormOptions($scope, KPICommonRepository);

	commonFactory.getDateFormat($scope);

	targetUtility.getAllProject($scope);

	projectDataFactory.getEmployeeSelectList(3).then(function (projectManagers) {
		$scope.projectManagerOptions = projectManagers;
	});

	$scope.projectOnChange = function (projectId) {
		$scope.isSelectedProject = false;
		$scope.target.milestoneId = null;

		if (projectId) {
			$scope.isSelectedProject = true;
			targetUtility.getMilestone(projectId, $scope);

			targetFactory.getProjectOrMilestoneDateById(projectId, null).success(function (projectDate) {
				$scope.target.startDate = new Date(projectDate.startDate);
				$scope.target.endDate = new Date(projectDate.endDate);
				targetDate.projectStartDate = $scope.target.startDate;
				targetDate.projectEndDate = $scope.target.endDate;
			}).error(function () {
				notificationFactory.error("ErrorMessage");
			});

			projectDataFactory.getProjectById(projectId).then(function (project) {
				$scope.target.projectManager = project.projectManager;
			}, function (status) {
				ErrorService.errorHandle(status);
			});
		}
		else {
			$scope.target.startDate = null;
			$scope.target.endDate = null;
		}
	};

	$scope.milestoneOnChange = function (projectId, milestoneId) {
		if (milestoneId) {
			targetFactory.getProjectOrMilestoneDateById(projectId, milestoneId).success(function (milestoneDate) {
				$scope.target.startDate = new Date(milestoneDate.startDate);
				$scope.target.endDate = new Date(milestoneDate.endDate);
				targetDate.milestoneStartDate = $scope.target.startDate;
				targetDate.milestoneEndDate = $scope.target.endDate;
			}).error(function () {
				notificationFactory.error("ErrorMessage");
			});
		}
		else {
			$scope.target.startDate = targetDate.projectStartDate;
			$scope.target.endDate = targetDate.projectEndDate;
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

	$scope.save = function (target) {
		$scope.submitted = true;
		$scope.disabledSubmit = true;

		if (targetUtility.validateTarget(target)) {
			var newTarget = angular.copy(target);

			newTarget.startDate = newTarget.startDate ? newTarget.startDate.toDateString() : null;
			newTarget.endDate = newTarget.endDate ? newTarget.endDate.toDateString() : null;

			targetFactory.createTarget(newTarget).success(function (response) {
				if (response.isSuccessful === true) {
					notificationFactory.success("DataChangeSuccessfully");
					$timeout(function () {
						$location.url('/Target/ViewTarget/');
					}, 2000);
				}
				else {
					notificationFactory.error('', response.message);
				}

				$scope.disabledSubmit = false;
			})
			.error(function (response, status) {
				if (status === 405) {
					ErrorService.errorHandle(status);
				}
				else {
					notificationFactory.error("ErrorMessage");
				}

				$scope.disabledSubmit = false;
			});
		}
		else {
			$scope.disabledSubmit = false;
		}
	};

	targetUtility.showMessageAfterValidate($scope);

	$scope.cancel = function () {
		$window.history.back();
	};

	$scope.backToView = function () {
		$location.url('/Target/ViewTarget/');
	};
} ]);

// View Detail of Target or Edit Target Controller
targetModule.controller("TargetDetailAndEditController",
['$scope', '$location', '$routeParams', '$window', 'targetUtility', 'targetFactory', 'employeeEPAFactory',
function ($scope, $location, $routeParams, $window, targetUtility, targetFactory, employeeEPAFactory) {

	var targetId = parseInt($routeParams.id),
		projectId = parseInt($routeParams.projectId),
		isMemberSelect = $routeParams.viewMembers ? true : false,
		isKPISelect = $routeParams.viewKPIs ? true : false;

	if (isMemberSelect || isKPISelect) {
		$scope.tabGeneralSelect = false;
		$scope.tabMemberSelect = isMemberSelect;
		$scope.tabKPISelect = isKPISelect;
	}

	targetFactory.getPermission().success(function (policy) {
		$scope.permission = {
			policy: policy
		};
	});

	employeeEPAFactory.getPermission().then(function (policy) {
		$scope.permissionMemberEPA = {
			policy: policy
		};
	});

	targetUtility.getTabHeading($scope);

	$scope.editTarget = function () {
		var tabSelect = isMemberSelect ? '&viewMembers' : (isKPISelect ? '&viewKPIs' : '');
		$scope.editTargetUrl = '/Target/EditTarget/' + targetId + '?projectId=' + projectId + '&isEditMode' + tabSelect;
	};

	$scope.cancel = function () {
		$window.history.back();
	};

	$scope.backToView = function () {
		$location.url('Target/ViewTarget');
	};
} ]);

// Target General Information Controller
targetModule.controller('TargetGeneralInformationController',
['$scope', '$routeParams', '$timeout', 'targetFactory', 'targetUtility', 'KPICommonRepository', 'commonFactory',
'notificationFactory', 'ErrorService', 'projectDataFactory',
function ($scope, $routeParams, $timeout, targetFactory, targetUtility, KPICommonRepository, commonFactory,
notificationFactory, ErrorService, projectDataFactory) {

	var targetId = parseInt($routeParams.id),
		isEditMode = $routeParams.isEditMode ? true : false;

	$scope.isEditMode = isEditMode;
	$scope.generalInformationLoaded = false;

	$scope.$watch("tabGeneralSelect", function (newVal) {
		if (newVal === true && $scope.generalInformationLoaded === false) {
			targetFactory.getTargetById(targetId)
			.success(function (target) {
				if (isEditMode) {
					target.startDate = target.startDate !== "" ? new Date(target.startDate) : "";
					target.endDate = target.endDate !== "" ? new Date(target.endDate) : "";
				}

				$scope.target = target;

				if (target.targetType && target.targetType.targetTypeId) {
					$scope.target.targetTypeId = target.targetType.targetTypeId;
				}

				if (target.project && target.project.projectId) {
					targetUtility.getAllProject($scope, target.project.projectId);
					$scope.target.projectId = target.project.projectId;

					$scope.isSelectedProject = true;
					targetUtility.getMilestone($scope.target.projectId, $scope);

					if (target.milestone && target.milestone.milestoneId) {
						$scope.target.milestoneId = target.milestone.milestoneId;
					}
				}

				if (target.projectManager) {
					$scope.target.projectManagerId = target.projectManager.employeeId;
				}
			})
			.error(function (response, status) {
				if (status === 405) {
					ErrorService.errorHandle(status);
				}
				else {
					notificationFactory.error('ErrorMessage');
				}
			});

			targetUtility.getFormOptions($scope, KPICommonRepository);
			commonFactory.getDateFormat($scope);
			targetUtility.getAllStatus($scope);
			projectDataFactory.getEmployeeSelectList(3).then(function (projectManagers) {
				$scope.projectManagerOptions = projectManagers;
			});
			$scope.generalInformationLoaded = true;
		}
	});

	$scope.projectOnChange = function (projectId) {
		$scope.isSelectedProject = false;

		if (projectId) {
			if ($scope.target) {
				$scope.target.milestoneId = '';
			}

			$scope.isSelectedProject = true;
			targetUtility.getMilestone(projectId, $scope);

			projectDataFactory.getProjectById(projectId).then(function (project) {
				$scope.target.projectManagerId = project.projectManager;
			}, function (status) {
				ErrorService.errorHandle(status);
			});
		}
	};

	if (isEditMode) {
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

		targetUtility.showMessageAfterValidate($scope);

		$scope.save = function (target) {
			$scope.submitted = true;
			$scope.disabledSubmit = true;

			var newTarget = {
				targetId: target.targetId,
				name: target.name,
				description: target.description,
				targetTypeId: target.targetTypeId,
				projectId: target.projectId,
				projectManager: target.projectManagerId,
				milestoneId: target.milestoneId,
				status: target.status,
				startDate: target.startDate ? target.startDate.toDateString() : null,
				endDate: target.endDate ? target.endDate.toDateString() : null
			};

			if (targetUtility.validateTarget(newTarget) === true) {
				targetFactory.editTarget(newTarget).success(function (response) {
					if (response.isSuccessful === true) {
						notificationFactory.success("DataChangeSuccessfully");
					}
					else {
						notificationFactory.error('', response.message);
					}

					$scope.disabledSubmit = false;
				})
				.error(function (response, status) {
					if (status === 405) {
						ErrorService.errorHandle(status);
					}
					else {
						notificationFactory.error('ErrorMessage');
					}

					$scope.disabledSubmit = false;
				});
			}
			else {
				$scope.disabledSubmit = false;
			}
		};
	}
} ]);

// Target Members Controller
targetModule.controller('TargetMembersController',
['$scope', '$modal', '$routeParams', 'targetFactory', 'ErrorService', 'targetUtility', 'employeeRepository',
'notificationFactory',
function ($scope, $modal, $routeParams, targetFactory, ErrorService, targetUtility, employeeRepository,
notificationFactory) {

	var targetId = parseInt($routeParams.id),
		projectId = parseInt($routeParams.projectId),
		isEditMode = $routeParams.isEditMode ? true : false;

	$scope.isEditMode = isEditMode;
	$scope.memberLoaded = false;

	$scope.search = {
		searchKey: ''
	};

	$scope.$watch("tabMemberSelect", function (newValue) {
		if (newValue === true && $scope.memberLoaded === false) {
			targetFactory.getEmployeeByTargetId(targetId).success(function (employees) {
				$scope.employees = employees;
			}).error(function (response, status) {
				if (status === 405) {
					ErrorService.errorHandle(status);
				}
				else {
					notificationFactory.error("ErrorMessage");
				}
			});

			$scope.memberLoaded = true;
		}
	});

	$scope.viewEPADetail = function (employeeId) {
		$scope.viewEPADetailUrl = '/EmployeeEPA/Index' + '?targetId=' + targetId + '&employeeId=' + employeeId;
	};

	if (isEditMode) {
		$scope.searchEmployees = [];

		// get employee by projectID and search key
		$scope.getEmployee = function (searchKey) {
			employeeRepository.getEmployeeByProjectId(projectId, searchKey).success(function (employeeList) {
				$scope.searchEmployees = employeeList;
				$scope.$apply;
			}).error(function () {
				notificationFactory.error("ErrorMessage");
			});
		};

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
						notificationFactory.success("DataChangeSuccessfully");
					}
					else {
						notificationFactory.error("DataChangeFail", response.message);
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
} ]);

// Target KPI Controller
targetModule.controller("TargetKPIController",
['$scope', '$routeParams', 'targetFactory', 'ErrorService', 'notificationFactory',
function ($scope, $routeParams, targetFactory, ErrorService, notificationFactory) {

	var targetId = parseInt($routeParams.id);
	$scope.kpiLoaded = false;

	var getKPIByTargetId = function (targetId, groupId) {
		$scope.disabledSubmitButton = true;

		targetFactory.getKPIByTargetId(targetId, groupId).success(function (listGroupKPI) {
			$scope.listGroupKPI = listGroupKPI;
			$scope.disabledSubmitButton = false;
		}).error(function (response, status) {
			if (status === 405) {
				ErrorService.errorHandle(status);
			}
			else {
				notificationFactory.error("ErrorMessage");
			}

			$scope.disabledSubmitButton = false;
		});
	};

	$scope.$watch("tabKPISelect", function (newValue) {
		if (newValue === true && $scope.kpiLoaded === false) {
			targetFactory.getKPIGroupByTargetId(targetId).success(function (groupList) {
				$scope.groupOptions = groupList;
			}).error(function () {
				notificationFactory.error("ErrorMessage");
			});

			getKPIByTargetId(targetId, null);
			$scope.kpiLoaded = true;
		}
	});

	$scope.filter = function (kpiGroupId) {
		getKPIByTargetId(targetId, kpiGroupId);
	};

	$scope.calculateIndex = function (parentIndex, childIndex, listKPI) {
		var index = 0;

		if (parentIndex === 0) {
			index = childIndex + 1;
		}
		else {
			$.each(listKPI, function (i) {
				if (i < parentIndex) {
					index += listKPI[i].kpIs.length;
				}
			});

			index += childIndex + 1;
		}

		return index;
	};
} ]);
