/* global registrationModule */
'use strict';

registrationModule.factory('groupRepository', function ($http, $q) {
    return {
        get: function () {
            var deferred = $q.defer();
            $http.get('/api/Group/GetGroups').success(deferred.resolve).error(deferred.reject);
            return deferred.promise;
        }
    };
});