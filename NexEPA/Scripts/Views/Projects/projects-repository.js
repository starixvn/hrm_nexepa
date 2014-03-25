/* global projectsSettingModule, toastr */
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
        },
        renderProjectPeriodSelectList: function () {
            var deferred = $q.defer();
            $http.get('/Project/RenderProjectPeriodSelectList').success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }
    };
});

projectsSettingModule.factory("projectsFactory", function ($resource) {
    return $resource('/api/Project', {}, {
        query: { method: 'GET', isArray: true },
        create: { method: 'POST', isArray: true }
    });
});

projectsSettingModule.factory("projectFactory", function ($resource) {
    return $resource('/api/Project/:id', {}, {
        details: { method: 'GET' },
        update: { method: 'PUT'},
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

projectsSettingModule.service('projectService', function () {
    var property;
    return {
        getProperty: function () {
            return property;
        },
        setProperty: function (value) {
            property = value;
        }
    };
});


var url = '/api/Milestone/';


projectsSettingModule.factory('milestoneFactory', function ($http) {
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

projectsSettingModule.factory('notificationFactory', function () {

    return {
        success: function () {
            toastr.success("Success");
        },
        error: function (text) {
            toastr.error(text, "Error!");
        }
    };
});





