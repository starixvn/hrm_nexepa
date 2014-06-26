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

		common.getFormOptionsExceptAll = function () {
			var deferred = $q.defer();
			$http.get('/api/Core/GetTargetTypeExceptAll').success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		};

		common.getGroupByTargetTypeId = function (targetTypeId) {
			var deferred = $q.defer();
			$http.get('/api/KPIGroup/GetGroupListByTargetTypeId', { params: { targetTypeId: targetTypeId} })
			.success(deferred.resolve).error(deferred.reject);
			return deferred.promise;
		};

		common.validateGroup = function (group) {
			if (!group) {
				return false;
			}
			else {
				if (!group.name) {
					return false;
				}

				if (!group.targetTypeId) {
					return false;
				}
			}

			return true;
		};

		common.validateKPI = function (kpi) {
			if (!kpi) {
				return false;
			}
			else {
				if (!kpi.name) {
					return false;
				}
				else if (!common.validateAndCreateStringKPIGroupId(kpi)) {
					return false;
				}
			}

			return true;
		};

		common.validateAndCreateStringKPIGroupId = function (item) {
			if (!item.kpiGroupIds || item.kpiGroupIds.length === 0) {
				return false;
			}

			item.kpiGroupIds = item.kpiGroupIds.toString();
			return true;
		};

		common.showMessageAfterValidateKPI = function ($scope) {
			$scope.$watchCollection('[submitted, form.$invalid, \
				form.itemGroupSelect.$error.required, form.itemName.$error.required ]', function (newVals) {
					if (newVals[0] && newVals[1]) {
						if (newVals[3]) {
							notificationFactory.error("TitleRequire");
						}
						if (newVals[2]) {
							notificationFactory.error("GroupRequire");
						}

						$scope.submitted = false;
					}
				});
		};

		common.showMessageAfterValidateKPIGroup = function ($scope) {
			$scope.$watchCollection('[submitted, form.$invalid, form.groupName.$error.required, form.groupSelect.$error.required ]',
			function (newVals) {
				if (newVals[0] && newVals[1]) {
					if (newVals[2]) {
						notificationFactory.error("NameRequire");
					}
					if (newVals[3]) {
						notificationFactory.error("FormRequire");
					}

					$scope.submitted = false;
				}
			});
		};

		return common;
	} ]);

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
			scope.$watchCollection('groupOptions', function () {
				iElement.trigger("chosen:updated");
			});
			iElement.on("change", scope.getSelectedOrDeselectedGroup);
			iElement.chosen();
		}
	};
});