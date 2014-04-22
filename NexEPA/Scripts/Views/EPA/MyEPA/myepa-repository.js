/* global epaModule */
'use strict';

epaModule.factory('employeeEPAFactory',['$http', '$q', function ($http, $q) {
	return {
		getMyEPA: function (targetId) {
			var deferred = $q.defer();
			$http.get('/api/EmployeeEPA/GetMyEPA', {
				params: { targetId: targetId }
			}).success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		}
	};
}]);