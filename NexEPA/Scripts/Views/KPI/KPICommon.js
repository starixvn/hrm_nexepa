'use strict';
/*global kpiSettingModule*/

kpiSettingModule.factory('KPICommonRepository', ['$http', '$q', '$translate',
	function ($http, $q, $translate) {
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

				if (group.targetTypeId === undefined || group.targetTypeId === null) {
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
				if (item.name === undefined) {
					return false;
				}
				else if (item.targetTypeId === undefined || item.targetTypeId === null) {
					return false;
				}
				else if (common.validateAndCreateStringKPIGroupId(item) === false) {
					return false;
				}
			}

			return true;
		};

		common.validateAndCreateStringKPIGroupId = function (item) {
			if (item.kpiGroupIds === null || item.kpiGroupIds === undefined) {
				return false;
			}

			item.kpiGroupIds = item.kpiGroupIds.toString();
			return true;
		};

		common.showSuccessMessage = function ($scope, keyMessage) {
			$scope.success = true;
			$translate(keyMessage).then(function (message) {
				$scope.successMessage = message;
			});
		};

		common.showErrorMessage = function ($scope, keyMessage, message) {
			$scope.success = false;

			if (message === '' || message === null || message === undefined) {
				$translate(keyMessage).then(function (errorMessage) {
					$scope.errorMessage = errorMessage;
				});
			}
			else {
				$scope.errorMessage = message;
			}
		};

		return common;
	} ]);

kpiSettingModule.factory("CacheFactory", ['$cacheFactory', function ($cacheFactory) {
	return $cacheFactory("KPICache");
}]);

kpiSettingModule.directive('numbersOnly', function () {
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

// multi-select use Chosen plugin
kpiSettingModule.directive('multiSelectChosen', function () {
	return {
		link: function (scope, iElement) {
			scope.$watch("groupOptions", function () {
				iElement.trigger("chosen:updated");
			});

			iElement.chosen();
		}
	};
});