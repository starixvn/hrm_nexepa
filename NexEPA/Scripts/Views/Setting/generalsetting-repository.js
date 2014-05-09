/* global generalSettingModule */
'use strict';

generalSettingModule.factory("generalSettingFactory", ['$http', '$q', function ($http, $q) {
	return {
		setDefaultTargetTemplate: function (targetTemplateId) {
			var deferred = $q.defer();
			$http.get('/api/setting/setDefaulTargetTemplate/', { params: { targetTemplateId: targetTemplateId} })
				.success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		},
		getTargetSetting: function () {
			var deferred = $q.defer();
			$http.get('/api/targetTemplate/getTargetSetting').success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		}
	};
} ]);