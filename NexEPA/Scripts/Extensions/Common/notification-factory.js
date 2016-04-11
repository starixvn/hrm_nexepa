/*global registrationModule, toastr*/
'use strict';

registrationModule.factory('notificationFactory', ['$translate', function ($translate) {
    return {
        success: function (keyMessage, message) {
            if (message === '' || message === null || message === undefined) {
                $translate(keyMessage).then(function (responseMessage) {
                    toastr.success(responseMessage);
                });
            }
            else {
                toastr.success(message);
            }
        },
        error: function (keyMessage, message) {
            if (message === '' || message === null || message === undefined) {
                $translate(keyMessage).then(function (responseMessage) {
                    toastr.error(responseMessage);
                });
            }
            else {
                toastr.error(message);
            }
        },
        warning: function (keyMessage, message) {
            if (message === '' || message === null || message === undefined) {
                $translate(keyMessage).then(function (responseMessage) {
                    toastr.warning(responseMessage);
                });
            }
            else {
                toastr.warning(message);
            }
        },
        info: function (keyMessage, message) {
            if (message === '' || message === null || message === undefined) {
                $translate(keyMessage).then(function (responseMessage) {
                    toastr.info(responseMessage);
                });
            }
            else {
                toastr.info(message);
            }
        }
    };
}]);

//projectsSettingModule.run(['$http', 'ProjectSettingCache', function ($http, ProjectSettingCache) {
//	$http.get('/api/Project/RenderProjectStatusSelectList').success(function (data) {
//		ProjectSettingCache.put("CacheListStatus", data);
//	});
//} ]);

//registrationModule.factory('AuthService', ['$http', '$q', function ($http, $q) {
//	return {
//		currentUser: function () {
//			var deferred = $q.defer();
//			$http.get('/api/core/GetCurrentUser').success(deferred.resolve).error(deferred.reject);
//			return deferred.promise;
//		}
//	};
//} ]);

//registrationModule.run(['AuthService', function (AuthService) {
//	AuthService.currentUser();
//} ]);