/* global targetTemplateModule */
'use strict';

targetTemplateModule.factory("targetTemplateFactory", function ($http, $q) {
    var url = '/api/targetTemplate/';
    return {
        getTargetTemplateById: function (targetId) {
            var deferred = $q.defer();
            $http.get('/api/targetTemplate/GetTargetTemplateById/', { params: { targetId: targetId} })
            .success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        },
        getAllTargetType: function () {
            var deferred = $q.defer();
            $http.get('/api/targetTemplate/GetAllTargetType').success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        },
        getAllTargetTemplate: function () {
            var deferred = $q.defer();
            $http.get('/api/targetTemplate/Get').success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        },
        addTargetTemplate: function (template) {
            var deferred = $q.defer();
            $http.post(url, template).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        },
        updateTargetTemplate: function (template) {
            var deferred = $q.defer();
            $http.put(url, template).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        },
        deleteTargetTemplate: function (targetId) {
            var deferred = $q.defer();
            $http['delete'](url, { params: { targetId: targetId} }).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }
    };
});