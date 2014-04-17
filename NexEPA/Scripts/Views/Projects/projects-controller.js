/* global projectsSettingModule */
'use strict';

projectsSettingModule.controller('ProjectController', function ($scope, $location, projectDataFactory, ProjectSettingCache, ErrorService) {
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
});

projectsSettingModule.controller('CreateProjectController',
function ($scope, $location, $window, projectDataFactory, ProjectSettingCache) {
	var cacheListPM = ProjectSettingCache.get("CacheListPM");
	var cacheListStatus = ProjectSettingCache.get("CacheListStatus");
	$scope.errors = [];
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
	var successAddProject = function () { $location.url('/Projects/Index'); };

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

	/**
	* Events 
	*/
	// Save
	$scope.save = function (project) {
		projectDataFactory.addProject(project).then(successAddProject, errorAddProject);
	};

	// Cancel
	$scope.cancel = function () { $window.history.back(); };
});

projectsSettingModule.controller('EditProjectController', function ($scope, $routeParams, $location, $window, projectDataFactory, ProjectSettingCache) {
    /**
    * Scope variables. 
    */
    var projectId = $routeParams.projectId;
    $scope.errors = [];
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
    var successUpdateProject = function () { $location.url('/Projects/Index'); };

    // Update Project failure callback.
    var errorUpdateProject = function (response) {
        $scope.errors = response;
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

    projectDataFactory.getProjectById(projectId).then(successGetProjectById);

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

	$(".boxtitle").on("click", function () {
		var header = $(this);
		//getting the next element
		var content = header.next();
		//open up the content needed - toggle the slide- if visible, slide up, if not slidedown.
		content.slideToggle(500, function () {
		});
	});

});

projectsSettingModule.controller('MilestoneController',
function ($scope, $routeParams, $modal, projectMilestoneService, milestoneFactory, notificationFactory) {
	/**
	* Scope variables. 
	*/
	var projectId = $routeParams.projectId;
	$scope.milestones = [];
	$scope.milestoneErrors = [];
	$scope.addMode = false;

	/**
	* Ajax event. 
	*/
	// Notify error popup when action is failure.
	var errorNotification = function (data) {
		notificationFactory.error(data);
	};

	// Notify success popup when action is succeed.
	var successNotification = function () {
		notificationFactory.success();
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

	/**
	* Events 
	*/
	// Add Milestone.
	$scope.addMilestone = function () {
		if ($scope.milestone == null) {
			var errorMessage = "Please fill out all required (*) fields.";
			$scope.milestoneErrors = errorMessage.split(',');
		}
		else {
			$scope.milestone.projectId = projectId;
			milestoneFactory.addMilestone($scope.milestone).then(successAddMilestone, errorAddUpdateMilestone);
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
			milestoneFactory.deleteMilestone(milestone.milestoneId).then(successNotification, errorNotification);
		});

	};
});