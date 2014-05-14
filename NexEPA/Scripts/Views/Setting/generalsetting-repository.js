/* global generalSettingModule */
'use strict';

generalSettingModule.factory("generalSettingFactory", ['$http', '$q', function ($http, $q) {
	return {
		getTargetSetting: function () {
			var deferred = $q.defer();
			$http.get('/api/targetTemplate/getTargetSetting')
			.success(deferred.resolve).error(function (data, status) {
				deferred.reject(status);
			});
			return deferred.promise;
		},
		setDefaultTargetTemplate: function (targetTemplate) {
			var deferred = $q.defer();
			$http.post('/api/targetTemplate/setDefaultTargetTemplate', targetTemplate)
			.success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		}
	};
} ]);