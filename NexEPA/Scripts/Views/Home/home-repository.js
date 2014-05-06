'use strict';
/*global homeModule*/

homeModule.factory('homeFactory', ['$http', function ($http) {
	return {
		getGeneralInformationOfCurrentEmployee: function () {
			return $http.get('/api/Home/GetGeneralInformationOfCurrentEmployee');
		}
	};
} ]);

