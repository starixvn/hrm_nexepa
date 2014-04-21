/* global epaModule */
'use strict';

epaModule.factory('myEPAFactory', function ($http, $q) {
	return {
		getMyEPA: function (targetId) {
			var deferred = $q.defer();
			$http.get('/api/MyEPA/GetMyEPA', {
				params: { targetId: targetId }
			}).success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		}
	};
});