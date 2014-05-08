'use strict';
/*global kpiSettingModule*/

kpiSettingModule.factory('KPICommonRepository', ['$http', '$q', '$translate', 'notificationFactory',
	function ($http, $q, $translate, notificationFactory) {
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
				if (group.name === undefined || group.name === null) {
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
				if (item.name === undefined || item.name === null) {
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

		common.showMessageAfterValidateKPI = function ($scope) {
			$scope.$watchCollection('[submitted, form.$invalid, form.itemFormSelect.$error.required, \
				form.itemGroupSelect.$error.required, form.itemName.$error.required ]', function (newVals) {
					if (newVals[0] === true && newVals[1] === true) {
						if (newVals[4] === true) {
							notificationFactory.error("TitleRequire");
						}
						if (newVals[3] === true) {
							notificationFactory.error("GroupRequire");
						}
						if (newVals[2] === true) {
							notificationFactory.error("FormRequire");
						}

						$scope.submitted = false;
					}
				});
		};

		common.showMessageAfterValidateKPIGroup = function ($scope) {
			$scope.$watchCollection('[submitted, form.$invalid, form.groupName.$error.required, form.groupSelect.$error.required ]',
			function (newVals) {
				if (newVals[0] === true && newVals[1] === true) {
					if (newVals[3] === true) {
						notificationFactory.error("FormRequire");
					}
					if (newVals[2] === true) {
						notificationFactory.error("NameRequire");
					}

					$scope.submitted = false;
				}
			});
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