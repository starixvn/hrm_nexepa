/* global projectsSettingModule */
'use strict';

projectsSettingModule.controller('ProjectController',
['$scope', '$location', 'projectDataFactory', 'ProjectSettingCache', 'ErrorService', 'tabService', 'notificationFactory', '$modal',
function ($scope, $location, projectDataFactory, ProjectSettingCache, ErrorService, tabService, notificationFactory, $modal) {
	/**
	* Project Setting Cache. 
	*/
	var cacheListPM = ProjectSettingCache.get("CacheListPM");
	var cacheListStatus = ProjectSettingCache.get("CacheListStatus");
	var cacheListPeriod = ProjectSettingCache.get("CacheListPeriod");
	/**
	* Ajax event Loaded. 
	*/
	// Get data to ProjectManager - Select List
	var successGetEmployeeSelectList = function (data) {
		$scope.pms = data;
		ProjectSettingCache.put("CacheListPM", data);
	};

	// Get data to Status - Select List
	var successGetProjectStatusSelectList = function (data) {
		$scope.status = data;
		ProjectSettingCache.put("CacheListStatus", data);
	};

	// Get data to Period - Select List
	var successGetPeriodSelectList = function (data) {
		$scope.periods = data;
		ProjectSettingCache.put("CacheListPeriod", data);
	};

	// Filter data by criteria.
	var successFilterData = function (data) {
		$scope.projects = data;
	};

	var errorHandle = function (status) {
		ErrorService.errorHandle(status);
	};

	var successUpdateProjectStatus = function () {
		notificationFactory.success('SuccessSaveData', '');
		projectDataFactory.filterData(null).then(successFilterData, errorHandle);
	};

	var successDeleteProject = function () {
		notificationFactory.success('SuccessDeleteData', '');
		projectDataFactory.filterData(null).then(successFilterData, errorHandle);
	};

	var errorNotification = function (response) { notificationFactory.error('', response); };

	/**
	* Initialize data to view Index.
	*/
	projectDataFactory.filterData(null).then(successFilterData, errorHandle);

	// Check for list ProjectManager has cache data.
	if (cacheListPM) {
		$scope.pms = cacheListPM;
	}
	else {
		projectDataFactory.getEmployeeSelectList(1).then(successGetEmployeeSelectList);
	}

	// Check for list Status has cache data.
	if (cacheListStatus) {
		$scope.status = cacheListStatus;
	}
	else {
		projectDataFactory.getProjectStatusSelectList().then(successGetProjectStatusSelectList);
	}

	// Check for list Periods has cache data.
	if (cacheListPeriod) {
		$scope.periods = cacheListPeriod;
	}
	else {
		projectDataFactory.getProjectPeriodSelectList().then(successGetPeriodSelectList);
	}

	/**
	* Event: Filter 
	*/
	$scope.getProjectList = function (filter) {
		projectDataFactory.filterData(filter).then(successFilterData);
	};

	$scope.editProject = function (projectId) {
		$location.path('/Projects/Edit/' + projectId).search('isEditMode', true);
	};

	// View detail of project
	$scope.detailProject = function (project, isMemberSelected) {
		$location.path('/Projects/Detail/' + project.projectId);
		tabService.setProperty(isMemberSelected);
	};

	$scope.updateProjectStatus = function (project, status) {
		var updateModalInstance = $modal.open({
			templateUrl: '/templates/Common/ModalConfirmContent.html',
			controller: "ModalConfirmController",
			resolve: {
				name: function () {
					return project.name;
				},
				action: function () {
					return "change status";
				}
			}
		});
		updateModalInstance.result.then(function () {
			project.status = status;
			projectDataFactory.updateProjectStatus(project).then(successUpdateProjectStatus, errorNotification);
		});
	};

	// Delete Milestone.
	$scope.deleteProject = function (project) {
		var deleteModalInstance = $modal.open({
			templateUrl: '/templates/Common/ModalConfirmContent.html',
			controller: "ModalConfirmController",
			resolve: {
				name: function () {
					return project.name;
				},
				action: function () {
					return "delete";
				}
			}
		});
		deleteModalInstance.result.then(function () {
			projectDataFactory.deleteProject(project.projectId).then(successDeleteProject, errorNotification);
		});
	};

	$scope.toggleDropdown = function ($event) {
		$event.preventDefault();
		$event.stopPropagation();
		$scope.status.isopen = !$scope.status.isopen;
	};

} ]);

