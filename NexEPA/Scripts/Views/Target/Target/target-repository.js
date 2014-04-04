'use strict';
/*global targetModule*/

targetModule.factory('targetFactory', ['$http', function ($http) {
	return {
		getTarget: function (targetTypeId, startDate, endDate) {
			return $http.get('/api/Target/GetTarget',
				{ params: { targetTypeId: targetTypeId, startDate: startDate, endDate: endDate } });
		}
	};
} ]);

targetModule.factory('targetCacheFactory', ['$cacheFactory', function ($cacheFactory) {
	return $cacheFactory('targetCache');
} ]);

