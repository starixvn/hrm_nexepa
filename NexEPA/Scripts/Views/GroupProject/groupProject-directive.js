'use strict';
/*global groupProjectModule*/

groupProjectModule.directive('multiSelectItem', function ($q) {
	return {
		restrict: 'E',
		require: 'ngModel',
		scope: {
			selectedLabel: "@",
			availableLabel: "@",
			displayAttr: "@",
			available: "=",
			model: "=ngModel"
		},
		template:
				'<div class="select col-sm-5">' +
					'<label class="control-label bold" for="multiSelectAvailable">{{ availableLabel }} ' +
						'({{ available.length }})</label>' +
					'<select ng-model="selected.available" multiple size="20" ' +
						'class="col-sm-12" ng-options="e as e[displayAttr] for e in available"></select>' +
				'</div>' +
				'<div class="select buttons col-sm-2">' +
					'<button class="button glyphicon glyphicon-chevron-right top-buffer" ng-click="add()" title="Add projects" ' +
						'ng-disabled="selected.available.length == 0">' +
					'</button>' +
					'<button class="button glyphicon glyphicon-chevron-left top-buffer" ng-click="remove()" title="Remove projects" ' +
						'ng-disabled="selected.current.length == 0">' +
					'</button>' +
				'</div>' +
				'<div class="select col-sm-5 required">' +
					'<label class="control-label bold" for="multiSelectSelected">{{ selectedLabel }} ' +
						'({{ model.length }})</label>' +
					'<select ng-model="selected.current" multiple size="20" ' +
						'class="col-sm-12" ng-options="e as e[displayAttr] for e in model">' +
					'</select>' +
				'</div>',
		link: function (scope, elm, attrs) {
			scope.selected = {
				available: [],
				current: []
			};

			/* Handles cases where scope data hasn't been initialized yet */
			var dataLoading = function (scopeAttr) {
				var loading = $q.defer();
				if (scope[scopeAttr]) {
					loading.resolve(scope[scopeAttr]);
				} else {
					scope.$watch(scopeAttr, function (newValue) {
						if (newValue !== undefined) {
							loading.resolve(newValue);
						}
					});
				}
				return loading.promise;
			};

			/* Filters out items in original that are also in toFilter. Compares by reference. */
			var filterOut = function (original, toFilter) {
				var filtered = [];
				angular.forEach(original, function (entity) {
					var match = false;
					for (var i = 0; i < toFilter.length; i++) {
						if (toFilter[i][attrs.displayAttr] === entity[attrs.displayAttr]) {
							match = true;
							break;
						}
					}
					if (!match) {
						filtered.push(entity);
					}
				});
				return filtered;
			};

			scope.refreshAvailable = function () {
				scope.available = filterOut(scope.available, scope.model);
				scope.selected.available = [];
				scope.selected.current = [];
			};

			scope.add = function () {
				scope.model = scope.model.concat(scope.selected.available);
				scope.refreshAvailable();
			};
			scope.remove = function () {
				scope.available = scope.available.concat(scope.selected.current);
				scope.model = filterOut(scope.model, scope.selected.current);
				scope.refreshAvailable();
			};

			$q.all([dataLoading("model"), dataLoading("available")]).then(function () {
				scope.refreshAvailable();
			});
		}
	};
});