projectsSettingModule.controller('CreateProjectController',
['$scope', '$location', '$window', 'projectDataFactory', 'ProjectSettingCache', 'commonFactory', '$timeout', 'notificationFactory',
function ($scope, $location, $window, projectDataFactory, ProjectSettingCache, commonFactory, $timeout, notificationFactory) {
	/**
	* Variables. 
	*/
	var cacheListPM = ProjectSettingCache.get("CacheListPM");
	var cacheListStatus = ProjectSettingCache.get("CacheListStatus");
	$scope.errors = [];
	$("#projectValid").hide();

	/**
	* Ajax event. 
	*/
	// Get data to ProjectManager - Select List
	var successGetEmployeeSelectList = function (data) {
		$scope.pms = data;
		ProjectSettingCache.put("CacheListPM", data);
	};

	// Get data to Status - Select List
	var successGetProjectStatusSelectList = function (data) {
		$scope.status = data;
		ProjectSettingCache.put("CacheListStatus", data);
	};

	// Success add new project callback.
	var successAddProject = function () {
		$scope.errors = [];
		notificationFactory.success('SuccessSaveData', '');
		$timeout(function () {
			$location.url('/Projects/Index');
		}, 2000);
	};

	// Errors occur during Add process.
	var errorAddProject = function (response) { $scope.errors = response; };

	/**
	* Initialize data to view Create Project. 
	*/
	// Check for list ProjectManager has cache data.
	if (cacheListPM) {
		$scope.pms = cacheListPM;
	}
	else {
		projectDataFactory.getEmployeeSelectList(1).then(successGetEmployeeSelectList);
	}

	// Check for list Status has cache data.
	if (cacheListStatus) {
		$scope.status = cacheListStatus;
	}
	else {
		projectDataFactory.getProjectStatusSelectList().then(successGetProjectStatusSelectList);
	}

	// get Date Format
	commonFactory.getDateFormat($scope);

	/**
	* Events 
	*/
	// Save
	$scope.save = function (project) {
		if (project == null) {
			$("#projectValid").show();
		}
		else {
			$("#projectValid").hide();
			if (project.startDate != null) {
				var selectedStartDate = new Date(project.startDate);
				project.startDate = selectedStartDate ? selectedStartDate.toDateString() : null;
			}
			if (project.endDate != null) {
				var selectedEndDate = new Date(project.endDate);
				project.endDate = selectedEndDate ? selectedEndDate.toDateString() : null;
			}
			projectDataFactory.addProject(project).then(successAddProject, errorAddProject);
		}
	};

	// Cancel
	$scope.cancel = function () { $window.history.back(); };

	// show start date calendar when click button start date
	$scope.openPopupStartDate = function ($event) {
		commonFactory.openPopupStartDate($event, $scope);
	};

	// show end date calendar when click button end date
	$scope.openPopupEndDate = function ($event) {
		commonFactory.openPopupEndDate($event, $scope);
	};

} ]);

