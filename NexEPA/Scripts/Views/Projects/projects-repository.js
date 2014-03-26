/* global projectsSettingModule */
'use strict';

projectsSettingModule.factory('projectDataFactory', function ($http, $q) {
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
        },
        renderProjectPeriodSelectList: function () {
            var deferred = $q.defer();
            $http.get('/Project/RenderProjectPeriodSelectList').success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        },
        getProjectById: function (projectId) {
            var deferred = $q.defer();
            $http.get('/Project/GetProjectById/', { params: {projectId: projectId} }).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }
    };
});

projectsSettingModule.factory("projectsFactory", function ($resource) {
    return $resource('/api/Project', {}, {
        query: { method: 'GET', isArray: true },
        create: { method: 'POST', isArray: true },
        update: { method: 'PUT'}
    });
});

projectsSettingModule.factory("projectFactory", function ($resource) {
    return $resource('/api/Project/:id', {}, {
        details: { method: 'GET' },
        deleteItem: { method: 'DELETE', params: { id: '@id'} }
    });
});


projectsSettingModule.factory("dataFilterFactory", function ($http, $q) {
    return {
        filterData: function (filter) {
            var deferred = $q.defer();
            $http.get('/api/Project/', { params: {
                projectManager: filter != null ? filter.projectManager : "",
                status: filter != null ? filter.status : "",
                period: filter != null ? filter.period : ""
            }
        }).success(deferred.resolve).error(deferred.reject);

            return deferred.promise;
        }
    };
});

projectsSettingModule.factory('milestoneFactory', function ($http) {
    var url = '/api/Milestone/';
    return {
        getMilestone: function (projectId) {
            return $http.get(url, {params: {projectId: projectId}});
        },
        addMilestone: function (milestone) {
            return $http.post(url, milestone);
        },
        deleteMilestone: function (milestoneId) {
            return $http['delete']('/api/Milestone/', { params: { milestoneId: milestoneId} });
        },
        updateMilestone: function (milestone) {
            return $http.put(url + milestone.milestoneId, milestone);
        }
    };
});





