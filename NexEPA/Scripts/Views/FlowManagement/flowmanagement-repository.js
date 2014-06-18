/* global flowManagementModule */
'use strict';

flowManagementModule.factory('flowDataFactory', ['$http', '$q', function ($http, $q) {
	return {
		getMilestone: function (projectId) {
			var deferred = $q.defer();
			$http.get('/api/Milestone/GetMilestones', { params: { projectId: projectId} }).success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		}
	};
} ]);