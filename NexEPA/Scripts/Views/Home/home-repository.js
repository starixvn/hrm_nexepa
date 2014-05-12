'use strict';
/*global homeModule*/

homeModule.factory('homeFactory', ['$http', function ($http) {
	return {
		getGeneralInformationOfCurrentEmployee: function () {
			return $http.get('/api/Home/GetGeneralInformationOfCurrentEmployee');
		},
		getCurrentEmployeeId: function () {
			return $http.get('/api/Home/GetCurrentEmployeeId');
		},
		getIsSimulatedLoginUser: function () {
			return $http.get('/api/Home/GetIsSimulatedLoginUser');
		},
		loginAsAnotherUser: function (domainUsername) {
			return $http.get('/api/Home/LoginAsAnotherUser', { params: { domainUsername: domainUsername } });
		}
	};
} ]);

homeModule.directive('truncate', function () {
	return {
		scope: { comment: '=ngBind', originalComment: '&originalComment' },
		link: function (scope) {
			var comment = scope.originalComment(),
				length = 60;

			if (comment) {
				if (comment.length <= length) {
					scope.comment = comment;
				}
				else {
					scope.comment = String(comment).substring(0, length);
				}
			}
		}
	};
});



