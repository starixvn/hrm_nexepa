/* global projectsSettingModule */
'use strict';

projectsSettingModule.factory('projectDataFactory', function ($http, $q) {
	var url = '/api/Project/';
	return {
		getEmployeeSelectList: function (roleId) {
			var deferred = $q.defer();
			$http.get('/api/Project/RenderEmployeeSelectList', {
				params: { roleId: roleId }
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
			$http.get('/api/Project/GetProjectById/', { params: { projectId: projectId} }).success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		},
		updateProject: function (project) {
			var deferred = $q.defer();
			$http.put(url, project).success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		},
		addProject: function (project) {
			var deferred = $q.defer();
			$http.post(url, project).success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		},
		filterData: function (filter) {
			var deferred = $q.defer();
			$http.get('/api/Project/Get', { params: {
				projectManager: filter != null ? filter.projectManager : "",
				status: filter != null ? filter.status : "",
				period: filter != null ? filter.period : ""
			}
			}).success(deferred.resolve).error(function () {
				deferred.reject;
			});
			return deferred.promise;
		}
	};
});

projectsSettingModule.factory('milestoneFactory', function ($http, $q) {
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
});