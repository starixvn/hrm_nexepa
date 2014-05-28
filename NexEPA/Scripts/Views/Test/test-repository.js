'use strict';
/*global testModule*/

testModule.factory("testFactory", ['$http', function ($http) {
	return {
		getIsSimulatedLoginUser: function () {
			return $http.get('/api/Test/GetIsSimulatedLoginUser');
		},
		loginAsAnotherUser: function (domainUsername) {
			return $http.get('/api/Test/LoginAsAnotherUser', { params: { domainUsername: domainUsername} });
		},
		logout: function () {
			return $http.get('/api/Test/Logout');
		}
	};
} ]);