projectsSettingModule.controller('ViewDetailAndEditProjectController',
['$scope', '$routeParams', '$location', '$window', '$modal', 'projectDataFactory', 'ProjectSettingCache',
'commonFactory', 'tabService', 'notificationFactory', 'ErrorService',
function ($scope, $routeParams, $location, $window, $modal, projectDataFactory,
ProjectSettingCache, commonFactory, tabService, notificationFactory, ErrorService) {
	/**
	* Scope variables. 
	*/
	var projectId = parseInt($routeParams.projectId);
	$scope.isEditMode = $routeParams.isEditMode ? true : false;
	$scope.errors = [];
	$scope.dataTab = {};
	var cacheListPM = ProjectSettingCache.get("CacheListPM");
	var cacheListStatus = ProjectSettingCache.get("CacheListStatus");

	/**
	* Ajax event. 
	*/
	// Get data to ProjectManager - Select List
	var successGetEmployeeSelectList = function (data) { $scope.pms = data; };

	// Get data to Status - Select List
	var successGetProjectStatusSelectList = function (data) { $scope.status = data; };

	// Get project by Id
	var successGetProjectById = function (data) { $scope.projectUpdate = data; };

	// Update Project success callback.
	var successUpdateProject = function () { notificationFactory.success('SuccessSaveData', ''); };

	// Update Project failure callback.
	var errorUpdateProject = function (response) {
		$scope.errors = response;
	};

	var errorHandle = function (status) {
		ErrorService.errorHandle(status);
	};

	/**
	* Initialize data to view Edit Project. 
	*/
	// Check for list ProjectManager has cache data.
	if (cacheListPM) {
		$scope.pms = cacheListPM;
	}
	else {
		projectDataFactory.getEmployeeSelectList(1).then(successGetEmployeeSelectList);
	}

	// Check for list Status has cache data.
	if (cacheListStatus) {
		$scope.status = cacheListStatus;
	}
	else {
		projectDataFactory.getProjectStatusSelectList().then(successGetProjectStatusSelectList);
	}

	// Get Selected Tab in Edit Project Information Page.
	if (tabService.getProperty() === true) {
		$scope.dataTab.member = true;
	}
	else {
		$scope.dataTab.information = true;
	}

	projectDataFactory.getProjectById(projectId).then(successGetProjectById, errorHandle);

	// get Date Format
	commonFactory.getDateFormat($scope);

	/**
	* Events 
	*/
	// Update
	$scope.update = function (project) {
		projectDataFactory.updateProject(project).then(successUpdateProject, errorUpdateProject);
	};

	// Cancel
	$scope.cancel = function () {
		$window.history.back();
	};

	$scope.calendar = {
		endDateOpened: false,
		startDateOpened: false
	};

	// show start date calendar when click button start date
	$scope.openPopupStartDate = function ($event) {
		commonFactory.openPopupStartDate($event, $scope);
	};

	// show end date calendar when click button end date
	$scope.openPopupEndDate = function ($event) {
		commonFactory.openPopupEndDate($event, $scope);
	};

	// Edit project
	$scope.editProject = function () {
		$location.path('/Projects/Edit/' + projectId).search('isEditMode', true);
	};

} ]);

