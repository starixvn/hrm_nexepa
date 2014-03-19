/* global registrationModule */
'use strict';

projectsSettingModule.factory('selectListDataFactory', function ($http, $q) {
    return {
        renderPMSelectList: function () {
            var deferred = $q.defer();
            $http.get('/Project/RenderPMSelectList').success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        },
        renderStatusSelectList: function () {
            var deferred = $q.defer();
            $http.get('/Project/RenderProjectStatusSelectList').success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }
    };
});

projectsSettingModule.factory("projectsFactory", function ($resource) {
    return $resource('/api/Project', {}, {
        query: { method: 'GET', isArray: true },
        create: { method: 'POST' }
    });
});

projectsSettingModule.factory("projectFactory", function ($resource) {
    return $resource('/api/Project/:id', {}, {
        details: { method: 'GET' },
        update: { method: 'PUT', params: { id: '@id'} },
        deleteItem: { method: 'DELETE', params: { id: '@id'} }
    });
});




