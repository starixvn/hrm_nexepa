/* global projectsSettingModule */
'use strict';

projectsSettingModule.factory("projectMembersFactory", ['$http', '$q', function ($http, $q) {
    var url = '/api/ProjectMember/';
    return {
        getMember: function (projectId, isShowDeletedMember, milestoneId) {
            var deferred = $q.defer();
            $http.get(url + 'Get', {
                params: {
                    projectId: projectId,
                    isShowDeletedMember: isShowDeletedMember != null ? isShowDeletedMember : null,
                    milestoneId: milestoneId != null ? milestoneId : ""
                }
            }).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        },
        getMembersByProject: function (projectId) {
            var deferred = $q.defer();
            $http.get(url + 'GetMembersByProject', {
                params: {
                    projectId: projectId,
                }
            }).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        },
        getGroupedMembersByProject: function (projectId) {
            var deferred = $q.defer();
            $http.get(url + 'GetGroupedMembersByProject', {
                params: {
                    projectId: projectId,
                }
            }).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        },
        getProjectRole: function (projectId, employeeId) {
            var deferred = $q.defer();
            $http.get('/api/ProjectMember/ProjectRole', {
                params:
                {
                    projectId: projectId,
                    employeeId: employeeId
                }
            }).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        },
        getMemberRoles: function (projectId, employeeId) {
            var deferred = $q.defer();
            $http.get('/api/ProjectMember/GetMemberRoleInProject/', {
                params: {
                    projectId: projectId, employeeId: employeeId
                }
            }).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        },
        getMilestone: function (projectId) {
            var deferred = $q.defer();
            $http.get('/api/Milestone/GetMilestones', { params: { projectId: projectId } }).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        },
        getProjectById: function (projectId) {
            var deferred = $q.defer();
            $http.get('/api/Project/GetProjectById/', { params: { projectId: projectId } }).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        },
        addMember: function (member) {
            var deferred = $q.defer();
            $http.post(url, member).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        },
        updateMember: function (member) {
            var deferred = $q.defer();
            $http.put(url, member).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        },
        deleteMember: function (member) {
            var deferred = $q.defer();
            $http['delete'](url, {
                params: {
                    projectId: member.projectId,
                    employeeId: member.employeeId
                }
            }).success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }
    };
}]);