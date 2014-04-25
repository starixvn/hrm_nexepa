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

		common.validateKPI = function ($scope, item, numberGroup) {
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
				else if (common.validateAndCreateStringKPIGroupId($scope, item, numberGroup) === false) {
					return false;
				}
			}

			return true;
		};

		common.validateAndCreateStringKPIGroupId = function ($scope, item, numberGroup) {
			var listKPIGroupId = '';

			for (var i = 0; i <= numberGroup; i++) {
				var key = "kpiGroupId" + "_" + i;
				var groupId = item[key];

				if (groupId === null || groupId === undefined) {
					common.showErrorMessage($scope, "GroupRequire");
					return false;
				}
				else {
					if (listKPIGroupId.search(groupId) !== -1) {
						common.showErrorMessage($scope, "GroupDuplicate");
						return false;
					}
					else {
						if (listKPIGroupId === '') {
							listKPIGroupId += groupId;
						}
						else {
							listKPIGroupId += "," + groupId;
						}
					}
				}
			}

			item.kpiGroupIds = listKPIGroupId;
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

// Add new KPI Group when create new or edit KPI
kpiSettingModule.directive('addNewGroup', ['$compile', function ($compile) {
	return {
		scope: true,
		link: function (scope, iElement) {
			scope.addGroup = function () {
				var parentFormGroup = iElement.parent();
				var numberGroup = scope.groupKPI.numberGroup + 1;

				var groupTemplate = '<div class="form-group">\
										<label class="col-sm-3 control-label"></label>\
										<div class="col-sm-8">\
											<select ng-model="item.kpiGroupId_' + numberGroup + '"\
												ng-options="opt.kpiGroupID as opt.name for opt in groupOptions"\
												ng-disabled="!isCreatedFromKPI" required class="form-control">\
												<option value="">-- Select the group --</option>\
											</select>\
										</div>\
										<a add-new-group class="glyphicon glyphicon-plus icon-add"\
												href="" ng-click="addGroup()" ng-model="groupKPI.numberGroup"></a>\
										<a remove-group class="glyphicon glyphicon-remove icon-remove"\
												href="" ng-click="removeGroup()" ng-model="groupKPI.numberGroup"></a>\
								</div>';

				parentFormGroup.after($compile(groupTemplate)(scope));
				scope.groupKPI.numberGroup = scope.groupKPI.numberGroup + 1;
			};
		}
	};
} ]);

// Remove KPI Group when create or edit KPI
kpiSettingModule.directive('removeGroup', function () {
	return {
		scope: true,
		link: function (scope, iElement) {
			scope.removeGroup = function () {
				var parentFormGroup = iElement.parent();
				parentFormGroup.remove();
				scope.groupKPI.numberGroup = scope.groupKPI.numberGroup - 1;
			};
		}
	};
});