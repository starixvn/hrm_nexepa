'use strict';
/*global registrationModule*/

registrationModule.factory('CommonRepository', ['$http', '$q', function ($http, $q) {
	var common = {};

	common.getFormOptions = function () {
		var deferred = $q.defer();
		$http.get('/api/Core/AllTargetType').success(deferred.resolve).error(deferred.reject);
		return deferred.promise;
	};

	common.getGroupByTargetTypeId = function (targetTypeId) {
		var deferred = $q.defer();
		$http.get('/api/KPIGroup/GetGroupListByTargetTypeId', { params: { targetTypeId: targetTypeId} })
			.success(deferred.resolve).error(deferred.reject);
		return deferred.promise;
	};

	common.validateGroup = function (group) {
		if (group === undefined) {
			return false;
		}
		else {
			if (group.name === undefined) {
				return false;
			}

			if (group.weight === undefined || group.weight === null || isNaN(group.weight) ||
					group.weight < 0 || group.weight > 100) {
				return false;
			}

			if (group.targetTypeId === undefined) {
				return false;
			}
		}

		return true;
	};

	common.validateKPI = function (item) {
		if (item === undefined) {
			return false;
		}
		else {
			if (item.name === undefined || item.kpiGroupId === undefined || item.targetTypeId === undefined) {
				return false;
			}
		}

		return true;
	};

	return common;
} ]);

registrationModule.factory("CacheFactory", ['$cacheFactory', function ($cacheFactory) {
	return $cacheFactory("KPICache");
}]);

registrationModule.controller("ModalConfirmController", function ($scope, $modalInstance, name, action) {
	$scope.name = name;
	$scope.action = action;

	$scope.ok = function () {
		$modalInstance.close();
	};

	$scope.cancel = function () {
		$modalInstance.dismiss("cancel");
	};
});

registrationModule.directive('numbersOnly', function () {
	return {
		require: 'ngModel',
		link: function (scope, element, attrs, modelCtrl) {
			modelCtrl.$parsers.push(function (inputValue) {
				if (inputValue === undefined) {
					return '';
				}

				var transformedInput = inputValue.replace(/[^0-9]/g, '');

				if (transformedInput !== inputValue) {
					modelCtrl.$setViewValue(transformedInput);
					modelCtrl.$render();
				}

				return transformedInput;
			});
		}
	};
});