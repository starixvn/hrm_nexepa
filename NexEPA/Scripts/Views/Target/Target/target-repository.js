'use strict';
/*global targetModule*/

targetModule.factory('targetFactory', ['$http', function ($http) {
	return {
		getTarget: function () {
			return $http.get('/api/Target/GetTarget');
		}
	};
} ]);

targetModule.factory('targetCacheFactory', ['$cacheFactory', function ($cacheFactory) {
	return $cacheFactory('targetCache');
} ]);

