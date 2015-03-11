/* global projectsSettingModule */
'use strict';

projectsSettingModule.factory('projectDataFactory', ['$http', '$q', function ($http, $q) {
	var url = '/api/Project/';
	return {
		getEmployeeSelectList: function (roleId, searchKey) {
			var deferred = $q.defer();
			$http.get('/api/Project/RenderEmployeeSelectList', {
				params: { roleId: roleId, searchKey: searchKey }
			}).success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		},
		getProjectStatusSelectList: function () {
			var deferred = $q.defer();
			$http.get('/api/Project/RenderProjectStatusSelectList').success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		},
		getProjectPeriodSelectList: function () {
			var deferred = $q.defer();
			$http.get('/api/Project/RenderProjectPeriodSelectList').success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		},
		getProjectById: function (projectId) {
			var deferred = $q.defer();
			$http.get('/api/Project/GetProjectById/', { params: { projectId: projectId} })
			.success(deferred.resolve).error(function (data, status) {
				deferred.reject(status);
			});
			return deferred.promise;
		},
		updateProject: function (project) {
			var deferred = $q.defer();
			$http.put(url, project).success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		},
		updateProjectStatus: function (project) {
			var deferred = $q.defer();
			$http.post('/api/Project/UpdateProjectStatus', project).success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		},
		addProject: function (project) {
			var deferred = $q.defer();
			$http.post('/api/Project/AddProject', project).success(deferred.resolve).error(function (data) {
				deferred.reject(data);
			});
			return deferred.promise;
		},
		filterData: function (filter) {
			var deferred = $q.defer();
			$http.get('/api/Project/Get', { params: {
				projectManager: filter != null ? filter.projectManager : "",
				status: filter != null ? filter.status : "",
				period: filter != null ? filter.period : ""
			}
			}).success(deferred.resolve).error(function (data, status) {
				deferred.reject(status);
			});

			return deferred.promise;
		},
		deleteProject: function (projectId) {
			var deferred = $q.defer();
			$http['delete'](url, { params: { projectId: projectId} }).success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		},
		currentUser: function () {
			var deferred = $q.defer();
			$http.get('/api/core/GetCurrentUser').success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		},
		getPermission: function () {
			var deferred = $q.defer();
			$http.get('/api/Project/GetPermission').success(deferred.resolve).error(function (data) {
				deferred.reject(data);
			});
			return deferred.promise;
		},
		getAllRedmineProjects: function () {
			var deferred = $q.defer();
			$http.get('/api/Project/RetrieveAllRedmineProjects').success(deferred.resolve).error(function (data, status) {
				deferred.reject(status);
			});

			return deferred.promise;
		}
	};
} ]);

projectsSettingModule.factory('milestoneFactory', ['$http', '$q', function ($http, $q) {
	var url = '/api/Milestone/';
	return {
		getMilestone: function (projectId) {
			var deferred = $q.defer();
			$http.get('/api/Milestone/GetMilestones', { params: { projectId: projectId} }).success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		},
		addMilestone: function (milestone) {
			var deferred = $q.defer();
			$http.post(url, milestone).success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		},
		deleteMilestone: function (milestoneId) {
			var deferred = $q.defer();
			$http['delete'](url, { params: { milestoneId: milestoneId} }).success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		},
		updateMilestone: function (milestone) {
			var deferred = $q.defer();
			$http.put(url, milestone).success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		}
	};
} ]);

projectsSettingModule.factory('projectUtility', ['notificationFactory', function (notificationFactory) {
	var utility = {};

	utility.validateProject = function (project) {
		if (!project) {
			return false;
		}
		else {
			if (!project.name) {
				return false;
			}
			if (!project.startDate) {
				return false;
			}
			if (!project.projectManager) {
				return false;
			}
			if (!project.status && project.status < 0) {
				return false;
			}
		}

		return true;
	};

	utility.showMessageAfterValidate = function ($scope) {
		$scope.$watchCollection('[submitted, projectForm.$invalid, projectForm.projectName.$error.required, \
			projectForm.startDate.$error.required, projectForm.projectManager.$error.required, \
			projectForm.status.$error.required ]',
			function (newVals) {
				if (newVals[0] && newVals[1]) {
					if (newVals[5]) {
						notificationFactory.error("StatusRequire");
					}
					if (newVals[4]) {
						notificationFactory.error("ProjectManagerRequire");
					}
					if (newVals[3]) {
						notificationFactory.error("StartDateRequire");
					}
					if (newVals[2]) {
						notificationFactory.error("NameRequire");
					}

					$scope.submitted = false;
				}
			});
	};

	return utility;
} ]);