projectsSettingModule.controller('MilestoneController',
['$scope', '$routeParams', '$modal', 'projectMilestoneService', 'projectDataFactory',
'milestoneFactory', 'notificationFactory', 'commonFactory', 'ProjectSettingCache',
function ($scope, $routeParams, $modal, projectMilestoneService, projectDataFactory, milestoneFactory, notificationFactory,
commonFactory, ProjectSettingCache) {
	/**
	* Scope variables. 
	*/
	var projectId = $routeParams.projectId;
	$scope.isEditMode = $routeParams.isEditMode ? true : false;
	$scope.milestones = [];
	$scope.milestoneErrors = [];
	$scope.addMode = false;
	var cacheListStatus = ProjectSettingCache.get("CacheListStatus");

	/**
	* Ajax event. 
	*/
	// Notify error popup when action is failure.
	var errorNotification = function (data) {
		notificationFactory.error('', data);
	};

	// Notify success popup when action is succeed.
	var successNotification = function () {
		notificationFactory.success('SuccessSaveData', '');
		return milestoneFactory.getMilestone(projectId).then(successGetMilestones, errorNotification);
	};

	// Get Milestone
	var successGetMilestones = function (data) {
		$scope.milestones = data;
		projectMilestoneService.setProperty(data);
		$scope.milestoneErrors = {};
	};

	// Callback Success for Post data.
	var successAddMilestone = function (data, status, headers, config) {
		successNotification(data, status, headers, config).then(function () {
			$scope.toggleAddMode();
			$scope.milestone = {};
		});
	};

	var errorAddUpdateMilestone = function (data) {
		$scope.milestoneErrors = data;
	};

	var successDeleteMilestone = function () {
		notificationFactory.success('SuccessDeleteData', '');
		return milestoneFactory.getMilestone(projectId).then(successGetMilestones, errorNotification);
	};

	// Get data to Status - Select List
	var successGetMilestoneStatusSelectList = function (data) {
		$scope.milestoneStatus = data;
		ProjectSettingCache.put("CacheListStatus", data);
	};

	/**
	* Switch mode Add/Update/Normal
	*/
	$scope.toggleAddMode = function () {
		$scope.addMode = !$scope.addMode;
		if ($scope.addMode === false) {
			$scope.milestoneErrors = {};
		}
	};

	$scope.toggleEditMode = function (milestone) {
		milestone.editMode = !milestone.editMode;
		if (milestone.editMode === false) {
			milestoneFactory.getMilestone(projectId).then(successGetMilestones, errorNotification);
		}
	};

	/**
	* Initialize data to view Project Milestone 
	*/
	milestoneFactory.getMilestone(projectId).then(successGetMilestones, errorNotification);

	// Check for list Status has cache data.
	if (cacheListStatus) {
		$scope.milestoneStatus = cacheListStatus;
	}
	else {
		projectDataFactory.getProjectStatusSelectList().then(successGetMilestoneStatusSelectList);
	}

	// get Date Format
	commonFactory.getDateFormat($scope);
	/**
	* Events 
	*/

	// show start date calendar when click button start date in Add Mode
	$scope.openPopupAddModeStartDate = function ($event) {
		commonFactory.openPopupAddModeStartDate($event, $scope);
	};

	// show end date calendar when click button end date in Add Mode
	$scope.openPopupAddModeEndDate = function ($event) {
		commonFactory.openPopupAddModeEndDate($event, $scope);
	};

	// show start date calendar when click button start date in Edit Mode
	$scope.openPopupEditModeStartDate = function ($event, milestoneId) {
		commonFactory.openPopupEditModeStartDate($event, $scope, milestoneId);
	};

	// show end date calendar when click button end date in Edit Mode.
	$scope.openPopupEditModeEndDate = function ($event, milestoneId) {
		commonFactory.openPopupEditModeEndDate($event, $scope, milestoneId);
	};

	// Add Milestone.
	$scope.addMilestone = function () {
		if ($scope.milestone == null) {
			var errorMessage = "Please fill out all required (*) fields.";
			$scope.milestoneErrors = errorMessage.split(',');
		}
		else {
			var milestone = $scope.milestone;
			if (milestone.startDate != null) {
				var selectedStartDate = new Date(milestone.startDate);
				milestone.startDate = selectedStartDate ? selectedStartDate.toDateString() : null;
			}
			if (milestone.endDate != null) {
				var selectedEndDate = new Date(milestone.endDate);
				milestone.endDate = selectedEndDate ? selectedEndDate.toDateString() : null;
			}
			milestone.projectId = projectId;
			milestoneFactory.addMilestone(milestone).then(successAddMilestone, errorAddUpdateMilestone);
		}
	};
	// Update Milestone.
	$scope.updateMilestone = function (milestone) {
		milestoneFactory.updateMilestone(milestone).then(successNotification, errorAddUpdateMilestone);
	};
	// Delete Milestone.
	$scope.deleteMilestone = function (milestone) {
		var deleteModalInstance = $modal.open({
			templateUrl: '/templates/Common/ModalConfirmContent.html',
			controller: "ModalConfirmController",
			resolve: {
				name: function () {
					return milestone.name;
				},
				action: function () {
					return "delete";
				}
			}
		});
		deleteModalInstance.result.then(function () {
			milestoneFactory.deleteMilestone(milestone.milestoneId).then(successDeleteMilestone, errorNotification);
		});
	};
} ]);