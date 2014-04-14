/* global projectsSettingModule */
'use strict';

projectsSettingModule.factory("projectMembersFactory", function ($http, $q) {
	var url = '/api/ProjectMembers/';
	return {
		getMember: function (projectId, isShowDeletedMember, milestoneId) {
			var deferred = $q.defer();
			$http.get(url, { params: {
				projectId: projectId,
				isShowDeletedMember: isShowDeletedMember != null ? isShowDeletedMember : null,
				milestoneId: milestoneId != null ? milestoneId : ""
			}
			}).success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		},
		getProjectRole: function (projectId, employeeId) {
			var deferred = $q.defer();
			$http.get('/api/ProjectMembers/ProjectRole', { params:
			{
				projectId: projectId,
				employeeId: employeeId
			} 
			}).success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		},
		getMemberRoles: function (projectId, employeeId) {
			var deferred = $q.defer();
			$http.get('/api/ProjectMembers/GetMemberRoleInProject/', { params: {
				projectId: projectId, employeeId: employeeId
			}
			}).success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		},
		getMilestone: function (projectId) {
			var deferred = $q.defer();
			$http.get('/api/Milestone/GetMilestones', { params: { projectId: projectId} }).success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		},
		getProjectById: function (projectId) {
			var deferred = $q.defer();
			$http.get('/api/Project/GetProjectById/', { params: { projectId: projectId} }).success(deferred.resolve).error(deferred.reject);
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
			$http['delete'](url, { params: {
				projectId: member.projectId,
				employeeId: member.employeeId,
				projectRoleId: member.projectRoleId
			}
			}).success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		}
	};
});