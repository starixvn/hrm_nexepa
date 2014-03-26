/* global projectsSettingModule */
'use strict';

projectsSettingModule.factory("projectMembersFactory", function ($http, $q) {
    var url = '/api/ProjectMembers/';
    return {
        getMember: function (projectId, isDisabled, milestoneId) {
            var deferred = $q.defer();
            $http.get(url, { params: {
                projectId: projectId,
                isDisabled: isDisabled != null ? isDisabled : null,
                milestoneId: milestoneId != null ? milestoneId : ""
            }
            }).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        },
        addMember: function (member) {
            var deferred = $q.defer();
            $http.post(url, member).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        },
        deleteMember: function (memberId) {
            var deferred = $q.defer();
            $http['delete'](url, { params: { memberId: memberId} }).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        },
        updateMember: function (member) {
            var deferred = $q.defer();
            $http.put(url, member).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        },
        getProjectRole: function () {
            var deferred = $q.defer();
            $http.put('/ProjectMembers/GetProjectRole').success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }
    };
});