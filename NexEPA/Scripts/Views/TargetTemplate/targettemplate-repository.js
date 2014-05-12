/* global targetTemplateModule */
'use strict';

targetTemplateModule.factory("targetTemplateFactory", ['$http', '$q', function ($http, $q) {
	var url = '/api/targetTemplate/';
	return {
		getTargetTemplateById: function (targetTemplateId) {
			var deferred = $q.defer();
			$http.get('/api/targetTemplate/GetTargetTemplateById/', { params: { targetTemplateId: targetTemplateId} })
            .success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		},
		getAllTargetType: function () {
			var deferred = $q.defer();
			$http.get('/api/targetTemplate/GetAllTargetType').success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		},
		getAllTargetTemplate: function (targetTypeId) {
			var deferred = $q.defer();
			$http.get('/api/targetTemplate/Get', { params: { targetTypeId: targetTypeId} })
			.success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		},
		getKPI: function (targetTemplateId, targetTypeId, groupId, onlySelected) {
			var deferred = $q.defer();
			$http.get('/api/targetTemplate/GetKPI/', { params: {
				targetTemplateId: targetTemplateId,
				targetTypeId: targetTypeId,
				groupId: groupId,
				onlySelected: onlySelected
			}
			})
            .success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		},
		getSelectedKPIByTargetTemplate: function (targetTemplateId) {
			var deferred = $q.defer();
			$http.get('/api/targetTemplate/GetSelectedKPIByTargetTemplate/', { params: { targetTemplateId: targetTemplateId} })
            .success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		},
		getSelectedKPIGroupByTargetTemplate: function (targetTemplateId) {
			var deferred = $q.defer();
			$http.get('/api/targetTemplate/GetSelectedKPIGroupByTargetTemplate', { params: { targetTemplateId: targetTemplateId} })
			.success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		},
		addTargetTemplate: function (template) {
			var deferred = $q.defer();
			$http.post('/api/targetTemplate/Post', template).success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		},
		addKPI: function (objectKPI) {
			var deferred = $q.defer();
			$http.post('/api/targetTemplate/AddKPI/', objectKPI)
            .success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		},
		addKPIGroups: function (objectKPIGroup) {
			var deferred = $q.defer();
			$http.post('/api/targetTemplate/AddKPIGroupWeight/', objectKPIGroup)
            .success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		},
		updateTargetTemplate: function (template) {
			var deferred = $q.defer();
			$http.put('/api/TargetTemplate/Put', template).success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		},
		deleteTargetTemplate: function (targetId) {
			var deferred = $q.defer();
			$http['delete'](url, { params: { targetId: targetId} })
            .success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		},
		updateTargetTemplateStatus: function (template) {
			var deferred = $q.defer();
			$http.put('/api/TargetTemplate/UpdateTargetTemplateStatus', template).success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		}
	};
} ]);